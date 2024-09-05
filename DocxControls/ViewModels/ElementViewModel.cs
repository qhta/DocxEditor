using Qhta.MVVM;
using System.Windows.Input;

namespace DocxControls;

/// <summary>
/// View model for a body element: paragraph, table, etc.
/// </summary>
public class ElementViewModel : ObjectViewModel
{
  /// <summary>
  /// Element of the document
  /// </summary>
  public DX.OpenXmlElement Element
  {
    get => (DX.OpenXmlElement)ModeledObject!;
    set => ModeledObject = value;
  }

  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="element"></param>
  public ElementViewModel(DX.OpenXmlElement element)
  {
    Element = element;
    // ReSharper disable once VirtualMemberCallInConstructor
    InitObjectProperties();
    DoubleClickCommand = new RelayCommand<object>(OnItemDoubleClicked);
    LeftMouseDownCommand = new RelayCommand<object>(OnItemLeftMouseDown);
    RightMouseUpCommand = new RelayCommand<object>(OnItemRightMouseUp);
  }

  /// <summary>
  /// Access to inner Xml of the element
  /// </summary>
  public string InnerXml => CleanXml(Element.InnerXml);

  /// <summary>
  /// Access to outer Xml of the element
  /// </summary>
  public string OuterXml => CleanXml(Element.OuterXml);

  private string CleanXml(string xml)
  {
    return xml.Replace(" xmlns:w=\"http://schemas.openxmlformats.org/wordprocessingml/2006/main\"", "").Replace("<w:", "<").Replace("</w:", "</");
  }

  /// <summary>
  /// Initializes the object properties
  /// </summary>
  protected virtual void InitObjectProperties()
  {

  }

  /// <summary>
  /// Command to handle the double click event
  /// </summary>
  public ICommand DoubleClickCommand { get; }

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
  public ICommand LeftMouseDownCommand { get; }

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
  public ICommand RightMouseUpCommand { get; }

  private void OnItemRightMouseUp(object? parameter)
  {
    if (parameter is ElementViewModel item)
    {
      Executables.ShowProperties(item);
    }
  }

}
