namespace DocxControls.ViewModels;

/// <summary>
/// View model for bookmark end element.
/// </summary>
public class BookmarkEnd : ElementViewModel, DA.BookmarkEnd
{

  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="bookmarksViewModel"></param>
  /// <param name="bookmarkEnd"></param>
  public BookmarkEnd(Bookmarks bookmarksViewModel, DXW.BookmarkEnd bookmarkEnd): base (bookmarksViewModel,bookmarkEnd)
  {
    _bookmarksViewModel = bookmarksViewModel;
  }

  private readonly Bookmarks _bookmarksViewModel;

  /// <summary>
  /// <c>BookmarkEndElementElement</c> element
  /// </summary>
  public DXW.BookmarkEnd BookmarkEndElement => (DXW.BookmarkEnd)Element!;

  /// <summary>
  /// Corresponding <c>BookmarkStartElement</c> element
  /// </summary>
  public DXW.BookmarkStart? BookmarkStartElement
  {
    get
    {
      if (_BookmarkStart==null)
      {
        _BookmarkStart = BookmarkEndElement?.GetBookmarkStart();
      }
      return _BookmarkStart;
    }
    set => _BookmarkStart = value;
  }
  private DXW.BookmarkStart? _BookmarkStart;

  /// <summary>
  /// Corresponding <c>BookmarkStartViewModel</c> element
  /// </summary>
  public BookmarkStart? BookmarkStartViewModel => _bookmarksViewModel.GetBookmarkStart(BookmarkEndElement?.Id?.Value);

  /// <summary>
  /// Integer identifier of the bookmark
  /// </summary>
  public int Id
  {
    get
    {
      if (Int32.TryParse(BookmarkEndElement?.Id?.Value, out var result))
        return result;
      return 0;
    }
  }

  #region explicit BookmarkEndElementElement interface implementations
  DA.Bookmark DA.BookmarkEnd.Bookmark => BookmarkStartViewModel!;
  DA.BookmarkStart DA.BookmarkEnd.BookmarkStart => BookmarkStartViewModel!;
  #endregion

  /// <summary>
  /// Name of the bookmark
  /// </summary>
  public string? Name
  {
    get => BookmarkStartElement?.Name;
    set
    {
      if (BookmarkStartElement == null)
        return;
      BookmarkStartElement.Name = value;
      NotifyPropertyChanged(nameof(Name));
      NotifyPropertyChanged(nameof(ToolTip));
      BookmarkStartViewModel?.NotifyPropertyChanged(nameof(Name));
      BookmarkStartViewModel?.NotifyPropertyChanged(nameof(ToolTip));
    }
  }

  /// <summary>
  /// Displayed tooltip with the name of the bookmark
  /// </summary>
  public new string ToolTip => ModeledObject?.GetType().Name + ": " + BookmarkStartElement?.Name;

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
  protected override ObjectPropertiesViewModel InitObjectProperties()
  {
    var objectProperties = new ObjectPropertiesViewModel(this);
    objectProperties.Add(new ObjectPropertyViewModel(this, nameof(Id)));
    objectProperties.Add(new ObjectPropertyViewModel(this, nameof(Name)));
    return objectProperties;
  }
}
