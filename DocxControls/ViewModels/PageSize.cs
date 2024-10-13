namespace DocxControls.ViewModels;

/// <summary>
/// Represents the size of a page.
/// </summary>
public class PageSize: ElementViewModel, DA.PageSize
{
  /// <summary>
  /// Constructor with owner object and modeled element.
  /// </summary>
  /// <param name="owner"></param>
  /// <param name="modeledElement"></param>
  public PageSize(ElementViewModel owner, DXW.PageSize modeledElement) : base(owner, modeledElement)
  {
  }

  internal DXW.PageSize OpenXmlElement => (DXW.PageSize)ModeledElement!;

  DA.Length? DA.PageSize.Width
  {
    get => Width;  
    set => Width = (Length?)value;
  }
  /// <summary>
  /// Represents the width of the page.
  /// </summary>
  public Length? Width
  {
    get
    {
      uint? value = OpenXmlElement.Width?.Value;
      if (value == null) return null;
      Twips twips = new Twips((uint)value);
      return new Length(twips.ToPoints());
    }
    set
    {
      Twips? oldValue = OpenXmlElement.Width?.Value;
      Twips? newValue = value?.ToTwips();
      if (newValue == oldValue) return;
      OpenXmlElement.Width = newValue is null ? null : new DX.UInt32Value((uint)newValue!);
      NotifyPropertyChanged(nameof(Width));
    }
  }

  DA.Length? DA.PageSize.Height
  {
    get => Height;
    set => Height = (Length?)value;
  }
  /// <summary>
  /// Represents the height of the page.
  /// </summary>
  public Length? Height
  {
    get
    {
      uint? value = OpenXmlElement.Height?.Value;
      if (value == null) return null;
      Twips twips = new Twips((uint)value);
      return new Length(twips.ToPoints());
    }
    set
    {
      Twips? oldValue = OpenXmlElement.Height?.Value;
      Twips? newValue = value?.ToTwips();
      if (newValue == oldValue) return;
      OpenXmlElement.Height = newValue is null ? null : new DX.UInt32Value((uint)newValue!);
      NotifyPropertyChanged(nameof(Height));
    }
  }

  /// <summary>
  /// Orientation of the page.
  /// </summary>
  public DA.PageOrientation? Orient
  {
    get => EnumValuesConverter.Convert<DA.PageOrientation>(OpenXmlElement.Orient);
    set
    {
      if (value == Orient) return;
      OpenXmlElement.Orient = (value is not null) ?
        EnumValuesConverter.ConvertBack<DXW.PageOrientationValues>(value) : null;
      NotifyPropertyChanged(nameof(Orient));
    }
  }

  /// <summary>
  ///  Numeric code for the paper size.
  /// </summary>
  public DA.PaperSize? PaperSize
  {
    get => (DA.PaperSize?)OpenXmlElement.Code?.Value;
    set
    {
      if (value == PaperSize) return;
      OpenXmlElement.Code = (value is not null) ? new DX.UInt16Value((ushort)value) : null;
      NotifyPropertyChanged(nameof(PaperSize));
    }
  }
}