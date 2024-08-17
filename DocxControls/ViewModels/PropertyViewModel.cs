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
  public virtual string? Caption => PropertiesCaptions.ResourceManager.GetString(Name!, CultureInfo.CurrentUICulture) ?? Name;

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
        NotifyPropertyChanged(nameof(Value));
      }
    }
  }
  private object? _Value;

  /// <summary>
  /// Boolean value of the property.
  /// </summary>
  public bool? AsBoolean
  {
    get
    {
      if (Value is bool b)
        return b;
      if (Value is DXO10W.OnOffValues onOffValues)
      {
        if (onOffValues == DXO10W.OnOffValues.One || onOffValues == DXO10W.OnOffValues.True)
          return true;
        if (onOffValues == DXO10W.OnOffValues.Zero || onOffValues == DXO10W.OnOffValues.False)
          return false;
      }
      return null;
    }

    set
    {
      if (Type == typeof(DXO10W.OnOffValues))
      {
        if (value == true)
          Value = DXO10W.OnOffValues.One;
        else if (value == false)
          Value = DXO10W.OnOffValues.Zero;
        else
          Value = null;
        return;
      }
      Value = value;
    }
  }

  /// <summary>
  /// Tooltip for the property
  /// </summary>
  public virtual string? Tooltip =>PropertiesTooltips.ResourceManager.GetString(Name!, CultureInfo.CurrentUICulture);

  /// <summary>
  /// Description of the property
  /// </summary>
  public virtual string? Description => PropertiesDescriptions.ResourceManager.GetString(Name!, CultureInfo.CurrentUICulture)?.Replace("<p/>", "\n");

  /// <summary>
  /// Value of the property.
  /// </summary>
  public bool IsExpanded
  {
    get => _IsExpanded;
    set
    {
      if (value != _IsExpanded)
      {
        _IsExpanded = value;
        NotifyPropertyChanged(nameof(IsExpanded));
      }
    }
  }
  private bool _IsExpanded;
}
