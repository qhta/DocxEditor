using Qhta.MVVM;

namespace DocxControls.ViewModels;

/// <summary>
/// Collection of elements.
/// </summary>
public class ElementViewModelCollection<T>: ElementCollection<T> where T: ElementViewModel
{
  /// <summary>
  /// Constructor with a owner object.
  /// </summary>
  /// <param name="parent"></param>
  public ElementViewModelCollection(ElementViewModel? parent) : base(parent)
  {
  }

  /// <summary>
  /// Constructor with owner object and modeled ModeledElement.
  /// </summary>
  /// <param name="owner">owner ViewModel</param>
  /// <param name="modeledElement">Modeled ModeledElement</param>
  public ElementViewModelCollection(ViewModel owner, DX.OpenXmlElement modeledElement) : base(owner)
  {
    ModeledElement = modeledElement;
  }

  internal DX.OpenXmlElement? ModeledElement { get; }
}


