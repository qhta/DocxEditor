using System.Windows.Data;

namespace DocxControls;

/// <summary>
/// Converts a type name to a string for display and sort
/// </summary>
public class TypeNameConverter : IValueConverter
{

  /// <summary>
  /// Converts a type to a string for display
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
      if (type.IsGenericType)
      {
        var name = SimpleName(type.Name);
        var index = name.IndexOf('`');
        if (index > 0)
        {
          name = name.Substring(0, index);
        }
        var args = type.GetGenericArguments();
        if (args.Length == 1)
        {
          return SimpleName(args[0].Name);
        }
        var argNames = args.Select(a => SimpleName(a.Name));

        return $"{name}<{string.Join(", ", argNames)}>";
      }
      return SimpleName(type.Name);
    }
    return null;
  }

  private string SimpleName(string name)
  {
    var index = name.LastIndexOf('.');
    if (index > 0)
    {
      name = name.Substring(index);
    }
    return name;
  }
  /// <summary>
  /// Converts a string to a type
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
