using System.ComponentModel;

namespace DocxControls;

/// <summary>
/// View model for the application-specific properties
/// </summary>
public class AppPropertiesViewModel : PropertiesViewModel
{

  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="owner"> </param>
  public AppPropertiesViewModel(DocumentViewModel owner): base(owner)
  {
    WordDocument = owner.WordDocument;
    AppProperties = WordDocument.GetExtendedFileProperties();
    var names = AppProperties.GetNames(ItemFilter.All);
    foreach (var name in names)
    {
      if (!AppProperties.IsVolatile(name) && AppProperties.AppliesToApplication(name, AppType.Word))
      {
        var type = AppProperties.GetType(name);
        var propertyViewModel = new PropertyViewModel(this)
        {
          Name = name,
          Type = type,
          Value = AppProperties.GetValue(name),

        };
        propertyViewModel.PropertyChanged += PropertiesViewModel_PropertyChanged;
        Items.Add(propertyViewModel);
      }
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
        AppProperties.SetValue(propertyName, propertyViewModel.Value);
      }
    }
  }


  private readonly DXEP.Properties AppProperties;

}
