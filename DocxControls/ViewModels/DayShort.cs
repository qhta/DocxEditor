using Docx.Automation;

namespace DocxControls.ViewModels;

/// <summary>
/// Specifies a current day of the month. Text is displayed in the format "DD" (e.g. "01").
/// </summary>
public class DayShort : ElementViewModel, DA.DayShort
{
  /// <summary>
  /// Constructor with owner and modeled element.
  /// </summary>
  /// <param name="owner"></param>
  /// <param name="modeledElement"></param>
  public DayShort(Run owner, DXW.MonthShort modeledElement) : base(owner, modeledElement)
  {
  }

  /// <summary>
  /// Parent run of this view model
  /// </summary>
  public DA.Run ParentRun => (DA.Run)Owner!;

  /// <summary>
  /// Result text
  /// </summary>
  public string? Text => DateTime.Now.ToString("DD");
}