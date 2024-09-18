namespace DocxControls.Automation;

/// <summary>
/// A collection of Bookmark objects that represent the bookmarks in the specified selection, range, or document.
/// </summary>
public interface Bookmarks: IElementCollection<Bookmark>
{
  ///// <summary>
  ///// Adds a new Bookmark to the Bookmarks collection.
  ///// </summary>
  ///// <param name="name">The name of the bookmark. The name cannot be more than 40 characters or include more than one word.</param>
  ///// <param name="range">The range of text marked by the bookmark. A bookmark can be set to a collapsed range.</param>
  //public void Add(string name, Range range);

  ///// <summary>
  ///// True if hidden bookmarks are included in the Bookmarks collection. The default value is False.
  ///// </summary>
  //public bool ShowHidden { get; set; }

  ///// <summary>
  ///// Returns or sets the sorting option for bookmarks in the collection.
  ///// </summary>
  //public BookmarkSortBy DefaultSorting { get; set; }

  ///// <summary>
  ///// Determines whether the specified bookmark exists.
  ///// </summary>
  ///// <param name="name"></param>
  ///// <returns></returns>
  //public bool Exists(string name);

}

