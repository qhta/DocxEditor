using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DocxControls;

/// <summary>
/// Interaction logic for PropertiesView.xaml
/// </summary>
public partial class PropertiesView : UserControl
{
  /// <summary>
  /// Default constructor
  /// </summary>
  public PropertiesView()
  {
    InitializeComponent();
    DataGrid.Loaded += DataGrid_Loaded;
    //DataGrid.ColumnWidthChanged += DataGrid_ColumnWidthChanged;
  }

  private void DataGrid_Sorting(object sender, DataGridSortingEventArgs e)
  {
    if (sender is DataGrid dataGrid)
    {
      e.Handled = true; // Prevent the default sorting

      ListSortDirection direction = (e.Column.SortDirection != ListSortDirection.Ascending) ?
        ListSortDirection.Ascending : ListSortDirection.Descending;
      SortDescription sortDescription;
      if (e.Column.Header.ToString() == Strings.Name)
        sortDescription = new SortDescription("Caption", direction);
      else
      if (e.Column.Header.ToString() == Strings.Type)
        sortDescription = new SortDescription("Type.Name", direction);
      else
      if (e.Column.Header.ToString() == Strings.Value)
        sortDescription = new SortDescription("ValueString", direction);
      else
        return;

      var collectionView = CollectionViewSource.GetDefaultView(dataGrid.ItemsSource);
      collectionView.SortDescriptions.Clear();
      collectionView.SortDescriptions.Add(sortDescription);
      collectionView.Refresh();

      e.Column.SortDirection = direction;
    }
  }

  private void FrameworkElement_OnSizeChanged(object sender, SizeChangedEventArgs e)
  {
    //Debug.WriteLine($"{sender.GetType().Name}_SizeChanged ({e.NewSize.Width},{e.NewSize.Height})");
  }

  private void DataGrid_Loaded(object sender, RoutedEventArgs e)
  {
    UpdateDataGridWidth();
  }


  private void UpdateDataGridWidth()
  {
    if (DataContext is PropertiesViewModel viewModel)
    {
      double totalWidth = DataGrid.Columns.Sum(column => column.ActualWidth)+DataGrid.RowHeaderActualWidth+2;
      viewModel.DataGridWidth = totalWidth;
      DataGrid.Columns.Last().Width = new DataGridLength(1, DataGridLengthUnitType.Star);
    }
  }

}
