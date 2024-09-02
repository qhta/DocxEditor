using System.Windows.Controls;

namespace DocxControls;
/// <summary>
/// Interaction logic for DocumentPropertiesView.xaml
/// </summary>
public partial class DocumentPropertiesView : UserControl
{
  /// <summary>
  /// Default constructor
  /// </summary>
  public DocumentPropertiesView()
  {
    InitializeComponent();
    DataContextChanged += DocumentPropertiesView_DataContextChanged;
  }

  private void DocumentPropertiesView_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
  {
    //Debug.WriteLine($"DataContextChanged({DataContext})");
  }
}
