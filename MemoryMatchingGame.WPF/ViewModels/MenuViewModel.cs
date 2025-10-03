using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MemoryMatchingGame.Core.Services.Interfaces;
using MemoryMatchingGame.WPF.Services.Interfaces;
using System.Windows.Input;

namespace MemoryMatchingGame.WPF.ViewModels;

public class MenuViewModel : ViewModelBase
{
    private readonly IGameContext _gameContext;
    private readonly IGameEngine _gameEngine;
    private readonly INavigation _navigationService;

    public ICommand StartNewGameCommand { get; }
    public ICommand OpenSettingsCommand { get; }    
    public ICommand ExitGameCommand { get; }

    public MenuViewModel()
    {
        
    }

    public MenuViewModel(
        IGameContext gameContext,
        IGameEngine gameEngine,
        INavigation navigationService)
    {
        _gameContext = gameContext;
        _gameEngine = gameEngine;
        _navigationService = navigationService;

        StartNewGameCommand = new RelayCommand(StartNewGame);
        OpenSettingsCommand = new RelayCommand(OpenSettings);   
        ExitGameCommand = new RelayCommand(ExitGame);   
    }

    public void StartNewGame()
    {
        _navigationService.NavigateTo<GameViewModel>();
    }

    public void OpenSettings()
    {
        _navigationService.NavigateTo<SettingsViewModel>();
    }

    public void ExitGame()
    {
        App.Current.Shutdown();
    }
}
