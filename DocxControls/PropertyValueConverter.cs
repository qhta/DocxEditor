using System.Globalization;
using System.Windows.Data;
using Qhta.OpenXmlTools;

namespace DocxControls;
public class PropertyValueConverter : IValueConverter
{
  public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
  {
    if (value is DateTime dateTime)
    {
      return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
    }
    return value?.AsString();
  }

  public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
  {
    if (targetType==typeof(object) && parameter is Type type)
    {
      targetType = type;
    }
    if (value is string str)
    {
      if (targetType == typeof(DateTime))
      {
        if (DateTime.TryParse(str, out var dateTime))
          return dateTime;
      }
      else
      {
        return str.FromString(targetType);
      }
    }
    return value;
  }

}
