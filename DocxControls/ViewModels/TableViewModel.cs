namespace DocxControls;

/// <summary>
/// View model for a table
/// </summary>
public class TableViewModel : ElementViewModel
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="ownerViewModel">Owner view model. Must be <see cref="BlockElementViewModel"/></param>
  /// <param name="table">Modeled table element</param>
  public TableViewModel(BlockElementViewModel ownerViewModel, DXW.Table table): base(ownerViewModel, table)
  {
  }

  /// <summary>
  /// Table element of the document
  /// </summary>
  public DXW.Table Table => (DXW.Table)Element!;

  /// <summary>
  /// Initializes the object properties
  /// </summary>
  protected override void InitObjectProperties()
  {
  }
}
