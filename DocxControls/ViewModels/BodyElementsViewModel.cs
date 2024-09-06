using System.Collections.ObjectModel;
using System.Windows.Input;

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
    var body = wordDocument.GetBody();
    currentElement = body.FirstChild;
    LoadMoreCommand = new RelayCommand( LoadMoreItems, () => !isLoading && currentElement!=null);
 }

  /// <summary>
  /// Command to load more elements. Used in lazy loading.
  /// </summary>
  public ICommand LoadMoreCommand { get; }

  private const int PageSize = 20;
  private bool isLoading;
  private int currentElementIndex = 0;

  private DX.OpenXmlElement? currentElement;

  private void LoadMoreItems()
  {
    if (isLoading) return;
    isLoading = true;
    Task.Run(() =>
    {
      //Thread.Sleep(100);
      Debug.WriteLine($"LoadMoreItems start");
      int i = PageSize;
      while (i-- > 0 && currentElement != null)
      {
        CreateChildViewModel(currentElement);
        currentElement = currentElement.NextSibling();
        currentElementIndex++;
      }
      Debug.WriteLine($"LoadMoreItems end. CurrentElementIndex = {currentElementIndex}");
      isLoading = false;
    });
  }

  private void CreateChildViewModel(DX.OpenXmlElement element)
  {
    ElementViewModel? bodyElementViewModel = element switch
    {
      DXW.Paragraph paragraph => new ParagraphViewModel(DocumentViewModel, paragraph),
      DXW.Table table => new TableViewModel(table),
      DXW.SectionProperties sectionProperties => new SectionPropertiesViewModel(sectionProperties),
      _ => null
    };
    if (bodyElementViewModel == null)
    {
      //Debug.WriteLine($"BodyElementsViewModel: Element {element.GetType().Name} not supported");
      bodyElementViewModel = new UnknownElementViewModel(element);
    }
    System.Windows.Application.Current.Dispatcher.Invoke(() =>
    {
      Elements.Add(bodyElementViewModel);
    });
  }
}
