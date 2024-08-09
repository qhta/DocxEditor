using System.Globalization;
using System.Windows.Data;

namespace DocxControls;
public class DateTimeConverter: IValueConverter
{
  public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
  {
    if (value is DateTime dateTime)
    {
      return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
    }
    return value?.ToString();
  }

  public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
  {
   if (targetType == typeof(DateTime) && value is string str)
    {
      if (DateTime.TryParse(str, out var dateTime))
      {
        return dateTime;
      }
    }
    return value;
  }
}
