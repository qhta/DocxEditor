using System.ComponentModel;

namespace DocxControls;

/// <summary>
/// View model for the statistics properties
/// </summary>
public class StatProperties : DocumentProperties
{

  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="parent"></param>
  public StatProperties(Document parent) : base(parent)
  {
    IsModifiedInternal = true;
    WordDocument = parent.WordDocument;
    StatPropertiesElement = WordDocument.GetExtendedFileProperties();
    var names = GetNames();
    foreach (var name in names)
    {
      if (StatPropertiesElement.IsVolatile(name) && StatPropertiesElement.AppliesToApplication(name, AppType.Word))
      {
        var type = StatPropertiesElement.GetType(name);
        var propertyViewModel = new DocumentProperty(this)
        {
          Name = name,
          Type = type,
          IsReadOnly = true,
          Value = StatPropertiesElement.GetValue(name),

        };
        propertyViewModel.PropertyChanged += PropertiesViewModel_PropertyChanged;
        Items.Add(propertyViewModel);
      }
    }
    IsModifiedInternal = false;
  }

  private void PropertiesViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
  {
    var propertyViewModel = (PropertyViewModel)sender!;
    var propertyName = e.PropertyName!;
    StatPropertiesElement.SetValue(propertyName, propertyViewModel.Value);
  }

  /// <summary>
  /// Get names of the statistics properties.
  /// </summary>
  /// <returns></returns>
  protected sealed override string[] GetNames() => StatPropertiesElement.GetNames(ItemFilter.All);


  private readonly DocumentFormat.OpenXml.ExtendedProperties.Properties StatPropertiesElement;

}
