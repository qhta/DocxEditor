using System.ComponentModel;

namespace DocxControls;

/// <summary>
/// View model for the document settings.
/// </summary>
public class DocumentSettings: PropertiesViewModel<DocumentSetting>
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="documentViewModel"></param>
  /// <param name="categories">Determines which categories to accept. Null for all</param>
  public DocumentSettings(Document documentViewModel, SettingCategory[]? categories = null): base(documentViewModel)
  {
    IsModifiedInternal = true;
    WordDocument = documentViewModel.WordDocument;
    DocumentSettingsElement = WordDocument.GetSettings();
    var names = DocumentSettingsElement.GetNames(ItemFilter.All);
    foreach (var name in names)
    {
      var openXmlType = DocumentSettingsElement.GetType(name);
      var type = openXmlType.ToSystemType(name);
      var category = DocumentSettingsElement.GetCategory(name);
      var setting = DocumentSettingsElement.GetValue(name);
      var value = setting.ToSystemValue(openXmlType);
      if (categories == null || categories.Contains(category))
      {
        if (name== "DocumentProtection")
          Debug.Assert(true);
        var settingViewModel = new DocumentSetting(this)
        {
          Name = name,
          Category = CTC(category),
          Type = type,
          OriginalType = openXmlType,
          Value = value,
          OriginalValue = setting,
        };
        settingViewModel.PropertyChanged += SettingsViewModel_PropertyChanged;
        Items.Add(settingViewModel);
      }
    }
    IsModifiedInternal = false;
  }

  private DA.SettingCategory CTC(SettingCategory category) => (DA.SettingCategory)category;

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
        DocumentSettingsElement.SetValue(propertyName, value);
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
        DocumentSettingsElement.SetValue(propertyName, value);
      }
    }
  }


  private readonly DXW.Settings DocumentSettingsElement;

}
