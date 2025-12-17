using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MemoryMatchingGame.Core.Entities;
using MemoryMatchingGame.Core.Services.Interfaces;
using MemoryMatchingGame.Core.Services.Interfaces.Rules;
using MemoryMatchingGame.WPF.Models;
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

    public ObservableCollection<CardImage>? Cards { get; set; }

    private BitmapImage _backImage;
    public BitmapImage BackImage 
    { 
        get => _backImage; 
        set => Set(ref _backImage, value);
    }

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
        var cards = _gameEngine.Cards!;
        (Rows, Columns) = _gameEngine.CalculateGrid(_ruleSet.TotalCards);
        string packsRoot = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ImagePacks");

        var packs = Directory.EnumerateDirectories(packsRoot)
            .Select(Path.GetFileName)
            .ToList();

        string packPath = Path.Combine(packsRoot, packs[0]);
        string frontPath = Path.Combine(packPath, "Front");
        string backPath = Path.Combine(packPath, "Back");

        var backImage = LoadImage(Directory.EnumerateFiles(backPath, "*.png").First());
        var frontImages = Directory.EnumerateFiles(frontPath, "*.png")
            .OrderBy(f => f)
            .Take(64)
            .Select(LoadImage)
            .ToList();

        var cardsWithImages = new List<CardImage>();
        for (int i = 0; i < cards.Count; i++)
        {
            cardsWithImages.Add(new CardImage() { Card = cards[i], Image = frontImages[i] });
        }

        BackImage = backImage;
        Cards = new ObservableCollection<CardImage>(cardsWithImages);
    }

    BitmapImage LoadImage(string uri)
    {
        var bmp = new BitmapImage();
        
        bmp.BeginInit();

        bmp.UriSource = new Uri(uri, UriKind.Absolute);
        bmp.CacheOption = BitmapCacheOption.OnLoad;
        bmp.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
        bmp.DecodePixelWidth = 100;

        bmp.EndInit();
        bmp.Freeze();

        return bmp;
    }
}
