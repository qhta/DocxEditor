namespace DocxControls;

/// <summary>
/// View model for a table
/// </summary>
public class TableViewModel : ElementViewModel
{
  /// <summary>
  /// Default constructor. Creates a new <see cref="Table"/>
  /// </summary>
  public TableViewModel(): this (new DXW.Table())
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="TableViewModel"/> class.
  /// </summary>
  /// <param name="table"></param>
  public TableViewModel(DXW.Table table): base(table)
  {
  }

  /// <summary>
  /// Table element of the document
  /// </summary>
  public DXW.Table Table => (DXW.Table)Element;
}
