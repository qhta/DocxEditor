namespace DocxControls.ViewModels;

/// <summary>
/// Represents a drawing in a document.
/// </summary>
public class Drawing: ElementViewModel<DXW.Drawing>, DA.Drawing
{
  /// <summary>
  /// Constructor with owner object and modeled element.
  /// </summary>
  /// <param name="owner"></param>
  /// <param name="modeledElement"></param>
  public Drawing(Run owner, DXW.Drawing? modeledElement) : base(owner, modeledElement)
  {
  }

  /// <summary>
  /// Run parent of the drawing.
  /// </summary>
  public DA.Run ParentRun => (DA.Run)Owner!;
}