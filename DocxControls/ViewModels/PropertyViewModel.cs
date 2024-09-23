using System.ComponentModel;

using DocxControls.Helpers;

using Qhta.MVVM;
using Qhta.TypeUtils;

namespace DocxControls.ViewModels;

/// <summary>
/// View model for a property of a document.
/// </summary>
public class PropertyViewModel : ViewModel, IToolTipProvider, IBooleanProvider, IEnumProvider, IObjectViewModelProvider, IPropertyProvider, ISelectable, IEditable, DA.IElement
{

  /// <summary>
  /// Default constructor needed to allow adding new properties.
  /// </summary>
  public PropertyViewModel()
  {
  }

  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="owner"></param>
  public PropertyViewModel(ViewModel owner)
  {
    Owner = owner;
  }

  /// <summary>
  /// Owner view model
  /// </summary>
  public ViewModel? Owner { get; set; }

  #region IElement implementation
  DA.Application DA.IElement.Application => DocxControls.Application.Instance;
  object? DA.IElement.Parent => Owner;
  #endregion

  /// <summary>
  /// Display caption for the property.
  /// </summary>
  public virtual string? Caption
  {
    get
    {
      try
      {
        if (Name != null)
        {
          var result = PropertiesCaptions.ResourceManager.GetString(Name, CultureInfo.CurrentUICulture);
          if (result == null)
          {
            result = Name;
            Debug.WriteLine($"{Name}");
          }
          return result;
        }
        return "Unnamed property";
      }
      catch
      {
        return Name;
      }

    }
  }


  /// <summary>
  /// Name of the property to get/set.
  /// </summary>
  public virtual string? Name
  {
    get;
    set;
  }

  /// <summary>
  /// ValueType of the property.
  /// </summary>
  public virtual Type? Type
  {
    get;
    set;
  }

  #region IPropertyProvider implementation
  /// <summary>
  /// Is the property value read-only?
  /// </summary>
  public virtual bool IsReadOnly { get; init; }

  /// <summary>
  /// Value of the property.
  /// </summary>
  public virtual object? Value
  {
    get => _Value;
    set
    {
      if (value != _Value && Name != null)
      {
        _Value = value;
        NotifyPropertyChanged(nameof(Value));
        if (Owner is ViewModel viewModel)
          viewModel.NotifyPropertyChanged(Name);
        IsModified = true;
      }
    }
  }
  /// <summary>
  /// Field for the property value.
  /// </summary>
  protected object? _Value;

  /// <summary>
  /// String value of the property for sorting.
  /// </summary>
  public virtual String? ValueString => (String?)Converter.Convert(Value, Type!, null, CultureInfo.CurrentUICulture);
  static readonly PropertyValueConverter Converter = new();

  /// <summary>
  /// Is the property obsolete?
  /// </summary>
  public bool IsObsolete => Name != null && (PropertiesDescriptions.ResourceManager
    .GetString(Name!, CultureInfo.InvariantCulture)?.Contains("Obsolete", StringComparison.InvariantCultureIgnoreCase) ?? false);

  /// <summary>
  /// Mask to use with <c>Exceed MaskedTextBox</c>.
  /// </summary>
  public virtual string? EditMask => GetEditMask(Type!);

  /// <summary>
  /// Get the edit mask for a type.
  /// </summary>
  /// <param name="type"></param>
  /// <returns></returns>
  public static string? GetEditMask(Type? type)
  {
    //Debug.WriteLine($"PropertyViewModel.GetWatermark({type})");
    if (type == null)
      return null;
    if (type.IsNullable(out var baseType))
      type = baseType;
    if (type == typeof(string))
      return null;
    if (type.BaseType == typeof(DXW.LongHexNumberType))
      type = typeof(HexInt);
    if (type == typeof(DateTime))
      return @"[1-2][X-9][X-9][X-9]-[X-1][X-9]-[X-3][X-9] [X-2][X-9]:[X-5][X-9]:[X-5][X-9]";
    if (type == typeof(TimeSpan))
      return @"(X[X-9]|1[X-9]|2[X-3]):[X-5][X-9]";
    if (type == typeof(uint) || type == typeof(byte) || type == typeof(ushort) || type == typeof(ulong))
      return @"\d+";
    if (type == typeof(int) || type == typeof(sbyte) || type == typeof(short) || type == typeof(long))
      return @"-?\d+";
    if (type == typeof(decimal) || type == typeof(double) || type == typeof(float))
      return @"-?\d+\.?\d*";
    if (type == typeof(bool))
      return "L";
    if (type == typeof(Guid))
      return "[0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f]-[0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f]-[0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f]-[0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f]-[0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f]";
    if (type == typeof(HexInt))
      return "[0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f]";
    if (type == typeof(HexRGB))
      return "[0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f]";
    if (type == typeof(HexByte))
      return "[0-9A-Fa-f][0-9A-Fa-f]";
    return null;
  }

