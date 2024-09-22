namespace DocxControls.ViewModels;

/// <summary>
/// View model for a setting of a document.
/// </summary>
public class CompatibilitySetting : PropertyViewModel
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="parent"></param>
  public CompatibilitySetting(object? parent) : base(parent)
  {
  }

  /// <summary>
  /// Display caption for the setting.
  /// </summary>
  public override string? Caption => SettingsCaptions.ResourceManager.GetString(Name!, CultureInfo.CurrentUICulture) ?? Name;

  /// <summary>
  /// Does the property have a tooltip?
  /// </summary>
  public override bool HasTooltip =>
    SettingsTooltips.ResourceManager.GetString(Name!, CultureInfo.CurrentUICulture) != null;

  /// <summary>
  /// Tooltip for the setting
  /// </summary>
  public override string? TooltipTitle => SettingsTooltips.ResourceManager.GetString(Name!, CultureInfo.CurrentUICulture) ?? Name;

  /// <summary>
  /// Description of the setting
  /// </summary>
  public override string? TooltipDescription => FixDescription(SettingsDescriptions.ResourceManager
    .GetString(Name!, CultureInfo.CurrentUICulture));

}
