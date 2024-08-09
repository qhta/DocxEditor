using System.ComponentModel;
using System.Globalization;
using DocumentFormat.OpenXml.Packaging;

using Qhta.OpenXmlTools;

namespace DocxControls;
public class CorePropertiesViewModel : PropertiesViewModel
{

  public CorePropertiesViewModel(WordprocessingDocument wordDocument)
  {
    WordDocument = wordDocument;
    CoreProperties = wordDocument.PackageProperties;
    var names = CoreProperties.GetNames(ItemFilter.All);
    Strings.Culture = CultureInfo.CurrentUICulture;
    foreach (var name in names)
    {
      var caption = Strings.ResourceManager.GetString(name) ?? name;
      var propertyViewModel = new PropertyViewModel
      {
        Caption = caption,
        Name = name,
        Type = typeof(string),
        Value = CoreProperties.GetValue(name),

      };
      propertyViewModel.PropertyChanged += CorePropertiesViewModel_PropertyChanged;
      Properties.Add(propertyViewModel);
    }
  }

  private void CorePropertiesViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
  {
    var propertyViewModel = (PropertyViewModel)sender!;
    var propertyName = e.PropertyName!;
    CoreProperties.SetValue(propertyName, propertyViewModel.Value);
  }

#pragma warning disable OOXML0001
  private readonly IPackageProperties CoreProperties;
#pragma warning restore OOXML0001

}
