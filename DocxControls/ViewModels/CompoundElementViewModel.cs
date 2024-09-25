using System.Collections.ObjectModel;
using System.Windows.Input;

using Qhta.MVVM;

namespace DocxControls.ViewModels;

/// <summary>
/// View model for the body elements: paragraphs, tables, etc.
/// </summary>
public class CompoundElementViewModel : ElementViewModel
{

  /// <summary>
  /// Observable collection of properties
  /// </summary>
  public ObservableCollection<ElementViewModel> Elements { get; } = new();

  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="ownerViewModel">Owner view model. Must be <see cref="Document"/></param>
  /// <param name="body">Modeled block element</param>
  public CompoundElementViewModel(ViewModel ownerViewModel, DX.OpenXmlCompositeElement body): base(ownerViewModel, body)
  {
    currentElement = body.FirstChild;
    LoadMoreCommand = new RelayCommand( LoadMoreElements, () => !isLoading && currentElement!=null);
 }

  /// <summary>
  /// Command to load more elements. Used in lazy loading.
  /// </summary>
  public ICommand LoadMoreCommand { get; }

  private const int PageSize = 20;
  private bool isLoading;
  private int currentElementIndex = 0;

  private DX.OpenXmlElement? currentElement;

  /// <summary>
  /// Load all child OpenXmlElements and create their view models
  /// </summary>
  public void LoadAllElements()
  {
    if (isLoading) return;
    isLoading = true;
    Task.Run(() =>
    {
      currentElement = Element?.FirstChild;
      while (currentElement != null)
      {
        var childViewModel = FindViewModel(currentElement);
        if (childViewModel == null)
          CreateChildViewModel(currentElement);
        currentElement = currentElement.NextSibling();
        currentElementIndex++;
      }
      isLoading = false;
    });
  }

  /// <summary>
  /// Load more child OpenXmlElements starting at currentElement and create their view models
  /// </summary>
  public void LoadMoreElements()
  {
    if (isLoading) return;
    isLoading = true;
    Task.Run(() =>
    {
      int i = PageSize;
      while (i-- > 0 && currentElement != null)
      {
        var childViewModel = FindViewModel(currentElement);
        if (childViewModel == null)
          CreateChildViewModel(currentElement);
        currentElement = currentElement.NextSibling();
        currentElementIndex++;
      }
      isLoading = false;
    });
  }

  /// <summary>
  /// Find a ViewModel created for an OpenXmlElement
  /// </summary>
  /// <param name="element"></param>
  /// <returns></returns>
  public virtual ElementViewModel? FindViewModel(DX.OpenXmlElement element)
  {
    var result = Elements.FirstOrDefault(vm => vm.Element == element);
    if (result == null)
    {
      foreach (var vm in Elements)
      {
        if (vm is CompoundElementViewModel childElementViewModel)
        {
          result = childElementViewModel.FindViewModel(element);
          if (result == null)
            break;
        }
      }
    }
    return result;
  }

  /// <summary>
  /// Creates a viewModel for an OpenXmlElement and adds it to elements.
  /// </summary>
  /// <param name="element"></param>
  public virtual void CreateChildViewModel(DX.OpenXmlElement element)
  {
    ElementViewModel childElementViewModel = Application.Instance.CreateViewModel(this, element);
    System.Windows.Application.Current.Dispatcher.Invoke(() =>
    {
      Elements.Add(childElementViewModel);
    });
  }

  #region ISelectable implementation ----------------------------------------------------------------------------------------------------------------

  /// <summary>
  /// Selects the object and its children.
  /// </summary>
  public override void SelectAll()
  {
    Select();
    foreach (var element in Elements)
    {
      element.SelectAll();
    }
  }
  #endregion
}
