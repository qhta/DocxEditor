using System.Globalization;
using System.Windows;

using Syncfusion.Licensing;

namespace DocxEditor;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
  public App()
  {
    // Add your Syncfusion license key for WPF platform with corresponding Syncfusion NuGet version referred in project. For more information about license key see https://help.syncfusion.com/common/essential-studio/licensing/license-key.
    // Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Add your license key here"); 
    SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NCaF1cXGdCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdnWXZfdXVWRmZfUERyWkM=");
  }

  protected override void OnStartup(StartupEventArgs e)
  {
    base.OnStartup(e);
    Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
  }


}

