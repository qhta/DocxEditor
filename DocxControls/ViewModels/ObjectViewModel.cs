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
      if (prop.CanRead && (prop.CanWrite || prop.PropertyType.IsClass && prop.PropertyType != typeof(string)))
      {
        var propName = prop.Name;
        var origType = prop.PropertyType;
        var type = origType.ToSystemType();
        if (type == typeof(bool))
          type = typeof(bool?);
        object? value = null;
        object? originalValue = null;
        if (ModeledObject != null)
        {
          originalValue = prop.GetValue(ModeledObject);
          value = originalValue.ToSystemValue(origType);
        }
        var propertyViewModel = new PropertyViewModel
        {
          Name = propName,
          Type = type,
          OriginalType = origType,
          Value = value,
          OriginalValue = originalValue
        };
        propertyViewModel.PropertyChanged += PropertiesViewModel_PropertyChanged;
        ObjectProperties.Add(propertyViewModel);
      }
    }
    if (IsContainer)
      foreach (var member in ((DX.OpenXmlCompositeElement)ModeledObject!).GetMembers())
      {
        var type = member.GetType();
        if (type.Name == "RsidRoot")
          Debug.Assert(true);
        if (ObjectProperties.Any(p=>p.OriginalValue==member))
          continue;
        var memberViewModel = new ObjectMemberViewModel(this, member.GetType(), member);
        ObjectMembers.Add(memberViewModel);
        memberViewModel.PropertyChanged += PropertiesViewModel_MemberChanged;
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

  private void PropertiesViewModel_MemberChanged(object? sender, PropertyChangedEventArgs e)
  {
    if (e.PropertyName == nameof(PropertyViewModel.Value))
    {
      var objectMemberViewModel = (ObjectMemberViewModel)sender!;
      if (objectMemberViewModel.Container == null)
      {
        var member = objectMemberViewModel.ModeledObject;
        if (member != null)
        {

        }
      }
      else
      {
        NotifyPropertyChanged(nameof(ModeledObject));
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
  public ObservableCollection<ObjectMemberViewModel> ObjectMembers { get; } = new();


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
  public virtual object? Value
  {
    get => (ModeledObject as DX.OpenXmlElement)?.ToSystemValue(ObjectType) ?? ModeledObject!;
    set
    {
      var val = value.ToOpenXmlValue(ObjectType);
      if (val != _modeledObject)
      {
        _modeledObject = val;
        NotifyPropertyChanged(nameof(Value));
      }
    }
  }

  /// <summary>
  /// Not implemented
  /// </summary>
  public bool IsObsolete => false;

  /// <summary>
  /// Mask to use with <c>Exceed MaskedTextBox</c>.
  /// </summary>
  public string? EditMask => PropertyViewModel.GetEditMask(ObjectType!);

  /// <summary>
  /// Watermark to display in the control.
  /// </summary>
  public string? Watermark => PropertyViewModel.GetWatermark(ObjectType!);
  #endregion IPropertyProvider implementation
}
