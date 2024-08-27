using System.Collections.ObjectModel;

namespace DocxControls;

/// <summary>
/// Defined to allow the collection to be refreshed.
/// </summary>
/// <typeparam name="T"></typeparam>
public class CustomObservableCollection<T> : ObservableCollection<T>
{
  /// <summary>
  /// Raises the CollectionChanged event with the provided arguments.
  /// </summary>
  public void Refresh()
  {
    OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
  }
}
