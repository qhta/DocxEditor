using Docx.Automation;
using Qhta.MVVM;

namespace Docx.Normalization;
public class JoinRuns: Command, PluginCommand
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="plugin"></param>
  public JoinRuns(Plugin plugin)
  {
    Plugin = plugin;
    Name = "JoinRuns";
  }

  public Plugin Plugin { get; }
  public string? Caption => CommandCaptions.JoinRuns;
  public string? Tooltip => CommandTooltips.JoinRuns;
  public string? Description => CommandDescriptions.JoinRuns;

  public override void Execute(object? parameter)
  {
    var document = Plugin.Application?.ActiveDocument;
    if (document is null) return;
    List<DA.Paragraph> paragraphs;
    
    if (!document.Selection.IsEmpty)
    {
      paragraphs = document.Selection.Paragraphs.ToList();
    }
    else
    {
      paragraphs = document.Selection.Paragraphs.ToList();
    }
    foreach (var paragraph in paragraphs)
    {
      JoinRunsInParagraph(paragraph);
    }
  }

  private void JoinRunsInParagraph(DA.Paragraph paragraph)
  {
    //paragraph.JoinRuns();
  }

}
