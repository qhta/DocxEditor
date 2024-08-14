using System.Collections.ObjectModel;
using System.ComponentModel;

using DocumentFormat.OpenXml.Packaging;

namespace DocxControls;

/// <summary>
/// View model for the document settings.
/// </summary>
public class DocumentSettingsViewModel
{


  /// <summary>
  /// Internal Wordprocessing document
  /// </summary>
  public WordprocessingDocument WordDocument { get; init; }

  /// <summary>
  /// Observable collection of properties
  /// </summary>
  public ObservableCollection<SettingViewModel> Settings { get; } = new();

  /// <summary>
  /// Initializes a new instance of the <see cref="AppPropertiesViewModel"/> class.
  /// </summary>
  /// <param name="wordDocument"></param>
  public DocumentSettingsViewModel(WordprocessingDocument wordDocument)
  {
    WordDocument = wordDocument;
    DocumentSettings = wordDocument.GetSettings();
    var names = DocumentSettings.GetNames(ItemFilter.All);
    Strings.Culture = CultureInfo.CurrentUICulture;
    foreach (var name in names)
    {
      var type = DocumentSettings.GetType(name);
      if (type.IsValueType || type == typeof(string))
      {
        var caption = Strings.ResourceManager.GetString(name) ?? name;
        var category = DocumentSettings.GetCategory(name);
        var settingViewModel = new SettingViewModel
        {
          Caption = caption,
          Name = name,
          Category = category,
          Type = type,
          Value = DocumentSettings.GetValue(name),

        };
        settingViewModel.PropertyChanged += PropertiesViewModel_PropertyChanged;
        Settings.Add(settingViewModel);
      }
    }
  }

  private void PropertiesViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
  {
    var propertyViewModel = (PropertyViewModel)sender!;
    var propertyName = e.PropertyName!;
    DocumentSettings.SetValue(propertyName, propertyViewModel.Value);
  }


  private readonly DXW.Settings DocumentSettings;

}
