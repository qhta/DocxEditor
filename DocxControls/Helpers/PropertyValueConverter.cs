using System.Windows.Data;

namespace DocxControls.Helpers;

/// <summary>
/// Converts a property value to a string for display and edit
/// </summary>
public class PropertyValueConverter : IValueConverter
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
    return value?.AsString();
  }

  /// <summary>
  /// Converts a string to a property value
  /// </summary>
  /// <param name="value"></param>
  /// <param name="targetType"></param>
  /// <param name="parameter"></param>
  /// <param name="culture"></param>
  /// <returns></returns>
  public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
  {
    if (targetType==typeof(object) && parameter is Type type)
    {
      targetType = type;
    }
    if (value is string str)
    {
      if (str==string.Empty)
        return null;
      return str.FromString(targetType);
    }
    return value;
  }

}
