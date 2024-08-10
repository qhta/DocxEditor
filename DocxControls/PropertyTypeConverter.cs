using System.Globalization;
using System.Windows.Data;
using Qhta.OpenXmlTools;

namespace DocxControls;

/// <summary>
/// Converts a property type to a type name
/// </summary>
public class PropertyTypeConverter : IValueConverter
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
    if (value is Type type)
    {
      return type.Name;
    }
    return value?.AsString();
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
    throw new NotImplementedException();
  }

}
