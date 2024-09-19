using System.ComponentModel;

using DocumentFormat.OpenXml.Packaging;

namespace DocxControls;
/// <summary>
/// View model for the core document properties
/// </summary>
public class CoreProperties : PropertiesViewModel<PropertyViewModel>
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="parent"></param>
  public CoreProperties(Document parent) : base(parent)
  {
    IsModifiedInternal = true;
    WordDocument = parent.WordDocument;
    CorePropertiesElement = WordDocument.PackageProperties;
    var names = CorePropertiesElement.GetNames(ItemFilter.All);
    foreach (var name in names)
    {
      var type = CorePropertiesElement.GetType(name);
      var value = CorePropertiesElement.GetValue(name);
      var propertyViewModel = new PropertyViewModel(this)
      {
        Name = name,
        Type = type,
        Value = value

      };
      propertyViewModel.PropertyChanged += PropertiesViewModel_PropertyChanged;
      Items.Add(propertyViewModel);
    }
    IsModifiedInternal = false;
  }

  private void PropertiesViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
  {
    if (e.PropertyName == nameof(PropertyViewModel.Value))
    {
      var propertyViewModel = (PropertyViewModel)sender!;
      var propertyName = propertyViewModel.Name;
      if (propertyName != null)
      {
        CorePropertiesElement.SetValue(propertyName, propertyViewModel.Value);
      }
    }
  }

#pragma warning disable OOXML0001
  private readonly IPackageProperties CorePropertiesElement;
#pragma warning restore OOXML0001

}
