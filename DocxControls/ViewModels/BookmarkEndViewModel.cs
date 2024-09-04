namespace DocxControls;

/// <summary>
/// View model for a paragraph run element
/// </summary>
public class BookmarkEndViewModel : ElementViewModel
{
  ///// <summary>
  ///// Default constructor. Creates a new <see cref="BookmarkEnd"/>
  ///// </summary>
  //public BookmarkEndViewModel(): this(new DXW.BookmarkEnd())
  //{
  //}

  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="bookmarksViewModel"></param>
  /// <param name="bookmarkEnd"></param>
  public BookmarkEndViewModel(BookmarksViewModel bookmarksViewModel, DXW.BookmarkEnd bookmarkEnd): base (bookmarkEnd)
  {
    _bookmarksViewModel = bookmarksViewModel;
  }

  private readonly BookmarksViewModel _bookmarksViewModel;

  /// <summary>
  /// <c>BookmarkEnd</c> element
  /// </summary>
  public DXW.BookmarkEnd BookmarkEnd => (DXW.BookmarkEnd)Element;

  /// <summary>
  /// Corresponding <c>BookmarkStart</c> element
  /// </summary>
  public DXW.BookmarkStart? BookmarkStart
  {
    get
    {
      if (_BookmarkStart==null)
        _BookmarkStart = BookmarkEnd.GetBookmarkStart();
      return _BookmarkStart;
    }
    set => _BookmarkStart = value;
  }
  private DXW.BookmarkStart? _BookmarkStart;

  /// <summary>
  /// Corresponding <c>BookmarkStartViewModel</c> element
  /// </summary>
  public BookmarkStartViewModel? BookmarkStartViewModel => _bookmarksViewModel.GetBookmarkStart(BookmarkEnd.Id?.Value);

  /// <summary>
  /// Integer identifier of the bookmark
  /// </summary>
  public int Id
  {
    get
    {
      if (Int32.TryParse(BookmarkEnd.Id?.Value, out var result))
        return result;
      return 0;
    }
  }

  /// <summary>
  /// Name of the bookmark
  /// </summary>
  public string? Name
  {
    get => BookmarkStart?.Name;
    set
    {
      if (BookmarkStart == null)
        return;
      BookmarkStart.Name = value;
      NotifyPropertyChanged(nameof(Name));
      NotifyPropertyChanged(nameof(ToolTip));
    }
  }

  /// <summary>
  /// Displayed tooltip with the name of the bookmark
  /// </summary>
  public string ToolTip => Strings.Bookmark_ + BookmarkStart?.Name;
    
}
