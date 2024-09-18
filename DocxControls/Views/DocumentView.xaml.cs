using System.Windows.Controls;

namespace DocxControls;
/// <summary>
/// Interaction logic for DocumentView.xaml
/// </summary>
public partial class DocumentView : UserControl
{
  /// <summary>
  /// Default constructor
  /// </summary>
  public DocumentView()
  {
    InitializeComponent();
  }

  /// <summary>
  /// Initialization constructor
  /// </summary>
  /// <param name="filePath"></param>
  /// <param name="isEditable"></param>
  public DocumentView(string filePath, bool isEditable)
  {
    InitializeComponent();
    // ReSharper disable once ObjectCreationAsStatement
    Open(filePath, isEditable);
  }

  /// <summary>
  /// View model for the document
  /// </summary>
  public Document DocumentViewModel { get; set; } = null!;

  /// <summary>
  /// OpenDocument a document for viewing/editing.
  /// </summary>
  /// <param name="filePath"></param>
  /// <param name="isEditable"></param>
  public void Open(string filePath, bool isEditable)
  {
    DocumentViewModel = new Document();
    DocumentViewModel.OpenDocument(filePath, isEditable);
    DataContext = DocumentViewModel;
  }

}
