using DocumentFormat.OpenXml.Packaging;

using System.ComponentModel;
using Qhta.OpenXmlTools;
using System.Globalization;

namespace DocxControls;

public class AppPropertiesViewModel : PropertiesViewModel
{

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
          PropType = type,
          Value = AppProperties.GetValue(name),

        };
        propertyViewModel.PropertyChanged += CorePropertiesViewModel_PropertyChanged;
        Properties.Add(propertyViewModel);
      }
    }
  }

  private void CorePropertiesViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
  {
    var propertyViewModel = (PropertyViewModel)sender!;
    var propertyName = e.PropertyName!;
    AppProperties.SetValue(propertyName, propertyViewModel.Value);
  }


  private readonly DocumentFormat.OpenXml.ExtendedProperties.Properties AppProperties;

}
