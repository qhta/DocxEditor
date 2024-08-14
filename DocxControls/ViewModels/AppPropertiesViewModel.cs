using System.ComponentModel;

using DocumentFormat.OpenXml.Packaging;

namespace DocxControls;

/// <summary>
/// View model for the application-specific properties
/// </summary>
public class AppPropertiesViewModel : PropertiesViewModel
{

  /// <summary>
  /// Initializes a new instance of the <see cref="AppPropertiesViewModel"/> class.
  /// </summary>
  /// <param name="wordDocument"></param>
  public AppPropertiesViewModel(WordprocessingDocument wordDocument)
  {
    WordDocument = wordDocument;
    AppProperties = wordDocument.GetExtendedFileProperties();
    var names = AppProperties.GetNames(ItemFilter.All);
    Strings.Culture = CultureInfo.CurrentUICulture;
    foreach (var name in names)
    {
      if (!AppProperties.IsVolatile(name) && AppProperties.AppliesToApplication(name, AppType.Word))
      {
        var caption = Strings.ResourceManager.GetString(name) ?? name;
        var type = AppProperties.GetType(name);
        var propertyViewModel = new PropertyViewModel
        {
          Caption = caption,
          Name = name,
          Type = type,
          Value = AppProperties.GetValue(name),

        };
        propertyViewModel.PropertyChanged += PropertiesViewModel_PropertyChanged;
        Properties.Add(propertyViewModel);
      }
    }
  }

  private void PropertiesViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
  {
    var propertyViewModel = (PropertyViewModel)sender!;
    var propertyName = e.PropertyName!;
    AppProperties.SetValue(propertyName, propertyViewModel.Value);
  }


  private readonly DXEP.Properties AppProperties;

}
