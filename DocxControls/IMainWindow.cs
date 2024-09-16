namespace DocxControls;

/// <summary>
/// 
/// </summary>
public interface IMainWindow
{

  /// <summary>
  /// Add a document view to the main window
  /// </summary>
  /// <param name="filePath"></param>
  /// <param name="isEditable"></param>
  public void OpenDocument(string filePath, bool isEditable);
}
