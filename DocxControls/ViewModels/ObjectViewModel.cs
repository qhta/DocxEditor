using System.ComponentModel;
using System.Windows.Input;

using DocumentFormat.OpenXml.Wordprocessing;

using DocxControls.Helpers;

using Qhta.MVVM;

namespace DocxControls.ViewModels;


/// <summary>
/// View model for complex object properties
/// </summary>
public class ObjectViewModel : ViewModel, IObjectViewModel, IToolTipProvider, IPropertyProvider, ISelectable, IEditable
{

  /// <summary>
  /// Initializing constructor
  /// for the new item placeholder view model when the type of the modeled object is unknown.
  /// </summary>
  protected ObjectViewModel()
  {
    _ObjectType = null;
    // ReSharper disable once VirtualMemberCallInConstructor
    DoubleClickCommand = new RelayCommand<object>(OnItemDoubleClicked);
    LeftMouseDownCommand = new RelayCommand<object>(OnItemLeftMouseDown);
    RightMouseUpCommand = new RelayCommand<object>(OnItemRightMouseUp);
  }

  /// <summary>
  /// Initializing constructor
  /// when the type of the modeled object is unknown, but the object may be known.
  /// </summary>
  protected ObjectViewModel(Object modeledObject) : this()
  {
    _ObjectType = modeledObject.GetType();
    Initialize(_ObjectType, modeledObject);
  }

  /// <summary>
  /// Initializing constructor
  /// when the type of the modeled object is unknown, but the object may be known.
  /// </summary>
  protected ObjectViewModel(ViewModel owner, Object modeledObject) : this()
  {
    Owner = owner;
    _ObjectType = modeledObject.GetType();
    Initialize(_ObjectType, modeledObject);
  }

  /// <summary>
  ///  ValueType of the object which properties are modeled
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
  public bool IsContainer
  {
    get
    {
      if (_ObjectType == null)
        return false;
      var result = _ObjectType?.IsContainer() ?? false;
      return result;
    }
  }

  /// <summary>
  /// Owner ViewModel.
  /// </summary>
  public ViewModel? Owner { get; set; }

  /// <summary>
  /// Title for the window
  /// </summary>
  public string WindowTitle
  {
    get
    {
      var result = ObjectTypeName ?? String.Empty;
      if (!IsEditable)
      {
        result += $" [{Strings.ReadOnly}]";
      }
      else if (IsModified)
      {
        result += $" [{Strings.Modified}]";
      }
      return result;
    }
  }

  /// <summary>
  /// Recursively gets the top document view model
  /// </summary>
  /// <returns></returns>
  /// <exception cref="InvalidOperationException"></exception>
  public Document GetDocumentViewModel() => (Owner as Document) ?? (Owner as ObjectViewModel)?.GetDocumentViewModel()
    ?? throw new InvalidOperationException("Owner is not a document view model");

  /// <summary>
  /// If ViewModel class is registered for the object type, creates an instance of the ViewModel class,
  /// otherwise creates an instance of the <see cref="ObjectViewModel"/> class.
  /// </summary>
  /// <param name="parent"></param>
  /// <param name="modeledObject"></param>
  /// <returns></returns>
  public static ObjectViewModel? Create(ViewModel parent, object modeledObject)
  {
    var modeledObjectType = modeledObject?.GetType();
    if (!(modeledObjectType != null && KnownObjectViewModelTypes.TryGetValue(modeledObjectType, out var viewModelType)))
      if (!(modeledObjectType?.BaseType != null && KnownObjectViewModelTypes.TryGetValue(modeledObjectType.BaseType, out viewModelType)))
        viewModelType = null;
    if (viewModelType == null)
    {
      if (modeledObject != null)
        return new ObjectViewModel(parent, modeledObject);
    }
    var result = viewModelType != null ? (ObjectViewModel)Activator.CreateInstance(viewModelType, parent, modeledObject)! : null;
    return result;
  }

