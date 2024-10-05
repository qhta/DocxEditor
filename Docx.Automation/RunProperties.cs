namespace Docx.Automation;

/// <summary>
/// Interface for run properties.
/// </summary>
public interface RunProperties
{
  /// <summary>
  /// Compare the properties of this run with another run.
  /// </summary>
  /// <param name="other"></param>
  /// <returns></returns>
  public bool Equals (RunProperties other);
}
