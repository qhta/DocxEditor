namespace Docx.Automation;

/// <summary>
/// Bookmark in a document.
/// </summary>
public interface Bookmark: IElement
{
  /// <summary>
  /// Integer identifier of the bookmark
  /// </summary>
  public int Id { get; }

  /// <summary>
  /// Name of the bookmark.
  /// </summary>
  public string? Name { get; }

  /// <summary>
  /// Determines if the bookmark is empty.
  /// </summary>
  public bool Empty { get; }

  /// <summary>
  /// Determines if the bookmark covers table columns.
  /// </summary>
  public bool Column { get; }

  /// <summary>
  /// First column of the bookmark range
  /// </summary>
  public int? ColumnFirst { get; set; }

  /// <summary>
  /// Last column of the bookmark range
  /// </summary>
  public int? ColumnLast { get; set; }

  /// <summary>
  /// Specifies the story type of bookmark.
  /// </summary>
  public StoryType StoryType { get; }

  /// <summary>
  /// Specifies the displacement of the bookmark by custom XML markup.
  /// </summary>
  public DisplacedByCustomXml DisplacedByCustomXml { get; }

  /// <summary>
  /// Reference to the element that starts the bookmark.
  /// </summary>
  public BookmarkStart BookmarkStart { get; }

  /// <summary>
  /// Reference to the element that ends the bookmark.
  /// </summary>
  public BookmarkEnd BookmarkEnd { get; }

}
