namespace DocxControls.ViewModels;

/// <summary>
/// Specifies that this position delimited the end of a page when this document was last saved by an application which paginates its content.
/// </summary>
public class LastRenderedPageBreak : ElementViewModel, DA.LastRenderedPageBreak
{
  /// <summary>
  /// Constructor with owner and modeled element.
  /// </summary>
  /// <param name="owner"></param>
  /// <param name="modeledElement"></param>
  public LastRenderedPageBreak(Run owner, DXW.LastRenderedPageBreak modeledElement) : base(owner, modeledElement)
  {
  }

  /// <summary>
  /// Parent run of this view model
  /// </summary>
  public DA.Run ParentRun => (DA.Run)Owner!;

}