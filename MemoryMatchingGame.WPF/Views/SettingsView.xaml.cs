using MemoryMatchingGame.WPF.ViewModels;
using System.Windows.Controls;

namespace MemoryMatchingGame.WPF.Views;

public partial class SettingsView : UserControl
{
    public SettingsView()
    {
        InitializeComponent();
        DataContext = App.ServiceProvider?.GetService(typeof(SettingsViewModel));
    }
}
