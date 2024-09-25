namespace DocxControls.ViewModels;

/// <summary>
/// View model for a section properties
/// </summary>
public class SectionPropertiesViewModel : ElementViewModel
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="ownerViewModel">Owner view model. Must be <see cref="CompoundElementViewModel"/></param>
  /// <param name="properties">Modeled section properties element</param>
  public SectionPropertiesViewModel(CompoundElementViewModel ownerViewModel, DXW.SectionProperties properties): base(ownerViewModel, properties)
  {
  }


}
