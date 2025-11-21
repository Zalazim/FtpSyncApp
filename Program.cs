using System;
using System.Windows.Forms;

namespace FtpSyncApp;

/// <summary>
/// Point d'entrée principal de l'application.
/// </summary>
internal static class Program
{
    /// <summary>
    /// Lance l'application WinForms.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        // Configuration “standard” des applications WinForms modernes :
        // DPI, styles visuels, police par défaut, etc.
        ApplicationConfiguration.Initialize();

        // Affiche la fenêtre principale de l'application.
        Application.Run(new MainForm());
    }
}
