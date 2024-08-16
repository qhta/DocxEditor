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
  public virtual string? Caption => Strings.ResourceManager.GetString(Name!, CultureInfo.CurrentUICulture) ?? Name;

  /// <summary>
  /// Name of the property to get/set.
  /// </summary>
  public virtual string? Name { get; set; }

  /// <summary>
  /// Type of the property.
  /// </summary>
  public virtual Type? Type { get; set; }

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
