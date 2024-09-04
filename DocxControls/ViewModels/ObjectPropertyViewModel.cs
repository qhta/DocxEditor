namespace DocxControls;

/// <summary>
/// View model for an object property. Replaces <see cref="PropertyViewModel"/> in the properties view.
/// </summary>
public class ObjectPropertyViewModel : PropertyViewModel
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="ownerObjectViewModel">New model for an object, which property is modeled here</param> 
  public ObjectPropertyViewModel(ObjectViewModel ownerObjectViewModel)
  {
    OwnerObjectViewModel = ownerObjectViewModel;
  }

  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="ownerObjectViewModel">New model for an object, which property is modeled here</param>
  /// <param name="propertyName">Name of property of the ownerObjectViewModel view model</param>
  /// <param name="origPropertyName">Name of property of the modeled object. If null then <paramref name="propertyName"/> is used</param>
  public ObjectPropertyViewModel(ObjectViewModel ownerObjectViewModel, string propertyName, string? origPropertyName = null)
  {
    OwnerObjectViewModel = ownerObjectViewModel;
    var property = ownerObjectViewModel.GetType().GetProperty(propertyName);
    if (property == null)
      throw new ArgumentException($"Property {propertyName} not found in {ownerObjectViewModel.GetType().Name}");
    origPropertyName ??= propertyName;
    var origProperty = ModeledObject.GetType().GetProperty(origPropertyName);
    if (property == null)
      throw new ArgumentException($"Property {origPropertyName} not found in {ModeledObject.GetType().Name}");
    //if (!(property.CanWrite))
    //  throw new ArgumentException($"Property {propertyName} is not writable in {ownerObjectViewModel.GetType().Name}");

    Property = property;
    var propName = property.Name;
    var origType = property.PropertyType;
    var type = origType.ToSystemType();
    if (type == typeof(bool))
      type = typeof(bool?);
    object? value = null;
    object? originalValue = null;
    originalValue = origProperty?.GetValue(ModeledObject);
    value = property.GetValue(OwnerObjectViewModel);
    base.Name = propName;
    base.Type = type;
    base.OriginalType = origType;
    base.Value = value;
    base.OriginalValue = originalValue;
  }

  /// <summary>
  /// Gets or sets the owner of the object property
  /// </summary>
  public ObjectViewModel? OwnerObjectViewModel { get; set; }

  /// <summary>
  /// OwnerObjectViewModel modeled object.
  /// </summary>
  public object ModeledObject => OwnerObjectViewModel?.ModeledObject!;

  /// <summary>
  /// Collection of object properties.
  /// </summary>
  public ObjectPropertiesViewModel? Collection { get; internal set; }

  /// <summary>
  /// Property to get/set the value of the object property.
  /// </summary>
  public PropertyInfo? Property { get; set; }

}