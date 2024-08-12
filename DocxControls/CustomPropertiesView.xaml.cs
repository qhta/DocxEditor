using System.Windows.Controls;
using System.Windows.Data;

namespace DocxControls;

/// <summary>
/// Interaction logic for PropertiesView.xaml
/// </summary>
public partial class CustomPropertiesView : UserControl
{
  private NotUniqueNameValidationRule? _validationRule;

  /// <summary>
  /// Default constructor
  /// </summary>
  public CustomPropertiesView()
  {
    InitializeComponent();
    PropertiesGrid.DataContextChanged += PropertiesGrid_DataContextChanged;
  }

  private void PropertiesGrid_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
  {
    Debug.WriteLine($"PropertiesGrid_DataContextChanged type is {PropertiesGrid.DataContext?.GetType().ToString() ?? "null"}");
    SetValidationRule();
    if (PropertiesGrid.DataContext is CustomPropertiesViewModel customPropertiesViewModel)
      SubscribeToCollectionChanged(customPropertiesViewModel.Properties);
  }

  private void SetValidationRule()
  {
    Debug.WriteLine($"SetValidationRule items count={(DataContext as CustomPropertiesViewModel)?.Count}");
    if (PropertiesGrid.Columns[0] is DataGridTextColumn textColumn)
    {
      Debug.WriteLine($"SetValidationRule for textColumn");
      if (textColumn.Binding is Binding binding)
      {
        var validationRule = _validationRule = new NotUniqueNameValidationRule
        {
          Items = PropertiesGrid.ItemsSource
        };
        binding.ValidationRules.Add(validationRule);
      }
      else
      {
        Debug.WriteLine($"textColumn.Binding is {textColumn.Binding?.GetType().Name ?? "null"}");
      }
    }
    else
    {
      Debug.WriteLine($"PropertiesGrid.Columns[0] is {PropertiesGrid.Columns[0].GetType()}");
    }
  }

  private void SubscribeToCollectionChanged(object itemsSource)
  {
    Debug.WriteLine($"SubscribeToCollectionChanged itemsSource is {itemsSource?.GetType().ToString() ?? "null"}");
    if (itemsSource is INotifyCollectionChanged notifyCollection)
    {
      Debug.WriteLine($"Subscribed to collection changed. ItemsSource is {itemsSource.GetType()}");
      notifyCollection.CollectionChanged += ItemsSource_CollectionChanged;
    }
  }

  private void ItemsSource_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
  {
    if (_validationRule != null)
    {
      Debug.WriteLine($"ItemsSource_CollectionChanged count={PropertiesGrid.ItemsSource.Cast<CustomPropertyViewModel>().Count()}");
      _validationRule.Items = PropertiesGrid.ItemsSource;
    }
  }

  private void PropertiesGrid_OnInitializingNewItem(object sender, InitializingNewItemEventArgs e)
  {
    if (e.NewItem is CustomPropertyViewModel viewModel)
    {
      if (PropertiesGrid.DataContext is not CustomPropertiesViewModel customPropertiesViewModel)
        return;
      var name0 = "New property";
      var name = name0;
      int i = 1;
      while (!customPropertiesViewModel.IsValidName(name))
        name = name0 + (++i);
      viewModel.Name = name;
      viewModel.Type = typeof(string);
    }
  }
}