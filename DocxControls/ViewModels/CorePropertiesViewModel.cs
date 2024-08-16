using System.ComponentModel;

using DocumentFormat.OpenXml.Packaging;

namespace DocxControls;
/// <summary>
/// View model for the core document properties
/// </summary>
public class CorePropertiesViewModel : PropertiesViewModel
{
  /// <summary>
  /// Initializes a new instance of the <see cref="CorePropertiesViewModel"/> class.
  /// </summary>
  /// <param name="wordDocument"></param>
  public CorePropertiesViewModel(WordprocessingDocument wordDocument)
  {
    WordDocument = wordDocument;
    CoreProperties = wordDocument.PackageProperties;
    var names = CoreProperties.GetNames(ItemFilter.All);
    foreach (var name in names)
    {
      var type = CoreProperties.GetType(name);
      var propertyViewModel = new PropertyViewModel
      {
        Name = name,
        Type = type,
        Value = CoreProperties.GetValue(name),

      };
      propertyViewModel.PropertyChanged += PropertiesViewModel_PropertyChanged;
      Items.Add(propertyViewModel);
    }
  }

  private void PropertiesViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
  {
    var propertyViewModel = (PropertyViewModel)sender!;
    var propertyName = e.PropertyName!;
    CoreProperties.SetValue(propertyName, propertyViewModel.Value);
  }

#pragma warning disable OOXML0001
  private readonly IPackageProperties CoreProperties;
#pragma warning restore OOXML0001

}
