namespace DocxControls.ViewModels;

/// <summary>
/// View model for a table
/// </summary>
public class TableViewModel : ElementViewModel
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="parentViewModel">Parent view model. Must be <see cref="BlockElementViewModel"/></param>
  /// <param name="table">Modeled table element</param>
  public TableViewModel(BlockElementViewModel parentViewModel, DXW.Table table): base(parentViewModel, table)
  {
  }

  /// <summary>
  /// Table element of the document
  /// </summary>
  public DXW.Table Table => (DXW.Table)Element!;


}
