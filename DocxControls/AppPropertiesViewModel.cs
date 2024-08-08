using DocumentFormat.OpenXml.Packaging;

using System.ComponentModel;
using Qhta.OpenXmlTools;

namespace DocxControls;

public class AppPropertiesViewModel : PropertiesViewModel
{

  public AppPropertiesViewModel(WordprocessingDocument wordDocument)
  {
    WordDocument = wordDocument;
    AppProperties = wordDocument.GetExtendedFileProperties();
    var names = AppProperties.GetNames();
    foreach (var name in names)
    {
      var propertyViewModel = new PropertyViewModel
      {
        Caption = name,
        Name = name,
        Type = typeof(string),
        Value = AppProperties.GetValue(name),

      };
      propertyViewModel.PropertyChanged += CorePropertiesViewModel_PropertyChanged;
      Properties.Add(propertyViewModel);
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
