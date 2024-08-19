using System.Windows;
using System.Windows.Controls;
namespace DocxControls;

/// <summary>
///  ComboBox that allows to select multiple items.
/// </summary>
public class CheckBoxComboBox : ComboBox
{
  static CheckBoxComboBox()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(CheckBoxComboBox), new FrameworkPropertyMetadata(typeof(CheckBoxComboBox)));
  }
}
