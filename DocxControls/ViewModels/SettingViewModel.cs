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
  /// Tooltip for the setting
  /// </summary>
  public override string? Tooltip => SettingsTooltips.ResourceManager.GetString(Name!, CultureInfo.CurrentUICulture) ?? Name;

  /// <summary>
  /// Description of the setting
  /// </summary>
  public override string? Description => SettingsDescriptions.ResourceManager.GetString(Name!, CultureInfo.CurrentUICulture)?.Replace("<p/>", "\n");
}
