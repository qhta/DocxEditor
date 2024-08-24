using System.Collections.ObjectModel;
using System.ComponentModel;

using DocumentFormat.OpenXml.Wordprocessing;

namespace DocxControls;


/// <summary>
/// View model for complex object properties
/// </summary>
public class ObjectViewModel : PropertiesViewModel, IObjectViewModel, IToolTipProvider, IPropertyProvider
{
  /// <summary>
  ///  Type of the object which properties are modeled
  /// </summary>
  public Type ObjectType { get; init; }

  /// <summary>
  /// Gets the name of the modeled object type.
  /// </summary>
  public string ObjectTypeName => ObjectType.Name;

  /// <summary>
  /// Determines if the modeled object can contain members.
  /// </summary>
  public bool IsContainer => ObjectType.IsSubclassOf(typeof(DX.OpenXmlCompositeElement));

  /// <summary>
  /// Initializes a new instance of the <see cref="ObjectViewModel"/> class.
  /// </summary>
  /// <param name="objectType">Type of the object which will be created if <paramref name="modeledObject"/> is null</param>
  /// <param name="modeledObject">object which properties are modeled</param>
  public ObjectViewModel(Type objectType, Object? modeledObject)
  {
    if (objectType.Name == "Rsids")
      Debug.Assert(true);
    ObjectType = objectType;
    ModeledObject = modeledObject;
    ModeledObject ??= Activator.CreateInstance(ObjectType);
    var properties = objectType.GetOpenXmlProperties().ToArray();
    foreach (var prop in properties)
    {
      if (prop.CanRead && (prop.CanWrite || prop.PropertyType.IsClass && prop.PropertyType!=typeof(string)))
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
    if (IsContainer)
      foreach (var member in ((DX.OpenXmlCompositeElement)ModeledObject!).GetMembers())
      {
        var memberViewModel = new ObjectViewModel(member.GetType(), member);
        ObjectMembers.Add(memberViewModel);
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
  /// Properties of the object.
  /// </summary>
  public ObservableCollection<PropertyViewModel> ObjectProperties => Items;

  /// <summary>
  /// Members of the object.
  /// </summary>
  public ObservableCollection<ObjectViewModel> ObjectMembers { get; } = new();


  #region IToolTipProvider implementation
  /// <summary>
  /// Not implemented
  /// </summary>
  public bool HasTooltip => false;

  /// <summary>
  /// Not implemented
  /// </summary>
  public string? TooltipTitle => null;

  /// <summary>
  /// Not implemented
  /// </summary>
  public string? TooltipDescription => null;
  #endregion IToolTipProvider implementation

  #region IPropertyProvider implementation
  /// <summary>
  /// Not implemented
  /// </summary>
  public bool IsReadOnly => false;

  /// <summary>
  /// Value of the property.
  /// </summary>
  public object? Value
  {
    get => _modeledObject;
    set
    {
      if (value != _modeledObject)
      {
        _modeledObject = value;
        NotifyPropertyChanged(nameof(Value));
      }
    }
  }

  /// <summary>
  /// Not implemented
  /// </summary>
  public bool IsObsolete => false;
  #endregion IPropertyProvider implementation
}
