using System;

namespace FtpSyncApp;

/// <summary>
/// Représente une action de synchronisation sur un fichier donné
/// (upload nouveau, mise à jour, suppression distante, etc.).
/// </summary>
public class SyncItem
{
    /// <summary>
    /// Chemin relatif depuis <see cref="SyncConfig.LocalRoot"/> /
    /// <see cref="SyncConfig.RemoteRoot"/> (ex : "gifs/Teen Titans/image01.png").
    /// </summary>
    public string RelativePath { get; set; } = string.Empty;

    /// <summary>
    /// Chemin complet du fichier local (peut être null pour une suppression distante).
    /// </summary>
    public string? LocalFullPath { get; set; }

    /// <summary>
    /// Chemin complet du fichier distant sur le serveur.
    /// </summary>
    public string RemoteFullPath { get; set; } = string.Empty;

    /// <summary>
    /// Action à effectuer (UploadNew, UploadUpdate, DeleteRemote, Skip).
    /// </summary>
    public SyncAction Action { get; set; }

    /// <summary>
    /// Statut d'exécution de l'action.
    /// </summary>
    public SyncStatus Status { get; set; } = SyncStatus.Pending;

    /// <summary>
    /// Taille du fichier local (en octets).
    /// </summary>
    public long LocalSize { get; set; }

    /// <summary>
    /// Date de dernière modification locale (UTC).
    /// </summary>
    public DateTime LocalLastWriteUtc { get; set; }

    /// <summary>
    /// Taille du fichier distant en octets (null si le fichier n'existe pas encore).
    /// </summary>
    public long? RemoteSize { get; set; }

    /// <summary>
    /// Date de dernière modification distante (UTC).
    /// </summary>
    public DateTime? RemoteLastWriteUtc { get; set; }

    /// <summary>
    /// Message d'erreur éventuel associé à l'action.
    /// </summary>
    public string? ErrorMessage { get; set; }
}
