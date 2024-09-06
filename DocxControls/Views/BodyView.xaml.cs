using System.Windows.Controls;

namespace DocxControls;
/// <summary>
/// Interaction logic for BodyView.xaml
/// </summary>
public partial class BodyView : UserControl
{
  /// <summary>
  /// Default constructor
  /// </summary>
  public BodyView()
  {
    InitializeComponent();
  }

  private void ListView_ScrollChanged(object sender, ScrollChangedEventArgs e)
  {
    if (e.VerticalOffset == e.ExtentHeight - e.ViewportHeight)
    {
      if (DataContext is DocumentViewModel documentViewModel)
      {
        var viewModel = documentViewModel.BodyElements;
        if (viewModel.LoadMoreCommand.CanExecute(null))
          viewModel.LoadMoreCommand.Execute(null);
      }
    }
  }
}
