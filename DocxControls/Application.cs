using Microsoft.Win32;
using DocxControls.Views;
using Qhta.MVVM;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using Qhta.WPF.Utils;

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
  DA.DocumentWindow? DA.Application.ActiveWindow => ActiveWindow;

  /// <summary>
  /// Returns a Window object that represents the active window (the window with the focus).
  /// May be null if no window is active.
  /// </summary>
  public DocumentWindow? ActiveWindow
  {
    get=> _ActiveWindow;
    set
    {
      if (_ActiveWindow == value) return;
      _ActiveWindow = value;
      NotifyPropertyChanged(nameof(ActiveWindow));
    }
  }
  private DocumentWindow? _ActiveWindow;

  DA.Documents DA.Application.Documents => Documents;
  /// <summary>
  /// List of opened documents.
  /// </summary>
  public VM.Documents Documents { get; } = new();

  DA.DocumentWindows DA.Application.Windows => DocumentWindows;
  /// <summary>
  /// List of opened document Windows.
  /// </summary>
  public VM.DocumentWindows DocumentWindows { get; } = new();
  #endregion

  /// <summary>
  /// Creates a view model for the specified element.
  /// </summary>
  /// <param name="parentViewModel"></param>
  /// <param name="element"></param>
  /// <returns></returns>
  /// <exception cref="InvalidOperationException"></exception>
  public VM.ElementViewModel CreateViewModel(ViewModel parentViewModel, DX.OpenXmlElement element)
  {
    if (element is DXW.Body body)
      return new VM.BodyViewModel(parentViewModel, body);
    if (parentViewModel is not VM.ElementViewModel parentElementViewModel)
      throw new InvalidOperationException($"Parent view model must be a ElementViewModel, but is {parentViewModel.GetType()}");
    return element switch
    {
      DXW.Paragraph paragraph => new VM.ParagraphViewModel(parentElementViewModel, paragraph),
      DXW.Table table => new VM.TableViewModel(parentElementViewModel, table),
      //DXW.TableRow tableRow => new VM.TableRow(null, tableRow),
      //DXW.TableCell tableCell => new VM.TableCell(null, tableCell),
      DXW.Run run => new VM.RunViewModel(parentElementViewModel, run),
      DXW.Text text => new VM.TextViewModel(parentElementViewModel, text),
      DXW.BookmarkStart bookmarkStart => new VM.BookmarkStart(parentElementViewModel.GetDocumentViewModel().Bookmarks, bookmarkStart),
      DXW.BookmarkEnd bookmarkEnd => new VM.BookmarkEnd(parentElementViewModel.GetDocumentViewModel().Bookmarks, bookmarkEnd),
      //DXW.Break breakElement => new VM.Break(null, breakElement),
      //DXW.TabChar tabChar => new VM.TabChar(null, tabChar),
      _ => new VM.ElementViewModel(parentElementViewModel, element)
    };
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
  /// Exit the document.
  /// </summary>
  /// <param name="document"></param>
  /// <returns></returns>
  public bool CloseDocument(VM.Document document)
  {
    var documentWindows = DocumentWindows.AsQueryable<DocumentWindow>().Where(w => w.DataContext == document).ToArray();
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
  /// Exit all opened documents.
  /// </summary>
  /// <returns></returns>
  public bool CloseAllDocuments()
  {
    foreach (var window in DocumentWindows.AsQueryable<DocumentWindow>().ToArray())
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

  void DA.Application.Activate(DA.DocumentWindow window) => Activate((DocumentWindow)window);
  /// <summary>
  /// Activates the specified window.
  /// </summary>
  /// <param name="window"></param>
  public void Activate(DocumentWindow window) => window.Activate();

  DA.DocumentWindow DA.Application.CreateNewWindow(DA.Document document) => CreateNewWindow((VM.Document)document);
  /// <summary>
  /// Creates a new window for existing document.
  /// </summary>
  public DocumentWindow CreateNewWindow(VM.Document document)
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
  public void Exit()
  {
    if (CloseAllDocuments())
    {
      BeforeExit?.Invoke(this, EventArgs.Empty);
    }
  }

  /// <summary>
  /// Selects all elements in the active Window.
  /// </summary>
  public void SelectAll()
  {

  }
  #endregion

  #region local methods ----------------------------------------------------------------------------------------------------------
  #endregion

  #region DA.Application events implementation ------------------------------------------------------------------------------------
  /// <summary>
  /// Occurs when the application is about to exit.
  /// </summary>
  public event EventHandler? BeforeExit;

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
  /// Used to notify listeners that a document window has been activated.
  /// </summary>
  /// <param name="window"></param>
  public void NotifyWindowActivated(DocumentWindow window) => WindowActivated?.Invoke(this, new DA.DocumentWindowEventArgs(window));

  /// <summary>
  /// Occurs when any document window is activated.
  /// </summary>
  public event DA.DocumentWindowEventHandler? WindowActivated;

  /// <summary>
  /// Used to notify listeners that a document window has been deactivated.
  /// </summary>
  /// <param name="window"></param>
  public void NotifyWindowDeactivated(DocumentWindow window) => WindowDeactivated?.Invoke(this, new DA.DocumentWindowEventArgs(window));
  /// <summary>
  /// Occurs when any document window is deactivated.
  /// </summary>
  public event DA.DocumentWindowEventHandler? WindowDeactivated;
  #endregion


}
