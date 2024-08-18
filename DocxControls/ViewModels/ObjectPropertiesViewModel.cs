using System.ComponentModel;

namespace DocxControls;


/// <summary>
/// View model for complex object properties
/// </summary>
public class ObjectPropertiesViewModel: PropertiesViewModel
{
  /// <summary>
  /// Initializes a new instance of the <see cref="ObjectPropertiesViewModel"/> class.
  /// </summary>
  /// <param name="objectType"></param>
  /// <param name="modeledObject">object which properties are modeled</param>
  public ObjectPropertiesViewModel(Type objectType, Object? modeledObject)
  {
    if (modeledObject == null)
      modeledObject = System.Activator.CreateInstance(objectType);
    ModeledObject = modeledObject!;
    var properties = ModeledObject.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
    foreach (var prop in properties)
    {
      if (prop.CanRead && prop.CanWrite)
      {
        var propertyViewModel = new PropertyViewModel
        {
          Name = prop.Name,
          Type = prop.PropertyType,
          Value = prop.GetValue(ModeledObject),

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
        var prop = ModeledObject.GetType().GetProperty(propertyName);
        if (prop!=null && prop.CanWrite)
          prop.SetValue(ModeledObject, propertyViewModel.Value);
      }
    }
  }


  private readonly object ModeledObject;
}
