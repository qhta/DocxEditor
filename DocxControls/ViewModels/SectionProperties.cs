namespace DocxControls.ViewModels;

/// <summary>
/// View model for a section properties
/// </summary>
public class SectionProperties : ElementViewModel
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="ownerViewModel">Owner view model. Must be <see cref="CompoundElementViewModel"/></param>
  /// <param name="properties">Modeled section properties element</param>
  public SectionProperties(CompoundElementViewModel ownerViewModel, DXW.SectionProperties properties): base(ownerViewModel, properties)
  {
  }

  /// <summary>
  /// Section properties element of the document
  /// </summary>
  public DXW.SectionProperties SectionPropertiesElement => (DXW.SectionProperties)OpenXmlElement!;
}
