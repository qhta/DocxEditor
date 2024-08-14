using DocumentFormat.OpenXml.Packaging;

using System.ComponentModel;
using Qhta.OpenXmlTools;
using System.Globalization;

namespace DocxControls;

/// <summary>
/// View model for the statistics properties
/// </summary>
public class StatPropertiesViewModel : PropertiesViewModel
{

  /// <summary>
  /// Initializes a new instance of the <see cref="StatPropertiesViewModel"/> class.
  /// </summary>
  /// <param name="wordDocument"></param>
  public StatPropertiesViewModel(WordprocessingDocument wordDocument)
  {
    WordDocument = wordDocument;
    StatProperties = wordDocument.GetExtendedFileProperties();
    var names = StatProperties.GetNames(ItemFilter.All);
    Strings.Culture = CultureInfo.CurrentUICulture;
    foreach (var name in names)
    {
      if (StatProperties.IsVolatile(name) && StatProperties.AppliesToApplication(name, AppType.Word))
      {
        var caption = Strings.ResourceManager.GetString(name) ?? name;
        var type = StatProperties.GetType(name);
        var propertyViewModel = new PropertyViewModel
        {
          Caption = caption,
          Name = name,
          Type = type,
          IsReadOnly = true,
          Value = StatProperties.GetValue(name),

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
    StatProperties.SetValue(propertyName, propertyViewModel.Value);
  }


  private readonly DocumentFormat.OpenXml.ExtendedProperties.Properties StatProperties;

}
