using MemoryMatchingGame.WPF.ViewModels;
using System.Windows.Controls;

namespace MemoryMatchingGame.WPF.Views;

public partial class GameView : UserControl
{
    public GameView()
    {
        InitializeComponent();
        DataContext = App.ServiceProvider?.GetService(typeof(GameViewModel));
    }
}
