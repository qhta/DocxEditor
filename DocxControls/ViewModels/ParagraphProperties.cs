namespace DocxControls.ViewModels;

/// <summary>
/// View model for a paragraph properties
/// </summary>
public class ParagraphProperties: ElementViewModel<DXW.ParagraphProperties>, DA.ParagraphProperties
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="paragraphViewModel">Owner view model. Must be <see cref="Paragraph"/></param>
  /// <param name="modeledElement">Modeled paragraph modeledElement element</param>
  public ParagraphProperties(Paragraph paragraphViewModel, DXW.ParagraphProperties? modeledElement): base(paragraphViewModel, modeledElement)
  {

  }

}
