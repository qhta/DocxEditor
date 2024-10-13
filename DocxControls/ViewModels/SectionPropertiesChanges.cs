namespace DocxControls.ViewModels;

/// <summary>
/// Represents a collection of section properties changes.
/// </summary>
public class SectionPropertiesChanges: ElementViewModelCollection<SectionPropertiesChange>, DA.SectionPropertiesChanges
{
  /// <summary>
  /// Constructor with parent object.
  /// </summary>
  /// <param name="parent"></param>
  public SectionPropertiesChanges(SectionProperties parent) : base(parent)
  {
    if (parent.ModeledElement is null) return;
    foreach (var element in parent.ModeledElement.Elements<DXW.SectionPropertiesChange>())
    {
      Add(new SectionPropertiesChange(parent, element));
    }
  }

  /// <summary>
  /// Enumeration of section properties changes.
  /// </summary>
  /// <returns></returns>
  /// <exception cref="NotImplementedException"></exception>
  public new IEnumerator<DA.SectionPropertiesChange> GetEnumerator() => base.GetEnumerator();


}