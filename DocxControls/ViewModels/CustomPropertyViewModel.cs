using System.ComponentModel;

using Qhta.MVVM;

namespace DocxControls;

/// <summary>
/// View model for a custom property of a document.
/// </summary>
public class CustomPropertyViewModel : PropertyViewModel//ViewModel, INotifyDataErrorInfo
{

  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="owner"></param>
  public CustomPropertyViewModel(ViewModel owner): base(owner)
  {
  }
  //{
  //  Owner = owner;
  //}

  /// <summary>
  /// Name of the property to get/set.
  /// </summary>
  public new string? Name
  {
    get => _name;
    set
    {
      if (value != _name)
      {
        _name = value!;
        NotifyPropertyChanged(nameof(Name));
        ValidateProperty(nameof(Name), _name);
      }
    }
  }
  private string? _name;

  /// <summary>
  /// Type of the property.
  /// </summary>
  public new Type? Type
  {
    get => _type;
    set
    {
      if (value != _type)
      {
        _type = value!;
        NotifyPropertyChanged(nameof(Type));
        ValidateProperty(nameof(Type), _type);
      }
    }
  }
  private Type? _type;

  /// <summary>
  /// Value of the property.
  /// </summary>
  public new object? Value
  {
    get => _value;
    set
    {
      if (value != _value)
      {
        ValidateProperty(nameof(Value), value);
        if (!HasErrors)
        {
          _value = value;
          NotifyPropertyChanged(nameof(Value));
        }
      }
    }
  }
  private object? _value;

  /// <summary>
  /// Checks if the property name and type are empty.
  /// </summary>
  public bool IsEmpty => string.IsNullOrWhiteSpace(Name) && Type == null;

  /// <summary>
  /// Determines if the view model is valid.
  /// </summary>
  public bool Validate()
  {
    _errors.Clear();
    ValidateProperty(nameof(Name), _name);
    ValidateProperty(nameof(Type), _type);
    ValidateProperty(nameof(Value), _value);
    return !HasErrors;
  }


  ///// <summary>
  ///// Determines if the view model is valid.
  ///// </summary>
  //public bool ValidateValueString(string? value)
  //{
  //  if (Type != null && value != null)
  //  {
  //    if (VariantTools.ValidateVariantString(Type, value))
  //      RemoveError(nameof(Value), Strings.InvalidValueForSpecifiedType);
  //    else
  //      AddError(nameof(Value), Strings.InvalidValueForSpecifiedType);
  //  }
  //}



  private readonly Dictionary<string, List<string>> _errors = new();

  /// <summary>
  /// Determines if the view model has errors.
  /// </summary>
  public bool HasErrors => _errors.Any();

  /// <summary>
  /// Occurs when the property value changes.
  /// </summary>
  public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

  /// <summary>
  /// Gets the errors for the property.
  /// </summary>
  /// <param name="propertyName"></param>
  /// <returns></returns>
  public IEnumerable GetErrors(string? propertyName)
  {
    if (propertyName != null)
      return _errors.GetValueOrDefault(propertyName) ?? Enumerable.Empty<string>();
    return _errors.SelectMany(x => x.Value);
  }

  internal void ValidateProperty(string propertyName, object? value)
  {
    switch (propertyName)
    {
      case nameof(Name):
        if (string.IsNullOrWhiteSpace((string?)value))
        {
          AddError(propertyName, Strings.NameCannotBeEmpty);
        }
        else
        {
          RemoveError(propertyName, Strings.NameCannotBeEmpty);
        }
        break;
      case nameof(Value):
        if (Type != null && value != null)
        {
          var str = value.AsString();
          if (VariantTools.ValidateVariantString(Type, str))
            RemoveError(propertyName, Strings.InvalidValueForSpecifiedType);
          else
            AddError(propertyName, Strings.InvalidValueForSpecifiedType);
        }
        break;
        // Add more validation logic as needed
    }
  }

  internal void AddError(string propertyName, string error)
  {
    if (!_errors.ContainsKey(propertyName))
    {
      _errors[propertyName] = new List<string>();
    }

    if (!_errors[propertyName].Contains(error))
    {
      _errors[propertyName].Add(error);
      OnErrorsChanged(propertyName);
    }
  }

  internal void RemoveError(string propertyName, string error)
  {
    if (_errors.ContainsKey(propertyName) && _errors[propertyName].Contains(error))
    {
      _errors[propertyName].Remove(error);
      if (_errors[propertyName].Count == 0)
      {
        _errors.Remove(propertyName);
      }
      OnErrorsChanged(propertyName);
    }
  }
  /// <summary>
  /// 
  /// </summary>
  /// <param name="propertyName"></param>
  protected void OnErrorsChanged(string propertyName)
  {
    ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
  }

}
