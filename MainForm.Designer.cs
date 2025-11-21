namespace FtpSyncApp;

partial class MainForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    private System.Windows.Forms.Label lblLocalRoot;
    private System.Windows.Forms.TextBox txtLocalRoot;
    private System.Windows.Forms.Button btnBrowseLocal;

    private System.Windows.Forms.Label lblRemoteRoot;
    private System.Windows.Forms.TextBox txtRemoteRoot;

    private System.Windows.Forms.GroupBox grpConnection;
    private System.Windows.Forms.Label lblHost;
    private System.Windows.Forms.TextBox txtHost;
    private System.Windows.Forms.Label lblPort;
    private System.Windows.Forms.TextBox txtPort;
    private System.Windows.Forms.Label lblUsername;
    private System.Windows.Forms.TextBox txtUsername;
    private System.Windows.Forms.Label lblPassword;
    private System.Windows.Forms.TextBox txtPassword;
    private System.Windows.Forms.RadioButton rbSftp;
    private System.Windows.Forms.RadioButton rbFtp;

    private System.Windows.Forms.TreeView tvFolders;

    private System.Windows.Forms.GroupBox grpExcludes;
    private System.Windows.Forms.ListBox lstExcludes;
    private System.Windows.Forms.TextBox txtNewExclude;
    private System.Windows.Forms.Button btnAddExclude;
    private System.Windows.Forms.Button btnRemoveExclude;

    private System.Windows.Forms.ProgressBar progressBarOverall;
    private System.Windows.Forms.Label lblProgress;
    private System.Windows.Forms.Button btnDryRun;
    private System.Windows.Forms.Button btnSync;
    private System.Windows.Forms.Button btnCancel;

    private System.Windows.Forms.TabControl tabLogs;
    private System.Windows.Forms.TabPage tabInfos;
    private System.Windows.Forms.TabPage tabWarnings;
    private System.Windows.Forms.TabPage tabErrors;
    private System.Windows.Forms.ListView lvInfos;
    private System.Windows.Forms.ListView lvWarnings;
    private System.Windows.Forms.ListView lvErrors;
    private System.Windows.Forms.ColumnHeader colTimeInfo;
    private System.Windows.Forms.ColumnHeader colMsgInfo;
    private System.Windows.Forms.ColumnHeader colFileInfo;
    private System.Windows.Forms.ColumnHeader colActionInfo;
    private System.Windows.Forms.ColumnHeader colStatusInfo;
    private System.Windows.Forms.ColumnHeader colTimeWarn;
    private System.Windows.Forms.ColumnHeader colMsgWarn;
    private System.Windows.Forms.ColumnHeader colFileWarn;
    private System.Windows.Forms.ColumnHeader colActionWarn;
    private System.Windows.Forms.ColumnHeader colStatusWarn;
    private System.Windows.Forms.ColumnHeader colTimeErr;
    private System.Windows.Forms.ColumnHeader colMsgErr;
    private System.Windows.Forms.ColumnHeader colFileErr;
    private System.Windows.Forms.ColumnHeader colActionErr;
    private System.Windows.Forms.ColumnHeader colStatusErr;

    private System.Windows.Forms.Button btnExportLogs;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        lblLocalRoot = new Label();
        txtLocalRoot = new TextBox();
        btnBrowseLocal = new Button();
        lblRemoteRoot = new Label();
        txtRemoteRoot = new TextBox();
        grpConnection = new GroupBox();
        rbFtp = new RadioButton();
        rbSftp = new RadioButton();
        lblPassword = new Label();
        txtPassword = new TextBox();
        lblUsername = new Label();
        txtUsername = new TextBox();
        lblPort = new Label();
        txtPort = new TextBox();
        lblHost = new Label();
        txtHost = new TextBox();
        tvFolders = new TreeView();
        grpExcludes = new GroupBox();
        btnRemoveExclude = new Button();
        btnAddExclude = new Button();
        txtNewExclude = new TextBox();
        lstExcludes = new ListBox();
        progressBarOverall = new ProgressBar();
        lblProgress = new Label();
        btnDryRun = new Button();
        btnSync = new Button();
        btnCancel = new Button();
        tabLogs = new TabControl();
        tabInfos = new TabPage();
        lvInfos = new ListView();
        colTimeInfo = new ColumnHeader();
        colMsgInfo = new ColumnHeader();
        colFileInfo = new ColumnHeader();
        colActionInfo = new ColumnHeader();
        colStatusInfo = new ColumnHeader();
        tabWarnings = new TabPage();
        lvWarnings = new ListView();
        colTimeWarn = new ColumnHeader();
        colMsgWarn = new ColumnHeader();
        colFileWarn = new ColumnHeader();
        colActionWarn = new ColumnHeader();
        colStatusWarn = new ColumnHeader();
        tabErrors = new TabPage();
        lvErrors = new ListView();
        colTimeErr = new ColumnHeader();
        colMsgErr = new ColumnHeader();
        colFileErr = new ColumnHeader();
        colActionErr = new ColumnHeader();
        colStatusErr = new ColumnHeader();
        btnExportLogs = new Button();
        grpConnection.SuspendLayout();
        grpExcludes.SuspendLayout();
        tabLogs.SuspendLayout();
        tabInfos.SuspendLayout();
        tabWarnings.SuspendLayout();
        tabErrors.SuspendLayout();
        SuspendLayout();
        // 
        // lblLocalRoot
        // 
        lblLocalRoot.AutoSize = true;
        lblLocalRoot.Location = new Point(12, 15);
        lblLocalRoot.Name = "lblLocalRoot";
        lblLocalRoot.Size = new Size(73, 15);
        lblLocalRoot.TabIndex = 0;
        lblLocalRoot.Text = "Dossier local";
        // 
        // txtLocalRoot
        // 
        txtLocalRoot.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtLocalRoot.Location = new Point(110, 12);
        txtLocalRoot.Name = "txtLocalRoot";
        txtLocalRoot.Size = new Size(600, 23);
        txtLocalRoot.TabIndex = 1;
        // 
        // btnBrowseLocal
        // 
        btnBrowseLocal.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnBrowseLocal.Location = new Point(716, 11);
        btnBrowseLocal.Name = "btnBrowseLocal";
        btnBrowseLocal.Size = new Size(90, 25);
        btnBrowseLocal.TabIndex = 2;
        btnBrowseLocal.Text = "Parcourir...";
        btnBrowseLocal.UseVisualStyleBackColor = true;
        btnBrowseLocal.Click += btnBrowseLocal_Click;
        // 
        // lblRemoteRoot
        // 
        lblRemoteRoot.AutoSize = true;
        lblRemoteRoot.Location = new Point(12, 47);
        lblRemoteRoot.Name = "lblRemoteRoot";
        lblRemoteRoot.Size = new Size(84, 15);
        lblRemoteRoot.TabIndex = 3;
        lblRemoteRoot.Text = "Dossier distant";
        // 
        // txtRemoteRoot
        // 
        txtRemoteRoot.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtRemoteRoot.Location = new Point(110, 44);
        txtRemoteRoot.Name = "txtRemoteRoot";
        txtRemoteRoot.Size = new Size(696, 23);
        txtRemoteRoot.TabIndex = 4;
        // 
        // grpConnection
        // 
        grpConnection.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        grpConnection.Controls.Add(rbFtp);
        grpConnection.Controls.Add(rbSftp);
        grpConnection.Controls.Add(lblPassword);
        grpConnection.Controls.Add(txtPassword);
        grpConnection.Controls.Add(lblUsername);
        grpConnection.Controls.Add(txtUsername);
        grpConnection.Controls.Add(lblPort);
        grpConnection.Controls.Add(txtPort);
        grpConnection.Controls.Add(lblHost);
        grpConnection.Controls.Add(txtHost);
        grpConnection.Location = new Point(512, 80);
        grpConnection.Name = "grpConnection";
        grpConnection.Size = new Size(294, 160);
        grpConnection.TabIndex = 5;
        grpConnection.TabStop = false;
        grpConnection.Text = "Connexion";
        // 
        // rbFtp
        // 
        rbFtp.AutoSize = true;
        rbFtp.Location = new Point(190, 132);
        rbFtp.Name = "rbFtp";
        rbFtp.Size = new Size(45, 19);
        rbFtp.TabIndex = 9;
        rbFtp.TabStop = true;
        rbFtp.Text = "FTP";
        rbFtp.UseVisualStyleBackColor = true;
        // 
        // rbSftp
        // 
        rbSftp.AutoSize = true;
        rbSftp.Checked = true;
        rbSftp.Location = new Point(130, 132);
        rbSftp.Name = "rbSftp";
        rbSftp.Size = new Size(51, 19);
        rbSftp.TabIndex = 8;
        rbSftp.TabStop = true;
        rbSftp.Text = "SFTP";
        rbSftp.UseVisualStyleBackColor = true;
        // 
        // lblPassword
        // 
        lblPassword.AutoSize = true;
        lblPassword.Location = new Point(16, 108);
        lblPassword.Name = "lblPassword";
        lblPassword.Size = new Size(77, 15);
        lblPassword.TabIndex = 7;
        lblPassword.Text = "Mot de passe";
        // 
        // txtPassword
        // 
        txtPassword.Location = new Point(100, 105);
        txtPassword.Name = "txtPassword";
        txtPassword.PasswordChar = '●';
        txtPassword.Size = new Size(180, 23);
        txtPassword.TabIndex = 6;
        // 
        // lblUsername
        // 
        lblUsername.AutoSize = true;
        lblUsername.Location = new Point(16, 79);
        lblUsername.Name = "lblUsername";
        lblUsername.Size = new Size(60, 15);
        lblUsername.TabIndex = 5;
        lblUsername.Text = "Utilisateur";
        // 
        // txtUsername
        // 
        txtUsername.Location = new Point(100, 76);
        txtUsername.Name = "txtUsername";
        txtUsername.Size = new Size(180, 23);
        txtUsername.TabIndex = 4;
        // 
        // lblPort
        // 
        lblPort.AutoSize = true;
        lblPort.Location = new Point(16, 50);
        lblPort.Name = "lblPort";
        lblPort.Size = new Size(29, 15);
        lblPort.TabIndex = 3;
        lblPort.Text = "Port";
        // 
        // txtPort
        // 
        txtPort.Location = new Point(100, 47);
        txtPort.Name = "txtPort";
        txtPort.Size = new Size(60, 23);
        txtPort.TabIndex = 2;
        txtPort.Text = "22";
        // 
        // lblHost
        // 
        lblHost.AutoSize = true;
        lblHost.Location = new Point(16, 22);
        lblHost.Name = "lblHost";
        lblHost.Size = new Size(33, 15);
        lblHost.TabIndex = 1;
        lblHost.Text = "Hôte";
        // 
        // txtHost
        // 
        txtHost.Location = new Point(100, 19);
        txtHost.Name = "txtHost";
        txtHost.Size = new Size(180, 23);
        txtHost.TabIndex = 0;
        // 
        // tvFolders
        // 
        tvFolders.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
        tvFolders.CheckBoxes = true;
        tvFolders.Location = new Point(12, 80);
        tvFolders.Name = "tvFolders";
        tvFolders.Size = new Size(494, 260);
        tvFolders.TabIndex = 6;
        tvFolders.AfterCheck += tvFolders_AfterCheck;
        // 
        // grpExcludes
        // 
        grpExcludes.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        grpExcludes.Controls.Add(btnRemoveExclude);
        grpExcludes.Controls.Add(btnAddExclude);
        grpExcludes.Controls.Add(txtNewExclude);
        grpExcludes.Controls.Add(lstExcludes);
        grpExcludes.Location = new Point(512, 246);
        grpExcludes.Name = "grpExcludes";
        grpExcludes.Size = new Size(294, 194);
        grpExcludes.TabIndex = 7;
        grpExcludes.TabStop = false;
        grpExcludes.Text = "Exclusions";
        // 
        // btnRemoveExclude
        // 
        btnRemoveExclude.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnRemoveExclude.Location = new Point(209, 160);
        btnRemoveExclude.Name = "btnRemoveExclude";
        btnRemoveExclude.Size = new Size(71, 23);
        btnRemoveExclude.TabIndex = 3;
        btnRemoveExclude.Text = "Supprimer";
        btnRemoveExclude.UseVisualStyleBackColor = true;
        btnRemoveExclude.Click += btnRemoveExclude_Click;
        // 
        // btnAddExclude
        // 
        btnAddExclude.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnAddExclude.Location = new Point(209, 131);
        btnAddExclude.Name = "btnAddExclude";
        btnAddExclude.Size = new Size(71, 23);
        btnAddExclude.TabIndex = 2;
        btnAddExclude.Text = "Ajouter";
        btnAddExclude.UseVisualStyleBackColor = true;
        btnAddExclude.Click += btnAddExclude_Click;
        // 
        // txtNewExclude
        // 
        txtNewExclude.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        txtNewExclude.Location = new Point(14, 131);
        txtNewExclude.Name = "txtNewExclude";
        txtNewExclude.PlaceholderText = "*.ini";
        txtNewExclude.Size = new Size(189, 23);
        txtNewExclude.TabIndex = 1;
        // 
        // lstExcludes
        // 
        lstExcludes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        lstExcludes.FormattingEnabled = true;
        lstExcludes.Location = new Point(14, 22);
        lstExcludes.Name = "lstExcludes";
        lstExcludes.Size = new Size(266, 94);
        lstExcludes.TabIndex = 0;
        // 
        // progressBarOverall
        // 
        progressBarOverall.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        progressBarOverall.Location = new Point(12, 350);
        progressBarOverall.Name = "progressBarOverall";
        progressBarOverall.Size = new Size(494, 23);
        progressBarOverall.TabIndex = 8;
        // 
        // lblProgress
        // 
        lblProgress.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        lblProgress.AutoSize = true;
        lblProgress.Location = new Point(12, 331);
        lblProgress.Name = "lblProgress";
        lblProgress.Size = new Size(30, 15);
        lblProgress.TabIndex = 9;
        lblProgress.Text = "0 / 0";
        // 
        // btnDryRun
        // 
        btnDryRun.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnDryRun.Location = new Point(512, 446);
        btnDryRun.Name = "btnDryRun";
        btnDryRun.Size = new Size(90, 27);
        btnDryRun.TabIndex = 10;
        btnDryRun.Text = "Simulation";
        btnDryRun.UseVisualStyleBackColor = true;
        btnDryRun.Click += btnDryRun_Click;
        // 
        // btnSync
        // 
        btnSync.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnSync.Location = new Point(608, 446);
        btnSync.Name = "btnSync";
        btnSync.Size = new Size(90, 27);
        btnSync.TabIndex = 11;
        btnSync.Text = "Synchroniser";
        btnSync.UseVisualStyleBackColor = true;
        btnSync.Click += btnSync_Click;
        // 
        // btnCancel
        // 
        btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnCancel.Enabled = false;
        btnCancel.Location = new Point(704, 446);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(90, 27);
        btnCancel.TabIndex = 12;
        btnCancel.Text = "Arrêter";
        btnCancel.UseVisualStyleBackColor = true;
        btnCancel.Click += btnCancel_Click;
        // 
        // tabLogs
        // 
        tabLogs.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        tabLogs.Controls.Add(tabInfos);
        tabLogs.Controls.Add(tabWarnings);
        tabLogs.Controls.Add(tabErrors);
        tabLogs.Location = new Point(12, 379);
        tabLogs.Name = "tabLogs";
        tabLogs.SelectedIndex = 0;
        tabLogs.Size = new Size(494, 152);
        tabLogs.TabIndex = 13;
        // 
        // tabInfos
        // 
        tabInfos.Controls.Add(lvInfos);
        tabInfos.Location = new Point(4, 24);
        tabInfos.Name = "tabInfos";
        tabInfos.Padding = new Padding(3);
        tabInfos.Size = new Size(486, 124);
        tabInfos.TabIndex = 0;
        tabInfos.Text = "Infos";
        tabInfos.UseVisualStyleBackColor = true;
        // 
        // lvInfos
        // 
        lvInfos.Columns.AddRange(new ColumnHeader[] { colTimeInfo, colMsgInfo, colFileInfo, colActionInfo, colStatusInfo });
        lvInfos.Dock = DockStyle.Fill;
        lvInfos.FullRowSelect = true;
        lvInfos.GridLines = true;
        lvInfos.Location = new Point(3, 3);
        lvInfos.Name = "lvInfos";
        lvInfos.Size = new Size(480, 118);
        lvInfos.TabIndex = 0;
        lvInfos.UseCompatibleStateImageBehavior = false;
        lvInfos.View = View.Details;
        // 
        // colTimeInfo
        // 
        colTimeInfo.Text = "Heure";
        // 
        // colMsgInfo
        // 
        colMsgInfo.Text = "Message";
        colMsgInfo.Width = 160;
        // 
        // colFileInfo
        // 
        colFileInfo.Text = "Fichier";
        colFileInfo.Width = 100;
        // 
        // colActionInfo
        // 
        colActionInfo.Text = "Action";
        // 
        // colStatusInfo
        // 
        colStatusInfo.Text = "Statut";
        // 
        // tabWarnings
        // 
        tabWarnings.Controls.Add(lvWarnings);
        tabWarnings.Location = new Point(4, 24);
        tabWarnings.Name = "tabWarnings";
        tabWarnings.Padding = new Padding(3);
        tabWarnings.Size = new Size(446, 124);
        tabWarnings.TabIndex = 1;
        tabWarnings.Text = "Avertissements";
        tabWarnings.UseVisualStyleBackColor = true;
        // 
        // lvWarnings
        // 
        lvWarnings.Columns.AddRange(new ColumnHeader[] { colTimeWarn, colMsgWarn, colFileWarn, colActionWarn, colStatusWarn });
        lvWarnings.Dock = DockStyle.Fill;
        lvWarnings.FullRowSelect = true;
        lvWarnings.GridLines = true;
        lvWarnings.Location = new Point(3, 3);
        lvWarnings.Name = "lvWarnings";
        lvWarnings.Size = new Size(440, 118);
        lvWarnings.TabIndex = 0;
        lvWarnings.UseCompatibleStateImageBehavior = false;
        lvWarnings.View = View.Details;
        // 
        // colTimeWarn
        // 
        colTimeWarn.Text = "Heure";
        // 
        // colMsgWarn
        // 
        colMsgWarn.Text = "Message";
        colMsgWarn.Width = 160;
        // 
        // colFileWarn
        // 
        colFileWarn.Text = "Fichier";
        colFileWarn.Width = 100;
        // 
        // colActionWarn
        // 
        colActionWarn.Text = "Action";
        // 
        // colStatusWarn
        // 
        colStatusWarn.Text = "Statut";
        // 
        // tabErrors
        // 
        tabErrors.Controls.Add(lvErrors);
        tabErrors.Location = new Point(4, 24);
        tabErrors.Name = "tabErrors";
        tabErrors.Padding = new Padding(3);
        tabErrors.Size = new Size(446, 124);
        tabErrors.TabIndex = 2;
        tabErrors.Text = "Erreurs";
        tabErrors.UseVisualStyleBackColor = true;
        // 
        // lvErrors
        // 
        lvErrors.Columns.AddRange(new ColumnHeader[] { colTimeErr, colMsgErr, colFileErr, colActionErr, colStatusErr });
        lvErrors.Dock = DockStyle.Fill;
        lvErrors.FullRowSelect = true;
        lvErrors.GridLines = true;
        lvErrors.Location = new Point(3, 3);
        lvErrors.Name = "lvErrors";
        lvErrors.Size = new Size(440, 118);
        lvErrors.TabIndex = 0;
        lvErrors.UseCompatibleStateImageBehavior = false;
        lvErrors.View = View.Details;
        // 
        // colTimeErr
        // 
        colTimeErr.Text = "Heure";
        // 
        // colMsgErr
        // 
        colMsgErr.Text = "Message";
        colMsgErr.Width = 160;
        // 
        // colFileErr
        // 
        colFileErr.Text = "Fichier";
        colFileErr.Width = 100;
        // 
        // colActionErr
        // 
        colActionErr.Text = "Action";
        // 
        // colStatusErr
        // 
        colStatusErr.Text = "Statut";
        // 
        // btnExportLogs
        // 
        btnExportLogs.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnExportLogs.Location = new Point(12, 537);
        btnExportLogs.Name = "btnExportLogs";
        btnExportLogs.Size = new Size(106, 27);
        btnExportLogs.TabIndex = 14;
        btnExportLogs.Text = "Exporter logs";
        btnExportLogs.UseVisualStyleBackColor = true;
        btnExportLogs.Click += btnExportLogs_Click;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(820, 576);
        Controls.Add(btnExportLogs);
        Controls.Add(tabLogs);
        Controls.Add(btnCancel);
        Controls.Add(btnSync);
        Controls.Add(btnDryRun);
        Controls.Add(lblProgress);
        Controls.Add(progressBarOverall);
        Controls.Add(grpExcludes);
        Controls.Add(tvFolders);
        Controls.Add(grpConnection);
        Controls.Add(txtRemoteRoot);
        Controls.Add(lblRemoteRoot);
        Controls.Add(btnBrowseLocal);
        Controls.Add(txtLocalRoot);
        Controls.Add(lblLocalRoot);
        Name = "MainForm";
        Text = "FTP / SFTP Sync (squelette)";
        grpConnection.ResumeLayout(false);
        grpConnection.PerformLayout();
        grpExcludes.ResumeLayout(false);
        grpExcludes.PerformLayout();
        tabLogs.ResumeLayout(false);
        tabInfos.ResumeLayout(false);
        tabWarnings.ResumeLayout(false);
        tabErrors.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
}
