using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace DocxControls;

/// <summary>
///  ComboBox that allows to edit a set o properties.
/// </summary>
public class ObjectViewComboBox : ComboBox
{
  static ObjectViewComboBox()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(ObjectViewComboBox), new FrameworkPropertyMetadata(typeof(ObjectViewComboBox)));
  }

  /// <summary>
  /// Default constructor.
  /// </summary>
  public ObjectViewComboBox()
  {
    Loaded += ComboBox_Loaded;
  }

  /// <summary>
  /// Dependency property for an <see cref="IObjectViewModel"/> object.
  /// </summary>
  public static readonly DependencyProperty ObjectViewModelProperty = DependencyProperty.Register(
    nameof(ObjectViewModel), typeof(IObjectViewModel), typeof(ObjectViewComboBox), new PropertyMetadata(default(object)));

  /// <summary>
  /// Provided view model for the object.
  /// </summary>
  public IObjectViewModel? ObjectViewModel
  {
    get => (IObjectViewModel)GetValue(ObjectViewModelProperty);
    set => SetValue(ObjectViewModelProperty, value);
  }
  private void ComboBox_Loaded(object sender, RoutedEventArgs e)
  {
    if (Template.FindName("Popup_Thumb", this) is Thumb thumb)
    {
      thumb.DragDelta += Thumb_DragDelta;
    }
    if (Template.FindName("PropertiesDataGrid", this) is DataGrid propertiesDataGrid)
    {
      propertiesDataGrid.Sorting += DataGrid_Sorting;
    }
    if (Template.FindName("MembersDataGrid", this) is DataGrid membersDataGrid)
    {
      MembersDataGrid = membersDataGrid;
      membersDataGrid.Sorting += DataGrid_Sorting;
      membersDataGrid.LoadingRow += MembersDataGrid_LoadingRow;
    }
    if (Template.FindName("PopupGrid", this) is Grid grid)
    {
      grid.DataContextChanged += Grid_DataContextChanged;
    }
  }

  private void Grid_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
  {
    if (sender is Grid grid && grid.DataContext is PropertyViewModel propertyViewModel && propertyViewModel.ObjectViewModel is ObjectViewModel objectViewModel)
    {
      if (!objectViewModel.IsContainer)
      {
        //grid.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Star);
        grid.RowDefinitions[1].Height = new GridLength(0);
      }
    }

  }

  private DataGrid? MembersDataGrid;

  private void MembersDataGrid_LoadingRow(object? sender, DataGridRowEventArgs e)
  {
    if (e.Row.IsNewItem)
    {
      e.Row.Header = ">";
      e.Row.Item = new ObjectMemberViewModel { IsNew = true, Collection = (MembersDataGrid?.DataContext as PropertyViewModel)?.ObjectViewModel?.ObjectMembers };
    }
    else
    {
      e.Row.Header = (e.Row.GetIndex() + 1).ToString();
    }
  }

  private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
  {
    if (sender is Thumb thumb && thumb.TemplatedParent is ObjectViewComboBox comboBox)
    {
      if (comboBox.Template.FindName("PART_Popup", comboBox) is Popup popup && popup.Child is FrameworkElement popupChild)
      {
        double newWidth = popupChild.ActualWidth + e.HorizontalChange;
        var minWidth = popupChild.MinWidth;
        if (Double.IsNaN(minWidth))
          minWidth = 0;
        var maxWidth = popupChild.MaxWidth;
        if (Double.IsNaN(maxWidth))
          maxWidth = Double.MaxValue;
        if (newWidth > minWidth && newWidth < maxWidth)
          popupChild.Width = newWidth;

        //double newHeight = popupChild.ActualHeight + e.VerticalChange;
        //var minHeight = popupChild.MinHeight;
        //if (Double.IsNaN(minHeight))
        //  minHeight = 0;
        //var maxHeight = popup.MaxHeight;
        //if (Double.IsNaN(maxHeight))
        //  maxHeight = Double.MaxValue;
        //if (newHeight > minHeight && newHeight < maxHeight)
        //  popupChild.Height = newHeight;
      }
    }
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

}
