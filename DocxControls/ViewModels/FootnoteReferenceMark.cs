namespace DocxControls.ViewModels;

/// <summary>
/// Specifies the presence of a footnote reference mark at the current location in the footnote.
/// </summary>
public class FootnoteReferenceMark: ElementViewModel<DXW.FootnoteReferenceMark>, DA.FootnoteReferenceMark
{
  /// <summary>
  /// Constructor with owner and modeled element.
  /// </summary>
  /// <param name="owner"></param>
  /// <param name="modeledElement"></param>
  public FootnoteReferenceMark(Run owner, DXW.FootnoteReferenceMark? modeledElement) : base(owner, modeledElement)
  {
  }

  /// <summary>
  /// Parent run of this view model
  /// </summary>
  public DA.Run ParentRun => (DA.Run)Owner!;

}