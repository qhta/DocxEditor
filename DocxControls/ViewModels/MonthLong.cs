namespace DocxControls.ViewModels;

/// <summary>
/// Specifies a current month. Text is displayed in the format "MMMM" (e.g. "january").
/// </summary>
public class MonthLong : ElementViewModel<DXW.MonthLong>, DA.MonthLong
{
  /// <summary>
  /// Constructor with owner and modeled element.
  /// </summary>
  /// <param name="owner"></param>
  /// <param name="modeledElement"></param>
  public MonthLong(Run owner, DXW.MonthLong? modeledElement) : base(owner, modeledElement)
  {
  }

  /// <summary>
  /// Parent run of this view model
  /// </summary>
  public DA.Run ParentRun => (DA.Run)Owner!;

  /// <summary>
  /// Result text
  /// </summary>
  public string? Text => DateTime.Now.ToString("MMMM");
}