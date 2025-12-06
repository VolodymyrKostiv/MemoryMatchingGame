using MemoryMatchingGame.WPF.Extensions;
using MemoryMatchingGame.WPF.Services.Implementations;
using MemoryMatchingGame.WPF.Services.Interfaces;
using MemoryMatchingGame.WPF.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace MemoryMatchingGame.WPF;

public partial class App : Application
{
    public static ServiceProvider? ServiceProvider { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var services = new ServiceCollection();
        ConfigureServices(services);

        ServiceProvider = services.BuildServiceProvider();

        var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddInfrastructure();
        
        services.AddSingleton<MainViewModel>();

        services.AddSingleton<INavigation, NavigationService>();
        services.AddSingleton<IImageService, ImageService>();   
        services.AddSingleton<IImageCacheService, ImageCacheService>();

        services.AddTransient<MenuViewModel>();
        services.AddTransient<SettingsViewModel>();
        services.AddTransient<GameViewModel>();

        services.AddTransient<MainWindow>();
    }   
}
