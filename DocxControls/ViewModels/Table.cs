namespace DocxControls.ViewModels;

/// <summary>
/// View model for a table
/// </summary>
public class Table : Block, DA.Table
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="ownerViewModel">Owner view model. Must be <see cref="ElementViewModel"/></param>
  /// <param name="table">Modeled table element</param>
  public Table(ElementViewModel ownerViewModel, DXW.Table table): base(ownerViewModel, table)
  {
  }

  /// <summary>
  /// Table element of the document
  /// </summary>
  public DXW.Table TableElement => (DXW.Table)ModeledElement!;


}
