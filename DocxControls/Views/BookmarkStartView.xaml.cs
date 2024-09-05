using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DocxControls;
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
    if (DataContext is BookmarkStartViewModel bookmarkViewModel)
    {
      var id = bookmarkViewModel.Id;
      Foreground = new SolidColorBrush(BaseColors.GetDarkColor(id));
    }
  }

  private void Bookmark_ToolTipOpening(object sender, ToolTipEventArgs e)
  {
    if (DataContext is BookmarkStartViewModel bookmarkViewModel)
    {
      bookmarkViewModel.IsHighlighted = true;
    }
  }

  private void Bookmark_ToolTipClosing(object sender, ToolTipEventArgs e)
  {
    if (DataContext is BookmarkStartViewModel bookmarkViewModel)
    {
      bookmarkViewModel.IsHighlighted = false;
    }
  }

}
