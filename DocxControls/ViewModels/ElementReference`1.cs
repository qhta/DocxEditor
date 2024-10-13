using Qhta.MVVM;

namespace DocxControls.ViewModels;

/// <summary>
/// Represents a reference to an element.
/// </summary>
/// <typeparam name="ID">type of identifier</typeparam>
/// <typeparam name="T">type of element</typeparam>
public abstract class ElementReference<ID, T> : ElementViewModel, DA.IElementReference<ID,T> where T : ElementViewModel
{
  /// <summary>
  /// Constructor with owner object and modeled ModeledElement.
  /// </summary>
  /// <param name="owner"></param>
  /// <param name="modeledElement"></param>
  protected ElementReference(ViewModel owner, DX.OpenXmlElement modeledElement) : base(owner, modeledElement)
  {
  }

  /// <summary>
  /// Identifier of the element.
  /// </summary>
  public abstract ID? Id { get; set; }

  /// <summary>
  /// Gets the element.
  /// </summary>
  /// <returns></returns>
  public abstract T? GetElement();


}