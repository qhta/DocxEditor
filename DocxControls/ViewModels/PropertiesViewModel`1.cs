using System.Collections.ObjectModel;
using System.ComponentModel;
using DocxControls.Helpers;
using DocumentFormat.OpenXml.Packaging;

using Qhta.MVVM;

namespace DocxControls.ViewModels;

/// <summary>
/// Abstract class for the properties view model
/// </summary>
public abstract class PropertiesViewModel<T> : ViewModel, IEditable, IDataGridCompanion where T : PropertyViewModel
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  protected PropertiesViewModel(object? parent)
  {
    Parent = parent;
    Items.CollectionChanged += (sender, e) =>
    {
      if (e.Action == NotifyCollectionChangedAction.Add)
      {
        foreach (PropertyViewModel item in e.NewItems!)
        {
          item.PropertyChanged += PropertyViewModel_PropertyChanged;
        }
      }
      else if (e.Action == NotifyCollectionChangedAction.Remove)
      {
        foreach (PropertyViewModel item in e.OldItems!)
        {
          item.PropertyChanged -= PropertyViewModel_PropertyChanged;
        }
      }
    };
  }

  /// <summary>
  /// Parent of the properties view model
  /// </summary>
  public object? Parent { get; }

  /// <summary>
  /// Application instance
  /// </summary>
  public DA.Application Application => DocxControls.Application.Instance;

  private void PropertyViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
  {
    //Debug.WriteLine($"{this}.PropertyViewModel_PropertyChanged({sender}, {e.PropertyName})");
  }

  /// <summary>
  /// Internal Wordprocessing document
  /// </summary>
  public WordprocessingDocument WordDocument { get; init; } = null!;


  /// <summary>
  /// Observable collection of properties
  /// </summary>
  public ObservableCollection<T> Items { get; } = new();

  /// <summary>
  /// Number of items in the collection
  /// </summary>
  public int Count => Items.Count;

  /// <summary>
  /// Width of the data grid in the view
  /// </summary>
  public double DataGridWidth
  {
    get => _dataGridWidth;
    set
    {
      if (_dataGridWidth != value)
      {
        _dataGridWidth = value;
        NotifyPropertyChanged(nameof(DataGridWidth));
      }
    }
  }
  private double _dataGridWidth;

  #region IEditable implementation
  /// <summary>
  /// Determines if the object is editable.
  /// </summary>
  public bool IsEditable => (Parent as IEditable)?.IsEditable ?? true;

  /// <summary>
  /// Was the object modified?
  /// </summary>
  public bool IsModified
  {
    get => _isModified;
    set
    {
      if (IsModifiedInternal) return;
      if (_isModified != value)
      {
        _isModified = value;
        NotifyPropertyChanged(nameof(IsModified));
        if (value && Parent is IEditable editable)
        {
          editable.IsModified = value;
        }
      }
    }
  }
  private bool _isModified;

  /// <summary>
  /// Is the object modified internally?
  /// </summary>
  public bool IsModifiedInternal { get; set; }


  #endregion IEditable implementation
}
