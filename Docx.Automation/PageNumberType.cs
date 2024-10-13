namespace Docx.Automation;

/// <summary>
/// Specifies the type of page numbering.
/// </summary>
public interface PageNumberType
{
  /// <summary>
  /// Specifies the number format that shall be used for all page numbering in this section.>
  /// </summary>
  public NumberFormat? Format { get; set; }
  /// <summary>
  /// Specifies the page number that appears on the first page of the section.
  /// </summary>
  public int? Start { get; set; }
  /// <summary>
  /// Specifies the one-based index of the heading style applied to chapter titles in the document
  /// which shall be used as chapter headings in all page numbers for this section,
  /// by locating the nearest heading of that style and extracting the numbering information.
  /// </summary>
  public int? ChapterStyle { get; set; }

  /// <summary>
  /// Specifies the separator character that shall appear between the chapter and page number,
  /// if a chapter style has been set for page numbers in this section.
  /// </summary>
  public ChapterSeparator? ChapterSeparator { get; set; }
}