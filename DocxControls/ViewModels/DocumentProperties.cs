namespace DocxControls.ViewModels;

/// <summary>
/// View model for the document properties
/// </summary>
public abstract class DocumentProperties: PropertiesViewModel<DocumentProperty>, DA.DocumentProperties
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="parent"></param>
  public DocumentProperties(Document parent): base(parent)
  {

  }

  /// <summary>
  /// Enumerates the document properties.
  /// </summary>
  /// <returns></returns>
  // ReSharper disable once NotDisposedResourceIsReturned
  public IEnumerator GetEnumerator() => Items.GetEnumerator();


  // ReSharper disable once NotDisposedResourceIsReturned
  IEnumerator<DA.DocumentProperty> IEnumerable<DA.DocumentProperty>.GetEnumerator() => Items.Cast<DA.DocumentProperty>().GetEnumerator();

  /// <summary>
  /// Checks if the collection is empty.
  /// </summary>
  public bool IsEmpty  => Items.Count == 0;

  /// <summary>
  /// Gets the names of the built-in properties.
  /// </summary>
  public string[] AvailableProperties => GetNames();

  /// <summary>
  /// Gets the names of the properties from the OpenXml package.
  /// </summary>
  /// <returns></returns>
  protected abstract string[] GetNames();

  /// <summary>
  /// Gets the value of a property by name.
  /// </summary>
  /// <param name="name">Name of the property. Must be one of AvailableProperties names.</param>
  /// <returns></returns>
  public object? Get(string name)
  {
    var property = Items.FirstOrDefault(p => p.Name == name);
    return property?.Value;
  }

  /// <summary>
  /// Sets the value of a property by name.
  /// </summary>
  /// <param name="name">Name of the property. Must be one of AvailableProperties names.</param>
  /// <param name="value">Value of the property. If it is null then property is deleted, otherwise Value must be assignable to the property</param>
  public void Set(string name, object? value)
  {
    var property = Items.FirstOrDefault(p => p.Name == name);
    if (property == null)
    {
      property = new DocumentProperty(this) { Name = name };
      Items.Add(property);
    }
    property.Value = value;
  }
}
