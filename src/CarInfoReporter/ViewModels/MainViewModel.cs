using System.IO;
using CarInfoReporter.Models;
using CarInfoReporter.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CarInfoReporter.ViewModels;

public partial class MainViewModel(IDialogService dialogService) : ObservableObject
{
    /// <summary>Path of the loaded XML file.</summary>
    [ObservableProperty]
    public partial string FilePath { get; set; } = "No file selected";

    /// <summary>All sold cars from the loaded file.</summary>
    [ObservableProperty]
    public partial List<CarSale> Sales { get; set; } = [];

    /// <summary>Weekend sales summed for each model.</summary>
    [ObservableProperty]
    public partial List<ModelSummary> WeekendSummaries { get; set; } = [];

    /// <summary>Lets the user pick an XML file and loads it. Bound to the "Open XML file" button.</summary>
    [RelayCommand]
    private void OpenFile()
    {
        var filePath = dialogService.SelectXmlFile();

        if (filePath is not null)
        {
            LoadFile(filePath);
        }
    }

    private void LoadFile(string filePath)
    {
        try
        {
            var sales = CarSalesXmlReader.Read(filePath);

            FilePath = filePath;
            Sales = sales;
            WeekendSummaries = WeekendSalesCalculator.SummarizeByModel(sales);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            // InvalidDataException is an IOException too, so a broken file ends up here as well.
            dialogService.ShowError("The file could not be loaded", ex.Message);
        }
    }
}
