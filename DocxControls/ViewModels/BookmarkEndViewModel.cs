using DocumentFormat.OpenXml.Wordprocessing;

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
  /// <c>BookmarkEnd</c> element
  /// </summary>
  public DXW.BookmarkEnd BookmarkEnd => (DXW.BookmarkEnd)Element;

  /// <summary>
  /// Corresponding <c>BookmarkStart</c> element
  /// </summary>
  public DXW.BookmarkStart? BookmarkStart => BookmarkEnd.GetBookmarkStart();

  /// <summary>
  /// Name of the bookmark
  /// </summary>
  public string? BookmarkName
  {
    get => BookmarkStart?.Name;
    set
    {
      if (BookmarkStart == null)
        return;
      BookmarkStart.Name = value;
      NotifyPropertyChanged(nameof(BookmarkName));
      NotifyPropertyChanged(nameof(ToolTip));
    }
  }

  /// <summary>
  /// Displayed tooltip with the name of the bookmark
  /// </summary>
  public string ToolTip => Strings.Bookmark_ + BookmarkStart?.Name;
}
