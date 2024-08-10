using System.Windows;
using System.Windows.Controls;

namespace DocxControls;

/// <summary>
/// Interaction logic for PropertiesView.xaml
/// </summary>
public partial class CustomPropertiesView : UserControl
{
  /// <summary>
  /// Default constructor
  /// </summary>
  public CustomPropertiesView()
  {
    InitializeComponent();
  }

  private void TypeColumn_LostFocus(object sender, RoutedEventArgs e)
  {
    throw new NotImplementedException();
  }
}