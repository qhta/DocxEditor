using Qhta.MVVM;

namespace DocxControls;

/// <summary>
/// View model for a property of a document.
/// </summary>
public class PropertyViewModel: ViewModel
{
  /// <summary>
  /// Display caption for the property.
  /// </summary>
  public string? Caption { get; init; } = null!;

  /// <summary>
  /// Name of the property to get/set.
  /// </summary>
  public string? Name { get; init; } = null!;

  /// <summary>
  /// Is this a custom property?
  /// </summary>
  public bool IsCustomProperty { get; init; }

  /// <summary>
  /// Type of the property.
  /// </summary>
  public Type? PropType { get; set; } = null!;

  /// <summary>
  /// Is the property value read-only?
  /// </summary>
  public bool IsReadOnly { get; init; }

  /// <summary>
  /// Value of the property.
  /// </summary>
  public object? Value
  {
    get => _Value;
    set
    {
      if (value != _Value && Name!=null)
      {
        _Value = value;
        NotifyPropertyChanged(Name);
      }
    }
  }
  private object? _Value;

}
