using System.Windows;
using System.Windows.Controls;

namespace DocxControls;

/// <summary>
/// Interaction logic for PropertiesView.xaml
/// </summary>
public partial class PropertiesView : UserControl
{
  /// <summary>
  /// Default constructor
  /// </summary>
  public PropertiesView()
  {
    InitializeComponent();
  }

  private void FrameworkElement_OnToolTipOpening(object sender, ToolTipEventArgs e)
  {
    if (sender is FrameworkElement frameworkElement)
    {
      if (frameworkElement.ToolTip is ToolTip toolTip)
      {
        if (frameworkElement.DataContext is PropertyViewModel propertyViewModel)
        {
          var title = propertyViewModel.Tooltip;
          var content = propertyViewModel.Description;
          if (title != null)
          {
            toolTip.DataContext = new CustomToolTipViewModel { Title = title, Content = content };
          }
          else
          {
            e.Handled = true;
          }
        }
      }
    }
  }

  private void DownButton_Click(object sender, RoutedEventArgs e)
  {
    throw new NotImplementedException();
  }
}