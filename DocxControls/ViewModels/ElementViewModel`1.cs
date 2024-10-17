namespace DocxControls.ViewModels;

/// <summary>
/// View model for any DocumentFormat.OpenXml.OpenXmlElement that can create modeled element.
/// </summary>
public abstract class ElementViewModel<T> : ElementViewModel where T : DX.OpenXmlElement, new()
{

  /// <summary>
  /// Constructor with owner object and modeled element.
  /// </summary>
  /// <param name="owner">owner ViewModel</param>
  /// <param name="modeledElement">Modeled ModeledElement</param>
  protected ElementViewModel(ViewModel owner, T? modeledElement) : base(owner, modeledElement ?? new T())
  {
  }

  /// <summary>
  /// ModeledElement element of the run
  /// </summary>
  internal T OpenXmlElement => (T)ModeledElement!;
}
