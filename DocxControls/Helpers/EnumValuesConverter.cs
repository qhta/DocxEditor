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
    if (value == null)
      return null;
    var result = value.AsString();
    return result;
  }

  /// <summary>
  /// converts a string to a property value
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

  /// <summary>
  /// Converts a property value to an enum value
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="value"></param>
  /// <returns></returns>
  public static T? Convert<T>(object? value) where T : struct, Enum
  {
    if (value == null)
      return null;
    var str = value.AsString();
    if (str == null) return null;
    if (Enum.TryParse<T>(str, true, out T result))
      return result;
    return null;
  }

  /// <summary>
  /// converts a string to a property value
  /// </summary>
  /// <param name="value"></param>
  /// <returns></returns>
  public static T? ConvertBack<T>(object? value) where T : struct
  {
    if (value == null)
      return null;
    var str = value.AsString();
    if (str == null) return null;
    return (T?)str.FromString(typeof(T));
  }
}
