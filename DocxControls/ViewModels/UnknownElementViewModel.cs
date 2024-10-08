using Qhta.MVVM;

namespace DocxControls.ViewModels;

/// <summary>
/// View model for an unknown element
/// </summary>
public class UnknownElementViewModel: ElementViewModel
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="ownerViewModel">Owner view model of any view model type</param>
  /// <param name="properties">Modeled properties</param>
  public UnknownElementViewModel(ViewModel ownerViewModel, DX.OpenXmlElement properties) : base(ownerViewModel, properties)
  {
  }

}