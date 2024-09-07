namespace DocxControls;

/// <summary>
/// View model for a Sdt element properties
/// </summary>
public class SdtPropertiesViewModel : ElementViewModel
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="sdtViewModel">Owner view model. Must be <see cref="SdtElementViewModel"/></param>
  /// <param name="properties">Modeled Sdt properties element</param>
  public SdtPropertiesViewModel(SdtElementViewModel sdtViewModel, DXW.SdtProperties properties): base(sdtViewModel, properties)
  {
  }

  /// <summary>
  /// Initializes the object properties
  /// </summary>
  protected override void InitObjectProperties()
  {
  }
}
