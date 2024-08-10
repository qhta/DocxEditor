namespace DocxControls;

/// <summary>
/// 
/// </summary>
public interface IMainWindow
{
  /// <summary>
  /// Set the title of the main window
  /// </summary>
  /// <param name="title"></param>
  public void SetTitle(string title);

  /// <summary>
  /// Add a document view to the main window
  /// </summary>
  /// <param name="documentView"></param>
  public void ShowDocumentView(DocumentView documentView);
}
