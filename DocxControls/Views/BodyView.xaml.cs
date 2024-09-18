using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
      if (DataContext is Document documentViewModel)
      {
        var viewModel = documentViewModel.Body;
        if (viewModel.LoadMoreCommand.CanExecute(null))
        {
          viewModel.LoadMoreCommand.Execute(null);
                    System.Windows.Application.Current.Dispatcher.InvokeAsync(() =>
          {
            var scrollViewer = GetScrollViewer(ListView);
            if (scrollViewer != null)
            {
              scrollViewer.ScrollToEnd();
            }
          });
        }
      }
    }
  }

  private ScrollViewer? GetScrollViewer(DependencyObject depObj)
  {
    if (depObj is ScrollViewer) return depObj as ScrollViewer;

    for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
    {
      var child = VisualTreeHelper.GetChild(depObj, i);
      var result = GetScrollViewer(child);
      if (result != null) return result;
    }
    return null;
  }
}
