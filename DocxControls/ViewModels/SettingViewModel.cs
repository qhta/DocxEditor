namespace DocxControls;

/// <summary>
/// View model for a setting of a document.
/// </summary>
public class SettingViewModel : PropertyViewModel
{
  /// <summary>
  /// Display caption for the setting.
  /// </summary>
  public override string? Caption => SettingsNames.ResourceManager.GetString(Name!, CultureInfo.CurrentUICulture) ?? Name;

  /// <summary>
  /// Category of the property.
  /// </summary>
  public virtual SettingCategory Category { get; set; }

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
  /// Tooltip for the setting.
  /// </summary>
  public string? Tooltip => SettingsTooltips.ResourceManager.GetString(Name!, CultureInfo.CurrentUICulture) ?? Name;
}
