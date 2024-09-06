using Qhta.MVVM;

namespace DocxControls;

/// <summary>
/// View model for an SDT element
/// </summary>
public class SdtElementViewModel: ElementViewModel
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="ownerViewModel">Owner view model</param>
  /// <param name="element">Modeled SdtElement</param>
  public SdtElementViewModel(ViewModel ownerViewModel, DXW.SdtElement element) : base(ownerViewModel, element)
  {
  }

  /// <summary>
  /// Initializes the object properties
  /// </summary>
  protected override void InitObjectProperties()
  {
  }
}