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
  /// <param name="parentViewModel">Parent view model of any view model type</param>
  /// <param name="element">Modeled element</param>
  public UnknownElementViewModel(ViewModel parentViewModel, DX.OpenXmlElement element) : base(parentViewModel, element)
  {
  }

}