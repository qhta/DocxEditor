namespace DocxControls.ViewModels;

/// <summary>
/// Specifies the presence of a separator mark within the current run.
/// A separator mark is a horizontal line which spans part of the width text extents.
/// </summary>
public class SeparatorMark : ElementViewModel<DXW.SeparatorMark>, DA.SeparatorMark
{
  /// <summary>
  /// Constructor with owner and modeled element.
  /// </summary>
  /// <param name="owner"></param>
  /// <param name="modeledElement"></param>
  public SeparatorMark(Run owner, DXW.SeparatorMark? modeledElement) : base(owner, modeledElement)
  {
  }

  /// <summary>
  /// Parent run of this view model
  /// </summary>
  public DA.Run ParentRun => (DA.Run)Owner!;

}