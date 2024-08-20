using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DocxControls;


/// <summary>
/// View model for complex object properties
/// </summary>
public class ObjectPropertiesViewModel : PropertiesViewModel
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
    var properties = ModeledObject.GetType()
      .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
    foreach (var prop in properties)
    {
      if (prop.CanRead && prop.CanWrite)
      {
        var propType = prop.PropertyType;
        propType = propType.ToSystemType() ?? propType;
        var propertyViewModel = new PropertyViewModel
        {
          Name = prop.Name,
          Type = propType,
          Value = null,//prop.GetValue(ModeledObject),

        };
        //propertyViewModel.PropertyChanged += PropertiesViewModel_PropertyChanged;
        Items.Add(propertyViewModel);
      }
    }
  }

  //private void PropertiesViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
  //{
  //  if (e.PropertyName == nameof(PropertyViewModel.Value))
  //  {
  //    var propertyViewModel = (PropertyViewModel)sender!;
  //    var propertyName = propertyViewModel.Name;
  //    if (propertyName != null)
  //    {
  //      var prop = ModeledObject.GetType().GetProperty(propertyName);
  //      if (prop != null && prop.CanWrite)
  //        prop.SetValue(ModeledObject, propertyViewModel.Value);
  //    }
  //  }
  //}

  /// <summary>
  /// Object which properties are modeled
  /// </summary>
  public object ModeledObject { get; set; }

  /// <summary>
  /// Observable collection of properties of the modeled object
  /// </summary>
  public ObservableCollection<PropertyViewModel> ObjectProperties
  {
    get
    {      

      var result = base.Items;
      if (ModeledObject.GetType().Name.EndsWith("DocumentProtectionValues"))
        Debug.WriteLine($"Items.Count={result.Count}");
      return result;
    }
  }


}
