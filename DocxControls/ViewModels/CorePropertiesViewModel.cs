using System.ComponentModel;

using DocumentFormat.OpenXml.Packaging;

namespace DocxControls;
/// <summary>
/// View model for the core document properties
/// </summary>
public class CorePropertiesViewModel : PropertiesViewModel
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="owner"></param>
  public CorePropertiesViewModel(Document owner) : base(owner)
  {
    WordDocument = owner.WordDocument;
    CoreProperties = WordDocument.PackageProperties;
    var names = CoreProperties.GetNames(ItemFilter.All);
    foreach (var name in names)
    {
      var type = CoreProperties.GetType(name);
      var propertyViewModel = new PropertyViewModel(this)
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
    if (e.PropertyName == nameof(PropertyViewModel.Value))
    {
      var propertyViewModel = (PropertyViewModel)sender!;
      var propertyName = propertyViewModel.Name;
      if (propertyName != null)
      {
        CoreProperties.SetValue(propertyName, propertyViewModel.Value);
      }
    }
  }

#pragma warning disable OOXML0001
  private readonly IPackageProperties CoreProperties;
#pragma warning restore OOXML0001

}
