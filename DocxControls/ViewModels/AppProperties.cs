using System.ComponentModel;

namespace DocxControls;

/// <summary>
/// View model for the application-specific properties
/// </summary>
public class AppProperties : DocumentProperties
{

  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="parent"> </param>
  public AppProperties(Document parent): base(parent)
  {
    IsModifiedInternal = true;
    WordDocument = parent.WordDocument;
    AppPropertiesElement = WordDocument.GetExtendedFileProperties();
    var names = AppPropertiesElement.GetNames(ItemFilter.All);
    foreach (var name in names)
    {
      if (!AppPropertiesElement.IsVolatile(name) && AppPropertiesElement.AppliesToApplication(name, AppType.Word))
      {
        var type = AppPropertiesElement.GetType(name);
        var propertyViewModel = new DocumentProperty(this)
        {
          Name = name,
          Type = type,
          Value = AppPropertiesElement.GetValue(name),

        };
        propertyViewModel.PropertyChanged += PropertiesViewModel_PropertyChanged;
        Items.Add(propertyViewModel);
      }
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
        AppPropertiesElement.SetValue(propertyName, propertyViewModel.Value);
      }
    }
  }

  /// <summary>
  /// Get names of the core properties.
  /// </summary>
  /// <returns></returns>
  protected sealed override string[] GetNames() => AppPropertiesElement.GetNames(ItemFilter.All);

  private readonly DXEP.Properties AppPropertiesElement;

}
