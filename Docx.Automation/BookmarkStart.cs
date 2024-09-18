namespace Docx.Automation;

/// <summary>
/// Element that marks a start of a bookmark.
/// </summary>
public interface BookmarkStart: IElement
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
  /// Reference to the element that ends the bookmark.
  /// </summary>
  public BookmarkEnd BookmarkEnd { get; }

}
