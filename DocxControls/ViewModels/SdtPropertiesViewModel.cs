using System.Xml;
using System.Xml.Linq;

using Qhta.MVVM;

namespace DocxControls.ViewModels;

/// <summary>
/// View model for a Sdt element properties
/// </summary>
public class SdtPropertiesViewModel : ElementViewModel
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="sdtViewModel">Owner view model. Must be <see cref="SdtElementViewModel"/></param>
  /// <param name="properties">Modeled Sdt properties element</param>
  public SdtPropertiesViewModel(ViewModel sdtViewModel, object properties): base(sdtViewModel, properties)
  {
  }

  /// <summary>
  /// Identifier of the SDT element
  /// </summary>
  public int? Id
  {
    get => SdtProperties.GetFirstElementDecimalNumberValue<DXW.SdtId>();
    set  => SdtProperties.SetFirstElementDecimalNumberValue<DXW.SdtId>(value);
  }

  /// <summary>
  /// Alias of the SDT element
  /// </summary>
  public string? Alias
  {
    get => SdtProperties.GetFirstElementStringTypeValue<DXW.SdtAlias>();
    set => SdtProperties.SetFirstElementStringTypeValue<DXW.SdtAlias>(value);
  }

  /// <summary>
  /// Is the element temporary
  /// </summary>
  public bool? Temporary
  {
    get => SdtProperties.GetOnOffTypeElement<DXW.TemporarySdt>();
    set => SdtProperties.SetOnOffTypeElement<DXW.TemporarySdt>(value);
  }

  /// <summary>
  /// Is the element locked
  /// </summary>
  public DXW.LockingValues? Lock
  {
    get => SdtProperties.GetFirstEnumTypeElementVal<DXW.Lock, DXW.LockingValues>();
    set => SdtProperties.SetFirstEnumTypeElementVal<DXW.Lock, DXW.LockingValues>(value);
  }

  /// <summary>
  /// Reference to the SDT element placeholder
  /// </summary>
  public string? Placeholder
  {
    get
    {
      var element = SdtProperties.Elements<DXW.SdtPlaceholder>().FirstOrDefault();
      if (element == null)
        return null;
      var value = element.DocPartReference?.Val?.Value;
      return value;
    }
    set
    {
      var element = SdtProperties.Elements<DXW.SdtPlaceholder>().FirstOrDefault();
      if (value != null)
      {
        if (element != null)
        {
          if (element.DocPartReference?.Val?.Value != value)
            element.DocPartReference = new DXW.DocPartReference { Val = new DX.StringValue(value) };
        }
        else
          SdtProperties.Append(new DXW.DocPartReference { Val = new DX.StringValue(value) });
      }
      else
        element?.Remove();
    }
  }

  /// <summary>
  /// Run properties of the SDT element
  /// </summary>
  public DXW.RunProperties? RunProperties
  {
    get
    {
      var element = SdtProperties.Elements<DXW.RunProperties>().FirstOrDefault();
      return element;
    }
    set
    {
      var element = SdtProperties.Elements<DXW.RunProperties>().FirstOrDefault();
      if (value != null)
      {
        if (element != null)
          element.Remove();
        SdtProperties.Append(value);
      }
      else
        element?.Remove();
    }
  }

  /// <summary>
  /// Modeled SDT properties element;
  /// </summary>
  public DXW.SdtProperties SdtProperties => (DXW.SdtProperties)ModeledObject!;

  /// <summary>
  /// Initializes the object properties
  /// </summary>
  protected override ObjectPropertiesViewModel InitObjectProperties()
  {
    var objectProperties = new ObjectPropertiesViewModel(this);
    objectProperties.Add(new ObjectPropertyViewModel(this, nameof(Id)));
    objectProperties.Add(new ObjectPropertyViewModel(this, nameof(Alias)));
    objectProperties.Add(new ObjectPropertyViewModel(this, nameof(Temporary)));
    objectProperties.Add(new ObjectPropertyViewModel(this, nameof(Lock)));
    objectProperties.Add(new ObjectPropertyViewModel(this, nameof(Placeholder)));
    objectProperties.Add(new ObjectPropertyViewModel(this, nameof(RunProperties)));
    return objectProperties;
  }
}
