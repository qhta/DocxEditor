using System.Collections.ObjectModel;

namespace DocxControls;

/// <summary>
/// View model for a paragraph
/// </summary>
public class ParagraphViewModel : ElementViewModel
{

  /// <summary>
  /// Internal Wordprocessing document view model
  /// </summary>
  public DocumentViewModel DocumentViewModel { get; init; }

  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="ownerViewModel">Owner view model. Must be <see cref="BodyElementsViewModel"/></param>
  /// <param name="paragraph"></param>
  public ParagraphViewModel(BodyElementsViewModel ownerViewModel, DXW.Paragraph paragraph) : base(ownerViewModel, paragraph)
  {
    DocumentViewModel = ownerViewModel.GetDocumentViewModel();
    foreach (var element in paragraph.Elements())
    {
      if (element is DXW.ParagraphProperties properties)
        ParagraphProperties = new ParagraphPropertiesViewModel(this, properties);
      else
      {
        ElementViewModel? paragraphViewModel = element switch
        {
          DXW.Run run => new RunViewModel(this, run),
          DXW.BookmarkStart bookmarkStart => DocumentViewModel.Bookmarks.RegisterBookmarkStart(bookmarkStart),
          DXW.BookmarkEnd bookmarkEnd => DocumentViewModel.Bookmarks.RegisterBookmarkEnd(bookmarkEnd),
          _ => null
        };
        if (paragraphViewModel == null)
        {
          //Debug.WriteLine($"ParagraphViewModel: Element {element.GetType().Name} not supported");
          paragraphViewModel = new UnknownElementViewModel(this, element);
        }
        Elements.Add(paragraphViewModel);
      }
    }
    ParagraphProperties ??= new ParagraphPropertiesViewModel(this, Paragraph.GetProperties());
  }



  /// <summary>
  /// Paragraph element of the document
  /// </summary>
  public DXW.Paragraph Paragraph => (DXW.Paragraph)Element;

  /// <summary>
  /// Paragraph properties view model
  /// </summary>
  public ParagraphPropertiesViewModel ParagraphProperties { get; set; }

  /// <summary>
  /// Observable collection of element view models
  /// </summary>
  public ObservableCollection<ElementViewModel> Elements { get; } = new();

  /// <summary>
  /// Initializes the object properties
  /// </summary>
  protected override void InitObjectProperties()
  {
  }
}
