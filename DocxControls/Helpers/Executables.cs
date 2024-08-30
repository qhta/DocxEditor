using System.Windows;
using System.Windows.Input;

using DocumentFormat.OpenXml.Packaging;

using Microsoft.Win32;
using Qhta.MVVM;

namespace DocxControls;

/// <summary>
/// Static class for executing commands.
/// </summary>
public static class Executables
{
  /// <summary>
  /// List of opened documents.
  /// </summary>
  public static List<WordprocessingDocument> Documents { get; } = new();


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
    if (openFileDialog.ShowDialog() == true)
    {
      string filePath = openFileDialog.FileName;
      OpenDocument(filePath, true);
    }
  }

  /// <summary>
  /// Open a document for viewing/editing.
  /// </summary>
  /// <param name="filePath"></param>
  /// <param name="isEditable"></param>
  public static void OpenDocument(string filePath, bool isEditable)
  {
    var window = Application.Current.MainWindow as IMainWindow;
    if (window == null) return;
    var documentView = new DocumentView(filePath, isEditable);

    Documents.Add(documentView.DocumentViewModel.WordDocument);
    window.SetTitle(filePath);
    window.ShowDocumentView(documentView);
  }

}
