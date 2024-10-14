namespace Docx.Automation;
/// <summary>
/// Interface for Color
/// </summary>
public interface WordColor : _Element
{
  /// <summary>
  /// Hexadecimal value of the color.
  /// </summary>
  public string? Value { get; set; }
  /// <summary>
  /// <para>Run Content Theme Color</para>
  /// </summary>
  public ThemeColor? ThemeColor { get; set; }
  /// <summary>
  /// <para>Run Content Theme Color Tint</para>
  /// </summary>
  public string? ThemeTint { get; set; }
  /// <summary>
  /// <para>Run Content Theme Color Shade</para>
  /// </summary>
  public string? ThemeShade { get; set; }
}