namespace DocxControls.ViewModels;

/// <summary>
/// Represents a collection of headers or footers.
/// </summary>
public class HeadersFooters<T>: ElementViewModelCollection<HeaderFooterReference<T>>, DA.HeadersFooters where T:HeaderFooter
{
  /// <summary>
  /// Constructor with parent object.
  /// </summary>
  /// <param name="parent"></param>
  public HeadersFooters(ElementViewModel parent) : base(parent)
  {
    Type elementType = typeof(T)== typeof(Header) ? typeof(DXW.Header) : typeof(DXW.Footer);
    if (parent.ModeledElement is null) return;
    foreach (var headerFooterType in parent.ModeledElement.Elements<DXW.HeaderFooterReferenceType>().Where(e=>e.GetType()==elementType))
    {
      Add(new HeaderFooterReference<T>(this, headerFooterType));
    }
  }

  /// <summary>
  /// Enumeration of headers or footers.
  /// </summary>
  /// <returns></returns>
  /// <exception cref="NotImplementedException"></exception>
  public new IEnumerator<DA.HeaderFooterReference> GetEnumerator() => base.GetEnumerator();


  /// <summary>
  /// Indexer to get or set a header or footer by its type.
  /// </summary>
  /// <param name="type"></param>
  /// <returns></returns>
  /// <exception cref="NotImplementedException"></exception>
  public DA.HeaderFooterReference? this[DA.HeaderFooterType type]
  {
    get => Items.FirstOrDefault(item => item.Type == type);
    set
    {
      var existingItem = Items.FirstOrDefault(item => item.Type == type);
      if (value== existingItem) return;
      if (existingItem is not null)
      {
        Remove(existingItem);
      }
      if (value is not null)
      {
        value.Type = type;
        Add((HeaderFooterReference<T>)value);
      }
    }
  }
}