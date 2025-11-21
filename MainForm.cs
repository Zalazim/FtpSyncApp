using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FtpSyncApp;

/// <summary>
/// Fenêtre principale : configuration de la synchro, lancement,
/// suivi en temps réel et export des logs.
/// </summary>
public partial class MainForm : Form
{
    private CancellationTokenSource? _cts;
    private readonly List<LogEntry> _logEntries = new();
    private bool _isUpdatingChecks;

    public MainForm()
    {
        InitializeComponent();
        InitDefaultExcludes();
    }

    #region Journalisation (logs)

    /// <summary>
    /// Ajoute une ligne dans le journal et dans le ListView correspondant
    /// (Infos, Avertissements, Erreurs).
    /// </summary>
    private void AppendLog(LogEntry entry)
    {
        _logEntries.Add(entry);

        var target = entry.Level switch
        {
            LogLevel.Info => lvInfos,
            LogLevel.Warning => lvWarnings,
            LogLevel.Error => lvErrors,
            _ => lvInfos
        };

        var item = new ListViewItem(entry.Timestamp.ToString("HH:mm:ss"));
        item.SubItems.Add(entry.Message);
        item.SubItems.Add(entry.FilePath ?? string.Empty);
        item.SubItems.Add(entry.Action?.ToString() ?? string.Empty);
        item.SubItems.Add(entry.Status?.ToString() ?? string.Empty);

        // Mise en couleur simple selon le niveau.
        switch (entry.Level)
        {
            case LogLevel.Warning:
                item.ForeColor = System.Drawing.Color.DarkOrange;
                break;
            case LogLevel.Error:
                item.ForeColor = System.Drawing.Color.Red;
                break;
        }

        target.Items.Add(item);
        target.EnsureVisible(target.Items.Count - 1);
    }

    /// <summary>
    /// Efface les messages de log actuellement affichés.
    /// </summary>
    private void ClearLogs()
    {
        _logEntries.Clear();
        lvInfos.Items.Clear();
        lvWarnings.Items.Clear();
        lvErrors.Items.Clear();
    }

    /// <summary>
    /// Active / désactive les boutons principaux en fonction de l'état
    /// (exécution en cours ou non).
    /// </summary>
    private void ToggleButtons(bool enabled)
    {
        btnSync.Enabled = enabled;
        btnDryRun.Enabled = enabled;
        btnCancel.Enabled = !enabled;
    }

    /// <summary>
    /// Met à jour la barre de progression globale et le libellé associé.
    /// </summary>
    private void UpdateProgress(int current, int total)
    {
        if (total <= 0)
        {
            total = 1;
        }

        progressBarOverall.Maximum = total;
        progressBarOverall.Value = Math.Min(current, total);
        lblProgress.Text = $"{current} / {total}";
    }

    /// <summary>
    /// Ajoute un résumé global de la synchro dans les logs.
    /// </summary>
    private void ShowSummary(int total, int ok, int failed, int ignored)
    {
        AppendLog(new LogEntry
        {
            Level = LogLevel.Info,
            Message = $"Résumé : {ok} ok, {failed} erreurs, {ignored} ignorés sur {total} fichiers."
        });
    }

    #endregion

    #region Gestion du dossier local et arborescence

