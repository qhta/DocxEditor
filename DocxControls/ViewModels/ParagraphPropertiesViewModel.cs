namespace DocxControls;

/// <summary>
/// View model for a paragraph properties
/// </summary>
public class ParagraphPropertiesViewModel : ElementViewModel
{
  /// <summary>
  /// Initializes a new instance of the <see cref="ParagraphPropertiesViewModel"/> class.
  /// </summary>
  /// <param name="properties"></param>
  public ParagraphPropertiesViewModel(DXW.ParagraphProperties properties): base(properties)
  {
  }
}
