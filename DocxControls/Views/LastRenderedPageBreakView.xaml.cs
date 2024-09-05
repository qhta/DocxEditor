using System.Windows.Controls;

namespace DocxControls;
/// <summary>
/// Interaction logic for LastRenderedPageBreakView.xaml
/// </summary>
public partial class LastRenderedPageBreakView : UserControl
{
  /// <summary>
  /// Default constructor
  /// </summary>
  public LastRenderedPageBreakView()
  {
    InitializeComponent();
  }

  private void OnToolTipOpening(object sender, ToolTipEventArgs e)
  {
    if (DataContext is ElementViewModel viewModel)
    {
      viewModel.IsHighlighted = true;
    }
  }

  private void OnToolTipClosing(object sender, ToolTipEventArgs e)
  {
    if (DataContext is ElementViewModel viewModel)
    {
      viewModel.IsHighlighted = false;
    }
  }

}
