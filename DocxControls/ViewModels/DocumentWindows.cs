using System.Windows.Input;

using DocxControls.Views;

namespace DocxControls.ViewModels;

/// <summary>
/// Collection of documents.
/// </summary>
public class DocumentWindows: ElementCollection<DocumentWindow>, DA.DocumentWindows
{
  /// <summary>
  /// Default constructor.
  /// </summary>
  public DocumentWindows(): base(null)
  {
  }

  /// <summary>
  /// Initializes a new instance of the Documents class.
  /// </summary>
  /// <param name="parent"></param>
  public DocumentWindows
    (object? parent) : base(parent)
  {

  }

  // ReSharper disable once NotDisposedResourceIsReturned
  IEnumerator<DA.DocumentWindow> IEnumerable<DA.DocumentWindow>.GetEnumerator() => Items.Cast<DA.DocumentWindow>().GetEnumerator();

  void DA.DocumentWindows.Add(DA.DocumentWindow window)
  {
    Add(window);
  }

  /// <summary>
  /// Adds a document window to the collection.
  /// </summary>
  /// <param name="window"></param>
  /// <exception cref="ArgumentNullException"></exception>
  /// <exception cref="ArgumentException"></exception>
  public void Add(DA.DocumentWindow window)
  {
    if (window == null)
      throw new ArgumentNullException(nameof(window));
    if (window is not DocumentWindow documentWindow)
      throw new ArgumentException("Window must be a DocumentWindow");
    Items.Add(documentWindow);
  }

}

