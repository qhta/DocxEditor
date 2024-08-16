using System.Windows;
using System.Windows.Controls;

namespace DocxControls;

/// <summary>
/// Interaction logic for SettingsView.xaml
/// </summary>
public partial class SettingsView : UserControl
{
  /// <summary>
  /// Default constructor
  /// </summary>
  public SettingsView()
  {
    InitializeComponent();
  }

  private void FrameworkElement_OnToolTipOpening(object sender, ToolTipEventArgs e)
  {
    if (sender is FrameworkElement frameworkElement)
    {
      if (frameworkElement.ToolTip is ToolTip toolTip)
      {
        if (frameworkElement.DataContext is SettingViewModel settingViewModel)
          toolTip.DataContext = new CustomToolTipViewModel { Title = settingViewModel.Tooltip, Content = settingViewModel.Description };
      }
    }
  }

}