    private void btnBrowseLocal_Click(object sender, EventArgs e)
    {
        using var dialog = new FolderBrowserDialog
        {
            ShowNewFolderButton = false
        };

        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            txtLocalRoot.Text = dialog.SelectedPath;
            LoadTreeView(dialog.SelectedPath);
        }
    }

    /// <summary>
    /// Charge l'arborescence de dossiers dans le TreeView à partir d'une racine.
    /// </summary>
    private void LoadTreeView(string rootPath)
    {
        tvFolders.Nodes.Clear();

        if (string.IsNullOrWhiteSpace(rootPath) || !Directory.Exists(rootPath))
            return;

        var root = new TreeNode
        {
            Tag = rootPath,
            Checked = true
        };

        SetNodeTextWithFileCount(root);
        tvFolders.Nodes.Add(root);
        LoadSubDirectories(root);
        tvFolders.ExpandAll();
    }

    /// <summary>
    /// Ajoute récursivement les sous-dossiers d'un nœud donné.
    /// </summary>
    private void LoadSubDirectories(TreeNode node)
    {
        var path = node.Tag as string;
        if (string.IsNullOrEmpty(path))
            return;

        try
        {
            foreach (var dir in Directory.GetDirectories(path))
            {
                var child = new TreeNode
                {
                    Tag = dir,
                    Checked = true
                };

                SetNodeTextWithFileCount(child);
                node.Nodes.Add(child);
                LoadSubDirectories(child);
            }
        }
        catch
        {
            // Dossiers non accessibles : ignorés silencieusement.
        }
    }

    /// <summary>
    /// Gestion du (dé)cochage dans le TreeView :
    /// - coche d'un parent → coche tous les enfants
    /// - coche d'un enfant → remonte la coche vers tous les parents
    /// - décochage → applique uniquement sur les descendants
    /// </summary>
    private void tvFolders_AfterCheck(object sender, TreeViewEventArgs e)
    {
        if (_isUpdatingChecks)
            return;

        _isUpdatingChecks = true;
        try
        {
            var isUserAction = e.Action != TreeViewAction.Unknown;
            if (!isUserAction)
            {
                // Changement initié par le code : aucune propagation.
                return;
            }

            if (e.Node.Checked)
            {
                // Coche descendante + coche de tous les parents.
                SetChildrenChecked(e.Node, true);
                SetParentsChecked(e.Node);
            }
            else
            {
                // Décoche uniquement les descendants.
                SetChildrenChecked(e.Node, false);
            }
        }
        finally
        {
            _isUpdatingChecks = false;
        }
    }

    /// <summary>
    /// Coche ou décoche récursivement tous les enfants d'un nœud.
    /// </summary>
    private void SetChildrenChecked(TreeNode node, bool isChecked)
    {
        foreach (TreeNode child in node.Nodes)
        {
            child.Checked = isChecked;
            SetChildrenChecked(child, isChecked);
        }
    }

    /// <summary>
    /// Coche tous les parents d'un nœud, sans toucher aux autres enfants.
    /// </summary>
    private void SetParentsChecked(TreeNode node)
    {
        var parent = node.Parent;
        while (parent != null)
        {
            if (!parent.Checked)
            {
                parent.Checked = true;
            }

            parent = parent.Parent;
        }
    }

    /// <summary>
    /// Met à jour le texte du nœud avec le nombre de fichiers
    /// (en respectant les exclusions).
    /// </summary>
    private void SetNodeTextWithFileCount(TreeNode node)
    {
        var path = node.Tag as string;
        if (string.IsNullOrEmpty(path))
            return;

        // Récupère les motifs d'exclusion courants (*.ini, *.txt, etc.).
        var patterns = lstExcludes.Items
            .Cast<object>()
            .Select(o => o?.ToString() ?? string.Empty)
            .ToList();

        var fileCount = 0;

        try
        {
            foreach (var file in Directory.EnumerateFiles(path, "*", SearchOption.TopDirectoryOnly))
            {
                var fileName = Path.GetFileName(file);
                if (IsExcluded(fileName, patterns))
                    continue;

                fileCount++;
            }
        }
        catch
        {
            // Dossier non accessible : on laisse 0.
        }

        var dirName = Path.GetFileName(path.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));
        if (string.IsNullOrEmpty(dirName))
        {
            dirName = path;
        }

        node.Text = $"{dirName} ({fileCount})";
    }

    /// <summary>
    /// Recalcule les nombres de fichiers pour tous les nœuds de l'arbre.
    /// Appelé notamment lorsqu'on modifie les exclusions.
    /// </summary>
    private void RefreshAllNodeCounts()
    {
        foreach (TreeNode node in tvFolders.Nodes)
        {
            RefreshNodeCountsRecursive(node);
        }
    }

    private void RefreshNodeCountsRecursive(TreeNode node)
    {
        SetNodeTextWithFileCount(node);

        foreach (TreeNode child in node.Nodes)
        {
            RefreshNodeCountsRecursive(child);
        }
    }

    #endregion

    #region Exclusions de fichiers

    /// <summary>
    /// Initialise les motifs d'exclusion par défaut (*.ini, *.txt).
    /// </summary>
    private void InitDefaultExcludes()
    {
        var defaults = new[] { "*.ini", "*.txt" };

        foreach (var pattern in defaults)
        {
            if (!lstExcludes.Items.Contains(pattern))
            {
                lstExcludes.Items.Add(pattern);
            }
        }
    }

    private void btnAddExclude_Click(object sender, EventArgs e)
    {
        var pattern = txtNewExclude.Text.Trim();
        if (pattern.Length == 0)
            return;

        if (!lstExcludes.Items.Contains(pattern))
        {
            lstExcludes.Items.Add(pattern);
            RefreshAllNodeCounts();
        }

        txtNewExclude.Clear();
    }

    private void btnRemoveExclude_Click(object sender, EventArgs e)
    {
        var selected = lstExcludes.SelectedItem;
        if (selected != null)
        {
            lstExcludes.Items.Remove(selected);
            RefreshAllNodeCounts();
        }
    }

    #endregion

    #region Construction de la configuration

    /// <summary>
    /// Construit un objet <see cref="SyncConfig"/> à partir de l'état de l'UI.
    /// </summary>
    private SyncConfig BuildConfigFromUI()
    {
        var config = new SyncConfig
        {
            LocalRoot = txtLocalRoot.Text.Trim(),
            RemoteRoot = txtRemoteRoot.Text.Trim(),
            Host = txtHost.Text.Trim(),
            Port = int.TryParse(txtPort.Text, out var p) ? p : 22,
            Username = txtUsername.Text.Trim(),
            Password = txtPassword.Text,
            UseSftp = rbSftp.Checked,
            DeleteRemoteOrphans = true
        };

        foreach (var item in lstExcludes.Items)
        {
            config.ExcludedPatterns.Add(item?.ToString() ?? string.Empty);
        }

        // Récupération des sous-dossiers cochés (chemins relatifs).
        foreach (TreeNode node in tvFolders.Nodes)
        {
            CollectCheckedFolders(node, config);
        }

        return config;
    }

    private void CollectCheckedFolders(TreeNode node, SyncConfig config)
    {
        var fullPath = node.Tag as string;

        if (node.Checked &&
            !string.IsNullOrEmpty(fullPath) &&
            Directory.Exists(fullPath) &&
            !string.Equals(fullPath, config.LocalRoot, StringComparison.OrdinalIgnoreCase))
        {
            var rel = Path.GetRelativePath(config.LocalRoot, fullPath);
            config.SelectedSubfolders.Add(rel);
        }

        foreach (TreeNode child in node.Nodes)
        {
            CollectCheckedFolders(child, config);
        }
    }

    #endregion

    #region Lancement de la synchronisation / simulation

    /// <summary>
    /// Lance une synchronisation complète (ou simulation) en asynchrone.
    /// </summary>
    private async Task StartSyncAsync(bool dryRun)
    {
        ToggleButtons(false);
        ClearLogs();
        _cts = new CancellationTokenSource();

        try
        {
            var config = BuildConfigFromUI();

            if (string.IsNullOrWhiteSpace(config.LocalRoot) || !Directory.Exists(config.LocalRoot))
            {
                AppendLog(new LogEntry
                {
                    Level = LogLevel.Error,
                    Message = "Dossier local invalide."
                });
                return;
            }

            if (string.IsNullOrWhiteSpace(config.RemoteRoot))
            {
                AppendLog(new LogEntry
                {
                    Level = LogLevel.Error,
                    Message = "Dossier distant vide."
                });
                return;
            }

            // Normalisation du chemin distant.
            config.RemoteRoot = NormalizeRemote(config.RemoteRoot);

            AppendLog(new LogEntry { Level = LogLevel.Info, Message = "Connexion au serveur..." });

            using IRemoteClient client = new SftpClientWrapper(
                config.Host,
                config.Port,
                config.Username,
                config.Password);

            await client.ConnectAsync();
            AppendLog(new LogEntry { Level = LogLevel.Info, Message = "Connecté." });

            // 1) Scan local.
            var localFiles = ScanLocalFiles(config);
            AppendLog(new LogEntry
            {
                Level = LogLevel.Info,
                Message = $"Fichiers locaux trouvés : {localFiles.Count}"
            });

            // 2) Scan distant.
            var remoteMap = await client.ListFilesRecursiveAsync(config.RemoteRoot, _cts.Token);
            var remoteFiles = BuildRemoteRelativeMap(config.RemoteRoot, remoteMap);
            AppendLog(new LogEntry
            {
                Level = LogLevel.Info,
                Message = $"Fichiers distants trouvés : {remoteFiles.Count}"
            });

            // 3) Construction du plan d'actions.
            var plan = BuildSyncPlan(config, localFiles, remoteFiles);

            var total = plan.Count;
            var current = 0;
            var ok = 0;
            var failed = 0;
            var ignored = 0;
            var sequentialErrors = 0;

            foreach (var item in plan)
            {
                _cts.Token.ThrowIfCancellationRequested();
                current++;
                UpdateProgress(current, total);

                // Alerte fichier volumineux.
                if (item.LocalSize > 0 && item.LocalSize >= config.LargeFileWarningBytes)
                {
                    AppendLog(new LogEntry
                    {
                        Level = LogLevel.Warning,
                        Message = $"Fichier volumineux (>500Mo) : {item.LocalFullPath}",
                        FilePath = item.LocalFullPath,
                        Action = item.Action,
                        Status = SyncStatus.Pending
                    });
                }

                if (dryRun)
                {
                    ignored++;
                    AppendLog(new LogEntry
                    {
                        Level = LogLevel.Info,
                        Message = $"[SIMU] {item.Action} : {item.RelativePath}",
                        FilePath = item.LocalFullPath ?? item.RemoteFullPath,
                        Action = item.Action,
                        Status = SyncStatus.Ignored
                    });
                    continue;
                }

                try
                {
                    switch (item.Action)
                    {
                        case SyncAction.UploadNew:
                        case SyncAction.UploadUpdate:
                            {
                                var dir = Path.GetDirectoryName(item.RemoteFullPath) ?? "/";
                                dir = NormalizeRemote(dir);

                                await client.EnsureDirectoryAsync(dir, _cts.Token);
                                await client.UploadFileAsync(item.LocalFullPath!, item.RemoteFullPath, _cts.Token);

                                item.Status = SyncStatus.Success;
                                ok++;
                                sequentialErrors = 0;

                                AppendLog(new LogEntry
                                {
                                    Level = LogLevel.Info,
                                    Message = $"{item.Action} : {item.RelativePath}",
                                    FilePath = item.LocalFullPath,
                                    Action = item.Action,
                                    Status = item.Status
                                });

                                break;
                            }

                        case SyncAction.DeleteRemote:
                            {
                                await client.DeleteFileAsync(item.RemoteFullPath, _cts.Token);

                                item.Status = SyncStatus.Success;
                                ok++;
                                sequentialErrors = 0;

                                AppendLog(new LogEntry
                                {
                                    Level = LogLevel.Info,
                                    Message = $"Supprimé sur serveur : {item.RelativePath}",
                                    FilePath = item.RemoteFullPath,
                                    Action = item.Action,
                                    Status = item.Status
                                });

                                break;
                            }

                        case SyncAction.Skip:
                            {
                                item.Status = SyncStatus.Ignored;
                                ignored++;

                                AppendLog(new LogEntry
                                {
                                    Level = LogLevel.Info,
                                    Message = $"Ignoré (identique) : {item.RelativePath}",
                                    FilePath = item.LocalFullPath,
                                    Action = item.Action,
                                    Status = item.Status
                                });

                                break;
                            }
                    }
                }
                catch (Exception ex)
                {
                    item.Status = SyncStatus.Failed;
                    failed++;
                    sequentialErrors++;

                    AppendLog(new LogEntry
                    {
                        Level = LogLevel.Error,
                        Message = $"Erreur sur {item.RelativePath} : {ex.Message}",
                        FilePath = item.LocalFullPath ?? item.RemoteFullPath,
                        Action = item.Action,
                        Status = item.Status
                    });

                    if (sequentialErrors >= config.MaxSequentialErrors)
                    {
                        AppendLog(new LogEntry
                        {
                            Level = LogLevel.Error,
                            Message = $"Arrêt : {sequentialErrors} erreurs consécutives."
                        });
                        break;
                    }
                }
            }

            ShowSummary(total, ok, failed, ignored);
        }
        catch (OperationCanceledException)
        {
            AppendLog(new LogEntry
            {
                Level = LogLevel.Warning,
                Message = "Synchronisation annulée."
            });
        }
        catch (Exception ex)
        {
            AppendLog(new LogEntry
            {
                Level = LogLevel.Error,
                Message = "Erreur critique : " + ex.Message
            });
        }
        finally
        {
            ToggleButtons(true);
        }
    }

    private static string NormalizeRemote(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            return "/";

        var p = path.Replace('\\', '/').Trim();
        if (!p.StartsWith("/"))
            p = "/" + p;

        return p.TrimEnd('/');
    }

    /// <summary>
    /// Scan local basé sur l'état réel des coches dans le TreeView.
    /// </summary>
    private Dictionary<string, (string FullPath, long Size, DateTime LastWriteUtc)>
        ScanLocalFiles(SyncConfig config)
    {
        var result = new Dictionary<string, (string, long, DateTime)>(StringComparer.OrdinalIgnoreCase);

        foreach (TreeNode node in tvFolders.Nodes)
        {
            ScanNodeFiles(node, config, result);
        }

        return result;
    }

    /// <summary>
    /// Parcourt un nœud du TreeView :
    /// - si le dossier est coché → fichiers directs uniquement
    /// - les enfants sont toujours inspectés, chacun décidant via sa propre coche.
    /// </summary>
    private void ScanNodeFiles(
        TreeNode node,
        SyncConfig config,
        Dictionary<string, (string FullPath, long Size, DateTime LastWriteUtc)> result)
    {
        var path = node.Tag as string;

        if (node.Checked &&
            !string.IsNullOrEmpty(path) &&
            Directory.Exists(path))
        {
            foreach (var file in Directory.EnumerateFiles(path, "*", SearchOption.TopDirectoryOnly))
            {
                var fileName = Path.GetFileName(file);
                if (IsExcluded(fileName, config.ExcludedPatterns))
                    continue;

                try
                {
                    var info = new FileInfo(file);
                    var relFromRoot = Path.GetRelativePath(config.LocalRoot, file);
                    var relNorm = relFromRoot.Replace('\\', '/');

                    result[relNorm] = (file, info.Length, info.LastWriteTimeUtc);
                }
                catch
                {
                    // Fichier illisible : ignoré.
                }
            }
        }

        foreach (TreeNode child in node.Nodes)
        {
            ScanNodeFiles(child, config, result);
        }
    }

    /// <summary>
    /// Retourne true si le fichier doit être exclu selon les patterns fournis.
    /// </summary>
    private bool IsExcluded(string fileName, List<string> patterns)
    {
        foreach (var raw in patterns)
        {
            var pattern = raw.Trim();
            if (pattern.Length == 0)
                continue;

            // Gestion simple des motifs du type "*.ext".
            if (pattern.StartsWith("*.", StringComparison.Ordinal))
            {
                var ext = pattern[1..]; // ex : ".ini"
                if (fileName.EndsWith(ext, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            else if (string.Equals(pattern, fileName, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Construit une map "chemin relatif → info distante" à partir des chemins absolus.
    /// </summary>
    private Dictionary<string, RemoteFileInfo> BuildRemoteRelativeMap(
        string remoteRoot,
        IDictionary<string, RemoteFileInfo> fullMap)
    {
        var result = new Dictionary<string, RemoteFileInfo>(StringComparer.OrdinalIgnoreCase);
        var root = NormalizeRemote(remoteRoot);

        foreach (var (full, info) in fullMap)
        {
            if (!full.StartsWith(root, StringComparison.OrdinalIgnoreCase))
                continue;

            var rel = full.Substring(root.Length).TrimStart('/');
            var relNorm = rel.Replace('\\', '/');
            result[relNorm] = info;
        }

        return result;
    }

    /// <summary>
    /// Construit la liste des actions à effectuer pour aligner le serveur sur le local.
    /// </summary>
    private List<SyncItem> BuildSyncPlan(
        SyncConfig config,
        Dictionary<string, (string FullPath, long Size, DateTime LastWriteUtc)> localFiles,
        Dictionary<string, RemoteFileInfo> remoteFiles)
    {
        var plan = new List<SyncItem>();
        var remoteRoot = NormalizeRemote(config.RemoteRoot);

        // 1) Fichiers locaux : Upload / Skip.
        foreach (var (rel, value) in localFiles)
        {
            var (full, size, lastUtc) = value;
            var remoteFull = $"{remoteRoot}/{rel}";

            if (remoteFiles.TryGetValue(rel, out var remote))
            {
                var sameSize = remote.Size == size;
                var sameDate = Math.Abs((remote.LastWriteTimeUtc - lastUtc.ToUniversalTime()).TotalSeconds) < 2;

                if (sameSize && sameDate)
                {
                    plan.Add(new SyncItem
                    {
                        RelativePath = rel,
                        LocalFullPath = full,
                        RemoteFullPath = remoteFull,
                        LocalSize = size,
                        LocalLastWriteUtc = lastUtc.ToUniversalTime(),
                        RemoteSize = remote.Size,
                        RemoteLastWriteUtc = remote.LastWriteTimeUtc,
                        Action = SyncAction.Skip
                    });
                }
                else
                {
                    plan.Add(new SyncItem
                    {
                        RelativePath = rel,
                        LocalFullPath = full,
                        RemoteFullPath = remoteFull,
                        LocalSize = size,
                        LocalLastWriteUtc = lastUtc.ToUniversalTime(),
                        RemoteSize = remote.Size,
                        RemoteLastWriteUtc = remote.LastWriteTimeUtc,
                        Action = SyncAction.UploadUpdate
                    });
                }
            }
            else
            {
                plan.Add(new SyncItem
                {
                    RelativePath = rel,
                    LocalFullPath = full,
                    RemoteFullPath = remoteFull,
                    LocalSize = size,
                    LocalLastWriteUtc = lastUtc.ToUniversalTime(),
                    Action = SyncAction.UploadNew
                });
            }
        }

        // 2) Fichiers distants orphelins : suppression.
        if (config.DeleteRemoteOrphans)
        {
            foreach (var (rel, info) in remoteFiles)
            {
                if (localFiles.ContainsKey(rel))
                    continue;

                var remoteFull = $"{remoteRoot}/{rel}";

                plan.Add(new SyncItem
                {
                    RelativePath = rel,
                    LocalFullPath = null,
                    RemoteFullPath = remoteFull,
                    LocalSize = 0,
                    LocalLastWriteUtc = DateTime.MinValue,
                    RemoteSize = info.Size,
                    RemoteLastWriteUtc = info.LastWriteTimeUtc,
                    Action = SyncAction.DeleteRemote
                });
            }
        }

        return plan;
    }

    private async void btnSync_Click(object sender, EventArgs e)
    {
        await StartSyncAsync(dryRun: false);
    }

    private async void btnDryRun_Click(object sender, EventArgs e)
    {
        await StartSyncAsync(dryRun: true);
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        _cts?.Cancel();
    }

    #endregion

    #region Export des logs

    private void btnExportLogs_Click(object sender, EventArgs e)
    {
        using var dialog = new SaveFileDialog
        {
            Filter = "CSV (*.csv)|*.csv|Texte (*.txt)|*.txt",
            FileName = "sync-log-" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".csv"
        };

        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            var lines = _logEntries.Select(e =>
                $"{e.Timestamp:yyyy-MM-dd HH:mm:ss};{e.Level};{e.Action};{e.Status};" +
                $"\"{e.FilePath?.Replace("\"", "\"\"")}\";\"{e.Message?.Replace("\"", "\"\"")}\"");

            File.WriteAllLines(dialog.FileName, lines);
        }
    }

    #endregion
}
