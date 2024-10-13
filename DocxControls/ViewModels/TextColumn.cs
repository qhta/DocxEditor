using Qhta.MVVM;

namespace DocxControls.ViewModels;

/// <summary>
/// Specifies the properties for a single column of text within this section
/// </summary>
public class TextColumn: ElementViewModel, DA.TextColumn
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="owner"></param>
  /// <param name="modeledElement"></param>
  public TextColumn(ViewModel owner, DXW.Column modeledElement) : base(owner, modeledElement)
  {
  }

  internal DXW.Column OpenXmlElement => (DXW.Column)ModeledElement!;

  DA.Length? DA.TextColumn.Width 
  {
    get => Width;
    set => Width = (Length?)value;
  }
  /// <summary>
  /// Specifies the width (in points) of this text column. 
  /// </summary>
  public Length? Width
  {
    get
    {
      string? value = OpenXmlElement.Width?.Value;
      if (value == null) return null;
      Twips twips = Twips.FromString(value);
      return new Length(twips.ToPoints());
    }
    set
    {
      Twips? oldValue = OpenXmlElement.Width?.Value;
      Twips? newValue = value?.ToTwips();
      if (newValue == oldValue) return;
      OpenXmlElement.Width = value is null ? null : new DX.StringValue(newValue);
      NotifyPropertyChanged(nameof(Width));
    }
  }

  DA.Length? DA.TextColumn.Space
  {
    get => Space;
    set => Space = (Length?)value;
  }
  /// <summary>
  /// Specifies the spacing (in points) between the current column and the next column. 
  /// </summary>
  public Length? Space
  {
    get
    {
      string? value = OpenXmlElement.Space?.Value;
      if (value == null) return null;
      Twips twips = Twips.FromString(value);
      return new Length(twips.ToPoints());
    }
    set
    {
      Twips? oldValue = OpenXmlElement.Space?.Value;
      Twips? newValue = value?.ToTwips();
      if (newValue == oldValue) return;
      OpenXmlElement.Space = value is null ? null : new DX.StringValue(newValue);
      NotifyPropertyChanged(nameof(Space));
    }
  }
}