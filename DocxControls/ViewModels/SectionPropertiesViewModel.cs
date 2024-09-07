namespace DocxControls;

/// <summary>
/// View model for a section properties
/// </summary>
public class SectionPropertiesViewModel : ElementViewModel
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="ownerViewModel">Owner view model. Must be <see cref="BlockElementViewModel"/></param>
  /// <param name="properties">Modeled section properties element</param>
  public SectionPropertiesViewModel(BlockElementViewModel ownerViewModel, DXW.SectionProperties properties): base(ownerViewModel, properties)
  {
  }

  /// <summary>
  /// Initializes the object properties
  /// </summary>
  protected override void InitObjectProperties()
  {
  }
}
