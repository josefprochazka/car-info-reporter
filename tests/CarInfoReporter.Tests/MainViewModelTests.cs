using CarInfoReporter.Services;
using CarInfoReporter.ViewModels;

namespace CarInfoReporter.Tests;

public class MainViewModelTests
{
    private static readonly string SampleFile = Path.Combine(AppContext.BaseDirectory, "data", "car-sales.xml");

    /// <summary>Replaces the real dialogs: "selects" the given file and remembers the shown error.</summary>
    private class FakeDialogService(string? fileToSelect) : IDialogService
    {
        public string? ShownError { get; private set; }

        public string? SelectXmlFile() => fileToSelect;

        public void ShowError(string title, string message) => ShownError = message;
    }

    [Fact]
    public void Selected_file_is_loaded_and_summarized()
    {
        var dialogs = new FakeDialogService(SampleFile);
        var viewModel = new MainViewModel(dialogs);

        viewModel.OpenFileCommand.Execute(null);

        Assert.Equal(SampleFile, viewModel.FilePath);
        Assert.Equal(13, viewModel.Sales.Count);
        Assert.Equal(3, viewModel.WeekendSummaries.Count);
        Assert.Null(dialogs.ShownError);
    }

    [Fact]
    public void Cancelled_dialog_changes_nothing()
    {
        var dialogs = new FakeDialogService(fileToSelect: null);
        var viewModel = new MainViewModel(dialogs);

        viewModel.OpenFileCommand.Execute(null);

        Assert.Empty(viewModel.Sales);
        Assert.Empty(viewModel.WeekendSummaries);
        Assert.Null(dialogs.ShownError);
    }

    [Fact]
    public void File_that_cannot_be_loaded_shows_an_error_and_changes_nothing()
    {
        var dialogs = new FakeDialogService("this-file-does-not-exist.xml");
        var viewModel = new MainViewModel(dialogs);

        viewModel.OpenFileCommand.Execute(null);

        Assert.NotNull(dialogs.ShownError);
        Assert.Empty(viewModel.Sales);
        Assert.Empty(viewModel.WeekendSummaries);
    }
}
