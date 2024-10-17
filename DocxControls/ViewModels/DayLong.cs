namespace DocxControls.ViewModels;

/// <summary>
/// Specifies a current day of the month. Text is displayed in the format "DDDD" (e.g. "monday").
/// </summary>
public class DayLong : ElementViewModel<DXW.DayLong>, DA.DayLong
{
  /// <summary>
  /// Constructor with owner and modeled element.
  /// </summary>
  /// <param name="owner"></param>
  /// <param name="modeledElement"></param>
  public DayLong(Run owner, DXW.DayLong? modeledElement) : base(owner, modeledElement)
  {
  }

  /// <summary>
  /// Parent run of this view model
  /// </summary>
  public DA.Run ParentRun => (DA.Run)Owner!;

  /// <summary>
  /// Result text
  /// </summary>
  public string? Text => DateTime.Now.ToString("DDDD");
}