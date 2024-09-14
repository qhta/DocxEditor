using System.ComponentModel;

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
  public ObjectPropertyViewModel(ObjectViewModel ownerObjectViewModel) : base(ownerObjectViewModel)
  {
  }

  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="ownerObjectViewModel">New model for an object, which property is modeled here</param>
  /// <param name="propertyName">Name of property of the ownerObjectViewModel view model</param>
  /// <param name="origPropertyName">Name of property of the modeled object. If null then <paramref name="propertyName"/> is used</param>
  public ObjectPropertyViewModel(ObjectViewModel ownerObjectViewModel, string propertyName, string? origPropertyName = null) : base(ownerObjectViewModel)
  {
    var viewModelProperty = ownerObjectViewModel.GetType().GetProperty(propertyName);
    origPropertyName ??= propertyName;
    var origProperty = ModeledObjectType!.GetProperty(origPropertyName);
    if (origProperty == null && viewModelProperty == null)
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

  /// <summary>
  /// Notifies that a property has changed.
  /// </summary>
  /// <param name="propertyName"></param>
  public new void NotifyPropertyChanged(string propertyName)
  {
    base.NotifyPropertyChanged(propertyName);
  }

  private void ObjectPropertyViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
  {
    if (e.PropertyName == nameof(Value))
    {
      var value = Value;
      var val = value!.ToOpenXmlValue(OriginalType);
      ViewModelProperty?.SetValue(Owner, val);
      OriginalProperty?.SetValue(ModeledObject, val);
      var val2 = OriginalProperty?.GetValue(ModeledObject);
      if (val2 != val)
        Debug.WriteLine($"Property {Name} of {ModeledObject} not set to {val}");
    }
  }

  /// <summary>
  /// Gets or sets the owner of the object property
  /// </summary>
  public new ObjectViewModel? Owner => (ObjectViewModel?)base.Owner;

  /// <summary>
  /// Owner modeled object.
  /// </summary>
  public object? ModeledObject => Owner?.ModeledObject;

  /// <summary>
  /// Owner modeled object type.
  /// </summary>
  public Type? ModeledObjectType => Owner?.ObjectType;

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

  #region IObjectViewModelProvider implementation

  /// <summary>
  /// Gets the value as an object view model.
  /// </summary>
  public override IObjectViewModel ObjectViewModel
  {
    get
    {
      if (_objectViewModel == null)
      {
        var value = Value;
        if (value == null)
        {
          var modeledObjectType = OriginalProperty?.PropertyType;
          if (modeledObjectType != null)
          {
            //if (modeledObjectType.Name=="RunFonts")
            //Debug.WriteLine($"modeledObjectType={modeledObjectType}");
            value = Activator.CreateInstance(modeledObjectType);
            _objectViewModel = CreateObjectViewModel(value);
            _objectViewModel.IsNew = true;
          }
        }
        return base.ObjectViewModel;
      }
      return _objectViewModel;
    }
  }

  /// <summary>
  /// Passes the property change event to the view model.
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  protected override void ObjectViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
  {
    //Debug.WriteLine($"{this}.ObjectViewModel_PropertyChanged({sender}, {e.PropertyName})");
    if (e.PropertyName == nameof(IObjectViewModel.IsEmpty))
    {
      if (sender is ObjectViewModel objectViewModel)
      {

        var value = (objectViewModel.IsEmpty == true) ? null : objectViewModel.ModeledObject;
        var thisValue = Value;
        if (thisValue != value)
        {
          Value = value;
          NotifyPropertyChanged(nameof(IsEmpty));
          objectViewModel.IsNew = false;
          //NotifyPropertyChanged(nameof(IsNew));
        }
      }
    }
    NotifyPropertyChanged(nameof(Value));
  }
  #endregion IObjectViewModelProvider implementation

  /// <summary>
  /// Determines if the property <see cref="ObjectViewModel"/> is new.
  /// </summary>
  public bool? IsNew => ObjectViewModel.IsNew;

  /// <summary>
  /// Determines if the property <see cref="ObjectViewModel"/> is empty.
  /// </summary>
  public bool? IsEmpty => ObjectViewModel.IsEmpty;


  /// <summary>
  /// Gets the string representation of the instance for debugging purposes.
  /// </summary>
  /// <returns></returns>
  public override string ToString()
  {
    return GetType().Name + "(" + (ModeledObject != null ? $"({ModeledObject.GetType().Name})" : "") + ")";
  }
}