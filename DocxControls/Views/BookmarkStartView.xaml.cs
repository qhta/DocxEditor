using System.Windows.Controls;
using System.Windows.Media;
using DocxControls.Helpers;

namespace DocxControls.Views;
/// <summary>
/// Interaction logic for BookmarkStartView.xaml
/// </summary>
public partial class BookmarkStartView : UserControl
{
  /// <summary>
  /// Default constructor
  /// </summary>
  public BookmarkStartView()
  {
    InitializeComponent();
    DataContextChanged += BookmarkView_DataContextChanged;
  }

  private void BookmarkView_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
  {
    if (DataContext is VM.BookmarkStart bookmarkViewModel)
    {
      var id = bookmarkViewModel.Id;
      Foreground = new SolidColorBrush(BaseColors.GetDarkColor(id));
    }
  }

  private void OnToolTipOpening(object sender, ToolTipEventArgs e)
  {
    if (DataContext is VM.ElementViewModel viewModel)
    {
      viewModel.IsHighlighted = true;
    }
  }

  private void OnToolTipClosing(object sender, ToolTipEventArgs e)
  {
    if (DataContext is VM.ElementViewModel viewModel)
    {
      viewModel.IsHighlighted = false;
    }
  }

}
