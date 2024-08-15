namespace DocxControls;

/// <summary>
/// View model for a paragraph run element
/// </summary>
public class BookmarkStartViewModel : ElementViewModel
{
  /// <summary>
  /// Default constructor. Creates a new <see cref="BookmarkStart"/>
  /// </summary>
  public BookmarkStartViewModel(): this(new DXW.BookmarkStart())
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="BookmarkStartViewModel"/> class.
  /// </summary>
  /// <param name="bookmarkStart"></param>
  public BookmarkStartViewModel(DXW.BookmarkStart bookmarkStart): base (bookmarkStart)
  {
  }

  /// <summary>
  /// BookmarkStart element
  /// </summary>
  public DXW.BookmarkStart BookmarkStart => (DXW.BookmarkStart)Element;
}
