using System.Windows.Data;

namespace DocxControls.Helpers;

/// <summary>
/// Decrements item control width by parameter value
/// </summary>
public class ItemWidthConverter : IValueConverter
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
    if (value is double width)
    {
      if (parameter is not double decrement)
      {
        if (!double.TryParse(parameter?.ToString(), out decrement))
          decrement = 0;
      }
      var result = width - decrement;
      if (result < 10)
        result = 10;
      //Debug.WriteLine($"ItemWidthConverter({value}, {parameter}) = {result}");
      return result;
    }
    return value;
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
    throw new NotImplementedException();
  }

}
