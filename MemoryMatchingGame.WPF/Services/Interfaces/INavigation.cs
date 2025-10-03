using GalaSoft.MvvmLight;

namespace MemoryMatchingGame.WPF.Services.Interfaces;

public interface INavigation
{
    void NavigateTo<TViewModel>() where TViewModel : ViewModelBase;
    void GoBack();
}
