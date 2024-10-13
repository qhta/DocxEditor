namespace Docx.Automation;

/// <summary>
/// Specifies printer-specific settings for the printer tray(s) that shall be used to print different pages in this section in the document.
/// </summary>
public interface PaperSource
{
  /// <summary>
  /// Specific code that uniquely identifies a specific printer tray to be used to print the first page of this section in the document.
  /// A first value of 1 (the default) is specifically used to indicate that the printer shall automatically select the appropriate printer tray based on the printed page size.
  /// </summary>
  public ushort? First { get; set; }
  /// <summary>
  /// Specifies a printer-specific code that uniquely identifies a specific printer tray to be used to print each subsequent (non-first) page of this section in the document.
  /// A value of 1 (the default) is specifically used to indicate that the printer shall automatically select the appropriate printer tray based on the printed page size.
  /// </summary>
  public ushort? Other { get; set; }
}