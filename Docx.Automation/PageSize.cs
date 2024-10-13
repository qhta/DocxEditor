namespace Docx.Automation;

/// <summary>
/// Specifies the size of a page.
/// </summary>
public interface PageSize
{
  /// <summary>
  /// <para>Page Width</para>
  /// </summary>
  public Length? Width { get; set; }

  /// <summary>
  /// <para>Page Height</para>
  /// </summary>
  public Length? Height { get; set; }

  /// <summary>
  /// <para>Page Orientation</para>
  /// </summary>
  public PageOrientation? Orient { get; set; }

  /// <summary>
  /// <para>Printer Paper Code</para>
  /// </summary>
  public PaperSize? PaperSize { get; set; }
}