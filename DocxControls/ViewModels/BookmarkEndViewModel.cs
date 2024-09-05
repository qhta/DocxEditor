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
      {
        _BookmarkStart = BookmarkEnd.GetBookmarkStart();
      }
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
      BookmarkStartViewModel?.NotifyPropertyChanged(nameof(Name));
      BookmarkStartViewModel?.NotifyPropertyChanged(nameof(ToolTip));
    }
  }

  /// <summary>
  /// Displayed tooltip with the name of the bookmark
  /// </summary>
  public new string ToolTip => ModeledObject?.GetType().Name + ": " + BookmarkStart?.Name;

  /// <summary>
  /// Allows other classes to notify about property changes
  /// </summary>
  /// <param name="propertyName"></param>
  public new void NotifyPropertyChanged(string propertyName)
  {
    base.NotifyPropertyChanged(propertyName);
  }

  /// <summary>
  /// Selects this and the corresponding <c>BookmarkStartViewModel</c> element
  /// </summary>
  public override bool IsSelected
  {
    get => base.IsSelected;
    set
    {
      if (value != base.IsSelected)
      {
        base.IsSelected = value;
        if (BookmarkStartViewModel != null)
          BookmarkStartViewModel.IsSelected = value;
      }
    }
  }

  /// <summary>
  /// Selects this and the corresponding <c>BookmarkStartViewModel</c> element
  /// </summary>
  public override bool IsHighlighted
  {
    get => base.IsHighlighted;
    set
    {
      if (value != base.IsHighlighted)
      {
        base.IsHighlighted = value;
        if (BookmarkStartViewModel != null)
          BookmarkStartViewModel.IsHighlighted = value;
      }
    }
  }

  /// <summary>
  /// Initializes the object properties
  /// </summary>
  protected override void InitObjectProperties()
  {
    ObjectProperties.Add(new ObjectPropertyViewModel(this, nameof(Id)));
    ObjectProperties.Add(new ObjectPropertyViewModel(this, nameof(Name)));
  }
}
