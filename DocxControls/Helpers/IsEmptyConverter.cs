using System.Windows.Data;
using System.Windows.Media;

namespace DocxControls;

/// <summary>
/// Converter for boolean to <c>SolidBrush</c>. True means <c>Gray</c>, false means <c>Black</c>.
/// </summary>
public class IsEmptyConverter: IValueConverter
{
  /// <summary>
  /// Converter for boolean to <c>SolidBrush</c>. True means <c>Gray</c>, false means <c>Black</c>.
  /// </summary>
  /// <param name="value"></param>
  /// <param name="targetType"></param>
  /// <param name="parameter"></param>
  /// <param name="culture"></param>
  /// <returns></returns>
  public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
  {
    if ((bool?)value == true)
      return new SolidColorBrush(Colors.Gray);
    else
      return new SolidColorBrush(Colors.Black);
  }

  /// <summary>
  /// Unused.
  /// </summary>
  /// <param name="value"></param>
  /// <param name="targetType"></param>
  /// <param name="parameter"></param>
  /// <param name="culture"></param>
  /// <returns></returns>
  /// <exception cref="NotImplementedException"></exception>
  public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
  {
    throw new NotImplementedException();
  }
}