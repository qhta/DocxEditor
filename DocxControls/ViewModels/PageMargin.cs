namespace DocxControls.ViewModels;

/// <summary>
/// Represents the margins of a page.
/// </summary>
public class PageMargin: ElementViewModel<DXW.PageMargin>, Docx.Automation.PageMargin
{
  /// <summary>
  /// Constructor with owner object and modeled element.
  /// </summary>
  /// <param name="owner"></param>
  /// <param name="modeledElement"></param>
  public PageMargin(ElementViewModel owner, DXW.PageMargin? modeledElement) : base(owner, modeledElement)
  {
  }

  DA.Length? DA.PageMargin.Right
  {
    get => Right;
    set => Right = (Length?)value;
  }

  DA.Length? DA.PageMargin.Bottom
  {
    get => Bottom;
    set => Bottom = (Length?)value;
  }

  DA.Length? DA.PageMargin.Left
  {
    get => Left;
    set => Left = (Length?)value;
  }

  DA.Length? DA.PageMargin.Header
  {
    get => Header;
    set => Header = (Length?)value;
  }

  DA.Length? DA.PageMargin.Footer
  {
    get => Footer;
    set => Footer = (Length?)value;
  }

  DA.Length? DA.PageMargin.Gutter
  {
    get => Gutter;
    set => Gutter = (Length?)value;
  }

  DA.Length? DA.PageMargin.Top
  {
    get => Top;
    set => Top = (Length?)value;
  }

  /// <summary>
  /// <para>Left Margin Spacing</para>
  /// </summary>
  public Length? Left
  {
    get
    {
      uint? value = OpenXmlElement.Left?.Value;
      if (value == null) return null;
      Twips twips = new Twips((uint)value);
      return new Length(twips.ToPoints());
    }
    set
    {
      Twips? oldValue = OpenXmlElement.Left?.Value;
      Twips? newValue = value?.ToTwips();
      if (newValue == oldValue) return;
      OpenXmlElement.Left = newValue is null ? null : new DX.UInt32Value((uint)newValue!);
      NotifyPropertyChanged(nameof(Left));
    }
  }

  /// <summary>
  /// <para>Top Margin Spacing</para>
  /// </summary>
  public Length? Top
  {
    get
    {
      int? value = OpenXmlElement.Top?.Value;
      if (value == null) return null;
      Twips twips = new Twips((int)value);
      return new Length(twips.ToPoints());
    }
    set
    {
      Twips? oldValue = OpenXmlElement.Top?.Value;
      Twips? newValue = value?.ToTwips();
      if (newValue == oldValue) return;
      OpenXmlElement.Top = newValue is null ? null : new DX.Int32Value((int)newValue!);
      NotifyPropertyChanged(nameof(Top));
    }
  }

  /// <summary>
  /// <para>Right Margin Spacing</para>
  /// </summary>
  public Length? Right
  {
    get
    {
      uint? value = OpenXmlElement.Right?.Value;
      if (value == null) return null;
      Twips twips = new Twips((uint)value);
      return new Length(twips.ToPoints());
    }
    set
    {
      Twips? oldValue = OpenXmlElement.Right?.Value;
      Twips? newValue = value?.ToTwips();
      if (newValue == oldValue) return;
      OpenXmlElement.Right = newValue is null ? null : new DX.UInt32Value((uint)newValue!);
      NotifyPropertyChanged(nameof(Right));
    }
  }

  /// <summary>
  /// <para>Page Bottom Spacing</para>
  /// </summary>
  public Length? Bottom
  {
    get
    {
      int? value = OpenXmlElement.Bottom?.Value;
      if (value == null) return null;
      Twips twips = new Twips((int)value);
      return new Length(twips.ToPoints());
    }
    set
    {
      Twips? oldValue = OpenXmlElement.Bottom?.Value;
      Twips? newValue = value?.ToTwips();
      if (newValue == oldValue) return;
      OpenXmlElement.Bottom = newValue is null ? null : new DX.Int32Value((int)newValue!);
      NotifyPropertyChanged(nameof(Bottom));
    }
  }

  /// <summary>
  /// <para>Spacing to Top of Header</para>
  /// </summary>
  public Length? Header
  {
    get
    {
      uint? value = OpenXmlElement.Header?.Value;
      if (value == null) return null;
      Twips twips = new Twips((uint)value);
      return new Length(twips.ToPoints());
    }
    set
    {
      Twips? oldValue = OpenXmlElement.Header?.Value;
      Twips? newValue = value?.ToTwips();
      if (newValue == oldValue) return;
      OpenXmlElement.Header = newValue is null ? null : new DX.UInt32Value((uint)newValue!);
      NotifyPropertyChanged(nameof(Header));
    }
  }


  /// <summary>
  /// <para>Spacing to Bottom of Footer</para>
  /// </summary>
  public Length? Footer
  {
    get
    {
      uint? value = OpenXmlElement.Footer?.Value;
      if (value == null) return null;
      Twips twips = new Twips((uint)value);
      return new Length(twips.ToPoints());
    }
    set
    {
      Twips? oldValue = OpenXmlElement.Footer?.Value;
      Twips? newValue = value?.ToTwips();
      if (newValue == oldValue) return;
      OpenXmlElement.Footer = newValue is null ? null : new DX.UInt32Value((uint)newValue!);
      NotifyPropertyChanged(nameof(Footer));
    }
  }

  /// <summary>
  /// <para>Page Gutter Spacing</para>
  /// </summary>
  public Length? Gutter
  {
    get
    {
      uint? value = OpenXmlElement.Gutter?.Value;
      if (value == null) return null;
      Twips twips = new Twips((uint)value);
      return new Length(twips.ToPoints());
    }
    set
    {
      Twips? oldValue = OpenXmlElement.Gutter?.Value;
      Twips? newValue = value?.ToTwips();
      if (newValue == oldValue) return;
      OpenXmlElement.Gutter = newValue is null ? null : new DX.UInt32Value((uint)newValue!);
      NotifyPropertyChanged(nameof(Gutter));
    }
  }

}