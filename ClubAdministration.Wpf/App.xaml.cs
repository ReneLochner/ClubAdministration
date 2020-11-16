using ClubAdministration.Wpf.Common;
using ClubAdministration.Wpf.ViewModels;
using System.Windows;

namespace ClubAdministration.Wpf
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    private async void App_Startup(object sender, StartupEventArgs e)
    {
      WindowController controller = new WindowController();
      controller.ShowWindow(await MainViewModel.CreateAsync(controller));
    }
  }
}
