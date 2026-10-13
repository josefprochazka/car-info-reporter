using System.Windows;
using CarInfoReporter.Services;
using CarInfoReporter.ViewModels;

namespace CarInfoReporter;

/// <summary>
/// The window has no logic of its own, everything is bound to <see cref="MainViewModel"/>.
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainViewModel(new DialogService());
    }
}
