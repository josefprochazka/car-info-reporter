using System.IO;
using CarInfoReporter.Models;
using CarInfoReporter.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CarInfoReporter.ViewModels;

public partial class MainViewModel(IDialogService dialogService) : ObservableObject
{
    // [ObservableProperty] generates a public property from each field (FilePath, Sales, WeekendSummaries)
    // that notifies the window when its value changes.

    /// <summary>Path of the loaded XML file.</summary>
    [ObservableProperty]
    private string _filePath = "No file selected";

    /// <summary>All sold cars from the loaded file.</summary>
    [ObservableProperty]
    private List<CarSale> _sales = [];

    /// <summary>Weekend sales summed for each model.</summary>
    [ObservableProperty]
    private List<ModelSummary> _weekendSummaries = [];

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
