using MemoryMatchingGame.WPF.ViewModels;
using System.Windows.Controls;

namespace MemoryMatchingGame.WPF.Views;

public partial class MenuView : UserControl
{
    public MenuView()
    {
        InitializeComponent();
        DataContext = App.ServiceProvider?.GetService(typeof(MenuViewModel));
    }
}
