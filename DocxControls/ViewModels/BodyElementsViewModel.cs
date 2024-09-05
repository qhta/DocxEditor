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
  /// Internal Wordprocessing document view model
  /// </summary>
  public DocumentViewModel DocumentViewModel { get; init; }

  /// <summary>
  /// Observable collection of properties
  /// </summary>
  public ObservableCollection<ElementViewModel> Elements { get; } = new();

  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="documentViewModel"></param>
  public BodyElementsViewModel(DocumentViewModel documentViewModel)
  {
    DocumentViewModel = documentViewModel;
    var wordDocument = documentViewModel.WordDocument;
    foreach (var element in wordDocument.GetBody())
    {
      ElementViewModel? bodyElementViewModel = element switch
      {
        DXW.Paragraph paragraph => new ParagraphViewModel(documentViewModel, paragraph),
        DXW.Table table => new TableViewModel(table),
        _ => null
      };
      if (bodyElementViewModel != null)
        Elements.Add(bodyElementViewModel);
    }
  }

}
