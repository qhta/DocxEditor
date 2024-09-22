using System.ComponentModel;
using System.Windows;



namespace DocxControls.Views;
/// <summary>
/// Interaction logic for DocumentWindow.xaml
/// </summary>
public partial class DocumentWindow : Window
{
  /// <summary>
  /// Default constructor.
  /// </summary>
  public DocumentWindow()
  {
    InitializeComponent();
  }

  /// <summary>
  /// When the window is closing, prompt the user to save changes in open documents.
  /// </summary>
  /// <param name="e"></param>
  protected override void OnClosing(CancelEventArgs e)
  {
    var documentViewModel = DataContext as VM.Document;
    if (documentViewModel == null) return;

    if (!CloseDocument())
    {
      e.Cancel = true;

    }
  }

  /// <summary>
  /// Prompts the user to save changes in the document.
  /// </summary>
  /// <returns></returns>
  public bool CloseDocument()
  {
    var documentViewModel = DataContext as VM.Document;
    if (documentViewModel == null) return true;
    if (documentViewModel.IsEditable && documentViewModel.IsModified)

    {
      var message = Strings.SaveChangesInDocument;
      var result = MessageBox.Show(message, Strings.ApplicationClosing, MessageBoxButton.YesNoCancel);
      if (result == MessageBoxResult.Cancel)
      {
        return false;
      }
      documentViewModel.Close(result == MessageBoxResult.Yes);
      Application.Instance.Documents.Remove(documentViewModel);
    }
    else
    {
      documentViewModel.Close(false);
      Application.Instance.Documents.Remove(documentViewModel);
    }
    Application.Instance.DocumentWindows.Remove(this);
    return true;
  }
}
