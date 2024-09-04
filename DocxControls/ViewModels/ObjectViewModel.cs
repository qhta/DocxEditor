using System.ComponentModel;

using Qhta.MVVM;

namespace DocxControls;


/// <summary>
/// View model for complex object properties
/// </summary>
public class ObjectViewModel : ViewModel, IObjectViewModel, IToolTipProvider, IPropertyProvider, ISelectable
{
  /// <summary>
  ///  Type of the object which properties are modeled
  /// </summary>
  public Type? ObjectType => ModeledObject?.GetType() ?? _ObjectType;

  private Type? _ObjectType;

  /// <summary>
  /// Gets the name of the modeled object type.
  /// </summary>
  public string? ObjectTypeName => ObjectType?.Name;

  /// <summary>
  /// Determines if the modeled object can contain members.
  /// </summary>
  public bool IsContainer => ObjectType?.IsContainer() ?? false;

  /// <summary>
  /// Initializing constructor
  /// for the new item placeholder view model when the type of the modeled object is unknown.
  /// </summary>
  public ObjectViewModel()
  {
     _ObjectType = null;
  }

  /// <summary>
  /// Initializing constructor
  /// when the type of the modeled object is unknown, but the object may be known.
  /// </summary>
  public ObjectViewModel(Object? modeledObject)
  {
    _ObjectType = modeledObject?.GetType();
    if (modeledObject!=null)
      Initialize(_ObjectType, modeledObject);
  }

  /// <summary>
  /// Initializing constructor.
  /// when the type of the modeled object is known.
  /// </summary>
  /// <param name="objectType">Type of the object which will be created if <paramref name="modeledObject"/> is null</param>
  /// <param name="modeledObject">object which properties are modeled</param>
  public ObjectViewModel(Type objectType, Object? modeledObject)
  {
    Initialize(objectType, modeledObject);
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="ObjectViewModel"/> class when the type of the modeled object is known.
  /// </summary>
  /// <param name="objectType"></param>
  /// <param name="modeledObject"></param>
  protected void Initialize(Type? objectType, object? modeledObject)
  {
    _ObjectType = objectType ?? modeledObject?.GetType();
    ModeledObject = modeledObject;
    if (_ObjectType == null)
      return;
    ModeledObject ??= Activator.CreateInstance(_ObjectType);
    var properties = _ObjectType.GetOpenXmlProperties().ToArray();
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
        var propertyViewModel = new ObjectPropertyViewModel (this)
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
    {
      foreach (var member in ((DX.OpenXmlCompositeElement)ModeledObject!).GetMembers())
      {
        if (ObjectProperties.Items.Any(p=>p.OriginalValue==member))
          continue;
        var memberViewModel = new ObjectMemberViewModel(this, member);
        ObjectMembers!.Add(memberViewModel);
        memberViewModel.PropertyChanged += PropertiesViewModel_MemberChanged;
      }
      ObjectMembers!.CollectionChanged += ObjectMembers_CollectionChanged;
    }
  }

  private void ObjectMembers_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
  {
    if (e.Action == NotifyCollectionChangedAction.Add)
    {
      foreach (ObjectMemberViewModel memberViewModel in e.NewItems!)
      {
        //if (memberViewModel.IsEmpty)
        //  ObjectMembers.Initialize(memberViewModel);
        //if (memberViewModel.Obj != null &&
        //    memberViewModel.Type != null && memberViewModel.Validate() && ValidateName(memberViewModel))
        if (ModeledObject is DX.OpenXmlCompositeElement container)
        {
          var member = memberViewModel.ModeledObject ?? memberViewModel.Value?.ToOpenXmlValue(memberViewModel.ObjectType);
          if (member is DX.OpenXmlElement element)
            container.AppendChild(element); 
          NotifyPropertyChanged(nameof(ModeledObject));
        }
        memberViewModel.Container = this;
        memberViewModel.PropertyChanged += PropertiesViewModel_MemberChanged;
      }
    }
    else if (e.Action == NotifyCollectionChangedAction.Remove)
    {
      //do nothing
      foreach (ObjectMemberViewModel memberViewModel in e.OldItems!)
      {
        if (ModeledObject is DX.OpenXmlCompositeElement container)
        {
          var member = memberViewModel.ModeledObject;
          if (member is DX.OpenXmlElement element && element.Parent==container)
            element.Remove(); 
        }
      }
    }
    else if (e.Action == NotifyCollectionChangedAction.Reset)
    {
      // do nothing
      //CustomProperties.Clear();
    }
    else if (e.Action == NotifyCollectionChangedAction.Replace)
    {
      // do nothing
      //var oldMemberViewModel = (ObjectMemberViewModel)e.OldItems![0]!;
      //var memberViewModel = (ObjectMemberViewModel)e.NewItems![0]!;
      //if (ModeledObject is DX.OpenXmlCompositeElement owner)
      //{
      //  var member = memberViewModel.ModeledObject ?? memberViewModel.Value?.ToOpenXmlValue(memberViewModel.ObjectType);
      //  if (member is DX.OpenXmlElement element)
      //    owner.AppendChild(element);
      //  NotifyPropertyChanged(nameof(ModeledObject));
      //}
    }
    else if (e.Action == NotifyCollectionChangedAction.Move)
    {
      // do nothing
    }
    else
    {
      throw new NotImplementedException();
    }
  }

  private void PropertiesViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
  {
    if (e.PropertyName == nameof(PropertyViewModel.Value))
    {
      var propertyViewModel = (PropertyViewModel)sender!;
      var propertyName = propertyViewModel.Name;
      if (propertyName != null && _ObjectType!=null)
      {
        ModeledObject ??= Activator.CreateInstance(_ObjectType);
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
          NotifyPropertyChanged(nameof(Value));
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
        NotifyPropertyChanged(nameof(Value));
        NotifyPropertyChanged(nameof(Watermark));
      }
    }
  }
  private object? _modeledObject;

  /// <summary>
  /// Properties of the object.
  /// </summary>
  public ObjectPropertiesViewModel ObjectProperties { get; } = new();

  /// <summary>
  /// Members of the object.
  /// </summary>
  public ObjectMembersViewModel? ObjectMembers
  {
    get
    {
      if (!IsContainer)
        return null;
      _objectMembers ??= new ObjectMembersViewModel();
      if (ObjectType != null)
        _objectMembers.MemberTypes = ObjectType.GetMemberTypes();
      var result =_objectMembers;
      //Debug.WriteLine($"ObjectMembers={result}");
      return result;
    }
    set
    {
      _objectMembers = value; 
      NotifyPropertyChanged(nameof(ObjectMembers));
    }
  }
  private ObjectMembersViewModel? _objectMembers;

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

  #region ISelectable implementation
  /// <summary>
  /// Determines if the object is selected.
  /// </summary>
  public bool IsSelected
  {
    get => _IsSelected;
    set
    {
      if (value != _IsSelected)
      {
        _IsSelected = value;
        NotifyPropertyChanged(nameof(IsSelected));
      }
    }
  }
  private bool _IsSelected;
  #endregion

  /// <summary>
  /// Determines if the object is new.
  /// </summary>
  public bool IsNew
  {
    get => _IsNew;
    set
    {
      if (value != _IsNew)
      {
        _IsNew = value;
        NotifyPropertyChanged(nameof(IsNew));
      }
    }
  }
  private bool _IsNew;
}
