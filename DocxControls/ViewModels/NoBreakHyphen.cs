namespace DocxControls.ViewModels;

/// <summary>
///  A non-breaking hyphen character
/// </summary>
public class NoBreakHyphen: ElementViewModel<DXW.NoBreakHyphen>, DA.NoBreakHyphen
{
  /// <summary>
  /// Constructor with owner and modeled element.
  /// </summary>
  /// <param name="owner"></param>
  /// <param name="modeledElement"></param>
  public NoBreakHyphen(Run owner, DXW.NoBreakHyphen? modeledElement) : base(owner, modeledElement)
  {
  }

  /// <summary>
  /// Parent run of this view model
  /// </summary>
  public DA.Run ParentRun => (DA.Run)Owner!;

  /// <summary>
  /// Returns the non-breaking hyphen character
  /// </summary>
  public string? Text => "\u2011";
}