  /// <summary>
  /// If ViewModel class is registered for the object type, creates an instance of the ViewModel class,
  /// otherwise creates an instance of the <see cref="ObjectViewModel"/> class.
  /// </summary>
  /// <param name="parent"></param>
  /// <param name="modeledObjectType"></param>
  /// <returns></returns>
  public static ObjectViewModel? Create(ViewModel parent, Type modeledObjectType)
  {
    if (!(KnownObjectViewModelTypes.TryGetValue(modeledObjectType, out var viewModelType)))
      if (!(modeledObjectType.BaseType != null && KnownObjectViewModelTypes.TryGetValue(modeledObjectType.BaseType, out viewModelType)))
        viewModelType = null;
    if (viewModelType == null)
    {
      throw new NotImplementedException($"ViewModelType({modeledObjectType}) not known");
    }
    var result =  (ObjectViewModel)Activator.CreateInstance(viewModelType, parent, null)!;
    return result;
  }
  /// <summary>
  /// ViewModel types declared in this assembly.
  /// </summary>
  public static readonly Dictionary<Type, Type> KnownObjectViewModelTypes = new()
  {
    { typeof(DXW.Body), typeof(BodyViewModel)},
    { typeof(DXW.BookmarkStart), typeof(BookmarkStart)},
    { typeof(DXW.BookmarkEnd), typeof(BookmarkEnd)},
    { typeof(DXW.Paragraph), typeof(Paragraph)},
    { typeof(DXW.Run), typeof(RunViewModel)},
    { typeof(DXW.RunProperties), typeof(RunPropertiesViewModel)},
    { typeof(DXW.Text), typeof(TextViewModel)},
    { typeof(DXW.Table), typeof(TableViewModel)},
    { typeof(DXW.SdtElement), typeof(SdtElementViewModel)},
    { typeof(DXW.SdtProperties), typeof(SdtPropertiesViewModel)},
    { typeof(DXW.LastRenderedPageBreak), typeof(LastRenderedPageBreakViewModel)},
  };


