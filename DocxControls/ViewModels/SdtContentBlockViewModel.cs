namespace DocxControls.ViewModels;

/// <summary>
/// View model for a structured document tag content block element.
/// </summary>
public class SdtContentBlockViewModel: BlockElementViewModel
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="sdtElementViewModel">Parent view model. Must be <see cref="SdtElementViewModel"/></param>
  /// <param name="sdtContentBlock">Modeled sdt content block element</param>
  public SdtContentBlockViewModel(SdtElementViewModel sdtElementViewModel, DXW.SdtContentBlock sdtContentBlock) : base(sdtElementViewModel, sdtContentBlock)
  {
  }
}
