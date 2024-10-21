using System.Windows.Controls;

namespace DocxControls.Helpers;

/// <summary>
/// Validates a property value
/// </summary>
public class PropertyValueValidation : ValidationRule
{
  /// <summary>
  /// Validates the property value
  /// </summary>
  /// <param name="value"></param>
  /// <param name="cultureInfo"></param>
  /// <returns></returns>
  public override ValidationResult Validate(object? value, CultureInfo cultureInfo)
  {
    string? input = value as string;

    // Add your custom validation logic here
    if (string.IsNullOrEmpty(input) || !IsValidInput(input))
    {
      return new ValidationResult(false, "Invalid input.");
    }

    return ValidationResult.ValidResult;
  }

  private bool IsValidInput(string input)
  {
    // Implement your validation logic here
    // For example, check if the input matches a specific pattern
    return false; // Replace with actual validation logic
  }
}