  /// <summary>
  /// Initializing constructor.
  /// when the type of the modeled object is known.
  /// </summary>
  /// <param name="objectType">ValueType of the object which will be created if <paramref name="modeledObject"/> is null</param>
  /// <param name="modeledObject">object which properties are modeled</param>
  public ObjectViewModel(Type objectType, Object? modeledObject) : this()
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
    if (ModeledObject == null)
      ModeledObject = Activator.CreateInstance(_ObjectType);
  }

  private void ObjectMembers_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
  {
    if (e.Action == NotifyCollectionChangedAction.Add)
    {
      foreach (ObjectMemberViewModel memberViewModel in e.NewItems!)
      {
        if (ModeledObject is DX.OpenXmlCompositeElement container)
        {
          var member = memberViewModel.ModeledObject ?? memberViewModel.Value?.ToOpenXmlValue(memberViewModel.ObjectType);
          if (member is DX.OpenXmlElement element)
            container.AppendChild(element);
          NotifyPropertyChanged(nameof(ModeledObject));
        }
        memberViewModel.Owner = this;
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
          if (member is DX.OpenXmlElement element && element.Parent == container)
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
      var propName = propertyViewModel.Name;
      //if (propName == "RunFonts")
      //{
      //Debug.WriteLine($"{this}.PropertiesViewModel_PropertyChanged({sender}, {propName})");
      if (ObjectProperties.Items.Any(p => p.Name == propName))
      {
        var prop = ObjectProperties.Items.First(p => p.Name == propName);
        if (prop.Value != propertyViewModel.Value)
        {
          var value = propertyViewModel.Value;
          prop.Value = propertyViewModel.Value;
          NotifyPropertyChanged(nameof(ModeledObject));
          NotifyPropertyChanged(nameof(IsEmpty));
        }
      }

      if (propName != null && _ObjectType != null)
      {
        ModeledObject ??= Activator.CreateInstance(_ObjectType);
        var prop = ModeledObject!.GetType().GetProperty(propName);
        if (prop != null && prop.CanWrite)
        {
          prop.SetValue(ModeledObject, propertyViewModel.Value.ToOpenXmlValue(propertyViewModel.OriginalType!));
          //if (Value!= ModeledObject)
          //{
          //  if (ModeledObject is DX.OpenXmlElement modeledElement)
          //  {
          //    if (modeledElement.IsEmpty())
          //      Value = null;
          //    else
          //      Value = modeledElement;
          //  }
          //}
          NotifyPropertyChanged(nameof(ModeledObject));
          NotifyPropertyChanged(nameof(IsEmpty));
        }
      }
    }
  }

  private void PropertiesViewModel_MemberChanged(object? sender, PropertyChangedEventArgs e)
  {
    if (e.PropertyName == nameof(PropertyViewModel.Value))
    {
      var objectMemberViewModel = (ObjectMemberViewModel)sender!;
      if (objectMemberViewModel.Owner == null)
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
  public ObjectPropertiesViewModel ObjectProperties
  {
    get
    {
      if (_objectProperties == null)
        _objectProperties = InitObjectProperties();
      return _objectProperties;
    }
  }
  private ObjectPropertiesViewModel? _objectProperties;

  /// <summary>
  /// Initializes the object properties
  /// </summary>
  protected virtual ObjectPropertiesViewModel InitObjectProperties()
  {
    var objectProperties = new ObjectPropertiesViewModel(this);
    if (ObjectType != null)
    {
      var properties = ObjectType.GetOpenXmlProperties().ToArray();
      foreach (var prop in properties)
      {
        if (prop.CanRead && (prop.CanWrite || prop.PropertyType.IsClass && prop.PropertyType != typeof(string)))
        {
          var propName = prop.Name;
          if (propName == "Val")
          {
            if (ObjectType != null)
              propName = ObjectType.Name + "." + propName;
          }
          var origType = prop.PropertyType;
          var type = origType.ToSystemType(propName);
          object? value = null;
          object? originalValue = null;
          var modeledObject = ModeledObject;
          if (modeledObject != null)
          {
            originalValue = prop.GetValue(modeledObject);
            value = originalValue?.ToSystemValue(origType);
          }
          var propertyViewModel =
            new ObjectPropertyViewModel(this, null, propName, null, prop, type, origType, value, originalValue);
          propertyViewModel.PropertyChanged += PropertiesViewModel_PropertyChanged;
          objectProperties.Add(propertyViewModel);
        }
      }
    }
    objectProperties.PropertyChanged += ObjectProperties_PropertyChanged;
    return objectProperties;
  }

  private void ObjectProperties_PropertyChanged(object? sender, PropertyChangedEventArgs e)
  {
    if (e.PropertyName == nameof(ObjectPropertiesViewModel.DataGridWidth) && sender is ObjectPropertiesViewModel propertiesViewModel)
    {
      DataGridWidth = propertiesViewModel.DataGridWidth;
    }
  }

  /// <summary>
  /// Members of the object.
  /// </summary>
  public ObjectMembersViewModel? ObjectMembers
  {
    get
    {
      if (IsContainer)
      {
        if (_objectMembers == null)
        {
          _objectMembers = InitObjectMembers();
        }
      }
      return _objectMembers;
    }
  }
  private ObjectMembersViewModel? _objectMembers;

  /// <summary>
  /// Initializes the object members
  /// </summary>
  /// <returns></returns>
  protected virtual ObjectMembersViewModel InitObjectMembers()
  {
    var objectMembers = new ObjectMembersViewModel();
    if (ObjectType != null)
      objectMembers.AllowedMemberTypes = ObjectType.GetAllowedMemberTypes();
    foreach (var member in ((DX.OpenXmlCompositeElement)ModeledObject!).GetMembers())
    {
      if (ObjectProperties.Items.Any(p => p.OriginalValue == member))
        continue;
      var memberViewModel = new ObjectMemberViewModel(this, member);
      objectMembers.Add(memberViewModel);
      memberViewModel.PropertyChanged += PropertiesViewModel_MemberChanged;
    }
    objectMembers.CollectionChanged += ObjectMembers_CollectionChanged;
    return objectMembers;
  }

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
        //Debug.WriteLine($"ObjectViewModel.Value={value}");
        //NotifyPropertyChanged(nameof(ModeledObject));
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
  public virtual bool IsSelected
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
  /// Determines if the object is highlighted (temporary selected).
  /// </summary>
  public virtual bool IsHighlighted
  {
    get => _IsHighlighted;
    set
    {
      if (value != _IsHighlighted)
      {
        _IsHighlighted = value;
        NotifyPropertyChanged(nameof(IsHighlighted));
      }
    }
  }
  private bool _IsHighlighted;

  /// <summary>
  /// Determines if the object is new.
  /// </summary>
  public bool IsNew
  {
    get
    {
      //Debug.WriteLine($"{this}.GetIsNew={_IsNew}");
      return _IsNew;
    }
    set
    {
      if (value != _IsNew)
      {
        _IsNew = value;
        //Debug.WriteLine($"{this}.SetIsNew={value}");
        NotifyPropertyChanged(nameof(IsNew));
      }
    }
  }
  private bool _IsNew;

  /// <summary>
  /// Determines if the object is new.
  /// </summary>
  public bool IsEmpty => (ModeledObject as DX.OpenXmlElement)?.IsEmpty() ?? true;

  /// <summary>
  /// Width of the data grid in the view
  /// </summary>
  public double DataGridWidth
  {
    get => _dataGridWidth;
    set
    {
      if (_dataGridWidth != value)
      {
        _dataGridWidth = value;
        NotifyPropertyChanged(nameof(DataGridWidth));
      }
    }
  }
  private double _dataGridWidth = double.NaN;


  /// <summary>
  /// Command to handle the double click event
  /// </summary>
  public ICommand? DoubleClickCommand { get; protected set; }

  private void OnItemDoubleClicked(object? parameter)
  {
    if (parameter is ObjectViewModel item)
    {
      // Do something with the item
    }
  }

  /// <summary>
  /// Command to handle the double click event
  /// </summary>
  public ICommand? LeftMouseDownCommand { get; protected set; }

  private void OnItemLeftMouseDown(object? parameter)
  {
    if (parameter is ObjectViewModel item)
    {
      item.IsSelected = !item.IsSelected;
    }
  }

  /// <summary>
  /// Command to handle the double click event
  /// </summary>
  public ICommand? RightMouseUpCommand { get; protected set; }

  private void OnItemRightMouseUp(object? parameter)
  {
    if (parameter is ObjectViewModel item)
    {
      Application.Instance.ShowProperties(item);
    }
  }

  /// <summary>
  /// Gets the string representation of the instance for debugging purposes.
  /// </summary>
  /// <returns></returns>
  public override string ToString()
  {
    if (_ObjectType==null)
      Debug.WriteLine($"ObjectViewModel._ObjectType is null, Owner is {Owner?.GetType().Name}");
    return GetType().Name + "(" + (_ObjectType != null ? $"{_ObjectType.Name}" : "") + ")";
  }

  #region IEditable implementation
  /// <summary>
  /// Determines if the object is editable.
  /// </summary>
  public bool IsEditable => (Owner as IEditable)?.IsEditable ?? true;

  /// <summary>
  /// Was the object modified?
  /// </summary>
  public bool IsModified
  {
    get => _isModified;
    set
    {
      if (IsModifiedInternal) return;
      if (_isModified != value)
      {
        _isModified = value;
        NotifyPropertyChanged(nameof(IsModified));
        NotifyPropertyChanged(nameof(WindowTitle));
        if (value && Owner is IEditable editable)
        {
          editable.IsModified = value;
        }
      }
    }
  }
  private bool _isModified;

  /// <summary>
  /// Is the object modified internally?
  /// </summary>
  public bool IsModifiedInternal { get; set; }

  #endregion IEditable implementation
}