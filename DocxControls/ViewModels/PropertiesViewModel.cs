﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using DocumentFormat.OpenXml.Packaging;

using Qhta.MVVM;

namespace DocxControls;

/// <summary>
/// Abstract class for the properties view model
/// </summary>
public abstract class PropertiesViewModel : ViewModel, IEditable
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  protected PropertiesViewModel(ViewModel? owner)
  {
    Owner = owner;
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
  /// Owner of the properties view model
  /// </summary>
  public ViewModel? Owner { get; private set; }

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
  public CustomObservableCollection<PropertyViewModel> Items { get; } = new();

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
  public bool IsEditable => (Owner as IEditable)?.IsEditable ?? true;

  /// <summary>
  /// Was the object modified?
  /// </summary>
  public bool IsModified
  {
    get => _isModified;
    set
    {
      if (_isModified != value)
      {
        _isModified = value;
        NotifyPropertyChanged(nameof(IsModified));
        if (value && Owner is IEditable editable)
        {
          editable.IsModified = value;
        }
      }
    }
  }
  private bool _isModified;
  #endregion IEditable implementation
}
