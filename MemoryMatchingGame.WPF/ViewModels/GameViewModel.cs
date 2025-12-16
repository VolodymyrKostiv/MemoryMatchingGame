using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MemoryMatchingGame.Core.Entities;
using MemoryMatchingGame.Core.Services.Interfaces;
using MemoryMatchingGame.Core.Services.Interfaces.Rules;
using MemoryMatchingGame.WPF.Services.Interfaces;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace MemoryMatchingGame.WPF.ViewModels;

public class GameViewModel : ViewModelBase
{
    private readonly IGameContext _gameContext;
    private readonly IGameEngine _gameEngine;
    private readonly IImageService _imageService;
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

    private BitmapImage _sourcePath;
    public BitmapImage SourcePath
    {
        get => _sourcePath;
        set => Set(ref _sourcePath, value);
    }

    public ObservableCollection<Card>? Cards { get; set; }

    public ICommand FlipCommand { get; set; }   

    public GameViewModel()
    {
        
    }

    public GameViewModel(
        IGameContext gameContext,
        IGameEngine gameEngine,
        IImageService imageService)
    {
        _gameContext = gameContext ?? throw new ArgumentNullException(nameof(gameContext));
        _gameEngine = gameEngine ?? throw new ArgumentNullException(nameof(gameEngine));
        _imageService = imageService ?? throw new ArgumentNullException(nameof(imageService));

        _ruleSet = _gameContext.CurrentRuleSet;

        FlipCommand = new RelayCommand<Card>(card => _gameEngine.FlipCard(card));
        _gameEngine.StartNewGame(_ruleSet);
        Cards = _gameEngine.Cards;
        (Rows, Columns) = _gameEngine.CalculateGrid(_ruleSet.TotalCards);

        var currentDirectory = Directory.GetCurrentDirectory();
        //currentDirectory.IndexOf()

        DirectoryInfo d = 
            new(@"D:\Projects\MemoryMatchingGame\MemoryMatchingGame.Resources\Assets\Images\Pokemons\Front");

        FileInfo[] Files = d.GetFiles("*.png");


        //var image = imageService.Load("Pokemons.Back.01_Pokeball");
        //SourcePath = image;
    }
}
