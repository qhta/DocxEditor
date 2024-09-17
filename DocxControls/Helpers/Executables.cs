using System.Windows;

using Microsoft.Win32;

namespace DocxControls;

/// <summary>
/// Static class for executing commands.
/// </summary>
public static class Executables
{
  /// <summary>
  /// List of opened documents.
  /// </summary>
  public static List<DocumentViewModel> Documents { get; } = new();

  /// <summary>
  /// List of opened document Windows.
  /// </summary>
  public static List<DocumentWindow> DocumentWindows { get; } = new();


  /// <summary>
  /// Execute the OpenFile command.
  /// </summary>
  public static void OpenFile()
  {
    // ReSharper disable once UseObjectOrCollectionInitializer
    OpenFileDialog openFileDialog = new ();
    openFileDialog.Filter = "Docx files (*.docx)|*.docx|All files (*.*)|*.*";
    openFileDialog.ShowReadOnly = true;
    if (openFileDialog.ShowDialog() == true)
    {
      string filePath = openFileDialog.FileName;
      var editable = !openFileDialog.ReadOnlyChecked;
      OpenDocument(filePath, editable);
    }
  }

  /// <summary>
  /// OpenDocument a document for viewing/editing.
  /// </summary>
  /// <param name="filePath"></param>
  /// <param name="isEditable"></param>
  public static void OpenDocument(string filePath, bool isEditable)
  {
    var documentViewModel = new DocumentViewModel();
    documentViewModel.OpenDocument(filePath, isEditable);
    Documents.Add(documentViewModel);

    var documentWindow = new DocumentWindow { DataContext = documentViewModel };
    DocumentWindows.Add(documentWindow);
    documentWindow.Owner = Application.Current.MainWindow;
    documentWindow.Show();
  }

  /// <summary>
  /// OpenDocument a properties window for the sender object.
  /// </summary>
  /// <param name="sender"></param>
  public static void ShowProperties(object sender)
  {
    var window = Application.Current.MainWindow;
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
  public static void NewDocument()
  {
    var documentViewModel = new DocumentViewModel();
    documentViewModel.NewDocument();
    Documents.Add(documentViewModel);

    var documentWindow = new DocumentWindow { DataContext = documentViewModel };
    DocumentWindows.Add(documentWindow);
    documentWindow.Owner = Application.Current.MainWindow;
    documentWindow.Show();
  }

  /// <summary>
  /// Close all opened documents.
  /// </summary>
  /// <returns></returns>
  public static bool CloseAllDocuments()
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

  /// <summary>
  /// Create a new document in the specified path.
  /// </summary>
  /// <param name="filePath"></param>
  public static void CreateDocument(string filePath)
  {

  }


  /// <summary>
  /// Extract the first paragraphs of each section of the document and creates a new document with them.
  /// </summary>
  /// <param name="sender"></param>
  public static void ExtractFirstParagraphs(object sender)
  {
    // ReSharper disable once UseObjectOrCollectionInitializer
    SaveFileDialog saveFileDialog = new();
    saveFileDialog.Filter = "Docx files (*.docx)|*.docx|All files (*.*)|*.*";
    if (saveFileDialog.ShowDialog() == true)
    {
      string filePath = saveFileDialog.FileName;
      CreateDocument(filePath);
    }
  }
}
