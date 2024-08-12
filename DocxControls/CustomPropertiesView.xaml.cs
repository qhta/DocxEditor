using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace DocxControls;

/// <summary>
/// Interaction logic for PropertiesView.xaml
/// </summary>
public partial class CustomPropertiesView : UserControl
{
  /// <summary>
  /// Default constructor
  /// </summary>
  public CustomPropertiesView()
  {
    InitializeComponent();
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