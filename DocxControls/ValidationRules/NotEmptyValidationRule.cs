using System.Windows.Controls;

namespace DocxControls;

/// <summary>
/// Validation rule that requires a string to be non-empty.
/// </summary>
public class NotEmptyValidationRule : ValidationRule
{
  /// <summary>
  /// Validates a string to be non-empty.
  /// </summary>
  /// <param name="value"></param>
  /// <param name="cultureInfo"></param>
  /// <returns></returns>
  public override ValidationResult Validate(object? value, CultureInfo cultureInfo)
  {
    if (string.IsNullOrWhiteSpace(value as string))
    {
      return new ValidationResult(false, Strings.ValueCannotBeEmpty);
    }
    return ValidationResult.ValidResult;
  }
}
