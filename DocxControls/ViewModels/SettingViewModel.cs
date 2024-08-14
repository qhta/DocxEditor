using Qhta.MVVM;

namespace DocxControls;

/// <summary>
/// View model for a setting of a document.
/// </summary>
public class SettingViewModel: ViewModel
{
  /// <summary>
  /// Display caption for the property.
  /// </summary>
  public string? Caption { get; set; }

  /// <summary>
  /// Name of the property to get/set.
  /// </summary>
  public virtual string? Name { get; set; }

  /// <summary>
  /// Category of the property.
  /// </summary>
  public virtual SettingCategories Category { get; set; }

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
