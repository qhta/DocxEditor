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
  public ObservableCollection<SettingViewModel> Items { get; } = new();

  /// <summary>
  /// Initializes a new instance of the <see cref="AppPropertiesViewModel"/> class.
  /// </summary>
  /// <param name="wordDocument"></param>
  /// <param name="categories">Determines which categories to accept. Null for all</param>
  public DocumentSettingsViewModel(WordprocessingDocument wordDocument, SettingCategory[]? categories = null)
  {
    WordDocument = wordDocument;
    DocumentSettings = wordDocument.GetSettings();
    var names = DocumentSettings.GetNames(ItemFilter.All);
    Strings.Culture = CultureInfo.CurrentUICulture;
    foreach (var name in names)
    {
      var type = DocumentSettings.GetType(name);

      var caption = Strings.ResourceManager.GetString(name) ?? name;
      var category = DocumentSettings.GetCategory(name);
      if (categories == null || categories.Contains(category))
      {
        var settingViewModel = new SettingViewModel
        {
          Name = name,
          Category = category,
          Type = type,
          Value = DocumentSettings.GetValue(name),

        };
        settingViewModel.PropertyChanged += SettingsViewModel_PropertyChanged;
        Items.Add(settingViewModel);
      }
    }
  }

  private void SettingsViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
  {
    var propertyViewModel = (SettingViewModel)sender!;
    var propertyName = e.PropertyName!;
    DocumentSettings.SetValue(propertyName, propertyViewModel.Value);
  }


  private readonly DXW.Settings DocumentSettings;

}
