using System.Windows.Controls;
using System.Windows.Data;

namespace DocxControls;

/// <summary>
/// Interaction logic for PropertiesView.xaml
/// </summary>
public partial class BookmarksView : UserControl
{
  //private NotUniqueNameValidationRule? _validationRule;

  /// <summary>
  /// Default constructor
  /// </summary>
  public BookmarksView()
  {
    InitializeComponent();
    //PropertiesGrid.DataContextChanged += PropertiesGrid_DataContextChanged;
  }

  //private void PropertiesGrid_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
  //{
  //  Debug.WriteLine($"PropertiesGrid_DataContextChanged type is {PropertiesGrid.DataContext?.GetType().ToString() ?? "null"}");
  //  SetValidationRule();
  //  if (PropertiesGrid.DataContext is CustomPropertiesViewModel customPropertiesViewModel)
  //    SubscribeToCollectionChanged(customPropertiesViewModel.RunProperties);
  //}

  //private void SetValidationRule()
  //{
  //  int n = 0;
  //  Debug.WriteLine($"SetValidationRule items count={(DataContext as CustomPropertiesViewModel)?.Count}");
  //  foreach (var column in PropertiesGrid.Columns)
  //  {
  //    Debug.WriteLine($"column {n++} is {column.GetType()}");
  //    if (column is DataGridTextColumn textColumn)
  //    {
  //      Debug.WriteLine($"SetValidationRule for textColumn");
  //      if (textColumn.Binding is Binding binding)
  //      {
  //        foreach (var rule in binding.ValidationRules)
  //        {
  //          if (rule is NotUniqueNameValidationRule notUniqueNameValidationRule)
  //          {
  //            _validationRule = notUniqueNameValidationRule;
  //            _validationRule.Items = PropertiesGrid.ItemsSource;
  //            return;
  //          }
  //        }
  //      }
  //    }
  //  }
  //}

  //private void SubscribeToCollectionChanged(object itemsSource)
  //{
  //  Debug.WriteLine($"SubscribeToCollectionChanged itemsSource is {itemsSource?.GetType().ToString() ?? "null"}");
  //  if (itemsSource is INotifyCollectionChanged notifyCollection)
  //  {
  //    Debug.WriteLine($"Subscribed to collection changed. ItemsSource is {itemsSource.GetType()}");
  //    notifyCollection.CollectionChanged += ItemsSource_CollectionChanged;
  //  }
  //}

  //private void ItemsSource_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
  //{
  //  if (_validationRule != null)
  //  {
  //    Debug.WriteLine($"ItemsSource_CollectionChanged count={PropertiesGrid.ItemsSource.Cast<CustomPropertyViewModel>().Count()}");
  //    _validationRule.Items = PropertiesGrid.ItemsSource;
  //  }
  //}

  //private void PropertiesGrid_OnInitializingNewItem(object sender, InitializingNewItemEventArgs e)
  //{
  //  if (e.NewItem is CustomPropertyViewModel viewModel)
  //  {
  //    if (PropertiesGrid.DataContext is not CustomPropertiesViewModel customPropertiesViewModel)
  //      return;
  //    viewModel.Parent = customPropertiesViewModel;
  //    var name0 = "New property";
  //    var name = name0;
  //    int i = 1;
  //    while (!customPropertiesViewModel.IsValidName(name))
  //      name = name0 + (++i);
  //    viewModel.Name = name;
  //    viewModel.Type = typeof(string);
  //  }
  //}
}