namespace Docx.Automation;

/// <summary>
/// Event handler for Document-Window events.
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
public delegate void DocumentWindowEventHandler(object? sender, DocumentWindowEventArgs e);

/// <summary>
/// Arguments for any Document-Window events.
/// </summary>
public class DocumentWindowEventArgs : DocumentEventArgs
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="document"></param>
  /// <param name="window"></param>
  public DocumentWindowEventArgs(Document document, Window window): base(document) 
  {
    Window = window;
  }

  /// <summary>
  /// The document that is being closed.
  /// </summary>
  public Window Window { get; }

}