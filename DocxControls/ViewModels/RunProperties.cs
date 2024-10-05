namespace DocxControls.ViewModels;

/// <summary>
/// View model for a run properties
/// </summary>
public class RunProperties : ElementViewModel, DA.RunProperties
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="runViewModel">Owner view model. Must be <see cref="Run"/></param>
  /// <param name="properties">Modeled run properties element</param>
  public RunProperties(Run runViewModel, DXW.RunProperties properties): base(runViewModel, properties)
  {
  }

  /// <summary>
  /// RunElement properties element
  /// </summary>
  public DXW.RunProperties RunPropertiesElement => (DXW.RunProperties)Element!;

  /// <summary>
  /// Compare the properties of this run with another run.
  /// </summary>
  /// <param name="other"></param>
  /// <returns></returns>
  public bool Equals(DA.RunProperties other)
  {
    return true;
  }
}
