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
  /// Execute the FileNew command.
  /// </summary>
  /// <param name="sender"></param>
  public static void FileNewExecute(object sender)
  {
    MessageBox.Show("FileNew clicked");
  }

  /// <summary>
  /// Execute the FileOpen command.
  /// </summary>
  /// <param name="sender"></param>
  public static void FileOpenExecute(object sender)
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
  /// Open a document for viewing/editing.
  /// </summary>
  /// <param name="filePath"></param>
  /// <param name="isEditable"></param>
  public static void OpenDocument(string filePath, bool isEditable)
  {
    var mainWindow = Application.Current.MainWindow as IMainWindow;
    if (mainWindow == null) return;
    mainWindow.OpenDocument(filePath, isEditable);
  }

  /// <summary>
  /// Open a properties window for the sender object.
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
  /// <param name="sender"></param>
  public static void NewDocument(object sender)
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
