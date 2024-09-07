using System.Collections.ObjectModel;

using Qhta.MVVM;

namespace DocxControls;

/// <summary>
/// View model for an SDT element
/// </summary>
public class SdtElementViewModel : ElementViewModel
{

  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="ownerViewModel">Owner view model</param>
  /// <param name="sdtElement">Modeled SdtElement</param>
  public SdtElementViewModel(ViewModel ownerViewModel, DXW.SdtElement sdtElement) : base(ownerViewModel, sdtElement)
  {
    foreach (var element in sdtElement.Elements())
    {
      if (element is DXW.SdtProperties properties)
        Properties = new SdtPropertiesViewModel(this, properties);
      else
      if (element is DXW.SdtEndCharProperties endCharProperties)
        EndCharProperties = new SdtEndCharPropertiesViewModel(this, endCharProperties);
      else
      {
        ElementViewModel? elementViewModel = element switch
        {
          DXW.SdtContentBlock contentBLock => new SdtContentBlockViewModel(this, contentBLock),
          _ => null
        };
        if (elementViewModel == null)
        {
          Debug.WriteLine($"SdtViewModel: Element {element.GetType().Name} not supported");
          elementViewModel = new UnknownElementViewModel(this, element);
        }
        Elements.Add(elementViewModel);
      }
    }
  }


  /// <summary>
  /// Modeled SdtElement
  /// </summary>
  public DXW.SdtElement SdtElement => (DXW.SdtElement)Element!;

  /// <summary>
  /// Sdt properties of the element
  /// </summary>
  public SdtPropertiesViewModel? Properties { get; }

  /// <summary>
  /// Sdt properties of the element
  /// </summary>
  public SdtEndCharPropertiesViewModel? EndCharProperties { get; }

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