using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MemoryMatchingGame.Core.Services.Implementations.Rules;
using MemoryMatchingGame.Core.Services.Interfaces;
using MemoryMatchingGame.Core.Services.Interfaces.Rules;
using MemoryMatchingGame.WPF.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MemoryMatchingGame.WPF.ViewModels;

public class SettingsViewModel : ViewModelBase
{
    private readonly IGameContext _gameContext;
    private readonly IRuleSetConstraints _ruleSetConstraints;
    private readonly INavigation _navigationService;

    private bool _isCustomRules;
    public bool IsCustomRules
    {
        get => _isCustomRules;
        set
        {
            Set(ref _isCustomRules, value);
            RaisePropertyChanged(nameof(IsValid));
        }
    }

    private int _totalCards;
    public int TotalCards
    {
        get => _totalCards;
        set
        {
            Set(ref _totalCards, value);
            RaisePropertyChanged(nameof(IsValid));
        }
    }

    private int _cardsPerMatch;
    public int CardsPerMatch
    {
        get => _cardsPerMatch;
        set
        {
            Set(ref _cardsPerMatch, value);
            RaisePropertyChanged(nameof(IsValid));
        }
    }

    private IRuleSet _selectedPredefinedRule;
    public IRuleSet SelectedPredefinedRule
    {
        get => _selectedPredefinedRule;
        set
        {
            Set(ref _selectedPredefinedRule, value);
            RaisePropertyChanged(nameof(IsValid));
        }
    }

    public bool IsValid => !IsCustomRules ||
        IsCustomRules && 
        _ruleSetConstraints.Validate(new CustomRuleSet(TotalCards, CardsPerMatch), out _);

    public ObservableCollection<IRuleSet> PredefinedRules { get; set; }

    public ICommand ApplyCommand { get; set; }

    public SettingsViewModel()
    {
        
    }

    public SettingsViewModel(
        IRuleSetProvider ruleSetProvider, 
        IGameContext gameContext,
        IRuleSetConstraints ruleSetConstraints,
        INavigation navigationService)
    {
        PredefinedRules = new ObservableCollection<IRuleSet>(
            ruleSetProvider.GetPredefined());
        SelectedPredefinedRule = PredefinedRules.First();

        _gameContext = gameContext;
        _ruleSetConstraints = ruleSetConstraints;
        _navigationService = navigationService;

        ApplyCommand = new RelayCommand(Apply, IsValid);
    }

    private void Apply()
    {
        if (_isCustomRules)
        {
            var rules = new CustomRuleSet(TotalCards, CardsPerMatch);
            _gameContext.CurrentRuleSet = rules;
        }
        else
        {
            _gameContext.CurrentRuleSet = SelectedPredefinedRule;
        }
        
        _navigationService.GoBack();
    }
}
