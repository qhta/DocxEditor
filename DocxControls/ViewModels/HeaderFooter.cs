using Qhta.MVVM;

namespace DocxControls.ViewModels;

/// <summary>
/// Represents a header or footer.
/// </summary>
public abstract class HeaderFooter : ElementViewModel, DA.HeaderFooter
{

  /// <summary>
  /// Constructor with owner object and modeled ModeledElement.
  /// </summary>
  /// <param name="owner"></param>
  /// <param name="modeledElement"></param>
  protected HeaderFooter(ViewModel owner, DX.OpenXmlElement modeledElement) : base(owner, modeledElement)
  {
  }
}