using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DocxControls;

public partial class PropertiesView : UserControl
{
  private object? _previouslySelectedItem;

  public PropertiesView()
  {
    InitializeComponent();
  }

  private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
  {
    if (sender is DataGrid dataGrid)
    {
      // Update the bindings of the previously selected item
      if (_previouslySelectedItem != null)
      {
        if (dataGrid.ItemContainerGenerator.ContainerFromItem(_previouslySelectedItem) is DataGridRow previousRow)
        {
          foreach (var column in dataGrid.Columns)
          {
            var cellContent = column.GetCellContent(previousRow);
            if (cellContent != null)
            {
              var textBox = FindVisualChild<TextBox>(cellContent);
              if (textBox != null)
              {
                var bindingExpression = textBox.GetBindingExpression(TextBox.TextProperty);
                if (bindingExpression != null)
                {
                  bindingExpression.UpdateSource();
                }
              }
            }
          }
        }
      }

      // Commit any pending edits
      dataGrid.CommitEdit(DataGridEditingUnit.Row, true);
      _previouslySelectedItem = dataGrid.SelectedItem;
    }
  }

  private void TextBox_KeyDown(object sender, KeyEventArgs e)
  {
    if (e.Key == Key.Enter)
    {
      // Find the parent DataGridCell
      if (sender is TextBox textBox)
      {
        var dataGridCell = FindParent<DataGridCell>(textBox);
        if (dataGridCell != null)
        {
          var bindingExpression = textBox.GetBindingExpression(TextBox.TextProperty);
          if (bindingExpression != null)
          {
            bindingExpression.UpdateSource();
          }

          // Commit the edit
          dataGridCell.IsEditing = false;
          var dataGrid = FindParent<DataGrid>(dataGridCell);
          if (dataGrid != null)
          {
            dataGrid.CommitEdit(DataGridEditingUnit.Cell, true);
          }
        }
      }
    }
  }

  private T? FindParent<T>(DependencyObject? child) where T : DependencyObject
  {
    DependencyObject? parentObject = VisualTreeHelper.GetParent(child!);

    if (parentObject == null) return null;

    if (parentObject is T parent)
      return parent;
    return FindParent<T>(parentObject);
  }

  private T? FindVisualChild<T>(DependencyObject? parent) where T : DependencyObject
  {
    if (parent == null) return null;

    for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
    {
      var child = VisualTreeHelper.GetChild(parent, i);
      if (child is T tChild)
      {
        return tChild;
      }

      var result = FindVisualChild<T>(child);
      if (result != null)
      {
        return result;
      }
    }
    return null;
  }
}