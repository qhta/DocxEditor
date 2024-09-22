﻿namespace DocxControls.ViewModels;

/// <summary>
/// View model for a last rendered page break
/// </summary>
public class LastRenderedPageBreakViewModel : ElementViewModel
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="runViewModel">Parent element</param>
  /// <param name="element">Modeled last rendered pager break element</param>
  public LastRenderedPageBreakViewModel(RunViewModel runViewModel, DXW.LastRenderedPageBreak element): base(runViewModel, element)
  {
  }

}
