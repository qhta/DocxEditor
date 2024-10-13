namespace Docx.Automation;

/// <summary>
/// Represents a length measurement.
/// </summary>
public interface Length
{
  /// <summary>
  /// The value of the length. Interpretation depends on the unit.
  /// </summary>
  public double Value { get; set; }

  /// <summary>
  /// The unit of measurement.
  /// </summary>
  public Unit Unit { get; set; }

  /// <summary>
  /// Converts the length to twips.
  /// </summary>
  /// <returns></returns>
  public int ToTwips();

  /// <summary>
  /// Converts the length to points.
  /// </summary>
  public double ToPoints();

  /// <summary>
  /// Converts the length to inches.
  /// </summary>
  public double ToInches();

  /// <summary>
  /// Converts the length to centimeters.
  /// </summary>
  public double ToCentimeters();

  /// <summary>
  /// Converts the length to millimeters.
  /// </summary>
  public double ToMillimeters();

  /// <summary>
  /// Converts the length to picas.
  /// </summary>
  /// <returns></returns>
  public double ToPicas();

  /// <summary>
  /// Converts the length to pixels.
  /// </summary>
  /// <returns></returns>
  public double ToPixels();
}