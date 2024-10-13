namespace DocxControls.ViewModels;

/// <summary>
/// Specifies the presence of an endnote reference mark at the current location in the endnote.
/// </summary>
public class EndnoteReferenceMark : ElementViewModel, DA.AnnotationReferenceMark
{
  /// <summary>
  /// Constructor with owner and modeled element.
  /// </summary>
  /// <param name="owner"></param>
  /// <param name="modeledElement"></param>
  public EndnoteReferenceMark(Run owner, DXW.EndnoteReferenceMark modeledElement) : base(owner, modeledElement)
  {
  }

  /// <summary>
  /// Parent run of this view model
  /// </summary>
  public DA.Run ParentRun => (DA.Run)Owner!;

}