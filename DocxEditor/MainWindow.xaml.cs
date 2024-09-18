using System.ComponentModel;
using System.Windows;

using DocxControls;

namespace DocxEditor;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{

  public MainWindow()
  {
    InitializeComponent();
  }

  private DocxControls.Application Application => DocxControls.Application.Instance;

  private void New_Click(object sender, RoutedEventArgs e)
  {
        Application.NewDocument();
  }

  private void Open_Click(object sender, RoutedEventArgs e)
  {
        Application.OpenFile();
  }

  private void Exit_Click(object sender, RoutedEventArgs e)
  {
    if (Application.CloseAllDocuments())
            System.Windows.Application.Current.Shutdown();
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
     if (!Application.CloseAllDocuments())
        e.Cancel = true;
  }


}
