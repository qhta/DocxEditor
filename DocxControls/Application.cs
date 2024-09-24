using Microsoft.Win32;
using DocxControls.Views;
using Qhta.MVVM;
using System.Windows;


namespace DocxControls;


/// <summary>
/// Static class for executing commands.
/// </summary>
public class Application : ViewModel, DA.Application
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

  #region DA.Application properties implementation ------------------------------------------------------------------------------------
  /// <summary>
  /// Returns a Window object that represents the active window (the window with the focus).
  /// May be null if no window is active.
  /// </summary>
  public DA.Window? ActiveWindow
  {
    get => _ActiveWindow;
    set
    {
      if (_ActiveWindow == value) return;
      if (_ActiveWindow != null)
        WindowDeactivated?.Invoke(this, new DA.DocumentWindowEventArgs(_ActiveWindow!.Document, _ActiveWindow!));
      else
        _ActiveWindow = value;
      NotifyPropertyChanged(nameof(ActiveWindow));
      if (_ActiveWindow != null)
        WindowActivated?.Invoke(this, new DA.DocumentWindowEventArgs(_ActiveWindow!.Document, _ActiveWindow!));
      DocumentChanged?.Invoke(this, EventArgs.Empty);
    }
  }

  private DA.Window? _ActiveWindow;

  /// <summary>
  /// List of opened documents.
  /// </summary>
  public VM.Documents Documents { get; } = new();

  DA.Documents DA.Application.Documents => Documents;

  /// <summary>
  /// List of opened document Windows.
  /// </summary>
  public List<DocumentWindow> DocumentWindows { get; } = new();
  #endregion

  /// <summary>
  /// Execute the OpenFile command.
  /// </summary>
  public void OpenFile()
  {
    // ReSharper disable once UseObjectOrCollectionInitializer
    OpenFileDialog openFileDialog = new();
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
    DocumentOpened?.Invoke(this, new DA.DocumentEventArgs(documentViewModel));
    DocumentChanged?.Invoke(this, EventArgs.Empty);
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
    var document = Documents.Last();
    if (document == null)
      throw new InvalidOperationException($"New document not created");
    var documentWindow = new DocumentWindow { DataContext = document };
    DocumentWindows.Add(documentWindow);
    documentWindow.Owner = System.Windows.Application.Current.MainWindow;
    documentWindow.Show();
    DocumentCreated?.Invoke(this, new DA.DocumentEventArgs(document));
    DocumentChanged?.Invoke(this, EventArgs.Empty);
    return document;
  }

  /// <summary>
  /// Checks if the document can be closed.
  /// If the document is not editable or not modified, it can be closed.
  /// If the DocumentBeforeClose event is handled, the event is raised.
  /// In other cases, the user is prompted to save changes in the document.
  /// </summary>
  /// <param name="document"></param>
  /// <param name="canBreak"> Specifies whether ask to break all further actions.</param>  /// <returns></returns>
  public DA.CancelArgs CanCloseDocument(VM.Document document, bool canBreak)
  {
    if (!document.IsEditable || !document.IsModified)
    {
      return new DA.CancelArgs();
    }
    if (DocumentBeforeClose != null)
    {
      var args = new DA.DocumentCancelEventArgs(document, canBreak);
      DocumentBeforeClose(this, args);
      return args.Cancel;
    }
    var message = Strings.SaveChangesInDocument;
    var buttons = canBreak ? MessageBoxButton.YesNoCancel : MessageBoxButton.YesNo;
    var result = MessageBox.Show(message, Strings.ApplicationClosing, buttons);
    if (result == MessageBoxResult.No)
      return new DA.CancelArgs { Cancel = true };
    else if (result == MessageBoxResult.Cancel)
      return new DA.CancelArgs { Break = true };
    return new DA.CancelArgs();
  }

  /// <summary>
  /// Close the document.
  /// </summary>
  /// <param name="document"></param>
  /// <returns></returns>
  public bool CloseDocument(VM.Document document)
  {
    var documentWindows = DocumentWindows.Where(w => w.DataContext == document).ToArray();
    foreach (var documentWindow in documentWindows)
    {
      if (!documentWindow.CloseDocument())
      {
        return false;
      }
    }
    return true;
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

  /// <summary>
  /// Checks if the document can be saved.
  /// If the document is not editable or not modified, it can not be saved.
  /// If the DocumentBeforeSave event is handled, the event is raised.
  /// In other cases, the user is prompted to save changes in the document.
  /// </summary>
  /// <param name="document"></param>
  ///  <param name="saveAsUIAs">
  /// True if the Save As dialog box is displayed, whether to save a new document.
  /// </param>
  /// <param name="canBreak"> Specifies whether ask to break all further actions.</param>
  /// <returns></returns>
  public DA.CancelArgs CanSaveDocument(VM.Document document, bool saveAsUIAs, bool canBreak)
  {
    if (!document.IsEditable || !document.IsModified)
    {
      return new DA.CancelArgs();
    }
    if (DocumentBeforeSave != null)
    {
      var args = new DA.DocumentSaveEventArgs(document, saveAsUIAs, canBreak);
      DocumentBeforeSave(this, args);
      return args.Cancel;
    }
    var message = Strings.SaveChangesInDocument;
    var buttons = canBreak ? MessageBoxButton.YesNoCancel : MessageBoxButton.YesNo;
    var result = MessageBox.Show(message, Strings.ApplicationClosing, buttons);
    if (result == MessageBoxResult.No)
      return new DA.CancelArgs { Cancel = true };
    else if (result == MessageBoxResult.Cancel)
      return new DA.CancelArgs { Break = true };
    return new DA.CancelArgs();
  }

  #region DA.Application methods implementation ------------------------------------------------------------------------------------

  void DA.Application.Activate(DA.Window window) => Activate((DocumentWindow)window);
  /// <summary>
  /// Activates the specified window.
  /// </summary>
  /// <param name="window"></param>
  public void Activate(DocumentWindow window) => window.Activate();

  DA.Window DA.Application.NewWindow(DA.Document document) => NewWindow((VM.Document)document);
  /// <summary>
  /// Creates a new window for existing document.
  /// </summary>
  public DocumentWindow NewWindow(VM.Document document)
  {
    var documentWindow = new DocumentWindow { DataContext = document };
    DocumentWindows.Add(documentWindow);
    documentWindow.Owner = System.Windows.Application.Current.MainWindow;
    documentWindow.Show();
    DocumentChanged?.Invoke(this, EventArgs.Empty);
    return documentWindow;
  }

  /// <summary>
  /// Closes all opened documents and quits the application.
  /// </summary>
  public void Quit()
  {
    if (CloseAllDocuments())
    {
      OnQuit?.Invoke(this, EventArgs.Empty);
    }
  }
  #endregion

  #region DA.Application events implementation ------------------------------------------------------------------------------------
  /// <summary>
  /// Occurs when the application is about to quit.
  /// </summary>
  public event EventHandler? OnQuit;

  /// <summary>
  /// Occurs when a new document is created.
  /// </summary>
  public event DA.DocumentEventHandler? DocumentCreated;

  /// <summary>
  /// Occurs when a document is opened.
  /// </summary>
  public event DA.DocumentEventHandler? DocumentOpened;

  /// <summary>
  /// Occurs when a new document is created, when an existing document is opened,
  /// or when another document is made the active document.
  /// </summary>
  public event EventHandler? DocumentChanged;

  /// <summary>
  /// Occurs immediately before any open document is saved.
  /// </summary>
  public event DA.DocumentSaveEventHandler? DocumentBeforeSave;

  /// <summary>
  /// Occurs immediately before any open document closes.
  /// </summary>
  public event DA.DocumentCloseEventHandler? DocumentBeforeClose;


  /// <summary>
  /// Occurs when any document window is activated.
  /// </summary>
  public event DA.DocumentWindowEventHandler? WindowActivated;

  /// <summary>
  /// Occurs when any document window is deactivated.
  /// </summary>
  public event DA.DocumentWindowEventHandler? WindowDeactivated;
  #endregion

}
