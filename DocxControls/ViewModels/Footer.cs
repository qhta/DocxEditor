using Qhta.MVVM;

namespace DocxControls.ViewModels;

/// <summary>
/// Represents a header.
/// </summary>
public class Header: HeaderFooter
{
  /// <summary>
  /// Constructor with owner object and modeled element.
  /// </summary>
  /// <param name="owner"></param>
  /// <param name="modeledElement"></param>
  public Header(ViewModel owner, DXW.Header modeledElement) : base(owner, modeledElement)
  {
  }
}