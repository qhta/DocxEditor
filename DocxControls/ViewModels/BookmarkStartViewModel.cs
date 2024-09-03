using DocumentFormat.OpenXml.Wordprocessing;
using DocxControls.Helpers;

namespace DocxControls;

/// <summary>
/// View model for a paragraph run element
/// </summary>
public class BookmarkStartViewModel : ElementViewModel
{
  ///// <summary>
  ///// Default constructor. Creates a new <see cref="BookmarkStart"/>
  ///// </summary>
  //public BookmarkStartViewModel() : this(new DXW.BookmarkStart())
  //{
  //}

  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="bookmarksViewModel"></param>
  /// <param name="bookmarkStart"></param>
  public BookmarkStartViewModel(BookmarksViewModel bookmarksViewModel, DXW.BookmarkStart bookmarkStart) : base(bookmarkStart)
  {
    _bookmarksViewModel = bookmarksViewModel;
  }


  private readonly BookmarksViewModel _bookmarksViewModel;

  /// <summary>
  /// <c>BookmarkStart</c> element
  /// </summary>
  public DXW.BookmarkStart BookmarkStart => (DXW.BookmarkStart)Element;

  /// <summary>
  /// Corresponding <c>BookmarkEnd</c> element
  /// </summary>
  public DXW.BookmarkEnd? BookmarkEnd
  {
    get
    {
      if (_BookmarkEnd == null)
        _BookmarkEnd = BookmarkStart.GetBookmarkEnd();
      return _BookmarkEnd;
    }
    set => _BookmarkEnd = value;
  }
  private DXW.BookmarkEnd? _BookmarkEnd;

  /// <summary>
  /// Corresponding <c>BookmarkEndViewModel</c> element
  /// </summary>
  public BookmarkEndViewModel? BookmarkEndViewModel => _bookmarksViewModel.GetBookmarkEnd(BookmarkStart.Id?.Value);

  /// <summary>
  /// Integer identifier of the bookmark
  /// </summary>
  public int Id
  {
    get
    {
      if (Int32.TryParse(BookmarkStart.Id?.Value, out var result))
        return result;
      return 0;
    }
  }

  /// <summary>
  /// Name of the bookmark
  /// </summary>
  public string? Name
  {
    get => BookmarkStart.Name;
    set
    {
      if (BookmarkStart.Name == value) return;
      BookmarkStart.Name = value;
      NotifyPropertyChanged(nameof(Name));
      NotifyPropertyChanged(nameof(ToolTip));
    }
  }

  /// <summary>
  /// First column of the bookmark range
  /// </summary>
  public int? ColumnFirst
  {
    get => BookmarkStart.ColumnFirst?.Value;
    set
    {
      if (BookmarkStart.ColumnFirst?.Value == value) return;
      BookmarkStart.ColumnFirst = value;
      NotifyPropertyChanged(nameof(ColumnFirst));
    }
  }

  /// <summary>
  /// Last column of the bookmark range
  /// </summary>
  public int? ColumnLast
  {
    get => BookmarkStart.ColumnLast?.Value;
    set
    {
      if (BookmarkStart.ColumnLast?.Value == value) return;
      BookmarkStart.ColumnLast = value;
      NotifyPropertyChanged(nameof(ColumnLast));
    }
  }

  /// <summary>
  /// Determines if the bookmark is displaced by custom XML and how
  /// </summary>
  public DisplacedByCustomXmlValues? DisplacedByCustomXml
  {
    get => BookmarkStart.DisplacedByCustomXml?.Value;
    set
    {
      if (BookmarkStart.DisplacedByCustomXml?.Value == value) return;
      if (value == null)
        BookmarkStart.DisplacedByCustomXml = null;
      else
        BookmarkStart.DisplacedByCustomXml = value;
      NotifyPropertyChanged(nameof(DisplacedByCustomXml));
    }
  }


  /// <summary>
  /// Displayed tooltip with the name of the bookmark
  /// </summary>
  public string ToolTip => Strings.Bookmark_ + BookmarkStart.Name;

  /// <summary>
  /// Determines if the bookmark is selected
  /// </summary>
  public bool IsSelected
  {
    get => _IsSelected;
    set
    {
      if (_IsSelected != value)
      {
        _IsSelected = value;
        NotifyPropertyChanged(nameof(IsSelected));
        if (BookmarkEndViewModel != null) BookmarkEndViewModel.IsSelected = value;
      }
    }
  }
  private bool _IsSelected;
}
