using System.Collections.ObjectModel;

namespace DocxControls;

/// <summary>
/// View model for a paragraph
/// </summary>
public class ParagraphViewModel : ElementViewModel
{
  /// <summary>
  /// Default constructor. Creates a new <see cref="Paragraph"/>
  /// </summary>
  public ParagraphViewModel() : this(new DXW.Paragraph())
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="ParagraphViewModel"/> class.
  /// </summary>
  /// <param name="paragraph"></param>
  public ParagraphViewModel(DXW.Paragraph paragraph) : base(paragraph)
  {
    foreach (var element in paragraph.Elements())
    {
      if (element is DXW.ParagraphProperties properties)
        Properties = new ParagraphPropertiesViewModel(properties);
      else
      {
        ElementViewModel paragraphViewModel = element switch
        {
          DXW.Run run => new RunViewModel(run),
          DXW.BookmarkStart bookmarkStart => new BookmarkStartViewModel(bookmarkStart),
          DXW.BookmarkEnd bookmarkEnd => new BookmarkEndViewModel(bookmarkEnd),
          _ => new ElementViewModel(element)
        };
        Elements.Add(paragraphViewModel);
      }
    }
    Properties ??= new ParagraphPropertiesViewModel(Paragraph.GetProperties());
  }

  /// <summary>
  /// Paragraph element of the document
  /// </summary>
  public DXW.Paragraph Paragraph => (DXW.Paragraph)Element;

  /// <summary>
  /// Paragraph properties view model
  /// </summary>
  public ParagraphPropertiesViewModel Properties { get; set; }

  /// <summary>
  /// Observable collection of element view models
  /// </summary>
  public ObservableCollection<ElementViewModel> Elements { get; } = new();

}
