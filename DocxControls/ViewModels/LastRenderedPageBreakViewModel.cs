namespace DocxControls.ViewModels;

/// <summary>
/// View model for a last rendered page break
/// </summary>
public class LastRenderedPageBreakViewModel : ElementViewModel
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="runViewModel">Owner properties</param>
  /// <param name="properties">Modeled last rendered pager break properties</param>
  public LastRenderedPageBreakViewModel(Run runViewModel, DXW.LastRenderedPageBreak properties): base(runViewModel, properties)
  {
  }

}
