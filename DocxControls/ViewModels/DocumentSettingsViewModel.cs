﻿using System.Collections.ObjectModel;
using System.ComponentModel;

using DocumentFormat.OpenXml.Packaging;
using Qhta.MVVM;

namespace DocxControls;

/// <summary>
/// View model for the document settings.
/// </summary>
public class DocumentSettingsViewModel: ViewModel
{

  /// <summary>
  /// Internal Wordprocessing document
  /// </summary>
  public WordprocessingDocument WordDocument { get; init; }

  /// <summary>
  /// Observable collection of properties
  /// </summary>
  public ObservableCollection<SettingViewModel> Items { get; } = new();

  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="wordDocument"></param>
  /// <param name="categories">Determines which categories to accept. Null for all</param>
  public DocumentSettingsViewModel(WordprocessingDocument wordDocument, SettingCategory[]? categories = null)
  {
    WordDocument = wordDocument;
    DocumentSettings = wordDocument.GetSettings();
    var names = DocumentSettings.GetNames(ItemFilter.All);
    foreach (var name in names)
    {
      var openXmlType = DocumentSettings.GetType(name);
      var type = openXmlType.ToSystemType();
      var category = DocumentSettings.GetCategory(name);
      var setting = DocumentSettings.GetValue(name);
      var value = setting.ToSystemValue(openXmlType);
      if (categories == null || categories.Contains(category))
      {
        var settingViewModel = new SettingViewModel(this)
        {
          Name = name,
          Category = category,
          Type = type,
          OriginalType = openXmlType,
          Value = value,
          OriginalValue = setting,
        };
        settingViewModel.PropertyChanged += SettingsViewModel_PropertyChanged;
        Items.Add(settingViewModel);
      }
    }
  }

  private void SettingsViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
  {
    if (e.PropertyName == nameof(PropertyViewModel.Value))
    {
      var settingViewModel = (SettingViewModel)sender!;
      var propertyName = settingViewModel.Name;
      if (propertyName != null)
      {
        var value = settingViewModel.Value.ToOpenXmlValue(settingViewModel.OriginalType);
        if (value is DX.OpenXmlElement element && element.IsEmpty())
          value = null;
        DocumentSettings.SetValue(propertyName, value);
      }
    }
    else
    if (e.PropertyName == nameof(ObjectViewModel.ModeledObject))
    {
      var settingViewModel = (SettingViewModel)sender!;
      var propertyName = settingViewModel.Name;
      if (propertyName != null)
      {
        var value = settingViewModel.Value.ToOpenXmlValue(settingViewModel.OriginalType);
        if (value is DX.OpenXmlElement element && element.IsEmpty())
          value = null;
        DocumentSettings.SetValue(propertyName, value);
      }
    }
  }


  private readonly DXW.Settings DocumentSettings;

}
