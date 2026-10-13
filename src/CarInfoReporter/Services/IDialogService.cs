namespace CarInfoReporter.Services;

/// <summary>
/// The dialogs the view model needs. Thanks to this interface the view model
/// does not know anything about windows and can be tested without them.
/// </summary>
public interface IDialogService
{
    /// <summary>Lets the user pick an XML file.</summary>
    /// <returns>Path of the selected file, or null when the user cancelled the dialog.</returns>
    string? SelectXmlFile();

    void ShowError(string title, string message);
}
