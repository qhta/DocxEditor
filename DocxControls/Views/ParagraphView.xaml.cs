using System.Windows.Controls;
using System.Windows.Input;

namespace DocxControls;
/// <summary>
/// Interaction logic for ParagraphView.xaml
/// </summary>
public partial class ParagraphView : UserControl
{
  /// <summary>
  /// Default constructor
  /// </summary>
  public ParagraphView()
  {
    InitializeComponent();
  }

  private void View_OnMouseRightButtonUp(object sender, MouseButtonEventArgs e)
  {
    Executables.ShowProperties(DataContext);
    e.Handled = true;
  }
}
