namespace DocxControls;

/// <summary>
/// View model for a setting of a document.
/// </summary>
public class SettingViewModel : PropertyViewModel
{
  /// <summary>
  /// Display caption for the setting.
  /// </summary>
  public override string? Caption => SettingsCaptions.ResourceManager.GetString(Name!, CultureInfo.CurrentUICulture) ?? Name;

  /// <summary>
  /// Category of the property.
  /// </summary>
  public virtual SettingCategory Category { get; set; }

  /// <summary>
  /// Original type of the setting.
  /// </summary>
  public Type? OriginalType { get; set; }

  ///// <summary>
  ///// Original value of the setting.
  ///// </summary>
  //public object? OriginalValue
  //{
  //  get => Value.ToOpenXmlValue(OriginalType!);
  //  set => Value = value.ToSystemValue(OriginalType!);
  //}

  /// <summary>
  /// Tooltip for the setting
  /// </summary>
  public override string? TooltipTitle => SettingsTooltips.ResourceManager.GetString(Name!, CultureInfo.CurrentUICulture) ?? Name;

  /// <summary>
  /// Description of the setting
  /// </summary>
  public override string? TooltipDescription => SettingsDescriptions.ResourceManager.GetString(Name!, CultureInfo.CurrentUICulture)?.Replace("<p/>", "\n");
}
