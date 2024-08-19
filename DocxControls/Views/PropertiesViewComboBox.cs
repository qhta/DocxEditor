using System.Windows;
using System.Windows.Controls;
namespace DocxControls;

/// <summary>
///  ComboBox that allows to edit a set o properties.
/// </summary>
public class PropertiesViewComboBox : ComboBox
{
  static PropertiesViewComboBox()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertiesViewComboBox), new FrameworkPropertyMetadata(typeof(PropertiesViewComboBox)));
  }
}
