using System.Windows;
using System.Windows.Controls;

using Docx.Automation;

using Syncfusion.Windows.Tools.Controls;

namespace DocxControls.Views;

/// <summary>
/// Interaction logic for DocumentWindow.xaml
/// </summary>
public partial class DocumentWindow : UserControl, DA.DocumentWindow, DA.IElement
{

  /// <summary>
  /// Default constructor.
  /// </summary>
  public DocumentWindow()
  {
    InitializeComponent();
    GotFocus += DocumentWindow_GotFocus;
    LostFocus += DocumentWindow_LostFocus;
  }

  private void DocumentWindow_LostFocus(object sender, RoutedEventArgs e)
  {
    Application.Instance.NotifyWindowDeactivated(this);
  }

  private void DocumentWindow_GotFocus(object sender, RoutedEventArgs e)
  {
    Application.ActiveWindow = this;
    Application.Instance.NotifyWindowActivated(this);
  }


  /// <summary>
  /// Access to the application object.
  /// </summary>
  public Application Application => Application.Instance;

  #region IElement implementation

  DA.Application DA.IElement.Application => Application;
  object? DA.IElement.Parent => base.Parent;

  #endregion

  ///// <summary>
  ///// When the window is closing, prompt the user to save changes in open documents.
  ///// </summary>
  ///// <param name="e"></param>
  //protected override void OnClosing(CancelEventArgs e)
  //{
  //  var documentViewModel = DataContext as VM.Document;
  //  if (documentViewModel == null) return;

  //  if (!CloseDocument())
  //  {
  //    e.Cancel = true;
  //  }
  //}

  /// <summary>
  /// Closes the document.
  /// </summary>
  /// <returns></returns>
  public bool CloseDocument()
  {
    var document = DataContext as VM.Document;
    if (document == null) return true;
    if (document.IsEditable && document.IsModified)
    {
      var result = Application.CanCloseDocument(document, true);
      if (result.Break)
      {
        return false;
      }
      document.Close(!result.Cancel);
      Application.Instance.Documents.Remove(document);
    }
    else
    {
      document.Close(false);
      Application.Instance.Documents.Remove(document);
    }
    Application.Instance.DocumentWindows.Remove(this);
    return true;
  }
  DA.Document DA.DocumentWindow.Document => (DataContext as DA.Document)!;
  /// <summary>
  /// Returns a Document object associated with the specified window.
  /// </summary>
  public VM.Document Document => (DataContext as VM.Document)!;

  #region DA.Window properties implementation

  /// <summary>
  /// True if the specified window is active.
  /// </summary>
  public bool Active => Application.ActiveWindow == this;

  /// <summary>
  /// Tries to activate the window and sets the Application ActiveWindow property.
  /// </summary>
  /// <returns></returns>
  public bool Activate()
  {
    if (System.Windows.Application.Current.MainWindow is DA.ApplicationWindow mainWindow)
    {
      mainWindow.Activate(this);
      return true;
    }
    return false;
  }

  /// <summary>
  /// Returns or sets the caption text for the window that is displayed in the title bar of the document or application window. 
  /// </summary>
  public string? Caption
  {
    get => Title;
    set => Title = value ?? "";
  }

