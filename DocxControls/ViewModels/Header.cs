using Qhta.MVVM;

namespace DocxControls.ViewModels;

/// <summary>
/// Represents a footer.
/// </summary>
public class Footer: HeaderFooter
{
  /// <summary>
  /// Constructor with owner object and modeled element.
  /// </summary>
  /// <param name="owner"></param>
  /// <param name="modeledElement"></param>
  public Footer(ViewModel owner, DXW.Footer modeledElement) : base(owner, modeledElement)
  {
  }
}