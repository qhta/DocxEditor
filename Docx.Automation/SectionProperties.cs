namespace Docx.Automation;

/// <summary>
/// Specifies the properties of a section.
/// </summary>
public interface SectionProperties: SectionPropertiesBase
{
  /// <summary>
  /// Collection of changes in section properties.
  /// </summary>
  public SectionPropertiesChanges SectionPropertiesChanges { get; }
}