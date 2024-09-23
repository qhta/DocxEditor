using Qhta.MVVM;

namespace DocxControls.ViewModels;

/// <summary>
/// View model for a setting of a document.
/// </summary>
public class DocumentSetting : DocumentProperty, DA.DocumentSetting
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="owner"></param>
  public DocumentSetting(ViewModel owner) : base(owner)
  {
  }

  /// <summary>
  /// Display caption for the setting.
  /// </summary>
  public override string? Caption => PropertiesCaptions.ResourceManager.GetString(Name!, CultureInfo.CurrentUICulture) ?? Name;

  /// <summary>
  /// Category of the property.
  /// </summary>
  public DA.SettingCategory Category { get; set; }

  /// <summary>
  /// Does the property have a tooltip?
  /// </summary>
  public override bool HasTooltip =>
    PropertiesTooltips.ResourceManager.GetString(Name!, CultureInfo.CurrentUICulture) != null;

  /// <summary>
  /// Tooltip for the setting
  /// </summary>
  public override string? TooltipTitle => PropertiesTooltips.ResourceManager.GetString(Name!, CultureInfo.CurrentUICulture) ?? Name;

  /// <summary>
  /// Description of the setting
  /// </summary>
  public override string? TooltipDescription => FixDescription(PropertiesDescriptions.ResourceManager
    .GetString(Name!, CultureInfo.CurrentUICulture));

}
