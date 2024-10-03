using System.Windows;

namespace DocxControls.Views;
/// <summary>
/// Interaction logic for PluginsWindow.xaml
/// </summary>
public partial class PluginsWindow : Window
{
  /// <summary>
  /// Default constructor.
  /// </summary>
  public PluginsWindow()
  {
    InitializeComponent();
    Closed += PluginsWindow_Closed;
  }

  private void PluginsWindow_Closed(object? sender, EventArgs e)
  {
    Application.Instance.PluginsWindow = null;
  }
}
