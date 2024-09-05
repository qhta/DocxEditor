using System.Windows.Controls;

namespace DocxControls;
/// <summary>
/// Interaction logic for SectionPropertiesView.xaml
/// </summary>
public partial class SectionPropertiesView : UserControl
{
  /// <summary>
  /// Default constructor
  /// </summary>
  public SectionPropertiesView()
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
