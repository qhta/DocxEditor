namespace DocxControls;

/// <summary>
/// View model for a body element
/// </summary>
public class BodyViewModel: BlockElementViewModel
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="documentViewModel">Owner view model. Must be <see cref="DocumentViewModel"/></param>
  /// <param name="body">Modeled body element</param>
  public BodyViewModel(DocumentViewModel documentViewModel, DXW.Body body) : base(documentViewModel, body)
  {
  }
}
