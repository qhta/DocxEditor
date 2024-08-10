using System.ComponentModel;
using System.Windows;

using DocumentFormat.OpenXml.Packaging;

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
    base.OnClosing(e);
    foreach (var doc in (Application.Current as App)!.Documents)
    {
      doc.Dispose();
    }
  }
}