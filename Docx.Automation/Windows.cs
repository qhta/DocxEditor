namespace Docx.Automation;

/// <summary>
/// Collection of <see cref="Window"/> elements.
/// </summary>
public interface Windows: IElementCollection<Window>
{

  /// <summary>
  /// Returns a Window object that represents a new window of a document.
  /// </summary>
  /// <param name="window">The Window object you want to open another window for. If this argument is omitted, a new window is opened for the active document.</param>
  /// <returns></returns>
  public Window Add(Window? window);

  
  ///// <summary>
  ///// Arranges all open document windows in the application workspace.
  ///// </summary>
  ///// <param name="arrangeStyle">The window arrangement. Can be either Icons or Tiled.</param>
  //public void Arrange(ArrangeStyle arrangeStyle);

  ///// <summary>
  ///// True enables scrolling the contents of the windows at the same time. 
  ///// </summary>
  //public bool SyncScrollingSideBySide { get; set; }
  
  ///// <summary>
  ///// Ends side by side mode if two windows are in side by side mode. Returns a Boolean that represents whether the method was successful.
  ///// </summary>
  ///// <returns></returns>
  //public bool BreakSideBySide();

  ///// <summary>
  ///// Opens two windows in side by side mode. Returns a Boolean.
  ///// </summary>
  ///// <param name="window"></param>
  //public bool CompareSideBySideWith(Document window);

  ///// <summary>
  ///// Resets two document windows that are in the Compare side by side with view mode.
  ///// For example, if a user minimizes or maximizes one of the two document windows being compared,
  ///// this method resets the display so that the two windows are displayed side by side again.
  ///// </summary>
  //public void ResetPositionsSideBySide();
  

}
