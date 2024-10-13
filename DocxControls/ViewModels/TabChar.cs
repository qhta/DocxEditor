namespace DocxControls.ViewModels;

/// <summary>
/// Tab character
/// </summary>
public class TabChar: ElementViewModel, DA.NoBreakHyphen
{
  /// <summary>
  /// Constructor with owner and modeled element.
  /// </summary>
  /// <param name="owner"></param>
  /// <param name="modeledElement"></param>
  public TabChar(Run owner, DXW.TabChar modeledElement) : base(owner, modeledElement)
  {
  }

  /// <summary>
  /// Parent run of this view model
  /// </summary>
  public DA.Run ParentRun => (DA.Run)Owner!;

  /// <summary>
  /// Returns the non-breaking hyphen character
  /// </summary>
  public string? Text => "\u0009";
}