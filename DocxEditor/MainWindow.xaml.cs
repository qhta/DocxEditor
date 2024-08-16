using System.ComponentModel;
using System.Windows;

using DocumentFormat.OpenXml.Packaging;
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
    var documents = Executables.Documents;
    var documentCount = documents.Count();
    if (documentCount == 0) return;
    var message = documentCount == 1 ? Strings.SaveChangesInDocument : Strings.SaveChangesInDocuments;
    if (documentCount == 1)
    {
      var result = MessageBox.Show(message, Strings.ApplicationClosing, MessageBoxButton.YesNoCancel);
      if (result == MessageBoxResult.Cancel)
      {
        e.Cancel = true;
        return;
      }
      if (result == MessageBoxResult.Yes)
        foreach (var doc in documents)
        {
          doc.Dispose();
        }
    }
  }

  public void SetTitle(string title)
  {
    Title = title;
  }

  public void ShowDocumentView(DocumentView documentView)
  {
    MainPanel.Content = documentView;
  }
}