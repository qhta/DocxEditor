using System.Windows.Controls;
using System.Windows.Data;

namespace DocxControls;

/// <summary>
/// Custom data grid that allows to sort the columns in 3-state mode.
/// </summary>
public class CustomDataGrid : DataGrid
{
  /// <summary>
  /// Original OnSorting method switches the sort direction between ascending and descending.
  /// So, we need to add the third state - no sorting.
  /// </summary>
  /// <param name="eventArgs"></param>
  protected override void OnSorting(DataGridSortingEventArgs eventArgs)
  {
    var column = eventArgs.Column;
    if (column == null) return;
    var sortDirection = column.SortDirection;
    base.OnSorting(eventArgs);

    if (sortDirection == System.ComponentModel.ListSortDirection.Descending)
    {
      column.SortDirection = null;
      var collectionView = CollectionViewSource.GetDefaultView(ItemsSource);
      if (collectionView != null)
      {
        collectionView.SortDescriptions.Clear();
        collectionView.Refresh();
      }
      eventArgs.Handled = true;
    }
  }
}
