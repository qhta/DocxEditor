using System.ComponentModel;

using Qhta.MVVM;

namespace DocxControls;

/// <summary>
/// View model for a property of a document.
/// </summary>
public class PropertyViewModel : ViewModel, IToolTipProvider, IBooleanProvider, IEnumProvider, IObjectViewModelProvider, IPropertyProvider
{
  /// <summary>
  /// Display caption for the property.
  /// </summary>
  public virtual string? Caption =>
    PropertiesCaptions.ResourceManager.GetString(Name!, CultureInfo.CurrentUICulture) ?? Name;

  /// <summary>
  /// Name of the property to get/set.
  /// </summary>
  public virtual string? Name { get; set; }

  /// <summary>
  /// Type of the property.
  /// </summary>
  public virtual Type? Type { get; set; }

  #region INotifyPropertyChanged implementation
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
      }
    }
  }

  private object? _Value;


  /// <summary>
  /// Is the property obsolete?
  /// </summary>
  public bool IsObsolete => PropertiesDescriptions.ResourceManager
    .GetString(Name!, CultureInfo.InvariantCulture)?.Contains("Obsolete", StringComparison.InvariantCultureIgnoreCase) ?? false;

  /// <summary>
  /// Mask to use with <c>Exceed MaskedTextBox</c>.
  /// </summary>
  public virtual string? EditMask => GetEditMask(Type!);

  /// <summary>
  /// Get the edit mask for a type.
  /// </summary>
  /// <param name="type"></param>
  /// <returns></returns>
  public static string? GetEditMask(Type type)
  {
    if (type == typeof(DateTime))
      return "0000-00-00 90:00:00";
    if (type == typeof(TimeSpan))
      return "90:00:00";
    if (type == typeof(int))
      return "0000000000";
    if (type == typeof(decimal))
      return "0000000000.00";
    if (type == typeof(double))
      return "0000000000.00";
    if (type == typeof(float))
      return "0000000000.00";
    if (type == typeof(long))
      return "000000000000000000";
    if (type == typeof(short))
      return "00000";
    if (type == typeof(byte))
      return "000";
    if (type == typeof(bool))
      return "L";
    if (type == typeof(Guid))
      return "{00000000-0000-0000-0000-000000000000}";
    if (type == typeof(HexInt))
      return "HHHHHHHH";
    return null;
  }

  /// <summary>
  /// Watermark to display in the control.
  /// </summary>
  public string? Watermark=> GetWatermark(Type!);

  /// <summary>
  /// Get the watermark for a type.
  /// </summary>
  /// <param name="type"></param>
  /// <returns></returns>
  public static string? GetWatermark(Type type)
  {
    if (type == typeof(DateTime))
      return "yyyy-MM-dd hh:mm:ss";
    if (type == typeof(TimeSpan))
      return "hh:mm:ss";
    if (type == typeof(int))
      return "0";
    if (type == typeof(decimal))
      return "0.00";
    if (type == typeof(double))
      return "0.00";
    if (type == typeof(float))
      return "0.00";
    if (type == typeof(long))
      return "0";
    if (type == typeof(short))
      return "0";
    if (type == typeof(byte))
      return "0";
    if (type == typeof(bool))
      return "true/false";
    if (type == typeof(Guid))
      return "00000000-0000-0000-0000-000000000000";
    if (type == typeof(HexInt))
      return "HHHHHHHH";
    if (type?.BaseType==typeof(DXW.LongHexNumberType))
      return "HHHHHHHH";
    return null;
  }
  #endregion INotifyPropertyChanged implementation

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
  public virtual bool HasTooltip =>
    PropertiesTooltips.ResourceManager.GetString(Name!, CultureInfo.CurrentUICulture) != null;

  /// <summary>
  /// Tooltip for the property
  /// </summary>
  public virtual string? TooltipTitle =>
    PropertiesTooltips.ResourceManager.GetString(Name!, CultureInfo.CurrentUICulture);

  /// <summary>
  /// Description of the property
  /// </summary>
  public virtual string? TooltipDescription => FixDescription(PropertiesDescriptions.ResourceManager
    .GetString(Name!, CultureInfo.CurrentUICulture));

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
        var systemType = type.ToSystemType();
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
      var type = Type;
      if (type != null)
      {
        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
          return true;
      }
      return type != typeof(bool);
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
  public bool IsEnum => Type != null && (Type.IsEnum || Type.Name.EndsWith("Values"));

  /// <summary>
  /// Is the property type an enum treated as separate bits?
  /// </summary>
  public bool IsFlags => Type != null && Type.IsEnum && Type.GetCustomAttributes(typeof(FlagsAttribute), false).Any();

  /// <summary>
  /// Integer value of the property.
  /// </summary>
  public int? AsInteger
  {
    get => Value as int?;
    set => Value = value;
  }

  /// <summary>
  /// 
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
      //DXW.DocumentProtectionValues

      if (Type != null)
      {
        if (Type.Name.EndsWith("Values"))
        {
          var result = new List<EnumValueViewModel>();
          result.Add(new EnumValueViewModel());
          result.AddRange(
            Type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public)
              .Select(CreateEnumPropValueViewModel));
          return result;
        }
        if (Type.IsEnum)
        {
          if (IsFlags)
            return Enum.GetValues(Type).Cast<object>().Select(CreateEnumFlagValueViewModel);
          return Enum.GetValues(Type).Cast<object>().Select(CreateEnumValueViewModel);
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
    if (Type != null)
    {
      string? str = value as String;
      if (Type.IsEnum)
      {
        str = Enum.GetName(Type, value);
      }
      if (str != null)
        str = PropertiesCaptions.ResourceManager.GetString(str, CultureInfo.CurrentUICulture) ?? str;
      return str;
    }
    return value.ToString();
  }

  private string? GetEnumTooltip(object value)
  {
    if (Type != null)
    {
      string? str = value as String;
      if (Type.IsEnum)
      {
        str = Enum.GetName(Type, value);
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
  public bool IsObject => (Type != null) && (Type.IsClass && Type != typeof(string));

  /// <summary>
  /// Gets the value as an object view model.
  /// </summary>
  public IObjectViewModel ObjectViewModel
  {
    get
    {
      if (_objectProperties == null)
      {
        _objectProperties = new ObjectViewModel(Type!, Value);
        _objectProperties.PropertyChanged += PropertiesViewModel_PropertyChanged; 
        _Value = _objectProperties.ModeledObject;
      }
      return _objectProperties;
    }
  }

  private void PropertiesViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
  {
    NotifyPropertyChanged(nameof(Value));
  }

  private ObjectViewModel? _objectProperties;

  #endregion IObjectViewModelProvider implementation
}
