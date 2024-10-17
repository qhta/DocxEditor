using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DocxControls.ViewModels;

/// <summary>
/// View model for the body elements: paragraphs, tables, etc.
/// </summary>
public abstract class CompoundElementViewModel : ElementViewModel
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
  protected CompoundElementViewModel(ViewModel ownerViewModel, DX.OpenXmlCompositeElement body) : base(ownerViewModel, body)
  {
    Elements.CollectionChanged += (sender, e) =>
    {
      if (e.Action == NotifyCollectionChangedAction.Add)
      {
        foreach (ElementViewModel element in e.NewItems!)
        {
          element.PropertyChanged += ChildElementViewModel_PropertyChanged;
        }
      }
      else if (e.Action == NotifyCollectionChangedAction.Remove)
      {
        foreach (ElementViewModel element in e.OldItems!)
        {
          element.PropertyChanged -= ChildElementViewModel_PropertyChanged;
          element.Remove();
        }
      }
      CurrentElementIndex = 0;
    };
    currentElement = body.FirstChild;
    LoadMoreCommand = new RelayCommand(LoadMoreElements, () => !IsLoading && currentElement != null);
  }

  /// <summary>
  /// Command to load more elements. Used in lazy loading.
  /// </summary>
  public ICommand LoadMoreCommand { get; }

  /// <summary>
  /// Count of loaded element at one time in <see cref="LoadMoreElements"/>
  /// </summary>
  protected const int PageSize = 20;
  /// <summary>
  /// Flag set if the elements are loaded
  /// </summary>
  protected bool IsLoading;
  /// <summary>
  /// Index of first element to load
  /// </summary>
  protected int CurrentElementIndex;

  private DX.OpenXmlElement? currentElement;

  /// <summary>
  /// Load all child OpenXmlElements and create their view models
  /// </summary>
  public virtual void LoadAllElements()
  {
    if (IsLoading) return;
    IsLoading = true;
    Task.Run(() =>
    {
      currentElement = ModeledElement?.FirstChild;
      while (currentElement != null)
      {
        var childViewModel = FindViewModel(currentElement);
        if (childViewModel == null)
          CreateChildViewModel(currentElement);
        currentElement = currentElement.NextSibling();
        CurrentElementIndex++;
      }
      IsLoading = false;
    });
  }

  /// <summary>
  /// Load more child OpenXmlElements starting at currentElement and create their view models
  /// </summary>
  public void LoadMoreElements()
  {
    if (IsLoading) return;
    IsLoading = true;
    Task.Run(() =>
    {
      int i = PageSize;
      while (i-- > 0 && currentElement != null)
      {
        var childViewModel = FindViewModel(currentElement);
        if (childViewModel == null)
          CreateChildViewModel(currentElement);
        currentElement = currentElement.NextSibling();
        CurrentElementIndex++;
      }
      IsLoading = false;
    });
  }

  /// <summary>
  /// Find a ViewModel created for an ModeledElement
  /// </summary>
  /// <param name="element"></param>
  /// <returns></returns>
  public virtual ElementViewModel? FindViewModel(DX.OpenXmlElement element)
  {
    var result = Elements.ToList().FirstOrDefault(vm => vm.ModeledElement == element);
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
  /// Creates a viewModel for an ModeledElement and adds it to elements.
  /// </summary>
  /// <param name="element"></param>
  public virtual void CreateChildViewModel(DX.OpenXmlElement element)
  {
    ElementViewModel childElementViewModel = Application.Instance.CreateViewModel(this, element);
    System.Windows.Application.Current.Dispatcher.Invoke(() =>
    {
      Elements.Add(childElementViewModel);
    });
    childElementViewModel.PropertyChanged += ChildElementViewModel_PropertyChanged;
  }

  private void ChildElementViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
  {
    //Debug.WriteLine($"ChildElementViewModel_PropertyChanged: {e.PropertyName}");
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

  /// <summary>
  /// Enumerates the selected items.
  /// </summary>
  /// <returns></returns>
  public IEnumerable<ElementViewModel> GetSelectedItems()
  {
    foreach (var element in Elements)
    {
      if (element.IsSelected)
        yield return element;
      if (element is CompoundElementViewModel container)
        foreach (var item in container.GetSelectedItems())
          yield return item;
    }
  }
  #endregion

  /// <summary>
  /// Gets the elements of a specific type.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <returns></returns>
  public IEnumerable<T> GetElements<T>() where T : ElementViewModel => Elements.OfType<T>();

  /// <summary>
  /// Gets the elements of a specific type and their descendants.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <returns></returns>
  public IEnumerable<T> GetDescendants<T>() where T : ElementViewModel
  {
    foreach (var element in Elements)
    {
      if (element is T t)
        yield return t;
      if (element is CompoundElementViewModel container)
        foreach (var item in container.GetDescendants<T>())
          yield return item;
    }
  }
}
