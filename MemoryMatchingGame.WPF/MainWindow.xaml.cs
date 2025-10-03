using GalaSoft.MvvmLight;
using MemoryMatchingGame.WPF.ViewModels;
using System.Windows;

namespace MemoryMatchingGame.WPF;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        var viewModel = App.ServiceProvider?.GetService(typeof(MainViewModel)) as MainViewModel;
        var currentViewModel = App.ServiceProvider?.GetService(typeof(MenuViewModel)) as ViewModelBase;
        viewModel!.CurrentViewModel = currentViewModel!;
        DataContext = viewModel;
    }
}