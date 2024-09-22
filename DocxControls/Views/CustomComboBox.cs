using System.Windows;
using System.Windows.Controls;
namespace DocxControls.Views;

/// <summary>
///  ComboBox with custom style
/// </summary>
public class CustomComboBox : ComboBox
{
  static CustomComboBox()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomComboBox), new FrameworkPropertyMetadata(typeof(CustomComboBox)));
  }

}
