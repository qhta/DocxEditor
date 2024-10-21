namespace DocxControls.ViewModels;

/// <summary>
/// View model for an object property. Replaces <see cref="PropertyViewModel"/> in the properties view.
/// </summary>
public class ObjectPropertyViewModel : PropertyViewModel
{
  ///// <summary>
  ///// Initializing constructor.
  ///// </summary>
  ///// <param name="ownerObjectViewModel">New model for an object, which property is modeled here</param> 
  //public ObjectPropertyViewModel(ObjectViewModel ownerObjectViewModel) : base(ownerObjectViewModel)
  //{
  //}

  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="ownerObjectViewModel">New model for an object, which objectProperty is modeled here</param>
  /// <param name="objectPropName">Name of objectProperty of the ownerObjectViewModel view model</param>
  /// <param name="origPropName">Name of objectProperty of the modeled object.</param>
  /// <param name="objectProperty">PropertyInfo of the view model</param>
  /// <param name="origProperty">PropertyInfo of the modeled object</param>
  /// <param name="valueType">ValueType of the view model value</param>
  /// <param name="origValueType">ValueType of the modeled object value</param>
  /// <param name="value">Value of the view model objectProperty</param>
  /// <param name="origValue">Value of the modeled object</param>

  public ObjectPropertyViewModel(ObjectViewModel ownerObjectViewModel, string? objectPropName, string? origPropName = null,
    PropertyInfo? objectProperty = null, PropertyInfo? origProperty = null,
    Type? valueType = null, Type? origValueType = null,
    object? value = null, object? origValue = null) : base(ownerObjectViewModel)
  {
    if (objectProperty == null && objectPropName != null)
      objectProperty = ownerObjectViewModel.GetType().GetProperty(objectPropName);

    if (origPropName == null && objectPropName != null)
      origPropName = objectPropName;
    else
    if (objectPropName == null && origPropName != null)
      objectPropName = origPropName;

    if (origProperty == null)
      origProperty = ModeledObjectType!.GetProperty(origPropName!);
    if (origProperty == null && objectProperty == null)
      throw new ArgumentException($"Property {origPropName} not found in {ModeledObjectType!.Name} and not in {ownerObjectViewModel.GetType().Name}");
    //if (!(objectProperty.CanWrite))
    //  throw new ArgumentException($"Property {objectPropName} is not writable in {ownerObjectViewModel.GetType().Name}");

    ViewModelObjectProperty = objectProperty;
    OriginalProperty = origProperty;
    if (objectPropName == null)
      objectPropName = origProperty?.Name ?? objectProperty?.Name;
    if (origValueType == null)
      origValueType = origProperty?.PropertyType;
    if (valueType == null)
      valueType = origValueType?.ToSystemType(objectPropName) ?? ViewModelObjectProperty?.PropertyType;
    //valueType = valueType!.GetNotNullableType();
    if (origValue == null)
      origValue = origProperty?.GetValue(ModeledObject);
    if (value == null)
      value = origValue.ToSystemValue(origValueType);
    base.Name = objectPropName;
    base.Type = valueType;
    OriginalType = origValueType;
    _Value = value;
    OriginalValue = origValue;
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
      try
      {
        IsValid = true;
        if (Owner is ElementViewModel elementViewModel)
        {
          if (value is string str)
            value = str.FromString(ViewModelObjectProperty!.PropertyType);
          ViewModelObjectProperty?.SetValue(elementViewModel, value);
        }
        else
        {
          var val = value!.ToOpenXmlValue(OriginalType);
          object owner = Owner!;
          if (Owner is ObjectViewModel objectViewModel)
            owner = objectViewModel.ModeledObject!;
          ViewModelObjectProperty?.SetValue(owner, val);
          OriginalProperty?.SetValue(ModeledObject, val);
          var val2 = OriginalProperty?.GetValue(ModeledObject);
          if (val2 != val)
            Debug.WriteLine($"Property {Name} of {ModeledObject} not set to {val}");
        }
      } catch (Exception exception)
      {
        ErrorMsg = exception.Message;
        IsValid = false;
        throw;
      }
    }
  }

  /// <summary>
  /// Owner modeled object.
  /// </summary>
  public object? ModeledObject => (Owner as ObjectViewModel)?.ModeledObject;

  /// <summary>
  /// Owner modeled object type.
  /// </summary>
  public Type? ModeledObjectType => (Owner as ObjectViewModel)?.ObjectType;

  /// <summary>
  /// Collection of object properties.
  /// </summary>
  public ObjectPropertiesViewModel? Collection { get; internal set; }

  /// <summary>
  /// Property to get/set the value of the object property.
  /// </summary>
  public PropertyInfo? ViewModelObjectProperty { get; private set; }

  /// <summary>
  /// Property to get/set the value of the object property.
  /// </summary>
  public PropertyInfo? OriginalProperty { get; private set; }

  #region IObjectViewModelProvider implementation

  /// <summary>
  /// Gets the value as an object view model.
  /// </summary>
  public override IObjectViewModel? ObjectViewModel
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
            if (modeledObjectType.IsSubclassOf(typeof(VM.ElementViewModel)))
            {
              var ownerViewModel = Owner;
              if (ownerViewModel is ObjectViewModel objectViewModel)
                ownerViewModel = objectViewModel.ModeledObject as ElementViewModel;
              if (ownerViewModel is ElementViewModel elementViewModel)
              {
                _objectViewModel = Application.Instance.CreateViewModel(elementViewModel, modeledObjectType);
                _objectViewModel.IsNew = true;
              }
              else
                throw new Exception($"Cannot create object of type {modeledObjectType}");
            }
            else
            {
              value = Activator.CreateInstance(modeledObjectType);
              if (value is DX.OpenXmlElement element)
                _objectViewModel = Application.Instance.CreateViewModel(this, element);
              else if (value is ObjectMembersViewModel)
                return null;
              else
                throw new Exception($"Cannot create object of type {modeledObjectType}");
              if (_objectViewModel != null)
                _objectViewModel.IsNew = true;
            }
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
  public new bool? IsNew => ObjectViewModel?.IsNew;

  /// <summary>
  /// Determines if the property <see cref="ObjectViewModel"/> is empty.
  /// </summary>
  public new bool? IsEmpty => ObjectViewModel?.IsEmpty;


  /// <summary>
  /// Gets the string representation of the instance for debugging purposes.
  /// </summary>
  /// <returns></returns>
  public override string ToString()
  {
    return GetType().Name + "(" + (ModeledObject != null ? $"({ModeledObject.GetType().Name})" : "") + ")";
  }
}