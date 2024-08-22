using System.Windows;
using System.Windows.Controls;
namespace DocxControls;

/// <summary>
///  ComboBox that allows to edit a set o properties.
/// </summary>
public class ObjectViewComboBox : ComboBox
{
  static ObjectViewComboBox()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(ObjectViewComboBox), new FrameworkPropertyMetadata(typeof(ObjectViewComboBox)));
  }

  /// <summary>
  /// Dependency property for an <see cref="IObjectViewModel"/> object.
  /// </summary>
  public static readonly DependencyProperty ObjectViewModelProperty = DependencyProperty.Register(
    nameof(ObjectViewModel), typeof(IObjectViewModel), typeof(ObjectViewComboBox), new PropertyMetadata(default(object)));

  /// <summary>
  /// Provided view model for the object.
  /// </summary>
  public IObjectViewModel? ObjectViewModel
  {
    get => (IObjectViewModel)GetValue(ObjectViewModelProperty);
    set => SetValue(ObjectViewModelProperty, value);
  }
}
