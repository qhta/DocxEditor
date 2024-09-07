using Qhta.MVVM;

namespace DocxControls;

/// <summary>
/// View model for an unknown element
/// </summary>
public class UnknownElementViewModel: ElementViewModel
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="ownerViewModel">Owner view model of any view model type</param>
  /// <param name="element">Modeled element</param>
  public UnknownElementViewModel(ViewModel ownerViewModel, DX.OpenXmlElement element) : base(ownerViewModel, element)
  {
  }

}