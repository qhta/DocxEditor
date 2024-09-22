namespace DocxControls.ViewModels;

/// <summary>
/// View model for a document property.
/// </summary>
public class DocumentProperty : PropertyViewModel, DA.DocumentProperty
{

  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="parent"></param>
  public DocumentProperty(object? parent) : base(parent)
  {
  }


  /// <summary>
  /// Get the application instance.
  /// </summary>
  public DA.Application Application => DocxControls.Application.Instance;

  /// <summary>
  /// ValueType of the property.
  /// </summary>
  public Type? ValueType
  {
    get => _valueType;
    set
    {
      if (value != _valueType)
      {
        _valueType = value!;
        NotifyPropertyChanged(nameof(ValueType));
      }
    }
  }

  private Type? _valueType;

  DA.PropertyType? DA.DocumentProperty.Type
  {
    get
    {
      if (ValueType == null)
        return null;
      if (ValueType == typeof(string))
        return DA.PropertyType.String;
      if (ValueType == typeof(bool))
        return DA.PropertyType.Boolean;
      if (ValueType == typeof(int))
        return DA.PropertyType.Number;
      if (ValueType == typeof(float))
        return DA.PropertyType.Float;
      if (ValueType == typeof(DateTime))
        return DA.PropertyType.Date;
      return DA.PropertyType.Variant;
    }
    set
    {
      switch (value)
      {
        case DA.PropertyType.String:
          ValueType = typeof(string);
          break;
        case DA.PropertyType.Boolean:
          ValueType = typeof(bool);
          break;
        case DA.PropertyType.Number:
          ValueType = typeof(int);
          break;
        case DA.PropertyType.Float:
          ValueType = typeof(float);
          break;
        case DA.PropertyType.Date:
          ValueType = typeof(DateTime);
          break;
      }
      if (value == null)
        ValueType = null;
    }
  }
}