  /// <summary>
  /// Dependency property for the Title property.
  /// </summary>
  public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
    nameof(Title), typeof(string), typeof(DocumentWindow), new PropertyMetadata(default(string)));


  /// <summary>
  /// Title of the window.
  /// </summary>
  public string Title
  {
    get => (string)GetValue(TitleProperty);
    set => SetValue(TitleProperty, value);
  }

  /// <summary>
  /// Left position of the window in MDI application.
  /// </summary>
  public double Left 
  { 
    get => GetBounds().Left ;
    set => SetBounds(GetBounds() with { X=value });
  }

  /// <summary>
  /// Top position of the window in MDI application.
  /// </summary>
  public double Top
  {
    get => GetBounds().Top;
    set => SetBounds(GetBounds() with { Y = value });
  }

  private Rect GetBounds() => DocumentContainer.GetMDIBounds(this);
  private void SetBounds(Rect value) => DocumentContainer.SetMDIBounds(this, value);

  /// <summary>
  /// Returns the width of the active working area in the specified document window.
  /// </summary>
  public double UsableWidth => DocumentView.ActualWidth;

  /// <summary>
  /// Returns the height of the active working area in the specified document window.
  /// </summary>
  public double UsableHeight => DocumentView.ActualHeight;

  /// <summary>
  /// True if the specified object is visible. Read/write Boolean.
  /// </summary>
  public bool Visible
  {
    get => Visibility == Visibility.Visible;
    set
    {
      if (Visible != value)
      {
        if (value)
        {
          Visibility = Visibility.Visible;
        }
        else
        {
          Visibility = Visibility.Hidden;
        }
      }
    }
  }

  ///// <summary>
  ///// Returns a Panes collection that represents all the window panes for the specified window.
  ///// </summary>
  //public Panes Panes { get; }

  ///// <summary>
  ///// Returns a Pane object that represents the active pane for the specified window.
  ///// </summary>
  //public Pane ActivePane { get; }

  ///// <summary>
  ///// True if a horizontal scroll bar is displayed for the specified window.
  ///// </summary>
  //public bool DisplayHorizontalScrollBar { get; set; }

  ///// <summary>
  ///// True if a vertical scroll bar is displayed for the specified window. Read/write Boolean.
  ///// </summary>
  //public bool DisplayVerticalScrollBar { get; set; }

  ///// <summary>
  ///// True if the vertical scroll bar appears on the left side of the document window.
  ///// </summary>
  //public bool DisplayLeftScrollBar { get; set; }

  ///// <summary>
  ///// True if rulers are displayed for the specified window or pane. Read/write Boolean.
  ///// </summary>
  //public bool DisplayRulers { get; set; }

  ///// <summary>
  ///// True if a vertical ruler is displayed for the specified window or pane. Read/write Boolean.
  ///// </summary>
  //public bool DisplayVerticalRuler { get; set; }

  ///// <summary>
  ///// True if the vertical ruler appears on the right side of the document window in print layout view. Read/write Boolean.
  ///// </summary>
  //public bool DisplayRightRuler { get; set; }

  ///// <summary>
  ///// True if comments, footnotes, endnotes, and hyperlinks are displayed as tips. Read/write Boolean.
  ///// </summary>
  //public bool DisplayScreenTips { get; set; }

  ///// <summary>
  ///// True if the document map is visible. Read/write Boolean.
  ///// </summary>
  //public bool DocumentMap { get; set; }

  ///// <summary>
  ///// True if the email message header is visible in the document window. The default value is False. Read/write Boolean.
  ///// </summary>
  //public bool EnvelopeVisible { get; set; }

  ///// <summary>
  ///// Returns or sets the horizontal scroll position as a percentage of the document width.
  ///// </summary>
  //public int HorizontalPercentScrolled { get; set; }

  ///// <summary>
  ///// Returns or sets the vertical scroll position as a percentage of the document length.
  ///// </summary>
  //public int VerticalPercentScrolled { get; set; }

  ///// <summary>
  ///// Returns the position of an item in a collection. Read-only.
  ///// </summary>
  //public int Index { get; }

  ///// <summary>
  ///// Returns the next document window in the collection of open document windows. 
  ///// </summary>
  //public Window? Next { get; }

  ///// <summary>
  ///// Returns the previous document window in the collection of open document windows. 
  ///// </summary>
  //public Window? Previous { get; }

  ///// <summary>
  ///// Returns a  number that indicates the window handle of the specified window. 
  ///// </summary>
  //public int Hwnd { get; }

  ///// <summary>
  ///// Returns or sets the default start-up mode for the Japanese Input Method Editor (IME). 
  ///// </summary>
  //public IMEMode IMEMode { get; set; }

  ///// <summary>
  ///// Returns the window number of the document displayed in the specified window.
  ///// For example, if the caption of the window is "Sales.doc:2", this property returns the number 2. Read-only Long.
  ///// </summary>
  //public int WindowNumber { get; }

  ///// <summary>
  ///// Returns or sets the state of the specified document window or task window.
  ///// </summary>
  //public WindowState WindowState { get; set; }

  ///// <summary>
  ///// Returns the window type. 
  ///// </summary>
  //public WindowType Type { get; }

  ///// <summary>
  ///// True if the window is split into multiple panes. Read/write Boolean.
  ///// </summary>
  //public bool Split { get; set; }

  ///// <summary>
  ///// Returns or sets the vertical split percentage for the specified window.
  ///// </summary>
  //public int SplitVertical { get; set; }

  ///// <summary>
  ///// Returns or sets the width of the style area in points. 
  ///// </summary>
  //public int StyleAreaWidth { get; set; }

  ///// <summary>
  ///// Returns or sets a WdShowSourceDocuments constant that represents how Microsoft Word displays source documents after a compare and merge process. 
  ///// </summary>
  //public ShowSourceDocuments ShowSourceDocuments { get; set; }

  ///// <summary>
  ///// Returns a View object that represents the view for the specified window.
  ///// </summary>
  //public View View { get; }

  ///// <summary>
  ///// Sets or returns a Boolean that represents whether thumbnail images of the pages in a document are displayed along the left side of the document window.
  ///// </summary>
  //public bool Thumbnails { get; set; }

  ///// <summary>
  ///// Returns the Selection object that represents a selected range or the insertion point. 
  ///// </summary>
  //public Range Selection { get; }
  #endregion

  #region DA.Window methods implementation

  /// <summary>
  /// Sets the focus of the specified document window.
  /// </summary>
  public void SetFocus()
  {
    Activate();
  }

  /// <summary>
  /// Opens a new window with the same document as the specified window. Returns a Window object.
  /// </summary>
  public DocumentWindow OpenNewWindow()
  {
    var document = (VM.Document)DataContext;
    return Application.CreateNewWindow(document);
  }

  /// <summary>
  /// Selects the entire document.
  /// </summary>
  public void SelectAll()
  {
    Document.SelectAll();
  }

  ///// <summary>
  ///// Returns the screen coordinates of the specified range or shape.
  ///// </summary>
  //public void GetPoint();

  ///// <summary>
  ///// Scrolls a window or pane by the specified number of screens.
  ///// </summary>
  ///// <param name="down">The number of screens to scroll down.</param>
  ///// <param name="up">The number of screens to scroll up.</param>
  ///// <param name="toRight">The number of screens to scroll to the right.</param>
  ///// <param name="toLeft">The number of screens to scroll to the left.</param>
  //public void LargeScroll(int? down, int? up, int? toRight, int? toLeft);

  ///// <summary>
  ///// Scrolls a window or pane by the specified number of screens.
  ///// </summary>
  ///// <param name="down">The number of lines to scroll the window down. A "line" corresponds to the distance scrolled by clicking the down scroll arrow on the vertical scroll bar once.</param>
  ///// <param name="up">The number of lines to scroll the window up. A "line" corresponds to the distance scrolled by clicking the up scroll arrow on the vertical scroll bar once.</param>
  ///// <param name="toRight">The number of lines to scroll the window to the right. A "line" corresponds to the distance scrolled by clicking the right scroll arrow on the horizontal scroll bar once.</param>
  ///// <param name="toLeft">The number of lines to scroll the window to the left. A "line" corresponds to the distance scrolled by clicking the left scroll arrow on the horizontal scroll bar once.</param>
  //public void SmallScroll(int? down, int? up, int? toRight, int? toLeft);

  ///// <summary>
  ///// Scrolls a window by the specified number of lines
  ///// </summary>
  ///// <param name="down">The number of pages to scroll down. If this argument is omitted, this value is assumed to be 1.</param>
  ///// <param name="up">The number of pages to scroll up.</param>
  //public void PageScroll(int? down, int? up);

  ///// <summary>
  ///// Prints all or part of the document displayed in the specified window.
  ///// </summary>
  ///// <param name="background">True to print in the background.</param>
  ///// <param name="append">True to append the document to the file if the file already exists.</param>
  ///// <param name="range">The range to be printed. Can be either AllDocument, Selection, CurrentPage, FromTo, or RangeOfPages.</param>
  ///// <param name="outputFileName">If <see cref="printToFile"/> is True, this argument specifies the path and file name of the output file.</param>
  ///// <param name="from">The first page to be printed. If <see cref="range"/> is FromTo, this argument is the first page to be printed.
  ///// If <see cref="range"/> is RangeOfPages, this argument is the first page in the range of pages to be printed.</param>
  ///// <param name="to">The last page to be printed. If Range is FromTo, this argument is the last page to be printed.
  ///// If <see cref="range"/> is RangeOfPages, this argument is the last page in the range of pages to be printed.</param>
  ///// <param name="item">The item to be printed. Can be either DocumentContent, Properties, Comments, Styles, AutoTextEntries, KeyAssignments, Envelope or DocumentWithMarkups.</param>
  ///// <param name="copies">The number of copies to be printed. If this argument is omitted, one copy is printed.</param>
  ///// <param name="pages">The page numbers and page ranges to be printed, separated by commas. For example, "2, 6-10" prints page 2 and pages 6 through 10.</param>
  ///// <param name="pageType">The type of pages to be printed. Can be either AllPages, OddPagesOnly, or EvenPagesOnly.</param>
  ///// <param name="printToFile">True to send printer instructions to a file. Make sure to specify a file name with OutputFileName.</param>
  ///// <param name="collate">When printing multiple copies of a document, True to print all pages of the document before printing the next copy.</param>
  ///// <param name="fileName">The path and file name of the document to be printed. If this argument is omitted, program prints the active document. (Available only with the Application object.)</param>
  ///// <param name="manualDuplexPrint">True to print on both sides of the paper. If the printer does not support duplex printing, this argument is ignored.</param>
  ///// <param name="printZoomColumn">The number of pages to fit horizontally on one page. Can be 1, 2, 3, or 4. Use with the <see cref="printZoomRow"/> argument to print multiple pages on a single sheet.</param>
  ///// <param name="printZoomRow">The number of pages to  fit vertically on one page. Can be 1, 2, or 4. Use with the <see cref="printZoomColumn"/>  argument to print multiple pages on a single sheet.</param>
  ///// <param name="printZoomPaperWidth">The width of the paper to which the document is printed, in twips (20 twips = 1 point; 72 points = 1 inch).</param>
  ///// <param name="printZoomPaperHeight">The height of the paper to which the document is printed, in twips (20 twips = 1 point; 72 points = 1 inch).</param>
  //public void PrintOut(bool? background, bool? append, PrintOutRange? range, string? outputFileName, 
  //  int? from, int? to, PrintOutItem? item, 
  //  int? copies, string? pages, PrintOutPages? pageType, 
  //  bool? printToFile, bool? collate, string? fileName, bool? manualDuplexPrint, 
  //  int? printZoomColumn, int? printZoomRow, int? printZoomPaperWidth, int? printZoomPaperHeight);

  ///// <summary>
  ///// Returns the Range or Shape object that is located at the point specified by the screen position coordinate pair.
  ///// </summary>
  //public void RangeFromPoint(int x, int y);

  ///// <summary>
  ///// Scrolls through the document window so the specified range or shape is displayed in the document window.
  ///// </summary>
  ///// <param name="obj">A Range or Shape object.</param>
  ///// <param name="start">True if the upper-left corner of the range or shape appears in the upper-left corner of the document window.
  ///// False if the lower-right corner of the range or shape appears in the lower-right corner of the document window. The default value is True.</param>
  //public void ScrollIntoView(object obj, bool? start = true);

  ///// <summary>
  ///// If the ribbon is visible, the ToggleRibbon method hides it; if the ribbon is hidden, the ToggleRibbon method shows it.
  ///// </summary>
  //public void ToggleRibbon();

  #endregion

  /// <summary>
  /// Not implemented
  /// </summary>
  /// <returns></returns>
  /// <exception cref="NotImplementedException"></exception>
  public bool Remove()
  {
    return false;
    throw new NotImplementedException();
  }
}
