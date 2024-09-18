namespace Docx.Automation;

/// <summary>
/// Specifies a range to print.
/// </summary>
public enum PrintOutRange
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
  AllDocument = 0, 
  Selection = 1, 
  CurrentPage =2, 
  FromTo =3, 
  RangeOfPages = 4,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
