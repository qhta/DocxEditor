namespace DocxControls.ViewModels;


/// <summary>
/// Struct representing a length measurement.
/// </summary>
public record struct Length: DA.Length
{
  /// <summary>
  /// The value of the length. Interpretation depends on the unit.
  /// </summary>
  public double Value { get; set; }

  /// <summary>
  /// The unit of measurement.
  /// </summary>
  public DA.Unit Unit { get; set; }

  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="value"></param>
  /// <param name="unit"></param>
  public Length(double value, DA.Unit unit = DA.Unit.Twips)
  {
    Value = value;
    Unit = unit;
  }

  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="value"></param>
  /// <param name="unit"></param>
  public Length(int value, DA.Unit unit = DA.Unit.Twips)
  {
    Value = value;
    Unit = unit;
  }

  /// <summary>
  /// Converts the length to twips.
  /// </summary>
  /// <returns></returns>
  public int ToTwips()
  {
    return Unit switch
    {
      DA.Unit.Twips => (int)Value,
      DA.Unit.Points => (int)(Value * 20),
      DA.Unit.Inches => (int)(Value * 1440),
      DA.Unit.Centimeters => (int)(Value * 567),
      DA.Unit.Millimeters => (int)(Value * 56.7),
      DA.Unit.Picas => (int)(Value * 240),
      DA.Unit.Pixels => (int)(Value * 15),
      _ => throw new ArgumentOutOfRangeException(nameof(Unit), "Invalid unit.")
    };
  }

  /// <summary>
  /// Converts the length to points.
  /// </summary>
  public double ToPoints()
  {
    return Unit switch
    {
      DA.Unit.Twips => Value / 20.0,
      DA.Unit.Points => Value,
      DA.Unit.Inches => Value * 72.0,
      DA.Unit.Centimeters => Value * 28.35,
      DA.Unit.Millimeters => Value * 2.835,
      DA.Unit.Picas => Value * 12,
      DA.Unit.Pixels => Value * 0.75,
      _ => throw new ArgumentOutOfRangeException(nameof(Unit), "Invalid unit.")
    };
  }

  /// <summary>
  /// Converts the length to inches.
  /// </summary>
  public double ToInches()
  {
    return Unit switch
    {
      DA.Unit.Twips => Value / 1440.0,
      DA.Unit.Points => Value / 72.0,
      DA.Unit.Inches => Value,
      DA.Unit.Centimeters => Value / 2.54,
      DA.Unit.Millimeters => Value / 25.4,
      DA.Unit.Picas => Value / 6,
      DA.Unit.Pixels => Value / 96,
      _ => throw new ArgumentOutOfRangeException(nameof(Unit), "Invalid unit.")
    };
  }

  /// <summary>
  /// Converts the length to centimeters.
  /// </summary>
  public double ToCentimeters()
  {
    return Unit switch
    {
      DA.Unit.Twips => Value / 567.0,
      DA.Unit.Points => Value / 28.35,
      DA.Unit.Inches => Value * 2.54,
      DA.Unit.Centimeters => Value,
      DA.Unit.Millimeters => Value / 10.0,
      DA.Unit.Picas => Value / 6 * 2.54,
      DA.Unit.Pixels => Value / 96 * 2.54,
      _ => throw new ArgumentOutOfRangeException(nameof(Unit), "Invalid unit.")
    };
  }

  /// <summary>
  /// Converts the length to millimeters.
  /// </summary>
  public double ToMillimeters()
  {
    return Unit switch
    {
      DA.Unit.Twips => Value / 56.7,
      DA.Unit.Points => Value / 2.835,
      DA.Unit.Inches => Value* 25.4,
      DA.Unit.Centimeters => Value* 10.0,
      DA.Unit.Millimeters => Value,
      DA.Unit.Picas => Value / 6 * 25.4,
      DA.Unit.Pixels => Value / 96 * 25.4,

      _ => throw new ArgumentOutOfRangeException(nameof(Unit), "Invalid unit.")
    };
  }

  /// <summary>
  /// Converts the length to picas.
  /// </summary>
  /// <returns></returns>
  public double ToPicas()
  {
    return Unit switch
    {
      DA.Unit.Twips => Value / 240.0,
      DA.Unit.Points => Value / 12.0,
      DA.Unit.Inches => Value * 6.0,
      DA.Unit.Centimeters => Value / 1.6929,
      DA.Unit.Millimeters => Value / 4.2333,
      DA.Unit.Picas => Value,
      DA.Unit.Pixels => Value * 16.0,
      _ => throw new ArgumentOutOfRangeException(nameof(Unit), "Invalid unit.")
    };
  }

  /// <summary>
  /// Converts the length to pixels.
  /// </summary>
  /// <returns></returns>
  public double ToPixels()
  {
    return Unit switch
    {
      DA.Unit.Twips => Value / 15.0,
      DA.Unit.Points => Value * 1.3333333333333333,
      DA.Unit.Inches => Value * 96.0,
      DA.Unit.Centimeters => Value * 37.795275590551181,
      DA.Unit.Millimeters => Value * 3.779527559055118,
      DA.Unit.Picas => Value / 16.0,
      DA.Unit.Pixels => Value,
      _ => throw new ArgumentOutOfRangeException(nameof(Unit), "Invalid unit.")
    };
  }
}
