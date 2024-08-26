using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
namespace DocxControls;

/// <summary>
///  ComboBox that allows to select multiple items.
/// </summary>
public class CustomComboBox : ComboBox
{
  static CustomComboBox()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomComboBox), new FrameworkPropertyMetadata(typeof(CustomComboBox)));
  }

}
