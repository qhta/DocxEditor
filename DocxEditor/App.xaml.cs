using System.Globalization;
using System.Windows;

namespace DocxEditor;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{


  protected override void OnStartup(StartupEventArgs e)
  {
    base.OnStartup(e);
    Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
  }


}

