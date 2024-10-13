namespace Docx.Automation;

/// <summary>
/// Kind of unit of length measurement.
/// </summary>
public enum Unit
{
  /// <summary>
  /// Twips (1/20th of a point).
  /// </summary>
  Twips = 0,

  /// <summary>
  /// Points (1/72nd of an inch).
  /// </summary>
  Points = 1,

  /// <summary>
  /// Inches (25.4 millimeters).
  /// </summary>
  Inches = 2,

  /// <summary>
  /// Centimeters (10 millimeters).
  /// </summary>
  Centimeters = 3,

  /// <summary>
  /// Millimeters.
  /// </summary>
  Millimeters = 4,

  /// <summary>
  /// Picas (1/6th of an inch).
  /// </summary>
  Picas = 5,

  /// <summary>
  /// Pixels (1/96th of an inch).
  /// </summary>
  Pixels = 6
}