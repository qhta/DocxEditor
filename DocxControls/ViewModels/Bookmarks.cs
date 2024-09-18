using DocxControls.Helpers;

namespace DocxControls;

/// <summary>
/// View model for the document settings.
/// </summary>
public class Bookmarks : ElementCollection<BookmarkStart>, DA.Bookmarks
{

  /// <summary>
  /// The parent story of bookmarks collection.
  /// </summary>
  public DA.IStory? ParentStory { get; set; }

  /// <summary>
  /// Dictionary of bookmarks indexed by ID.
  /// </summary>
  public Dictionary<string, (BookmarkStart? start, BookmarkEnd? end)> BookmarkIds { get; } = new();

  /// <summary>
  /// Initializing constructor.
  /// </summary>
  public Bookmarks(BlockElementViewModel parent) : base(parent)
  {
    if (parent.Element != null)
      Task.Run(() =>
      {
        GetBookmarks(parent.Element);
      });
  }

  /// <summary>
  /// Scan the descendants of the block element for bookmarks start and end elements.
  /// </summary>
  /// <param name="blockElement"></param>
  private void GetBookmarks(DX.OpenXmlElement blockElement)
  {
    var elements = blockElement.Descendants();
    foreach (var element in elements)
    {
      if (element is DXW.BookmarkStart bookmarkStart)
      {
        RegisterBookmarkStart(bookmarkStart);
      }
      else if (element is DXW.BookmarkEnd bookmarkEnd)
      {
        RegisterBookmarkEnd(bookmarkEnd);
      }
    }
  }

  /// <summary>
  /// Register a bookmark start element.
  /// Creates a new view model if it does not already exist.
  /// Get the bookmark start view model if it already exists.
  /// </summary>
  /// <param name="bookmarkStart"></param>
  public BookmarkStart RegisterBookmarkStart(DXW.BookmarkStart bookmarkStart)
  {
    BookmarkStart? result = null;
    System.Windows.Application.Current.Dispatcher.Invoke(() =>
    {
      lock (BookmarkIds)
      {
        var id = bookmarkStart.Id?.Value ?? "";
        if (!BookmarkIds.TryGetValue(id, out var value))
        {
          result = new BookmarkStart(this, bookmarkStart);
          value = (result, null);
          BookmarkIds.Add(id, value);
          Items.Add(value.start);
        }
        else
        {
          if (value.start == null)
          {
            result = new BookmarkStart(this, bookmarkStart);
            value.start = result;
            BookmarkIds[id] = value;
            Items.Add(value.start);
          }
          else
            result = value.start;
        }
      }
    });
    return result!;
  }

  /// <summary>
  /// Register a bookmark end element.
  /// Creates a new view model if it does not already exist.
  /// Get the bookmark end view model if it already exists.
  /// </summary>
  /// <param name="bookmarkEnd"></param>
  public BookmarkEnd RegisterBookmarkEnd(DXW.BookmarkEnd bookmarkEnd)
  {
    BookmarkEnd? result = null;
    System.Windows.Application.Current.Dispatcher.Invoke(() =>
    {
      lock (BookmarkIds)
      {
        var id = bookmarkEnd.Id?.Value ?? "";
        if (!BookmarkIds.TryGetValue(id, out var value))
        {
          result = new BookmarkEnd(this, bookmarkEnd);
          value = (null, result);
          BookmarkIds.Add(id, value);
        }
        else
        {
          if (value.end == null)
          {
            result = new BookmarkEnd(this, bookmarkEnd);
            value.end = result;
            BookmarkIds[id] = value;
          }
          else
            result = value.end;
        }
      }
    });
    return result!;
  }

  /// <summary>
  /// Get the bookmark start view model by ID.
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  public BookmarkStart? GetBookmarkStart(string? id)
  {
    if (id == null)
      return null;
    lock (BookmarkIds)
    {
      if (BookmarkIds.TryGetValue(id, out var value))
        return value.start;
      return null;
    }
  }

  /// <summary>
  /// Get the bookmark end view model by ID.
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  public BookmarkEnd? GetBookmarkEnd(string? id)
  {
    if (id == null)
      return null;
    lock (BookmarkIds)
    {
      if (BookmarkIds.TryGetValue(id, out var value))
        return value.end;
      return null;
    }
  }
  //private void BookmarkViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
  //{

  //}

  /// <summary>
  /// Helper for DataGridComboBoxColumn
  /// </summary>
  private EnumValuesHelper DisplacedByCustomXmlHelper { get; } = new EnumValuesHelper(typeof(DXW.BookmarkStart).GetProperty("DisplacedByCustomXml")!);

  /// <summary>
  /// Enum values for DisplacedByCustomXml property.
  /// </summary>
  public IEnumerable<Object> DisplacedByCustomXmlValues => DisplacedByCustomXmlHelper.EnumValues;


  /// <summary>
  /// Notifies 
  /// </summary>
  /// <param name="id"></param>
  /// <param name="propertyName"></param>
  public void NotifyBookmarkEndPropertyChanged(int id, String propertyName)
  {

  }

  // ReSharper disable once NotDisposedResourceIsReturned
  IEnumerator<DA.Bookmark> IEnumerable<DA.Bookmark>.GetEnumerator() => Items.Cast<DA.Bookmark>().GetEnumerator();

}
