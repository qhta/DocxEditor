using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DocxControls;
/// <summary>
/// Interaction logic for BookmarkEndView.xaml
/// </summary>
public partial class BookmarkEndView : UserControl
{
  /// <summary>
  /// Default constructor
  /// </summary>
  public BookmarkEndView()
  {
    InitializeComponent();
    DataContextChanged += BookmarkView_DataContextChanged;
  }

  private void BookmarkView_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
  {
    if (DataContext is BookmarkEndViewModel bookmarkViewModel)
    {
      var id = bookmarkViewModel.Id;
      Foreground = new SolidColorBrush(BaseColors.GetDarkColor(id));
    }
  }

  private void Bookmark_ToolTipOpening(object sender, ToolTipEventArgs e)
  {
    if (DataContext is BookmarkEndViewModel bookmarkViewModel)
    {
      bookmarkViewModel.IsSelected = true;
    }
  }

  private void Bookmark_ToolTipClosing(object sender, ToolTipEventArgs e)
  {
    if (DataContext is BookmarkEndViewModel bookmarkViewModel)
    {
      bookmarkViewModel.IsSelected = false;
    }
  }

}
