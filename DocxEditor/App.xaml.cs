using System.Configuration;
using System.Data;
using System.Globalization;
using System.Windows;
using DocumentFormat.OpenXml.Packaging;

using DocxControls;

namespace DocxEditor;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{

  public List<WordprocessingDocument> Documents { get; } = new();


  protected override void OnStartup(StartupEventArgs e)
  {
    base.OnStartup(e);
    Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
  }

  public void OpenDocument(string filePath, bool isEditable)
  {
    var window = Application.Current.MainWindow as MainWindow;
    if (window == null) return;
    var documentView = new DocumentView(filePath, isEditable);
    window.Title = filePath;
    window.MainPanel.Content = documentView;
    Documents.Add(documentView.DocumentViewModel.WordDocument);
  }
}

