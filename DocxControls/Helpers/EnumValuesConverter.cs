using System.Windows.Data;

using Qhta.TypeUtils;

namespace DocxControls.Helpers;

/// <summary>
/// Converts a property type to a type name
/// </summary>
public class EnumValuesConverter : IValueConverter
{

  /// <summary>
  /// Converts a property value to a string for display and edit
  /// </summary>
  /// <param name="value"></param>
  /// <param name="targetType"></param>
  /// <param name="parameter"></param>
  /// <param name="culture"></param>
  /// <returns></returns>
  public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
  {
    if (value== null)
      return null;
    //var sourceType = value.GetType();
    //if (sourceType.IsOpenXmlEnum())
    //{
    //  var props = sourceType.GetOpenXmlProperties();
    //  var prop = props.FirstOrDefault(p => p.GetValue(null)?.Equals(value)==true);
    //  return prop?.Name;
    //}
    var result = value?.AsString();
    return result;
  }

  /// <summary>
  /// Not implemented
  /// </summary>
  /// <param name="value"></param>
  /// <param name="targetType"></param>
  /// <param name="parameter"></param>
  /// <param name="culture"></param>
  /// <returns></returns>
  public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
  {
    if (value == null)
      return null;
    if (targetType.IsNullable(out var baseType))
      targetType = baseType;
    if (value is string str)
    {
      if (str == string.Empty)
        return null;

      return str.FromString(targetType);
    }
    return value;
  }

}
