using Qhta.MVVM;

namespace DocxControls.Helpers;

/// <summary>
/// Helper class for enum values.
/// </summary>
public class EnumValuesHelper: ViewModel, IEnumProvider
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="property"></param>
  public EnumValuesHelper(PropertyInfo property)
  {
    PropertyInfo = property;
    var aType = property.PropertyType;
    if (aType.IsGenericType && aType.GetGenericTypeDefinition() == typeof(DX.EnumValue<>))
    {
      EnumValueType = aType;
      aType = aType.GetGenericArguments()[0];
      ValProperty = aType.GetProperty("Val");
    }
    ValueType = aType;
  }

  private PropertyInfo PropertyInfo { get; }
  private PropertyInfo? ValProperty { get; }
  private Type? EnumValueType { get; }
  private Type ValueType { get; }

  //private object? Value
  //{
  //  get
  //  {
  //    var value = PropertyInfo.GetValue(BaseObject);
  //    if (ValProperty != null)
  //      value = ValProperty.GetValue(value);
  //    return value;
  //  }
  //  set
  //  {
  //    if (ValProperty != null)
  //    {
  //      var val = value;
  //      value = Activator.CreateInstance(EnumValueType!, val);
  //      ValProperty.SetValue(value, value);
  //    }
  //    PropertyInfo.SetValue(BaseObject, value);
  //  }
  //}

  /// <summary>
  /// Selected enum value of the property.
  /// </summary>
  public object? SelectedEnum
  {
    get;
    set;
    //get; set;=> Value;
    //set
    //{
    //  if (Value != value)
    //  {
    //    Value = value;
    //    NotifyPropertyChanged(nameof(SelectedEnum));
    //  }
    //}
  }

  /// <summary>
  /// Is the property type an enum?
  /// </summary>
  public bool IsEnum => (ValueType.IsEnum || ValueType.IsOpenXmlEnum());

  /// <summary>
  /// Is the property type an enum treated as separate bits?
  /// </summary>
  public bool IsFlags => ValueType.IsEnum && ValueType.GetCustomAttributes(typeof(FlagsAttribute), false).Any();

  /// <summary>
  /// Enum values of the property.
  /// </summary>
  public IEnumerable<object> EnumValues
  {
    get
    {
      if (ValueType.IsOpenXmlEnum())
      {
        var result = new List<EnumValueViewModel>();
        result.Add(new EnumValueViewModel());
        result.AddRange(
          ValueType.GetOpenXmlProperties()
            .Select(CreateEnumPropValueViewModel));
        return result;
      }
      if (ValueType.IsEnum)
      {
        if (IsFlags)
          return Enum.GetValues(ValueType).Cast<object>().Select(CreateEnumFlagValueViewModel);
        return Enum.GetValues(ValueType).Cast<object>().Select(CreateEnumValueViewModel);
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
    var result = new EnumFlagValueViewModel(this, ValueType!, value)
    {
      Value = value,
      Caption = GetEnumCaption(value),
      Tooltip = GetEnumTooltip(value)
    };
    return result;
  }

  private string? GetEnumCaption(object value)
  {
    string? str = value as String;
    if (ValueType.IsEnum)
    {
      str = Enum.GetName(ValueType, value);
    }
    if (str != null)
      str = PropertiesCaptions.ResourceManager.GetString(str, CultureInfo.CurrentUICulture) ?? str;
    return str;
  }

  private string? GetEnumTooltip(object value)
  {
    string? str = value as String;
    if (ValueType.IsEnum)
    {
      str = Enum.GetName(ValueType, value);
    }
    if (str != null)
      str = PropertiesTooltips.ResourceManager.GetString(str, CultureInfo.CurrentUICulture) ?? str;
    return str;
  }

}
