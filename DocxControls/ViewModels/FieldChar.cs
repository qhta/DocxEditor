namespace DocxControls.ViewModels;

/// <summary>
/// Specifies the presence of a complex field character at the current location in the parent run.
/// A complex field character is a special character which delimits the start and end of a complex field or separates its field codes from its current field result.
/// </summary>
public class FieldChar: ElementViewModel, DA.FieldChar
{
  /// <summary>
  /// Constructor with owner and modeled element.
  /// </summary>
  /// <param name="owner"></param>
  /// <param name="modeledElement"></param>
  public FieldChar(Run owner, DXW.FieldChar modeledElement) : base(owner, modeledElement)
  {
  }

  internal DXW.FieldChar OpenXmlElement => (DXW.FieldChar)ModeledElement!;

  /// <summary>
  /// Parent run of this view model
  /// </summary>
  public DA.Run ParentRun => (DA.Run)Owner!;

  /// <summary>
  /// Specifies the type of the current complex field character in the document.
  /// </summary>
  public DA.FieldCharType? FieldCharType
  {
    get => EnumValuesConverter.Convert<DA.FieldCharType>(OpenXmlElement.FieldCharType?.Value);
    set
    {
      if (value == FieldCharType) return;
      OpenXmlElement.FieldCharType = (value is not null) ?
        EnumValuesConverter.ConvertBack<DXW.FieldCharValues>(value) : null;
      NotifyPropertyChanged(nameof(FieldCharType));
    }
  }

  /// <summary>
  /// Specifies that the parent complex field shall not have its field result recalculated,
  /// even if an application attempts to recalculate the results of all fields in the document or a recalculation is explicitly requested.
  /// </summary>
  public bool? FieldLock { get; set; }  // TODO: Implement FieldLock

  /// <summary>
  /// Specifies that this field has been flagged by an application to indicate that its current results are no longer correct (stale) due to other modifications made to the document,
  /// and these contents should be updated before they are displayed if this functionality is supported by the next processing application.
  /// </summary>
  public bool? Dirty { get; set; }  // TODO: Implement Dirty

  /// <summary>
  /// <para>
  /// Specifies a set of properties which shall be associated with the parent form field within the document.
  /// This form field can be any of the following types:
  /// </para>
  /// <list type="bullet">
  /// <item>Checkbox</item>
  /// <item>Drop-down List</item>
  /// <item>Text Box</item>
  /// </list> 
  /// </summary>
  public DA.FormFieldData? FormFieldData { get; set; } // TODO: Implement FormFieldData
}