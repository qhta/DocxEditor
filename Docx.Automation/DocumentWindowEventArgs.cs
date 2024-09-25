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
public class DocumentWindowEventArgs : EventArgs
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="window"></param>
  public DocumentWindowEventArgs(DocumentWindow window)
  {
    Window = window;
  }

  /// <summary>
  /// The document that is being closed.
  /// </summary>
  public DocumentWindow Window { get; }

}