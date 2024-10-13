namespace Docx.Automation;

/// <summary>
/// Represents a collection of headers or footers.
/// </summary>
public interface HeadersFooters: IElementCollection<HeaderFooterReference>
{
  /// <summary>
  /// Indexer to get or set a header or footer by its type.
  /// </summary>
  /// <param name="type"></param>
  public HeaderFooterReference? this[HeaderFooterType type] { get; set; }
}