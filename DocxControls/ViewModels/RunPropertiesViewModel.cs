﻿namespace DocxControls;

/// <summary>
/// View model for a run properties
/// </summary>
public class RunPropertiesViewModel : ElementViewModel
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="runViewModel">Parent view model. Must be <see cref="RunViewModel"/></param>
  /// <param name="properties">Modeled run properties element</param>
  public RunPropertiesViewModel(RunViewModel runViewModel, DXW.RunProperties properties): base(runViewModel, properties)
  {
  }

  /// <summary>
  /// Run properties element
  /// </summary>
  public DXW.RunProperties RunProperties => (DXW.RunProperties)Element!;

}
