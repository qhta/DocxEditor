using Microsoft.Win32;
using DocxControls.Views;


namespace DocxControls;


/// <summary>
/// Static class for executing commands.
/// </summary>
public class Application: DA.Application
{
  /// <summary>
  ///  Default private constructor prevents creating instances of the class by other classes.
  /// </summary>
  private Application()
  {
  }

  /// <summary>
  /// Singleton instance of the class.
  /// </summary>
  public static Application Instance { get; } = new();

  /// <summary>
  /// List of opened documents.
  /// </summary>
  public VM.Documents Documents { get; } = new();

  DA.Documents DA.Application.Documents => Documents;

  /// <summary>
  /// List of opened document Windows.
  /// </summary>
  public List<DocumentWindow> DocumentWindows { get; } = new();


  /// <summary>
  /// Execute the OpenFile command.
  /// </summary>
  public void OpenFile()
  {
    // ReSharper disable once UseObjectOrCollectionInitializer
    OpenFileDialog openFileDialog = new ();
    openFileDialog.Filter = "Docx files (*.docx)|*.docx|All files (*.*)|*.*";
    openFileDialog.ShowReadOnly = true;
    if (openFileDialog.ShowDialog() == true)
    {
      string filePath = openFileDialog.FileName;
      var readOnly = openFileDialog.ReadOnlyChecked;
      OpenDocument(filePath, readOnly);
    }
  }

  /// <summary>
  /// OpenDocument a document for viewing/editing.
  /// </summary>
  /// <param name="fileName">The full filename of the document.</param>
  /// <param name="readOnly">True to open the document as read-only. The default value is False.</param>
  /// <param name="visible">True to open the document in a visible window. The default value is True.</param>
  public VM.Document OpenDocument(string fileName, bool readOnly = false, bool visible = true)
  {
    Documents.Open(fileName, readOnly, visible);
    var documentViewModel = Documents.Last();
    if (documentViewModel == null)
      throw new InvalidOperationException($"Document \"{fileName}\" not opened");
    var documentWindow = new DocumentWindow { DataContext = documentViewModel };
    DocumentWindows.Add(documentWindow);
    documentWindow.Owner = System.Windows.Application.Current.MainWindow;
    documentWindow.Show();
    return documentViewModel;
  }

  /// <summary>
  /// OpenDocument a properties window for the sender object.
  /// </summary>
  /// <param name="sender"></param>
  public void ShowProperties(object sender)
  {
    var window = System.Windows.Application.Current.MainWindow;
    if (window == null) return;
    var propertiesWindow = new PropertiesWindow
    {
      Owner = window,
      DataContext = sender
    };
    propertiesWindow.ShowDialog();
  }


  /// <summary>
  /// Creates a new document.
  /// </summary>
  public VM.Document NewDocument()
  {
    Documents.Add();
    var documentViewModel = Documents.Last();
    if (documentViewModel == null)
      throw new InvalidOperationException($"New document not created");
    var documentWindow = new DocumentWindow { DataContext = documentViewModel };
    DocumentWindows.Add(documentWindow);
    documentWindow.Owner = System.Windows.Application.Current.MainWindow;
    documentWindow.Show();
    return documentViewModel;
  }

  /// <summary>
  /// Close all opened documents.
  /// </summary>
  /// <returns></returns>
  public bool CloseAllDocuments()
  {
    foreach (var window in DocumentWindows.ToArray())
    {
      if (!window.CloseDocument())
      {
        return false;
      }
    }
    return true;
  }


}
