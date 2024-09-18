namespace Docx.Automation;
/// <summary>
/// Specifies the item to print.
/// </summary>
public enum PrintOutItem
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
  DocumentContent = 0, 
  Properties = 1, 
  Comments = 2, 
  Styles = 3, 
  AutoTextEntries = 4, 
  KeyAssignments = 5,
  Envelope = 6,
  DocumentWithMarkup = 7
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
