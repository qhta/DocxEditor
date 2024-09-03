using System.Windows;

namespace DocxControls;

/// <summary>
/// Proxy class for binding to a property in a non-visual object.
/// </summary>
public class BindingProxy : Freezable
{
  /// <inheritdoc />
  protected override Freezable CreateInstanceCore()
  {
    return new BindingProxy();
  }

  /// <summary>
  /// Dependency property for the data property.
  /// </summary>
  public static readonly DependencyProperty DataProperty =
    DependencyProperty.Register(nameof(Data), typeof(object), typeof(BindingProxy), new UIPropertyMetadata(null));

  /// <summary>
  /// Data property to bind to.
  /// </summary>
  public object Data
  {
    get => GetValue(DataProperty);
    set => SetValue(DataProperty, value);
  }
}
