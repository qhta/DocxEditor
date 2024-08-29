using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

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
  /// Default constructor.
  /// </summary>
  public ObjectViewComboBox()
  {
    Loaded += ComboBox_Loaded;
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
  private void ComboBox_Loaded(object sender, RoutedEventArgs e)
  {
    if (Template.FindName("Popup_Thumb", this) is Thumb thumb)
    {
      thumb.DragDelta += Thumb_DragDelta;
    }
  }

  private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
  {
    if (sender is Thumb thumb && thumb.TemplatedParent is ObjectViewComboBox comboBox)
    {
      if (comboBox.Template.FindName("PART_Popup", comboBox) is Popup popup && popup.Child is FrameworkElement popupChild)
      {
        double newWidth = popupChild.ActualWidth + e.HorizontalChange;

        if (newWidth > popupChild.MinWidth)
          popupChild.Width = newWidth;

        //double newHeight = popupChild.ActualHeight + e.VerticalChange;
        //if (newHeight > popupChild.MinHeight)
        //  popupChild.Height = newHeight;
      }
    }
  }
}
