namespace Docx.Automation;

/// <summary>
/// Specifies the change in section properties.
/// </summary>
public interface SectionPropertiesChange: IElement
{
  /// <summary>
  /// <para>author</para>
  /// </summary>
  public string? Author { get; set; }

  /// <summary>
  /// <para>date</para>
  /// </summary>
  public DateTime? Date { get; set; }

  /// <summary>
  /// <para>dateUtc, this property is only available in Microsoft 365 and later.</para>
  /// </summary>
  public DateTime? DateUtc { get; set; }

  /// <summary>
  /// <para>Annotation Identifier</para>
  /// </summary>
  public String? Id { get; set; }

  /// <summary>
  /// Previous section properties.
  /// </summary>
  public PreviousSectionProperties? PreviousSectionProperties { get; set; }

}