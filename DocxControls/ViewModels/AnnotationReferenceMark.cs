namespace DocxControls.ViewModels;

/// <summary>
/// Specifies the presence of an annotation reference mark at the current location in the comment.
/// </summary>
public class AnnotationReferenceMark : ElementViewModel<DXW.AnnotationReferenceMark>, DA.AnnotationReferenceMark
{
  /// <summary>
  /// Constructor with owner and modeled element.
  /// </summary>
  /// <param name="owner"></param>
  /// <param name="modeledElement"></param>
  public AnnotationReferenceMark(Run owner, DXW.AnnotationReferenceMark? modeledElement=null) : base(owner, modeledElement)
  {
  }

  /// <summary>
  /// Parent run of this view model
  /// </summary>
  public DA.Run ParentRun => (DA.Run)Owner!;

}