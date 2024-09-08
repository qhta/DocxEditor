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
    var viewModelProperty = ownerObjectViewModel.GetType().GetProperty(propertyName);
    origPropertyName ??= propertyName;
    var origProperty = ModeledObjectType!.GetProperty(origPropertyName);
    if (origProperty == null && viewModelProperty==null)
      throw new ArgumentException($"Property {origPropertyName} not found in {ModeledObjectType!.Name} and not in {ownerObjectViewModel.GetType().Name}");
    //if (!(property.CanWrite))
    //  throw new ArgumentException($"Property {propertyName} is not writable in {ownerObjectViewModel.GetType().Name}");

    ViewModelProperty = viewModelProperty;
    OriginalProperty = origProperty;
    var propName = origProperty?.Name ?? viewModelProperty?.Name;
    var origType = origProperty?.PropertyType;
    var type = origType?.ToSystemType() ?? viewModelProperty?.PropertyType;
    if (type == typeof(bool))
      type = typeof(bool?);
    object? value = null;
    object? originalValue = null;
    originalValue = origProperty?.GetValue(ModeledObject);
    value = originalValue.ToSystemValue(origType);
    base.Name = propName;
    base.Type = type;
    base.OriginalType = origType;
    base.Value = value;
    base.OriginalValue = originalValue;
    PropertyChanged += ObjectPropertyViewModel_PropertyChanged;
  }

  private void ObjectPropertyViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
  {
    if (e.PropertyName == nameof(Value))
    {
      var value = Value;
      var val = value!.ToOpenXmlValue(OriginalType);
      ViewModelProperty?.SetValue(OwnerObjectViewModel, val);
      OriginalProperty?.SetValue(ModeledObject, val);
      var val2 = OriginalProperty?.GetValue(ModeledObject);
      if (val2 != val)
        Debug.WriteLine($"Property {Name} of {ModeledObject} not set to {val}");
    }
  }

  /// <summary>
  /// Gets or sets the owner of the object property
  /// </summary>
  public new ObjectViewModel? OwnerObjectViewModel { get; set; }

  /// <summary>
  /// OwnerObjectViewModel modeled object.
  /// </summary>
  public object? ModeledObject => OwnerObjectViewModel?.ModeledObject;

  /// <summary>
  /// OwnerObjectViewModel modeled object type.
  /// </summary>
  public Type? ModeledObjectType => OwnerObjectViewModel?.ObjectType;

  /// <summary>
  /// Collection of object properties.
  /// </summary>
  public ObjectPropertiesViewModel? Collection { get; internal set; }

  /// <summary>
  /// Property to get/set the value of the object property.
  /// </summary>
  public PropertyInfo? ViewModelProperty { get; set; }

  /// <summary>
  /// Property to get/set the value of the object property.
  /// </summary>
  public PropertyInfo? OriginalProperty { get; set; }


}