  /// <summary>
  /// Watermark to display in the control.
  /// </summary>
  public string? Watermark => GetWatermark(Type!);

  /// <summary>
  /// Get the watermark for a type.
  /// </summary>
  /// <param name="type"></param>
  /// <returns></returns>
  public static string? GetWatermark(Type? type)
  {
    //Debug.WriteLine($"PropertyViewModel.GetWatermark({type})");
    if (type == null)
      return null;
    if (type.IsNullable(out var baseType))
      type = baseType;
    if (type == typeof(string))
      return null;
    if (type.BaseType == typeof(DXW.LongHexNumberType))
      type = typeof(HexInt);
    if (type == typeof(DateTime))
      return "yyyy-MM-dd hh:mm:ss";
    if (type == typeof(TimeSpan))
      return "hh:mm:ss";
    if (type == typeof(uint) || type == typeof(byte) || type == typeof(ushort) || type == typeof(ulong))
      return @"0";
    if (type == typeof(int) || type == typeof(sbyte) || type == typeof(short) || type == typeof(long))
      return @"0";
    if (type == typeof(decimal) || type == typeof(double) || type == typeof(float))
      return @"0";
    if (type == typeof(bool))
      return "true/false";
    if (type == typeof(Guid))
      return "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX";
    if (type == typeof(HexInt))
      return "XXXXXXXX";
    if (type == typeof(HexRGB))
      return "XXXXXX";
    if (type == typeof(HexByte))
      return "XX";
    return null;
  }

  /// <summary>
  /// Determines if the property value is null.
  /// </summary>
  public bool IsEmpty
  {
    get
    {
      var result = ObjectViewModel?.IsEmpty ?? Value == null;
      //if (Name == "DocumentProtection")
      //  Debug.WriteLine($"PropertyViewModel.IsEmpty({Name})={result}");
      return result;
    }
  }

  /// <summary>
  /// Determines if the property Owner is null.
  /// </summary>
  public bool IsNew => Owner == null || (ObjectViewModel?.IsNew ?? true);

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
  /// Not nullable type of the property.
  /// </summary>
  public virtual Type? NotNullableType
  {
    get
    {
      var type = Type;
      if (type != null)
      {
        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
          return type.GetGenericArguments()[0];
      }
      return type;
    }
  }

  /// <summary>
  /// Original type of the setting.
  /// </summary>
  public Type? OriginalType { get; set; }

  /// <summary>
  /// Original value of the property.
  /// </summary>
  public object? OriginalValue { get; set; }

  #region IToolTipProvider implementation

  /// <summary>
  /// Does the property have a tooltip?
  /// </summary>
  public virtual bool HasTooltip => Name != null &&
    PropertiesTooltips.ResourceManager.GetString(Name, CultureInfo.CurrentUICulture) != null;

  /// <summary>
  /// Tooltip for the property
  /// </summary>
  public virtual string? TooltipTitle => (Name != null) ?
    PropertiesTooltips.ResourceManager.GetString(Name, CultureInfo.CurrentUICulture) : null;

  /// <summary>
  /// Description of the property
  /// </summary>
  public virtual string? TooltipDescription => (Name != null) ? FixDescription(PropertiesDescriptions.ResourceManager
    .GetString(Name!, CultureInfo.CurrentUICulture)) : null;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="description"></param>
  /// <returns></returns>
  protected string? FixDescription(string? description)
  {
    return description?.Replace("<p/>", "\n\n").Replace("<br/>", "\n");
  }

  #endregion IToolTipProvider implementation

