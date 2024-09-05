namespace DocxControls;

/// <summary>
/// View model for a table
/// </summary>
public class TableViewModel : ElementViewModel
{
  /// <summary>
  /// Default constructor. Creates a new <c>Table</c>>
  /// </summary>
  public TableViewModel(): this (new DXW.Table())
  {
  }

  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="table"></param>
  public TableViewModel(DXW.Table table): base(table)
  {
  }

  /// <summary>
  /// Table element of the document
  /// </summary>
  public DXW.Table Table => (DXW.Table)Element;

  /// <summary>
  /// Initializes the object properties
  /// </summary>
  protected override void InitObjectProperties()
  {
  }
}
