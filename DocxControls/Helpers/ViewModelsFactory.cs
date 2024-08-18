using Qhta.MVVM;

namespace DocxControls;
/// <summary>
/// Factory for creating view models
/// </summary>

public class ViewModelsFactory
{
  /// <summary>
  /// Internal Wordprocessing document
  /// </summary>
  public DXPack.WordprocessingDocument WordprocessingDocument { get; init; }

  /// <summary>
  /// Creates a new instance of the <see cref="ViewModelsFactory"/> class.
  /// </summary>
  /// <param name="document"></param>
  public ViewModelsFactory(DXPack.WordprocessingDocument document)
  {
    WordprocessingDocument = document;
  }

  /// <summary>
  /// Creates a new view model for the specified object type.
  /// </summary>
  /// <param name="objectType"></param>
  /// <returns></returns>
  public ViewModel? CreateViewModel(Type objectType)
  {
#pragma warning disable OOXML0001
    if (objectType == typeof(DXPack.IPackageProperties))
#pragma warning restore OOXML0001
      return new CorePropertiesViewModel(WordprocessingDocument);
    if (objectType == typeof(DXEP.Properties))
#pragma warning restore OOXML0001
      return new CorePropertiesViewModel(WordprocessingDocument);
    return null;
  }
}
