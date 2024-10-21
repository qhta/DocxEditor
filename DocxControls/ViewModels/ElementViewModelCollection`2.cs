using System.Collections.ObjectModel;

namespace DocxControls.ViewModels;

/// <summary>
/// Collection of elements.
/// </summary>
public class ElementViewModelCollection<T, U>: ElementViewModel<T>, DA.IElementCollection<U>
  where T: DX.OpenXmlCompositeElement, new()

  where U: ElementViewModel, new()
{

  /// <summary>
  /// Constructor with owner object and modeled element.
  /// </summary>
  /// <param name="owner">owner ViewModel</param>
  /// <param name="modeledElement">Modeled ModeledElement</param>
  public ElementViewModelCollection(ViewModel? owner, T? modeledElement) : base(owner, modeledElement ?? new T())
  {
  }

  /// <summary>
  ///  Collection of Elements objects.
  /// </summary>
  public ObservableCollection<U> Items { get; } = new();

  /// <summary>
  /// Checks if the collection is empty
  /// </summary>
  public new bool IsEmpty => Items.Count == 0;

  /// <summary>
  /// Returns an enumerator that iterates through the collection.
  /// </summary>
  /// <returns></returns>
  public IEnumerator<U> GetEnumerator()
  {
    // ReSharper disable once NotDisposedResourceIsReturned
    return Items.GetEnumerator();
  }

  IEnumerator IEnumerable.GetEnumerator()
  {
    // ReSharper disable once NotDisposedResourceIsReturned
    return ((IEnumerable)Items).GetEnumerator();
  }

  IEnumerator<U> IEnumerable<U>.GetEnumerator()
  {
    // ReSharper disable once NotDisposedResourceIsReturned
    return ((IEnumerable<U>)Items).GetEnumerator();
  }

  /// <summary>
  /// Gets the application object.
  /// </summary>
  public new  DA.Application Application => DocxControls.Application.Instance;

  /// <summary>
  /// Gets the parent object.
  /// </summary>
  public new object? Parent => Owner;

  /// <summary>
  /// Returns the number of items in the collection.
  /// </summary>
  public int Count => Items.Count;

  /// <summary>
  /// Adds a new item to the collection.
  /// </summary>
  /// <param name="item"></param>
  internal void Add(U item)
  {
    Items.Add(item);
  }

  /// <summary>
  /// Removes an item from the collection.
  /// </summary>
  internal void Clear()
  {
    Items.Clear();
  }

  /// <summary>
  /// Determines if the collection contains the specified item.
  /// </summary>
  /// <param name="item"></param>
  /// <returns></returns>
  internal bool Contains(U item)
  {
    return Items.Contains(item);
  }

  /// <summary>
  /// Removes the specified item from the collection.
  /// </summary>
  /// <param name="item"></param>
  /// <returns></returns>
  internal bool Remove(U item)
  {
    return Items.Remove(item);
  }

  /// <summary>
  /// Gets the first item in the collection.
  /// </summary>
  /// <returns></returns>
  internal U? First() => Items.FirstOrDefault();

  /// <summary>
  /// Gets the last item in the collection.
  /// </summary>
  /// <returns></returns>
  internal U? Last() => Items.LastOrDefault();


}


