namespace DocxControls.ViewModels;

/// <summary>
/// View model for a paragraph properties
/// </summary>
public class ParagraphProperties : ElementViewModel, DA.ParagraphProperties
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="paragraphViewModel">Owner view model. Must be <see cref="Paragraph"/></param>
  /// <param name="properties">Modeled paragraph properties element</param>
  public ParagraphProperties(Paragraph paragraphViewModel, DXW.ParagraphProperties properties): base(paragraphViewModel, properties)
  {
  }

}
