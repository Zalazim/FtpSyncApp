using System.Collections.Generic;

namespace FtpSyncApp;

/// <summary>
/// Ensemble des paramètres utilisés pour une opération de synchronisation.
/// </summary>
public class SyncConfig
{
    /// <summary>
    /// Dossier racine local (ex : G:\Mon Drive).
    /// </summary>
    public string LocalRoot { get; set; } = string.Empty;

    /// <summary>
    /// Dossier racine distant sur le serveur (ex : /htdocs/content-2).
    /// </summary>
    public string RemoteRoot { get; set; } = string.Empty;

    /// <summary>
    /// Sous-dossiers sélectionnés sous <see cref="LocalRoot"/> (chemins relatifs).
    /// </summary>
    public List<string> SelectedSubfolders { get; } = new();

    /// <summary>
    /// Motifs d'exclusion (ex : "*.ini", "*.txt").
    /// </summary>
    public List<string> ExcludedPatterns { get; } = new();

    /// <summary>
    /// Indique si les fichiers présents sur le serveur mais absents en local
    /// doivent être supprimés.
    /// </summary>
    public bool DeleteRemoteOrphans { get; set; } = true;

    /// <summary>
    /// Seuil à partir duquel un fichier est considéré comme “volumineux”
    /// et génère un avertissement (en octets).
    /// </summary>
    public long LargeFileWarningBytes { get; set; } = 500L * 1024 * 1024;

    /// <summary>
    /// Nombre maximum d'erreurs consécutives avant d'arrêter la synchronisation.
    /// </summary>
    public int MaxSequentialErrors { get; set; } = 5;

    /// <summary>
    /// Nom d'hôte ou adresse du serveur SFTP/FTP.
    /// </summary>
    public string Host { get; set; } = string.Empty;

    /// <summary>
    /// Port du serveur (22 par défaut pour SFTP).
    /// </summary>
    public int Port { get; set; } = 22;

    /// <summary>
    /// Nom d'utilisateur pour l'authentification.
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Mot de passe pour l'authentification.
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Indique si la connexion doit se faire en SFTP (sinon FTP).
    /// Pour l'instant seul SFTP est implémenté côté code.
    /// </summary>
    public bool UseSftp { get; set; } = true;
}
