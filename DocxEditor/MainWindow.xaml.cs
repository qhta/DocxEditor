using System.ComponentModel;
using System.Windows;

using DocxControls;
using DocxControls.Resources;

namespace DocxEditor;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window, IMainWindow
{

  public MainWindow()
  {
    InitializeComponent();
  }

  private void New_Click(object sender, RoutedEventArgs e)
  {
    Executables.FileNewExecute(this);
  }

  private void Open_Click(object sender, RoutedEventArgs e)
  {
    Executables.FileOpenExecute(this);
  }

  private void Exit_Click(object sender, RoutedEventArgs e)
  {
    Application.Current.Shutdown();
  }

  private void Cut_Click(object sender, RoutedEventArgs e)
  {
    MessageBox.Show("Cut clicked");
  }

  private void Copy_Click(object sender, RoutedEventArgs e)
  {
    MessageBox.Show("Copy clicked");
  }

  private void Paste_Click(object sender, RoutedEventArgs e)
  {
    MessageBox.Show("Paste clicked");
  }

  private void About_Click(object sender, RoutedEventArgs e)
  {
    MessageBox.Show("About clicked");
  }

  protected override void OnClosing(CancelEventArgs e)
  {
    foreach (var window in Executables.DocumentWindows.ToArray())
    {
      if (!window.CloseDocument())
      {
        e.Cancel = true;
        return;
      }
    }
  }

  public void OpenDocument(string filePath, bool isEditable)
  {
    var documents = Executables.Documents;

    var documentViewModel = new DocumentViewModel(filePath, isEditable);

    documents.Add(documentViewModel);

    var documentWindow = new DocumentWindow { DataContext = documentViewModel };
    Executables.DocumentWindows.Add(documentWindow);
    documentWindow.Owner = this;
    documentWindow.Show();
  }
}
