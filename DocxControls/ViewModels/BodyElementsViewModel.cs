using System.Collections.ObjectModel;

using DocumentFormat.OpenXml.Packaging;

using Qhta.MVVM;

namespace DocxControls;

/// <summary>
/// View model for the body elements: paragraphs, tables, etc.
/// </summary>
public class BodyElementsViewModel : ViewModel
{

  /// <summary>
  /// Internal Wordprocessing document
  /// </summary>
  public WordprocessingDocument WordDocument { get; init; }

  /// <summary>
  /// Observable collection of properties
  /// </summary>
  public ObservableCollection<ElementViewModel> Elements { get; } = new();

  /// <summary>
  /// Initializes a new instance of the <see cref="BodyElementsViewModel"/> class.
  /// </summary>
  /// <param name="wordDocument"></param>
  public BodyElementsViewModel(WordprocessingDocument wordDocument)
  {
    WordDocument = wordDocument;
    foreach (var element in wordDocument.GetBody())
    {
      ElementViewModel bodyElementViewModel = element switch
      {
        DXW.Paragraph paragraph => new ParagraphViewModel(paragraph),
        DXW.Table table => new TableViewModel(table),
        _ => new ElementViewModel(element)
      };
      Elements.Add(bodyElementViewModel);
    }
  }

}
