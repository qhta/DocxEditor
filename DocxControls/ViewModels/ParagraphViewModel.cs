using System.Collections.ObjectModel;

namespace DocxControls.ViewModels;

/// <summary>
/// View model for a paragraph
/// </summary>
public class ParagraphViewModel : ElementViewModel
{



  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="parentViewModel">Parent view model. Must be <see cref="BlockElementViewModel"/></param>
  /// <param name="paragraph"></param>
  public ParagraphViewModel
    (BlockElementViewModel parentViewModel, DXW.Paragraph paragraph) : base(parentViewModel, paragraph)
  {
    var DocumentViewModel = parentViewModel.GetDocumentViewModel();
    foreach (var element in paragraph.Elements())
    {
      if (element is DXW.ParagraphProperties properties)
        ParagraphProperties = new ParagraphPropertiesViewModel(this, properties);
      else
      {
        ElementViewModel? elementViewModel = element switch
        {
          DXW.Run run => new RunViewModel(this, run),
          DXW.BookmarkStart bookmarkStart => DocumentViewModel.Bookmarks.RegisterBookmarkStart(bookmarkStart),
          DXW.BookmarkEnd bookmarkEnd => DocumentViewModel.Bookmarks.RegisterBookmarkEnd(bookmarkEnd),
          _ => null
        };
        if (elementViewModel == null)
        {
          Debug.WriteLine($"ParagraphViewModel: Element {element.GetType().Name} not supported");
          elementViewModel = new UnknownElementViewModel(this, element);
        }
        Elements.Add(elementViewModel);
      }
    }
    ParagraphProperties ??= new ParagraphPropertiesViewModel(this, Paragraph.GetProperties());
  }



  /// <summary>
  /// Paragraph element of the document
  /// </summary>
  public DXW.Paragraph Paragraph => (DXW.Paragraph)Element!;

  /// <summary>
  /// Paragraph properties view model
  /// </summary>
  public ParagraphPropertiesViewModel? ParagraphProperties { get; set; }

  /// <summary>
  /// Observable collection of element view models
  /// </summary>
  public ObservableCollection<ElementViewModel> Elements { get; } = new();

  ///// <summary>
  ///// Initializes the object properties
  ///// </summary>
  //protected override ObjectPropertiesViewModel InitObjectProperties()
  //{
  //  var objectProperties = base.InitObjectProperties();
  //  objectProperties.Remove("ParagraphProperties");
  //  AddMoreObjectProperties(objectProperties);
  //  return objectProperties;
  //}

  ///// <summary>
  ///// Initializes the object properties
  ///// </summary>
  //protected void AddMoreObjectProperties(ObjectPropertiesViewModel objectProperties)
  //{
  //  if (ParagraphProperties != null)
  //  {
  //    foreach (var property in ParagraphProperties.ObjectProperties.Items)
  //    {
  //      objectProperties.Add(property);
  //    }
  //  }
  //}
}
