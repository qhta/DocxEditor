﻿using System.Collections.ObjectModel;

namespace DocxControls;

/// <summary>
/// View model for a paragraph
/// </summary>
public class ParagraphViewModel : ElementViewModel
{
  ///// <summary>
  ///// Default constructor. Creates a new <see cref="Paragraph"/>
  ///// </summary>
  //public ParagraphViewModel() : this(new DXW.Paragraph())
  //{
  //}

  /// <summary>
  /// Internal Wordprocessing document view model
  /// </summary>
  public DocumentViewModel DocumentViewModel { get; init; }

  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="documentViewModel"></param>
  /// <param name="paragraph"></param>
  public ParagraphViewModel(DocumentViewModel documentViewModel, DXW.Paragraph paragraph) : base(paragraph)
  {
    DocumentViewModel = documentViewModel;
    foreach (var element in paragraph.Elements())
    {
      if (element is DXW.ParagraphProperties properties)
        ParagraphProperties = new ParagraphPropertiesViewModel(properties);
      else
      {
        ElementViewModel? paragraphViewModel = element switch
        {
          DXW.Run run => new RunViewModel(run),
          DXW.BookmarkStart bookmarkStart => DocumentViewModel.Bookmarks.RegisterBookmarkStart(bookmarkStart),
          DXW.BookmarkEnd bookmarkEnd => DocumentViewModel.Bookmarks.RegisterBookmarkEnd(bookmarkEnd),
          _ => null
        };
        if (paragraphViewModel != null)
          Elements.Add(paragraphViewModel);
        else
          Debug.WriteLine($"ParagraphViewModel: Element {element.GetType().Name} not supported");
      }
    }
    ParagraphProperties ??= new ParagraphPropertiesViewModel(Paragraph.GetProperties());
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
