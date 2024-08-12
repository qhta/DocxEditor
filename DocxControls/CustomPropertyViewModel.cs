using Qhta.MVVM;

namespace DocxControls;

/// <summary>
/// View model for a custom property of a document.
/// </summary>
public class CustomPropertyViewModel: ViewModel
{

  /// <summary>
  /// Name of the property to get/set.
  /// </summary>
  public string? Name
  {
     get => _Name;
      set
      {
        if (value != _Name)
        {
          _Name = value!;
          NotifyPropertyChanged(nameof(Name));
        }
      }
  }
  private string? _Name;

  /// <summary>
  /// Type of the property.
  /// </summary>
  public Type? Type
  {
    get => _type;
    set
    {
      if (value != _type)
      {
        _type = value!;
        NotifyPropertyChanged(nameof(Type));
      }
    }
  }
  private Type? _type;

  /// <summary>
  /// Value of the property.
  /// </summary>
  public object? Value
  {
    get => _Value;
    set
    {
      if (value != _Value)
      {
        _Value = value;
        NotifyPropertyChanged(nameof(Value));
      }
    }
  }
  private object? _Value;
}
