using Docx.Automation;

namespace DocxControls.ViewModels;

/// <summary>
/// Specifies a current year. Text is displayed in the format "YY" (e.g. "01").
/// </summary>
public class YearShort : ElementViewModel, DA.YearShort
{
  /// <summary>
  /// Constructor with owner and modeled element.
  /// </summary>
  /// <param name="owner"></param>
  /// <param name="modeledElement"></param>
  public YearShort(Run owner, DXW.YearShort modeledElement) : base(owner, modeledElement)
  {
  }

  /// <summary>
  /// Parent run of this view model
  /// </summary>
  public DA.Run ParentRun => (DA.Run)Owner!;

  /// <summary>
  /// Result text
  /// </summary>
  public string? Text => DateTime.Now.ToString("YY");
}