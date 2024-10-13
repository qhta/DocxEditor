using Qhta.MVVM;

namespace DocxControls.ViewModels;

/// <summary>
/// Represents a header or footer reference.
/// </summary>
public class HeaderFooterReference<T> : ElementReference<string, HeaderFooter>, DA.HeaderFooterReference where T: HeaderFooter
{
  /// <summary>
  /// Constructor with owner object and modeled ModeledElement.
  /// </summary>
  /// <param name="owner"></param>
  /// <param name="modeledElement"></param>
  public HeaderFooterReference(ViewModel owner, DXW.HeaderFooterReferenceType modeledElement) : base(owner, modeledElement)
  {
  }

  internal DXW.HeaderFooterReferenceType OpenXmlElement => (DXW.HeaderFooterReferenceType)ModeledElement!;

  /// <summary>
  /// Identifier of the header/footer relationship.
  /// </summary>
  public override string? Id
  {
    get => OpenXmlElement.Id;
    set => OpenXmlElement.Id = value;
  }

  DA.HeaderFooter? DA.IElementReference<string,DA.HeaderFooter>.GetElement() => GetElement();

  /// <summary>
  /// Gets the header/footer from the document.
  /// </summary>
  public override T? GetElement()
  {
    throw new NotImplementedException();
  }


  /// <summary>
  /// Type of the header/footer.
  /// </summary>
  public DA.HeaderFooterType? Type
  {
    get => EnumValuesConverter.Convert<DA.HeaderFooterType>(OpenXmlElement.Type?.Value);
    set
    {
      if (value == Type) return;
      OpenXmlElement.Type = (value is not null) ?
        EnumValuesConverter.ConvertBack<DXW.HeaderFooterValues>(value) : null;
      NotifyPropertyChanged(nameof(Type));
    }
  }
}