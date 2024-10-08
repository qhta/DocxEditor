namespace DocxControls.ViewModels;

/// <summary>
/// Represents a block in a document.
/// </summary>
public class Block: CompoundElementViewModel, DA.Block
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="ownerViewModel">Owner view model. Must be <see cref="ElementViewModel"/></param>
  /// <param name="block"></param>
  public Block(ElementViewModel ownerViewModel, DX.OpenXmlCompositeElement block) : base(ownerViewModel, block)
  {
    LoadAllElements();
  }
}