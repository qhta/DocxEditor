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
      bookmarkViewModel.IsSelected = true;
    }
  }

  private void Bookmark_ToolTipClosing(object sender, ToolTipEventArgs e)
  {
    if (DataContext is BookmarkStartViewModel bookmarkViewModel)
    {
      bookmarkViewModel.IsSelected = false;
    }
  }
  /// <summary>
  /// Open the properties window.
  /// </summary>
  /// <param name="e"></param>
  protected override void OnMouseRightButtonUp(MouseButtonEventArgs e)
  {
    Executables.ShowProperties(DataContext);
    e.Handled = true;
  }
}
