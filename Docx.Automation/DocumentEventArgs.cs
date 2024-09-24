namespace Docx.Automation;

/// <summary>
/// Event handler for any Document event.
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
public delegate void DocumentEventHandler(object? sender, DocumentEventArgs e);

/// <summary>
/// Arguments for any Document event.
/// </summary>
public class DocumentEventArgs : EventArgs
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="document"></param>
  public DocumentEventArgs(Document document)
  {
    Document = document;
  }

  /// <summary>
  /// The document that is being closed.
  /// </summary>
  public Document Document { get; }

}