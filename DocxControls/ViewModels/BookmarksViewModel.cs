using System.Collections.ObjectModel;

using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

using DocxControls.Helpers;
using Qhta.MVVM;

namespace DocxControls;

/// <summary>
/// View model for the document settings.
/// </summary>
public class BookmarksViewModel: ViewModel
{

  /// <summary>
  /// Internal Wordprocessing document
  /// </summary>
  public WordprocessingDocument WordDocument { get; init; }

  /// <summary>
  /// Items collection of bookmarks.
  /// </summary>
  public ObservableCollection<BookmarkStartViewModel> Items { get; } = new();

  /// <summary>
  /// Dictionary of bookmarks indexed by ID.
  /// </summary>
  public Dictionary<string, (BookmarkStartViewModel? start, BookmarkEndViewModel? end)> Bookmarks { get; } = new();

  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="wordDocument"></param>
  public BookmarksViewModel(WordprocessingDocument wordDocument)
  {
    WordDocument = wordDocument;
    var mainDocumentPart = wordDocument.MainDocumentPart;
    if (mainDocumentPart == null)
      return;

    Task.Run(() =>
    {
      var body = mainDocumentPart.Document.Body;
      if (body != null)
        GetBookmarks(body);
      foreach (var part in mainDocumentPart.HeaderParts)
        GetBookmarks(part.Header);
      foreach (var part in mainDocumentPart.FooterParts)
        GetBookmarks(part.Footer);
    });
  }

  private void GetBookmarks(DX.OpenXmlElement body)
  {
    var elements = body.Descendants();
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
  public BookmarkStartViewModel RegisterBookmarkStart(DXW.BookmarkStart bookmarkStart)
  {
    BookmarkStartViewModel? result = null;
    System.Windows.Application.Current.Dispatcher.Invoke(() =>
    {
      lock (Bookmarks)
      {
        var id = bookmarkStart.Id?.Value ?? "";
        if (!Bookmarks.TryGetValue(id, out var value))
        {
          result = new BookmarkStartViewModel(this, bookmarkStart);
          value = (result, null);
          Bookmarks.Add(id, value);
          Items.Add(value.start);
        }
        else
        {
          if (value.start == null)
          {
            result = new BookmarkStartViewModel(this, bookmarkStart);
            value.start = result;
            Bookmarks[id] = value;
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
  public BookmarkEndViewModel RegisterBookmarkEnd(DXW.BookmarkEnd bookmarkEnd)
  {
    BookmarkEndViewModel? result = null;
    System.Windows.Application.Current.Dispatcher.Invoke(() =>
    {
      lock (Bookmarks)
      {
        var id = bookmarkEnd.Id?.Value ?? "";
        if (!Bookmarks.TryGetValue(id, out var value))
        {
          result = new BookmarkEndViewModel(this, bookmarkEnd);
          value = (null, result);
          Bookmarks.Add(id, value);
        }
        else
        {
          if (value.end == null)
          {
            result = new BookmarkEndViewModel(this, bookmarkEnd);
            value.end = result;
            Bookmarks[id] = value;
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
  public BookmarkStartViewModel? GetBookmarkStart(string? id)
  {
    if (id == null)
      return null;
    lock (Bookmarks)
    {
      if (Bookmarks.TryGetValue(id, out var value))
        return value.start;
      return null;
    }
  }

  /// <summary>
  /// Get the bookmark end view model by ID.
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  public BookmarkEndViewModel? GetBookmarkEnd(string? id)
  {
    if (id == null)
      return null;
    lock (Bookmarks)
    {
      if (Bookmarks.TryGetValue(id, out var value))
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
  private EnumValuesHelper DisplacedByCustomXmlHelper { get; } = new EnumValuesHelper(typeof(BookmarkStart).GetProperty("DisplacedByCustomXml")!);

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

}
