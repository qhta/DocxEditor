namespace DocxControls.ViewModels;

/// <summary>
///  A carriage return character
/// </summary>
public class CarriageReturn: ElementViewModel<DXW.CarriageReturn>, DA.CarriageReturn
{
  /// <summary>
  /// Constructor with owner and modeled element.
  /// </summary>
  /// <param name="owner"></param>
  /// <param name="modeledElement"></param>
  public CarriageReturn(Run owner, DXW.CarriageReturn modeledElement) : base(owner, modeledElement)
  {
  }

  /// <summary>
  /// Parent run of this view model
  /// </summary>
  public DA.Run ParentRun => (DA.Run)Owner!;

  /// <summary>
  /// Returns the carriage return character
  /// </summary>
  public string? Text => "\u000D";
}