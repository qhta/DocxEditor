using Qhta.MVVM;

namespace DocxControls;

/// <summary>
/// View model for a property of a document.
/// </summary>
public class PropertyViewModel : ViewModel, IToolTipProvider, IBooleanProvider, IEnumProvider
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

  /// <summary>
  /// Is the property value read-only?
  /// </summary>
  public bool IsReadOnly { get; init; }

  /// <summary>
  /// Value of the property.
  /// </summary>
  public object? Value
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

  #region IToolTipProvider implementation

  /// <summary>
  /// Tooltip for the property
  /// </summary>
  public virtual string? TooltipTitle =>
    PropertiesTooltips.ResourceManager.GetString(Name!, CultureInfo.CurrentUICulture);

  /// <summary>
  /// Description of the property
  /// </summary>
  public virtual string? TooltipDescription => PropertiesDescriptions.ResourceManager
    .GetString(Name!, CultureInfo.CurrentUICulture)?.Replace("<p/>", "\n");

  #endregion IToolTipProvider implementation

  #region IBooleanProvider implementation

  /// <summary>
  /// Is the property a boolean?
  /// </summary>
  public bool IsBoolean => NotNullableType == typeof(bool) || NotNullableType == typeof(DXO10W.OnOffValues);


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
  public bool IsEnum => Type?.IsEnum ?? false;

  /// <summary>
  /// Is the property type an enum treated as separate bits?
  /// </summary>
  public bool IsFlags => false;

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
    set => Value = value;
  }

  /// <summary>
  /// Enum values of the property.
  /// </summary>
  public IEnumerable<object> EnumValues
  {
    get
    {
      if (Type != null)
      {
        if (Type.IsEnum)
          return Enum.GetValues(Type).Cast<object>().Select(CreateEnumValueViewModel);
      }
      return [];
    }
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

  private string? GetEnumCaption(object value)
  {
    if (Type != null)
    {
      if (Type.IsEnum)
      {
        var str = Enum.GetName(Type, value);
        if (str != null)
          str = PropertiesCaptions.ResourceManager.GetString(str, CultureInfo.CurrentUICulture) ?? str;
        return str;
      }
    }
    return value.ToString();
  }

  private string? GetEnumTooltip(object value)
  {
    if (Type != null)
    {
      if (Type.IsEnum)
      {
        var str = Enum.GetName(Type, value);
        if (str != null)
          str = PropertiesTooltips.ResourceManager.GetString(str, CultureInfo.CurrentUICulture) ?? str;
        return str;
      }
    }
    return value.ToString();
  }
  #endregion IEnumProvider implementation
}
