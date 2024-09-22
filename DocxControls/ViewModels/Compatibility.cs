using System.ComponentModel;

namespace DocxControls.ViewModels;

/// <summary>
/// View model for the document settings.
/// </summary>
public class Compatibility: DA.DocumentSetting
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="documentSettings"></param>

  public Compatibility(DocumentSettings documentSettings): base(documentSettings)
  {
    IsModifiedInternal = true;
    var compatibility = documentSettings.WordDocument.GetCompatibilitySettings();
    CompatibilityElement = compatibility;
    var names = CompatibilityElement.GetNames(ItemFilter.All);
    foreach (var name in names)
    {
      var openXmlType = CompatibilityElement.GetType(name);
      var type = openXmlType.ToSystemType(name);
      var setting = CompatibilityElement.GetValue(name);
      var value = setting.ToSystemValue(openXmlType);
      var compatibilitySetting = new CompatibilitySetting(this)
      {
        Name = name,
        Type = type,
        OriginalType = openXmlType,
        Value = value,
        OriginalValue = setting,
      };
      compatibilitySetting.PropertyChanged += SettingsViewModel_PropertyChanged;
      Items.Add(compatibilitySetting);
    }
    IsModifiedInternal = false;
  }

  private void SettingsViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
  {
    if (e.PropertyName == nameof(PropertyViewModel.Value))
    {
      var settingViewModel = (DocumentSetting)sender!;
      var propertyName = settingViewModel.Name;
      if (propertyName != null)
      {
        var value = settingViewModel.Value.ToOpenXmlValue(settingViewModel.OriginalType);
        if (value is DX.OpenXmlElement element && element.IsEmpty())
          value = null;
        CompatibilityElement.SetValue(propertyName, value);
      }
    }
    else
    if (e.PropertyName == nameof(ObjectViewModel.ModeledObject))
    {
      var settingViewModel = (DocumentSetting)sender!;
      var propertyName = settingViewModel.Name;
      if (propertyName != null)
      {
        var value = settingViewModel.Value.ToOpenXmlValue(settingViewModel.OriginalType);
        if (value is DX.OpenXmlElement element && element.IsEmpty())
          value = null;
        CompatibilityElement.SetValue(propertyName, value);
      }
    }
  }


  private readonly DXW.Compatibility CompatibilityElement;

}
