namespace Docx.Automation;

/// <summary>
/// Specifies the presence of a complex field character at the current location in the parent run.
/// A complex field character is a special character which delimits the start and end of a complex field or separates its field codes from its current field result.
/// </summary>
public interface FieldChar : IElement, IRunItem
{
  /// <summary>
  /// <para>Field Character Type</para>
  /// </summary>
  public FieldCharType? FieldCharType { get; set; }
  /// <summary>
  /// <para>Field Should Not Be Recalculated</para>
  /// </summary>
  public bool? FieldLock { get; set; }
  /// <summary>
  /// <para>Field Result Invalidated</para>
  /// </summary>
  public bool? Dirty { get; set; }
  /// <summary>
  /// <para>Form Field Properties.</para>
  /// </summary>
  public FormFieldData? FormFieldData { get; set; }

}