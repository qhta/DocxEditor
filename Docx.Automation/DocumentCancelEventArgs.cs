namespace Docx.Automation;


/// <summary>
/// Structure for the Cancel event argument.
/// </summary>
public struct CancelArgs
{
  /// <summary>
  /// Specifies whether to cancel event. By setting this to true, the action will not be performed.
  /// </summary>
  public bool Cancel;

  /// <summary>
  /// Input flag that specifies whether ask to break all further actions.
  /// </summary>
  public bool CanBreak;

  /// <summary>
  /// Output flag that specifies whether to break all further actions.
  /// </summary>
  public bool Break;
}

/// <summary>
/// Event handler for the DocumentClose event.
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
public delegate void DocumentCloseEventHandler(object? sender, DocumentCancelEventArgs e);

/// <summary>
/// Arguments for the DocumentClose event.
/// </summary>
public class DocumentCancelEventArgs : DocumentEventArgs
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="document"></param>
  /// <param name="canBreak">Specifies whether all further operations can be broken</param>
  public DocumentCancelEventArgs(Document document, bool canBreak): base(document)
  {
    Cancel = new CancelArgs { CanBreak = canBreak };
  }

  /// <summary>
  /// Specifies whether to cancel event.
  /// </summary>
  public CancelArgs Cancel
  {
    get; set;
  }

}