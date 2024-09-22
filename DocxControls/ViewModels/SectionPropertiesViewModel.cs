namespace DocxControls.ViewModels;

/// <summary>
/// View model for a section properties
/// </summary>
public class SectionPropertiesViewModel : ElementViewModel
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="parentViewModel">Parent view model. Must be <see cref="BlockElementViewModel"/></param>
  /// <param name="properties">Modeled section properties element</param>
  public SectionPropertiesViewModel(BlockElementViewModel parentViewModel, DXW.SectionProperties properties): base(parentViewModel, properties)
  {
  }


}
