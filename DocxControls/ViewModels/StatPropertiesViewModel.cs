using System.ComponentModel;

using DocumentFormat.OpenXml.Packaging;

namespace DocxControls;

/// <summary>
/// View model for the statistics properties
/// </summary>
public class StatPropertiesViewModel : PropertiesViewModel
{

  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="owner"></param>
  public StatPropertiesViewModel(DocumentViewModel owner) : base(owner)
  {
    WordDocument = owner.WordDocument;
    StatProperties = WordDocument.GetExtendedFileProperties();
    var names = StatProperties.GetNames(ItemFilter.All);
    foreach (var name in names)
    {
      if (StatProperties.IsVolatile(name) && StatProperties.AppliesToApplication(name, AppType.Word))
      {
        var type = StatProperties.GetType(name);
        var propertyViewModel = new PropertyViewModel(this)
        {
          Name = name,
          Type = type,
          IsReadOnly = true,
          Value = StatProperties.GetValue(name),

        };
        propertyViewModel.PropertyChanged += PropertiesViewModel_PropertyChanged;
        Items.Add(propertyViewModel);
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
