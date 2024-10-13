namespace Docx.Automation;

/// <summary>
/// Represents a header or footer reference.
/// </summary>
public interface HeaderFooterReference: IElementReference<string, HeaderFooter>
{
  /// <summary>
  /// Type of header or footer.
  /// </summary>
  public HeaderFooterType? Type { get; set; }
}