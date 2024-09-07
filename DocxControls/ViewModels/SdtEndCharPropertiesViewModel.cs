namespace DocxControls;

/// <summary>
/// View model for a Sdt element end char properties
/// </summary>
public class SdtEndCharPropertiesViewModel : ElementViewModel
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="sdtViewModel">Owner view model. Must be <see cref="SdtElementViewModel"/></param>
  /// <param name="properties">Modeled Sdt properties element</param>
  public SdtEndCharPropertiesViewModel(SdtElementViewModel sdtViewModel, DXW.SdtEndCharProperties? properties): base(sdtViewModel, properties)
  {
  }

}
