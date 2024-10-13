namespace DocxControls.ViewModels;

/// <summary>
/// Represents a break in a document.
/// </summary>
public class Break: ElementViewModel, DA.Break
{
  /// <summary>
  /// Constructor with owner object and modeled element.
  /// </summary>
  /// <param name="owner"></param>
  /// <param name="modeledElement"></param>
  public Break(Run owner, DXW.Break modeledElement) : base(owner, modeledElement)
  {
  }

  internal DXW.Break OpenXmlElement => (DXW.Break)ModeledElement!;

  /// <summary>
  /// Parent run of the break.
  /// </summary>
  public DA.Run ParentRun => (DA.Run)Owner!;

  /// <summary>
  /// Type of the break.
  /// </summary>
  public DA.BreakType? Type
  {
    get => EnumValuesConverter.Convert<DA.BreakType>(OpenXmlElement.Type?.Value);
    set
    {
      if (value == Type) return;
      OpenXmlElement.Type = (value is not null) ?
        EnumValuesConverter.ConvertBack<DXW.BreakValues>(value) : null;
      NotifyPropertyChanged(nameof(Type));
    }
  }

  /// <summary>
  /// Text of the break.
  /// </summary>
  public string? Text
  {
    get
    {
      switch (Type)
      {
        case DA.BreakType.Page:
          return "\u000B";
        case DA.BreakType.Column:
          return "\u000C";
        case DA.BreakType.TextWrapping:
          return "\u000A";
      }
      return null;
    }
  }
}