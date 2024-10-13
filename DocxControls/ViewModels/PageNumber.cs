namespace DocxControls.ViewModels;

/// <summary>
/// Specifies the presence of a page number block at the current location in the run content.
/// A page number block is a non-editable region of text which shall display the current page using ascending decimal numbers.
/// </summary>
public class PageNumber : ElementViewModel, DA.PageNumber
{
  /// <summary>
  /// Constructor with owner and modeled element.
  /// </summary>
  /// <param name="owner"></param>
  /// <param name="modeledElement"></param>
  public PageNumber(Run owner, DXW.PageNumber modeledElement) : base(owner, modeledElement)
  {
  }

  /// <summary>
  /// Parent run of this view model
  /// </summary>
  public DA.Run ParentRun => (DA.Run)Owner!;

}