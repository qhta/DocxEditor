namespace DocxControls;

/// <summary>
/// View model for an unknown element
/// </summary>
public class UnknownElementViewModel: ElementViewModel
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="element"></param>
  public UnknownElementViewModel(DX.OpenXmlElement element) : base(element)
  {
  }

  /// <summary>
  /// Initializes the object properties
  /// </summary>
  protected override void InitObjectProperties()
  {
  }
}