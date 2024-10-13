namespace Docx.Automation;

/// <summary>
/// Represents the margins of a page.
/// </summary>
public interface PageMargin
{
  /// <summary>
  /// <para>Top Margin Spacing</para>
  /// </summary>
  public Length? Top { get; set; }
  /// <summary>
  /// <para>Right Margin Spacing</para>
  /// </summary>
  public Length? Right { get; set; }
  /// <summary>
  /// <para>Page Bottom Spacing</para>
  /// </summary>
  public Length? Bottom { get; set; }
  /// <summary>
  /// <para>Left Margin Spacing</para>
  /// </summary>
  public Length? Left { get; set; }
  /// <summary>
  /// <para>Spacing to Top of Header</para>
  /// </summary>
  public Length? Header { get; set; }
  /// <summary>
  /// <para>Spacing to Bottom of Footer</para>
  /// </summary>
  public Length? Footer { get; set; }
  /// <summary>
  /// <para>Page Gutter Spacing</para>
  /// </summary>
  public Length? Gutter { get; set; }
}