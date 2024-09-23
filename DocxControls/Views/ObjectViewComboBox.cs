using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

using DocxControls.Helpers;


namespace DocxControls.Views;

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
    DropDownOpened += ObjectViewComboBox_DropDownOpened;
    DropDownClosed += ObjectViewComboBox_DropDownClosed;
  }

  private void ObjectViewComboBox_DropDownOpened(object? sender, EventArgs e)
  {
    if (DataContext is VM.PropertyViewModel propertyViewModel)
    {
      if (propertyViewModel.IsObject)
      {
        if (propertyViewModel.ObjectViewModel == null && propertyViewModel.OriginalType != null)
        {
          propertyViewModel.ObjectViewModel = new VM.ObjectViewModel(propertyViewModel.OriginalType, null)
          { IsNew = true };
        }
      }
    }

  }

  private void ObjectViewComboBox_DropDownClosed(object? sender, EventArgs e)
  {
    if (DataContext is VM.PropertyViewModel propertyViewModel)
    {
      if (propertyViewModel.IsObject)
      {
        if (propertyViewModel.ObjectViewModel != null && propertyViewModel.ObjectViewModel.IsEmpty)
        {
          propertyViewModel.ObjectViewModel = null;
        }
      }
    }
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
    SizeChanged += FrameworkElement_OnSizeChanged;
    if (Template.FindName("ToggleButton", this) is ToggleButton toggleButton)
    {
      ToggleButton = toggleButton;
      ToggleButton.Focusable = true;
      ToggleButton.PreviewKeyUp += ToggleButton_KeyDown;
    }
    if (Template.FindName("DeleteButton", this) is Button deleteButton)
    {
      DeleteButton = deleteButton;
      DeleteButton.Click += DeleteButton_Click;
    }
    if (Template.FindName("TextBlock", this) is TextBlock textBlock)
    {
      TextBlock = textBlock;
    }
    if (Template.FindName("Popup_Thumb", this) is Thumb thumb)
    {
      thumb.DragDelta += Thumb_DragDelta;
    }
    if (Template.FindName("PropertiesDataGrid", this) is DataGrid propertiesDataGrid)
    {
      PropertiesDataGrid = propertiesDataGrid;
      PropertiesDataGrid.Sorting += DataGrid_Sorting;
      PropertiesDataGrid.LoadingRow += PropertiesDataGrid_LoadingRow;
      PropertiesDataGrid.Loaded += (s, e) => UpdateDataGridWidth(PropertiesDataGrid);
    }
    if (Template.FindName("MembersDataGrid", this) is DataGrid membersDataGrid)
    {
      MembersDataGrid = membersDataGrid;
      MembersDataGrid.Sorting += DataGrid_Sorting;
      MembersDataGrid.LoadingRow += MembersDataGrid_LoadingRow;
      MembersDataGrid.Loaded += (s, e) => UpdateDataGridWidth(MembersDataGrid);
    }
    if (Template.FindName("PopupGrid", this) is Grid grid)
    {
      grid.DataContextChanged += Grid_DataContextChanged;
    }
  }

  private void ToggleButton_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
  {
    if (e.Key == System.Windows.Input.Key.Enter)
    {
      if (Template.FindName("Popup", this) is Popup popup)
      {
        popup.IsOpen = !popup.IsOpen;
      }
    }
    else
    if (e.Key == System.Windows.Input.Key.Escape)
    {
      if (Template.FindName("Popup", this) is Popup popup)
      {
        popup.IsOpen = false;
      }
    }
    else
    if (e.Key == System.Windows.Input.Key.F2)
    {
      if (Template.FindName("Popup", this) is Popup popup)
      {
        popup.IsOpen = true;
      }
    }
    else
    if (e.Key == System.Windows.Input.Key.Delete)
    {
      DeleteButton_Click(sender, new RoutedEventArgs());
    }
  }

  private void DeleteButton_Click(object sender, RoutedEventArgs e)
  {
    if (DataContext is VM.PropertyViewModel propertyViewModel)
    {
      if (propertyViewModel.IsObject)
      {
        if (propertyViewModel.ObjectViewModel != null)
        {
          propertyViewModel.ObjectViewModel = null;
        }
        if (propertyViewModel.Value != null)
        {
          propertyViewModel.Value = null;
        }
      }
    }
  }

  private void FrameworkElement_OnSizeChanged(object sender, SizeChangedEventArgs e)
  {
    //Debug.WriteLine($"{sender.GetType().Name}_SizeChanged ({e.NewSize.Width},{e.NewSize.Height})");
  }

  private void Grid_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
  {
    if (sender is Grid grid && grid.DataContext is VM.PropertyViewModel propertyViewModel && propertyViewModel.ObjectViewModel is VM.ObjectViewModel objectViewModel)
    {
      if (!objectViewModel.IsContainer)
      {
        //grid.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Star);
        grid.RowDefinitions[1].Height = new GridLength(0);
      }
    }
  }

  private ToggleButton? ToggleButton;
  private Button? DeleteButton;
  private TextBlock? TextBlock;
  private DataGrid? PropertiesDataGrid;
  private DataGrid? MembersDataGrid;


  private void PropertiesDataGrid_LoadingRow(object? sender, DataGridRowEventArgs e)
  {
    //    Debug.WriteLine($"e.Row.DataContext={e.Row.DataContext}");
    if (e.Row.DataContext is VM.ObjectPropertyViewModel objectProperty && objectProperty.Caption == "RunFonts")
    {
      //Debug.WriteLine($"objectProperty.Caption={objectProperty.Caption}");
      //Debug.WriteLine($"objectProperty.OriginalProperty={objectProperty.OriginalProperty?.Name} objectProperty.ViewModelProperty={objectProperty.ViewModelProperty?.Name}");
    }
  }

  private void MembersDataGrid_LoadingRow(object? sender, DataGridRowEventArgs e)
  {
    if (e.Row.IsNewItem)
    {
      e.Row.Header = ">";
      var propertyViewModel = MembersDataGrid?.DataContext as VM.PropertyViewModel;
      //Debug.WriteLine($"{this}.MembersDataGrid_LoadingRow propertyViewModel = {propertyViewModel}");
      var objectViewModel = propertyViewModel?.ObjectViewModel as VM.ObjectViewModel;
      //Debug.WriteLine($"{this}.MembersDataGrid_LoadingRow objectViewModel = {objectViewModel}");
      var memberType = objectViewModel?.ObjectType;
      //Debug.WriteLine($"{this}.MembersDataGrid_LoadingRow memberType = {memberType}");
      var collection = objectViewModel?.ObjectMembers;
      //Debug.WriteLine($"{this}.MembersDataGrid_LoadingRow collection = {collection}");
      //var allowedTypes = collection?.AllowedMemberTypes;
      e.Row.Item = new VM.ObjectMemberViewModel { Owner = objectViewModel, MemberType = memberType, IsNew = true, Collection = collection };
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
        sortDescription = new SortDescription("ValueType.Name", direction);
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

  private void UpdateDataGridWidth(DataGrid dataGrid)
  {
    // ReSharper disable once InvertIf
    if (dataGrid.DataContext is IObjectViewModelProvider viewModel)
    {
      double totalWidth = dataGrid.Columns.Sum(column => column.ActualWidth) + dataGrid.RowHeaderActualWidth + 20;
      if (viewModel.ObjectViewModel != null)
      {
        if (!double.IsNaN(viewModel.ObjectViewModel.DataGridWidth))
          if (totalWidth < viewModel.ObjectViewModel.DataGridWidth)
            totalWidth = viewModel.ObjectViewModel.DataGridWidth;
        viewModel.ObjectViewModel.DataGridWidth = totalWidth;
      }
      dataGrid.Columns.Last().Width = new DataGridLength(1, DataGridLengthUnitType.Star);
    }
  }
}
