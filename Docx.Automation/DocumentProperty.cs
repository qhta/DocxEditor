﻿namespace Docx.Automation;
/// <summary>
/// Represents a single instance of a document property.
/// </summary>
public interface DocumentProperty : _Element
{
  /// <summary>
  /// Returns the name of the custom property.
  /// </summary>
  public string? Name { get; set; }


  /// <summary>
  /// Type of the property.
  /// </summary>
  public PropertyType? Type { get; set; }


  /// <summary>
  /// The data value of the Value property
  /// </summary>
  public object? Value { get; set; }
}
