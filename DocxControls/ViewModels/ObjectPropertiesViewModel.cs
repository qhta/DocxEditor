using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DocxControls;


/// <summary>
/// View model for complex object properties
/// </summary>
public class ObjectPropertiesViewModel : PropertiesViewModel
{
  /// <summary>
  ///  Type of the object which properties are modeled
  /// </summary>
  public Type ObjectType { get; init; }

  /// <summary>
  /// Initializes a new instance of the <see cref="ObjectPropertiesViewModel"/> class.
  /// </summary>
  /// <param name="objectType">Type of the object which will be created if <paramref name="modeledObject"/> is null</param>
  /// <param name="modeledObject">object which properties are modeled</param>
  public ObjectPropertiesViewModel(Type objectType, Object? modeledObject)
  {
    ObjectType = objectType;
    ModeledObject = modeledObject;
    ModeledObject ??= Activator.CreateInstance(ObjectType);
    var properties = objectType
      .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
    foreach (var prop in properties)
    {
      if (prop.CanRead && prop.CanWrite)
      {
        var propName = prop.Name;
        var origType = prop.PropertyType;
        var type = origType.ToSystemType();
        if (type == typeof(bool))
          type = typeof(bool?);
        object? value = null;
        if (ModeledObject != null)
        {
          value = prop.GetValue(ModeledObject);
          if (origType != type)
            value = value.ToSystemValue(origType);
        }
        var propertyViewModel = new PropertyViewModel
        {
          Name = propName,
          Type = type,
          OriginalType = origType,
          Value = value,
          OriginalValue = modeledObject
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
        ModeledObject ??= Activator.CreateInstance(ObjectType);
        var prop = ModeledObject!.GetType().GetProperty(propertyName);
        if (prop != null && prop.CanWrite)
        {
          prop.SetValue(ModeledObject, propertyViewModel.Value.ToOpenXmlValue(propertyViewModel.OriginalType!));
          NotifyPropertyChanged(nameof(ModeledObject));
        }
      }
    }
  }

  /// <summary>
  /// Object which properties are modeled
  /// </summary>
  public object? ModeledObject
  {
    get => _modeledObject;
    set
    {
      if (value != _modeledObject)
      {
        _modeledObject = value;
        NotifyPropertyChanged(nameof(ModeledObject));
      }
    }
  }
  private object? _modeledObject;

  /// <summary>
  /// Inherited Items
  /// </summary>
  public ObservableCollection<PropertyViewModel> ObjectProperties => Items;
}
