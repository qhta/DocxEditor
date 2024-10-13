namespace DocxControls.ViewModels;

/// <summary>
/// Specifies the presence of a continuation separator mark within the current run.
/// A continuation separator mark is a horizontal line which spans the width of the main story's text extents.
/// </summary>
public class ContinuationSeparatorMark : ElementViewModel, DA.SeparatorMark
{
  /// <summary>
  /// Constructor with owner and modeled element.
  /// </summary>
  /// <param name="owner"></param>
  /// <param name="modeledElement"></param>
  public ContinuationSeparatorMark(Run owner, DXW.ContinuationSeparatorMark modeledElement) : base(owner, modeledElement)
  {
  }

  /// <summary>
  /// Parent run of this view model
  /// </summary>
  public DA.Run ParentRun => (DA.Run)Owner!;

}