  #region IBooleanProvider implementation

  /// <summary>
  /// Is the property a boolean?
  /// </summary>
  public bool IsBoolean
  {
    get
    {
      var type = NotNullableType;
      if (type != null)
      {
        if (type == typeof(bool))
          return true;
        var systemType = type.ToSystemType(Name);
        if (systemType == typeof(bool) || systemType == typeof(bool?))
          return true;
      }
      return false;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public bool IsThreeState
  {
    get
    {
      if (Name == "WordPerfectJustification")
        Debug.Assert(true);
      var type = Type;
      var result = false;
      if (type != null)
      {
        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
          result = true;
      }
      //Debug.WriteLine($"PropertyViewModel({Name}).IsThreeState({type})={result}");
      return result;
    }
  }

  /// <summary>
  /// Boolean value of the property.
  /// </summary>
  public bool? AsBoolean
  {
    get
    {
      if (Value is bool b)
        return b;
      if (Value is DXO10W.OnOffValues onOffValues)
      {
        if (onOffValues == DXO10W.OnOffValues.One || onOffValues == DXO10W.OnOffValues.True)
          return true;
        if (onOffValues == DXO10W.OnOffValues.Zero || onOffValues == DXO10W.OnOffValues.False)
          return false;
      }
      return null;
    }

    set
    {
      if (Type == typeof(DXO10W.OnOffValues))
      {
        if (value == true)
          Value = DXO10W.OnOffValues.One;
        else if (value == false)
          Value = DXO10W.OnOffValues.Zero;
        else
          Value = null;
        return;
      }
      Value = value;
    }
  }
  #endregion IBooleanProvider implementation

  #region IEnumProvider implementation

  /// <summary>
  /// Is the property type an enum?
  /// </summary>
  public virtual bool IsEnum
  {
    get
    {
      var type = NotNullableType;
      if (type != null)
      {
        if (type.IsEnum)
          return true;
        if (type.IsOpenXmlEnum())
          return true;
      }
      return false;
    }
  }

  /// <summary>
  /// Is the property type an enum treated as separate bits?
  /// </summary>
  public virtual bool IsFlags => Type != null && Type.IsEnum && Type.GetCustomAttributes(typeof(FlagsAttribute), false).Any();

  ///// <summary>
  ///// Integer value of the property.
  ///// </summary>
  //public int? AsInteger
  //{
  //  get => Value as int?;
  //  set => Value = value;
  //}

  /// <summary>
  /// Selected enum value of the property.
  /// </summary>
  public object? SelectedEnum
  {
    get => Value;
    set
    {
      if (Value != value)
      {
        Value = value;
        NotifyPropertyChanged(nameof(SelectedEnum));
      }
    }
  }

  /// <summary>
  /// Enum values of the property.
  /// </summary>
  public IEnumerable<object> EnumValues
  {
    get
    {
      var type = NotNullableType;
      if (type != null)
      {
        if (type.IsOpenXmlEnum())
        {
          var result = new List<EnumValueViewModel>();
          result.Add(new EnumValueViewModel());
          result.AddRange(
            type.GetOpenXmlProperties()
              .Select(CreateEnumPropValueViewModel));
          return result;
        }
        if (type.IsEnum)
        {
          if (IsFlags)
            return Enum.GetValues(type).Cast<object>().Select(CreateEnumFlagValueViewModel);
          return Enum.GetValues(type).Cast<object>().Select(CreateEnumValueViewModel);
        }
      }
      return [];
    }
  }

  private EnumValueViewModel CreateEnumPropValueViewModel(PropertyInfo value)
  {
    var name = value.Name;
    return new EnumValueViewModel
    {
      Value = name,
      Caption = GetEnumCaption(name),
      Tooltip = GetEnumTooltip(name)
    };
  }
  private EnumValueViewModel CreateEnumValueViewModel(object value)
  {
    return new EnumValueViewModel
    {
      Value = value,
      Caption = GetEnumCaption(value),
      Tooltip = GetEnumTooltip(value)
    };
  }

  private EnumFlagValueViewModel CreateEnumFlagValueViewModel(object value)
  {
    var result = new EnumFlagValueViewModel(this, Type!, value)
    {
      Value = value,
      Caption = GetEnumCaption(value),
      Tooltip = GetEnumTooltip(value)
    };
    return result;
  }

  private string? GetEnumCaption(object value)
  {
    var type = NotNullableType;
    if (type != null)
    {
      string? str = value as String;
      if (type.IsEnum)
      {
        str = Enum.GetName(type, value);
      }
      if (str != null)
        str = PropertiesCaptions.ResourceManager.GetString(str, CultureInfo.CurrentUICulture) ?? str;
      return str;
    }
    return value.ToString();
  }

  private string? GetEnumTooltip(object value)
  {
    var type = NotNullableType;
    if (type != null)
    {
      string? str = value as String;
      if (type.IsEnum)
      {
        str = Enum.GetName(type, value);
      }
      if (str != null)
        str = PropertiesTooltips.ResourceManager.GetString(str, CultureInfo.CurrentUICulture) ?? str;
      return str;
    }
    return value.ToString();
  }
  #endregion IEnumProvider implementation

  #region IObjectViewModelProvider implementation

  /// <summary>
  /// Is the property of object type?
  /// </summary>
  public virtual bool IsObject => (Type != null) && (Type.IsClass && Type != typeof(string));

  /// <summary>
  /// Gets the value as an object view model.
  /// </summary>
  public virtual IObjectViewModel? ObjectViewModel
  {
    get
    {
      if (_objectViewModel == null)
      {
        var value = _Value;
        if (value != null)
          _objectViewModel = CreateObjectViewModel(value);
        if (_objectViewModel != null)
          _Value = _objectViewModel.ModeledObject;
      }
      return _objectViewModel;
    }
    set
    {
      if (_objectViewModel != value)
      {
        if (_objectViewModel != null)
          _objectViewModel.PropertyChanged -= ObjectViewModel_PropertyChanged;
        _objectViewModel = value as ObjectViewModel;
        if (_objectViewModel != null)
        {
          _objectViewModel.Owner = this;
          _objectViewModel.PropertyChanged += ObjectViewModel_PropertyChanged;
        }
        _Value = _objectViewModel?.ModeledObject;
        NotifyPropertyChanged(nameof(ObjectViewModel));
      }
    }
  }

  /// <summary>
  /// Field for the object view model.
  /// </summary>
  protected ObjectViewModel? _objectViewModel;

  /// <summary>
  /// Creates a new ObjectViewModel;
  /// </summary>
  /// <param name="value"></param>
  /// <returns></returns>
  protected ObjectViewModel? CreateObjectViewModel(object value)
  {
    var result = VM.ObjectViewModel.Create(this, value);
    if (result != null)
      result.PropertyChanged += ObjectViewModel_PropertyChanged;
    return result;
  }

  /// <summary>
  /// Creates a new ObjectViewModel;
  /// </summary>
  /// <param name="type"></param>
  /// <returns></returns>
  protected ObjectViewModel? CreateObjectViewModel(Type type)
  {
    var result = VM.ObjectViewModel.Create(this, type);
    if (result != null)
      result.PropertyChanged += ObjectViewModel_PropertyChanged;
    return result;
  }

  /// <summary>
  /// Passes the property change event to the view model.
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  protected virtual void ObjectViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
  {
    //Debug.WriteLine($"{this}.ObjectViewModel_PropertyChanged({sender}, {e.PropertyName})");
    if (sender is ObjectViewModel objectViewModel)
    {
      if (e.PropertyName == nameof(IObjectViewModel.IsEmpty))
      {
        var value = objectViewModel.IsEmpty ? null : objectViewModel.ModeledObject;
        if (Value != value)
        {
          Value = value;
          NotifyPropertyChanged(nameof(IsEmpty));
          objectViewModel.IsNew = false;
          NotifyPropertyChanged(nameof(IsNew));
        }
      }
    }
    NotifyPropertyChanged(nameof(Value));
  }


  #endregion IObjectViewModelProvider implementation

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
      NotifyPropertyChanged(nameof(IsEmpty));
      if (_isModified != value)
      {
        _isModified = value;
        NotifyPropertyChanged(nameof(IsModified));
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
  public bool IsModifiedInternal => (Owner as IEditable)?.IsModifiedInternal ?? true;

  #endregion IEditable implementation
}
