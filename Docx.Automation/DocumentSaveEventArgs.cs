namespace Docx.Automation;

/// <summary>
/// Event handler for the DocumentBeforeSave event.
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
public delegate void DocumentSaveEventHandler(object? sender, DocumentSaveEventArgs e);

/// <summary>
/// Arguments for the DocumentBeforeSave event.
/// </summary>
public class DocumentSaveEventArgs : DocumentCancelEventArgs
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="document"></param>
  /// <param name="saveAsUIAs">
  /// True if the Save As dialog box is displayed, whether to save a new document.
  /// </param>
  /// <param name="canBreak">Specifies whether all further operations can be broken</param>
  public DocumentSaveEventArgs(Document document, bool saveAsUIAs, bool canBreak): base(document, canBreak)
  {
    SaveAsUI = saveAsUIAs;
  }

  /// <summary>
  /// True if the Save As dialog box is displayed, whether to save a new document,
  /// in response to the Save command; or in response to the SaveAs command or method.
  /// </summary>
  public bool SaveAsUI { get; }
}