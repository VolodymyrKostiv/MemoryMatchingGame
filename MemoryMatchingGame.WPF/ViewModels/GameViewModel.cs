using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MemoryMatchingGame.Core.Entities;
using MemoryMatchingGame.Core.Services.Interfaces;
using MemoryMatchingGame.Core.Services.Interfaces.Rules;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MemoryMatchingGame.WPF.ViewModels;

public class GameViewModel : ViewModelBase
{
    private readonly IGameContext _gameContext;
    private readonly IGameEngine _gameEngine;
    private readonly IRuleSet _ruleSet;

    private int _rows;
    public int Rows
    {
        get => _rows;
        set => Set(ref _rows, value);
    }

    private int _columns;
    public int Columns
    {
        get => _columns;
        set => Set(ref _columns, value);
    }

    public ObservableCollection<Card> Cards { get; set; }

    public ICommand FlipCommand { get; set; }   

    public GameViewModel()
    {
        
    }

    public GameViewModel(
        IGameContext gameContext,
        IGameEngine gameEngine)
    {
        _gameContext = gameContext;
        _gameEngine = gameEngine;
        _ruleSet = _gameContext.CurrentRuleSet;

        FlipCommand = new RelayCommand<Card>(card => _gameEngine.FlipCard(card));
        _gameEngine.StartNewGame(_ruleSet);
        Cards = _gameEngine.Cards;
        (Rows, Columns) = _gameEngine.CalculateGrid(_ruleSet.TotalCards);
    }
}
