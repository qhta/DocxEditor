namespace DocxControls.ViewModels;

/// <summary>
/// View model for a paragraph properties
/// </summary>
public class ParagraphPropertiesViewModel : ElementViewModel
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="paragraphViewModel">Parent view model. Must be <see cref="ParagraphViewModel"/></param>
  /// <param name="properties">Modeled paragraph properties element</param>
  public ParagraphPropertiesViewModel(ParagraphViewModel paragraphViewModel, DXW.ParagraphProperties properties): base(paragraphViewModel, properties)
  {
  }

}
