using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace CarInfoReporter.Services;

/// <summary>Shows the standard Windows dialogs.</summary>
public class DialogService : IDialogService
{
    public string? SelectXmlFile()
    {
        var dialog = new OpenFileDialog
        {
            Title = "Select an XML file with car sales",
            Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*",
            // The sample data is copied next to the exe on build.
            InitialDirectory = Path.Combine(AppContext.BaseDirectory, "data"),
        };

        return dialog.ShowDialog() == true ? dialog.FileName : null;
    }

    public void ShowError(string title, string message)
    {
        MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);
    }
}
