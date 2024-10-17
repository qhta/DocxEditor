namespace DocxControls.ViewModels;

/// <summary>
/// View model for a Sdt element end char properties
/// </summary>
public class SdtEndCharPropertiesViewModel: ElementViewModel<DXW.SdtEndCharProperties>
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="sdtViewModel">Owner view model. Must be <see cref="SdtElementViewModel"/></param>
  /// <param name="modeledElement">Modeled Sdt modeledElement element</param>
  public SdtEndCharPropertiesViewModel(SdtElementViewModel sdtViewModel, DXW.SdtEndCharProperties modeledElement): base(sdtViewModel, modeledElement)
  {
  }

}
