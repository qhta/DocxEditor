namespace DocxControls.Helpers;

/// <summary>
/// Interface for view model that cooperates with data grid.
/// </summary>
public interface IDataGridCompanion
{
  /// <summary>
  /// Provides the width of the data grid in the view.
  /// </summary>
  public double DataGridWidth { get; set; }
}
