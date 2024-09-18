namespace Docx.Automation;

/// <summary>
/// Element that marks the end of a bookmark.
/// </summary>
public interface BookmarkEnd: IElement
{
  /// <summary>
  /// Integer identifier of the bookmark
  /// </summary>
  public int Id { get; }

  /// <summary>
  /// Reference to the bookmark definition.
  /// </summary>
  public Bookmark Bookmark { get; }

  /// <summary>
  /// Reference to the element that starts the bookmark.
  /// </summary>
  public BookmarkStart BookmarkStart { get; }


}
