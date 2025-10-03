using GalaSoft.MvvmLight;

namespace MemoryMatchingGame.WPF.ViewModels;

public class MainViewModel : ViewModelBase
{
    private ViewModelBase _currentView;
    public ViewModelBase CurrentViewModel
    {
        get => _currentView;
        set => Set(ref _currentView, value);
    }

    public MainViewModel()
    {
        
    }
}
