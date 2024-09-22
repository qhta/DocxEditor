namespace DocxControls.ViewModels;

/// <summary>
/// View model for a bookmark start element.
/// </summary>
public class BookmarkStart : ElementViewModel, DA.Bookmark, DA.BookmarkStart
{

  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="bookmarksViewModel"></param>
  /// <param name="bookmarkStartElement"></param>
  public BookmarkStart(Bookmarks bookmarksViewModel, DXW.BookmarkStart bookmarkStartElement) : base(bookmarksViewModel, bookmarkStartElement)
  {
    _bookmarksViewModel = bookmarksViewModel;
  }

  private readonly Bookmarks _bookmarksViewModel;



  /// <summary>
  /// <c>BookmarkStartElement</c> element
  /// </summary>
  public DXW.BookmarkStart BookmarkStartElement => (DXW.BookmarkStart)Element!;

  /// <summary>
  /// Corresponding <c>BookmarkEndElementElement</c> element
  /// </summary>
  public DXW.BookmarkEnd? BookmarkEndElementElement
  {
    get
    {
      if (_BookmarkEndElement == null)
        _BookmarkEndElement = BookmarkStartElement.GetBookmarkEnd();
      return _BookmarkEndElement;
    }
    set => _BookmarkEndElement = value;
  }
  private DXW.BookmarkEnd? _BookmarkEndElement;

  #region explicit Bookmark interface implementations
  DA.Bookmark DA.BookmarkStart.Bookmark => this;
  DA.BookmarkStart DA.Bookmark.BookmarkStart => this;
  DA.BookmarkEnd DA.BookmarkStart.BookmarkEnd => BookmarkEndViewModel!;
  DA.BookmarkEnd DA.Bookmark.BookmarkEnd => BookmarkEndViewModel!;
  #endregion
  /// <summary>
  /// Corresponding <c>BookmarkEndViewModel</c> element
  /// </summary>
  public BookmarkEnd? BookmarkEndViewModel => _bookmarksViewModel.GetBookmarkEnd(BookmarkStartElement?.Id?.Value);

  /// <summary>
  /// Integer identifier of the bookmark
  /// </summary>
  public int Id
  {
    get
    {
      if (Int32.TryParse(BookmarkStartElement?.Id?.Value, out var result))
        return result;
      return 0;
    }
  }

  /// <summary>
  /// Name of the bookmark
  /// </summary>
  public string? Name
  {
    get => BookmarkStartElement?.Name;
    set
    {
      if (BookmarkStartElement?.Name == value) return;
      if (BookmarkStartElement != null)
        BookmarkStartElement.Name = value;
      NotifyPropertyChanged(nameof(Name));
      NotifyPropertyChanged(nameof(ToolTip));
      BookmarkEndViewModel?.NotifyPropertyChanged(nameof(Name));
      BookmarkEndViewModel?.NotifyPropertyChanged(nameof(ToolTip));
    }
  }

  /// <summary>
  /// Determines if the bookmark is empty.
  /// </summary>
  public bool Empty => BookmarkStartElement?.NextSibling()==BookmarkEndElementElement;

  /// <summary>
  /// Determines if the bookmark covers table columns.
  /// </summary>
  public bool Column => BookmarkStartElement?.ColumnFirst != null && BookmarkStartElement?.ColumnLast != null;

  /// <summary>
  /// First column of the bookmark range
  /// </summary>
  public int? ColumnFirst
  {
    get => BookmarkStartElement?.ColumnFirst?.Value;
    set
    {
      if (BookmarkStartElement?.ColumnFirst?.Value == value) return;
      if (BookmarkStartElement != null)
        BookmarkStartElement.ColumnFirst = value;
      NotifyPropertyChanged(nameof(ColumnFirst));
    }
  }

  /// <summary>
  /// Last column of the bookmark range
  /// </summary>
  public int? ColumnLast
  {
    get => BookmarkStartElement?.ColumnLast?.Value;
    set
    {

      if (BookmarkStartElement?.ColumnLast?.Value == value) return;
      if (BookmarkStartElement != null)
        BookmarkStartElement.ColumnLast = value;
      NotifyPropertyChanged(nameof(ColumnLast));
    }
  }

  /// <summary>
  /// Specifies the story type of bookmark.
  /// </summary>
  public DA.StoryType StoryType
  {
    get
    {
      var parentStory = _bookmarksViewModel.ParentStory;
      if (parentStory == null) return 0;
      return parentStory.StoryType;
    }
  }


  /// <summary>
  /// Determines if the bookmark is displaced by custom XML and how
  /// </summary>
  public DA.DisplacedByCustomXml DisplacedByCustomXml
  {
    get
    {
      if (BookmarkStartElement?.DisplacedByCustomXml == null) return 0;
      var value = BookmarkStartElement.DisplacedByCustomXml.Value;
      if (value == DXW.DisplacedByCustomXmlValues.Next)
        return DA.DisplacedByCustomXml.Next;
      if (value == DXW.DisplacedByCustomXmlValues.Previous)
        return DA.DisplacedByCustomXml.Previous;
      return 0;
    }
    set
    {
      if (value == 0)
      {
        if (BookmarkStartElement?.DisplacedByCustomXml != null)
          BookmarkStartElement.DisplacedByCustomXml = null;
      }
      else if (value == DA.DisplacedByCustomXml.Next)
      {
        if (BookmarkStartElement.DisplacedByCustomXml?.Value != DXW.DisplacedByCustomXmlValues.Next)
          BookmarkStartElement.DisplacedByCustomXml = DXW.DisplacedByCustomXmlValues.Next;
      }
      else if (value == DA.DisplacedByCustomXml.Previous)
      {
        if (BookmarkStartElement.DisplacedByCustomXml?.Value != DXW.DisplacedByCustomXmlValues.Previous)
          BookmarkStartElement.DisplacedByCustomXml = DXW.DisplacedByCustomXmlValues.Previous;
      }
      NotifyPropertyChanged(nameof(DisplacedByCustomXml));
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
  /// Selects this and the corresponding <c>BookmarkEndViewModel</c> element
  /// </summary>
  public override bool IsSelected
  {
    get => base.IsSelected;
    set
    {
      if (value != base.IsSelected)
      {
        base.IsSelected = value;
        if (BookmarkEndViewModel != null)
          BookmarkEndViewModel.IsSelected = value;
      }
    }
  }

  /// <summary>
  /// Selects this and the corresponding <c>BookmarkEndViewModel</c> element
  /// </summary>
  public override bool IsHighlighted
  {
    get => base.IsHighlighted;
    set
    {
      if (value != base.IsHighlighted)
      {
        base.IsHighlighted = value;
        if (BookmarkEndViewModel != null)
          BookmarkEndViewModel.IsHighlighted = value;
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
    objectProperties.Add(new ObjectPropertyViewModel(this, nameof(ColumnFirst)));
    objectProperties.Add(new ObjectPropertyViewModel(this, nameof(ColumnLast)));
    objectProperties.Add(new ObjectPropertyViewModel(this, nameof(DisplacedByCustomXml)));
    return objectProperties;
  }
}
