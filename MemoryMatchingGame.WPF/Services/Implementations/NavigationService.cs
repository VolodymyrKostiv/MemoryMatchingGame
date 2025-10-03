using GalaSoft.MvvmLight;
using MemoryMatchingGame.WPF.Services.Interfaces;
using MemoryMatchingGame.WPF.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace MemoryMatchingGame.WPF.Services.Implementations;

public class NavigationService : INavigation
{
    private readonly IServiceProvider _serviceProvider;
    private readonly Stack<ViewModelBase> _history = new();

    public MainViewModel MainWindowVM { get; }

    public NavigationService(IServiceProvider sp, MainViewModel mainWindowVM)
    {
        _serviceProvider = sp;
        MainWindowVM = mainWindowVM;
    }

    public void NavigateTo<TViewModel>() where TViewModel : ViewModelBase
    {
        if (MainWindowVM.CurrentViewModel != null)
        {
            _history.Push(MainWindowVM.CurrentViewModel);
        }

        var vm = _serviceProvider.GetRequiredService<TViewModel>();
        MainWindowVM.CurrentViewModel = vm;
    }

    public void GoBack()
    {
        if (_history.Count > 0)
        {
            MainWindowVM.CurrentViewModel = _history.Pop();
        }
    }
}
