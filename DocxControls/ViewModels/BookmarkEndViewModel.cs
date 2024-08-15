namespace DocxControls;

/// <summary>
/// View model for a paragraph run element
/// </summary>
public class BookmarkEndViewModel : ElementViewModel
{
  /// <summary>
  /// Default constructor. Creates a new <see cref="BookmarkEnd"/>
  /// </summary>
  public BookmarkEndViewModel(): this(new DXW.BookmarkEnd())
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="BookmarkEndViewModel"/> class.
  /// </summary>
  /// <param name="bookmarkEnd"></param>
  public BookmarkEndViewModel(DXW.BookmarkEnd bookmarkEnd): base (bookmarkEnd)
  {
  }

  /// <summary>
  /// BookmarkEnd element
  /// </summary>
  public DXW.BookmarkEnd BookmarkEnd => (DXW.BookmarkEnd)Element;
}
