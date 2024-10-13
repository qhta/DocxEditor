using Docx.Automation;

namespace DocxControls.ViewModels;

/// <summary>
/// Specifies a current year. Text is displayed in the format "YYYY" (e.g. "2001").
/// </summary>
public class YearLong : ElementViewModel, DA.YearLong
{
  /// <summary>
  /// Constructor with owner and modeled element.
  /// </summary>
  /// <param name="owner"></param>
  /// <param name="modeledElement"></param>
  public YearLong(Run owner, DXW.YearLong modeledElement) : base(owner, modeledElement)
  {
  }

  /// <summary>
  /// Parent run of this view model
  /// </summary>
  public DA.Run ParentRun => (DA.Run)Owner!;

  /// <summary>
  /// Result text
  /// </summary>
  public string? Text => DateTime.Now.ToString("YYYY");
}