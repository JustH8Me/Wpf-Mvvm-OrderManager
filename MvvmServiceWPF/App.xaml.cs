using System.Globalization;
using System.Windows;
using System.Windows.Markup;
using Microsoft.Extensions.DependencyInjection;
using MvvmServiceWPF.Models;
using MvvmServiceWPF.ViewModels;

namespace MvvmServiceWPF;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public IServiceProvider Services { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        var culture = new CultureInfo("ru-RU");
        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = culture;
        FrameworkElement.LanguageProperty.OverrideMetadata(
            typeof(FrameworkElement),
            new FrameworkPropertyMetadata(
                XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
        base.OnStartup(e);
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        Services = serviceCollection.BuildServiceProvider();
        var mainWindow = Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IOrderService, SqlOrderService>();
        services.AddTransient<ActiveOrdersViewModel>();
        services.AddTransient<AddOrderViewModel>();
        services.AddTransient<HomeViewModel>();
        services.AddTransient<HistoryViewModel>();
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<MainWindow>();
    }
}