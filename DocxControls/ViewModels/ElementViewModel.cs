using System.Windows.Input;

using Qhta.MVVM;

namespace DocxControls;

/// <summary>
/// View model for a body element: paragraph, table, etc.
/// </summary>
public abstract class ElementViewModel : ObjectViewModel
{
  /// <summary>
  /// Owner element view model
  /// </summary>
  public ViewModel? Owner { get; }

  /// <summary>
  /// Element of the document
  /// </summary>
  public DX.OpenXmlElement? Element
  {
    get => (DX.OpenXmlElement?)ModeledObject;
    set => ModeledObject = value;
  }

  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="owner">owner ViewModel</param>
  /// <param name="element">Modeled OpenXmlElement</param>
  protected ElementViewModel(ViewModel? owner, DX.OpenXmlElement? element)
  {
    Owner = owner;
    Element = element;
    // ReSharper disable once VirtualMemberCallInConstructor
    DoubleClickCommand = new RelayCommand<object>(OnItemDoubleClicked);
    LeftMouseDownCommand = new RelayCommand<object>(OnItemLeftMouseDown);
    RightMouseUpCommand = new RelayCommand<object>(OnItemRightMouseUp);
  }

  /// <summary>
  /// Recursively gets the top document view model
  /// </summary>
  /// <returns></returns>
  /// <exception cref="InvalidOperationException"></exception>
  public DocumentViewModel GetDocumentViewModel() => (Owner as DocumentViewModel) ?? (Owner as ElementViewModel)?.GetDocumentViewModel() 
    ?? throw new InvalidOperationException("Owner is not a document view model");

  /// <summary>
  /// Access to outer Xml of the element
  /// </summary>
  public virtual string? DisplayText
  {
    get
    {
      var str = CleanXml(Element?.OuterXml);
      if (str!= null && str.Length > 1000)
        str = str.Substring(0, 996) + " ...";
      return str;
    }
  }

  /// <summary>
  /// Removes unnecessary tags from xml.
  /// </summary>
  /// <param name="xml"></param>
  /// <returns></returns>
  protected string? CleanXml(string? xml)
  {
    return xml?.Replace(" xmlns:w=\"http://schemas.openxmlformats.org/wordprocessingml/2006/main\"", "").Replace("<w:", "<").Replace("</w:", "</");
  }


  /// <summary>
  /// Command to handle the double click event
  /// </summary>
  public ICommand? DoubleClickCommand { get; protected set; }

  private void OnItemDoubleClicked(object? parameter)
  {
    if (parameter is ElementViewModel item)
    {
      // Do something with the item
    }
  }

  /// <summary>
  /// Command to handle the double click event
  /// </summary>
  public ICommand? LeftMouseDownCommand { get; protected set; }

  private void OnItemLeftMouseDown(object? parameter)
  {
    if (parameter is ElementViewModel item)
    {
      item.IsSelected = !item.IsSelected;
    }
  }

  /// <summary>
  /// Command to handle the double click event
  /// </summary>
  public ICommand? RightMouseUpCommand { get; protected set; }

  private void OnItemRightMouseUp(object? parameter)
  {
    if (parameter is ElementViewModel item)
    {
      Executables.ShowProperties(item);
    }
  }

  /// <summary>
  /// Displayed tooltip with the name of the bookmark
  /// </summary>
  public string? ToolTip => ModeledObject?.GetType().Name;
}
