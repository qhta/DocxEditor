namespace Docx.Automation;

/// <summary>
/// Interface for the application main window that hosts document windows.
/// </summary>
public interface ApplicationWindow
{
  /// <summary>
  /// The application instance.
  /// </summary>
  public Application Application { get; }

  /// <summary>
  /// Adds a document window to the application window.
  /// </summary>
  /// <param name="window"></param>
  public void AddDocumentWindow(DocumentWindow window);

  //public void AddPluginCommandsToMenu();


}