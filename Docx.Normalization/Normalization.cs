using Docx.Automation;

namespace Docx.Normalization;

public class Normalization : PluginClass
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="application"></param>
  public Normalization(Application application): base(application)
  {
  }

  public override string Caption => CommandCaptions.Normalization;
  public override string Tooltip => CommandTooltips.Normalization;
  public override string Description => CommandDescriptions.Normalization;

  public override void StartUp()
  {
    _commands.Add(new JoinRuns( this));
  }

}
