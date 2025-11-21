using System;

namespace FtpSyncApp;

/// <summary>
/// Niveau d'importance d'un message de log.
/// </summary>
public enum LogLevel
{
    Info,
    Warning,
    Error
}

/// <summary>
/// Type d'action de synchronisation réalisée ou prévue.
/// </summary>
public enum SyncAction
{
    UploadNew,
    UploadUpdate,
    DeleteRemote,
    Skip
}

/// <summary>
/// Statut d'exécution d'une action de synchronisation.
/// </summary>
public enum SyncStatus
{
    Pending,
    Running,
    Success,
    Failed,
    Ignored
}

/// <summary>
/// Représente une ligne de log affichée dans l'interface.
/// </summary>
public class LogEntry
{
    /// <summary>
    /// Horodatage du message.
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.Now;

    /// <summary>
    /// Niveau d'importance.
    /// </summary>
    public LogLevel Level { get; set; }

    /// <summary>
    /// Message lisible par l'utilisateur.
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Chemin de fichier concerné (local ou distant, si applicable).
    /// </summary>
    public string FilePath { get; set; } = string.Empty;

    /// <summary>
    /// Action de synchronisation associée au message (optionnel).
    /// </summary>
    public SyncAction? Action { get; set; }

    /// <summary>
    /// Statut de l'action associée (optionnel).
    /// </summary>
    public SyncStatus? Status { get; set; }
}
