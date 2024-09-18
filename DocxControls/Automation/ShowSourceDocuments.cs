namespace DocxControls.Automation;
/// <summary>
/// Specifies how to display source documents when two documents are compared using the Word Compare functions.
/// </summary>
public enum ShowSourceDocuments
{

  /// <summary>
  /// Shows neither the original nor the revised documents for the source document used in a Compare function.
  /// </summary>
  None = 0,
  /// <summary>
  /// Shows the original document only.
  /// </summary>
  Original = 1,
  /// <summary>
  ///  Shows the revised document only.
  /// </summary>
  Revised = 2,
  /// <summary>
  /// Shows both original and revised documents.
  /// </summary>
  Both = 3,
}
