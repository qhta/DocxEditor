using System.Windows;
using DocxControls;
using Microsoft.Win32;

namespace DocxEditor;
public static class Executables
{
  public static void FileNewExecute(object sender)
  {
    MessageBox.Show("New clicked");
  }

  public static void FileOpenExecute(object sender)
  {
    if (sender is not MainWindow window) return;
    // ReSharper disable once UseObjectOrCollectionInitializer
    OpenFileDialog openFileDialog = new ();
    openFileDialog.Filter = "Docx files (*.docx)|*.docx|All files (*.*)|*.*";
    if (openFileDialog.ShowDialog() == true)
    {
      string filePath = openFileDialog.FileName;
      window.Title = filePath;
      var documentView = new DocumentView(filePath, true);
      window.MainPanel.Content = documentView;
    }
  }
}
