using System.Windows;
using System.Windows.Data;

namespace DocxControls;

/// <summary>
/// Converter for boolean to <c>FontStyle</c>. True means <c>FontStyles.Italic</c>, false means <c>FontStyles.Normal</c>.
/// </summary>
public class FontStyleConverter: IValueConverter
{
  /// <summary>
  /// Converts a boolean to <c>FontStyle</c>. True means <c>FontStyles.Italic</c>, false means <c>FontStyles.Normal</c>.
  /// </summary>
  /// <param name="value"></param>
  /// <param name="targetType"></param>
  /// <param name="parameter"></param>
  /// <param name="culture"></param>
  /// <returns></returns>
  public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
  {
    if ((bool?)value == true)
      return FontStyles.Italic;
    else
      return FontStyles.Normal;
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