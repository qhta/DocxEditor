﻿using System.Collections.ObjectModel;
using Qhta.MVVM;

namespace DocxControls;

/// <summary>
/// Collection of elements.
/// </summary>
public class ElementCollection<T>: ViewModel, DA.IElementCollection<T> where T : DA.IElement
{
  /// <summary>
  /// Constructor with a parent object.
  /// </summary>
  /// <param name="parent"></param>
  public ElementCollection(object? parent)
  {
    Parent = parent;
  }

  /// <summary>
  /// Returns an Application object that represents the DocxControls application.
  /// </summary>
  public DA.Application Application => DocxControls.Application.Instance;

  /// <summary>
  /// Returns the parent object for collection.
  /// </summary>
  public object? Parent { get; }

  /// <summary>
  ///  Collection of Elements objects.
  /// </summary>
  public ObservableCollection<T> Items { get; } = new();


  /// <summary>
  /// Returns an enumerator that iterates through the collection.
  /// </summary>
  /// <returns></returns>
  public IEnumerator<T> GetEnumerator()
  {
    // ReSharper disable once NotDisposedResourceIsReturned
    return Items.GetEnumerator();
  }

  IEnumerator IEnumerable.GetEnumerator()
  {
    // ReSharper disable once NotDisposedResourceIsReturned
    return ((IEnumerable)Items).GetEnumerator();
  }

  /// <summary>
  /// Returns the number of items in the collection.
  /// </summary>
  public int Count => Items.Count;

  /// <summary>
  /// Adds a new item to the collection.
  /// </summary>
  /// <param name="item"></param>
  internal void Add(T item)
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
  internal bool Contains(T item)
  {
    return Items.Contains(item);
  }

  /// <summary>
  /// Removes the specified item from the collection.
  /// </summary>
  /// <param name="item"></param>
  /// <returns></returns>
  internal bool Remove(T item)
  {
    return Items.Remove(item);
  }

  /// <summary>
  /// Gets the first item in the collection.
  /// </summary>
  /// <returns></returns>
  internal T? First() => Items.FirstOrDefault();

  /// <summary>
  /// Gets the last item in the collection.
  /// </summary>
  /// <returns></returns>
  internal T? Last() => Items.LastOrDefault();
}

