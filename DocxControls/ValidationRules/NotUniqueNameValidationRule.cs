using System.Windows.Controls;

namespace DocxControls;

/// <summary>
/// Validation rule that requires a string to be unique in a list of entries.
/// </summary>
public class NotUniqueNameValidationRule : ValidationRule
{
  /// <summary>
  /// Gets or sets the collection of items to check for uniqueness.
  /// </summary>
  public IEnumerable? Items { get; set; }

  /// <summary>
  /// Validates a string to be unique.
  /// </summary>
  /// <param name="value"></param>
  /// <param name="cultureInfo"></param>
  /// <returns></returns>
  public override ValidationResult Validate(object? value, CultureInfo cultureInfo)
  {
    Debug.WriteLine($"Validate value={value}");
    string? input = value as string;

    if (string.IsNullOrWhiteSpace(input))
    {
      Debug.WriteLine("Name cannot be empty");
      return new ValidationResult(false, Strings.NameCannotBeEmpty);
    }

    if (Items != null)
    {

      foreach (var item in Items)
      {
        var nameProp = item.GetType().GetProperty("Name");
        if (nameProp == null)
        {
          throw new InvalidOperationException("Items must have a 'Name' property.");
        }
        if (string.Equals(input, nameProp.GetValue(item) as string, StringComparison.OrdinalIgnoreCase))
        {
          Debug.WriteLine("Name is not unique");
          return new ValidationResult(false, Strings.NameMustBeUnique);
        }
      }
    }
    else
    {
      Debug.WriteLine("Items is null");
    }
    return ValidationResult.ValidResult;
  }
}
