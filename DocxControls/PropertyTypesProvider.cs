namespace DocxControls;

/// <summary>
/// Provides a list of property types
/// </summary>
public class PropertyTypesProvider
{
  private static readonly List<Type> _items =
  [
    typeof(string),
    typeof(bool),
    typeof(int),
    typeof(double),
    typeof(DateTime),
  ];

  /// <summary>
  /// List of available property types
  /// </summary>
  public static List<Type> Items => _items;
}

