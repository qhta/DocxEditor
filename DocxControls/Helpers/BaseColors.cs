using System.Windows.Media;

namespace DocxControls.Helpers;

/// <summary>
/// Base colors for highlighting.
/// </summary>
public static class BaseColors
{
  /// <summary>
  /// Gets the color from Maroon to Navy for the given index.
  /// </summary>
  /// <param name="ndx"></param>
  /// <returns></returns>
  public static Color GetDarkColor(int ndx)
  {
    ndx %= 6;
    return Colors[ndx + 9];
  }

  /// <summary>
  /// Array of base colors.
  /// </summary>
  public static readonly Color[] Colors = new Color[]
  {
    Color.FromRgb(0, 0, 0), // Black
    Color.FromRgb(255, 0, 0), // Red
    Color.FromRgb(0, 255, 0), // Green
    Color.FromRgb(0, 0, 255), // Blue
    Color.FromRgb(255, 255, 0), // Yellow
    Color.FromRgb(0, 255, 255), // Cyan
    Color.FromRgb(255, 0, 255), // Magenta
    Color.FromRgb(255, 255, 255), // White
    Color.FromRgb(128, 128, 128), // Gray
    Color.FromRgb(128, 0, 0), // Maroon
    Color.FromRgb(128, 128, 0), // Olive
    Color.FromRgb(0, 128, 0), // Green
    Color.FromRgb(128, 0, 128), // Purple
    Color.FromRgb(0, 128, 128), // Teal
    Color.FromRgb(0, 0, 128), // Navy
  };
}