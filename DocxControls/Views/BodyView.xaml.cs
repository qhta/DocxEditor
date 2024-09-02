using System.Windows.Controls;
using System.Windows.Input;

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

  /// <summary>
  /// Ope
  /// </summary>
  /// <param name="e"></param>
  protected override void OnMouseRightButtonUp(MouseButtonEventArgs e)
  {
    Executables.ShowProperties(DataContext);
    e.Handled = true;
  }
}
