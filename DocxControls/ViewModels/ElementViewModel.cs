using Qhta.MVVM;

namespace DocxControls;

/// <summary>
/// View model for a body element: paragraph, table, etc.
/// </summary>
public class ElementViewModel : ViewModel
{
  /// <summary>
  /// Element of the document
  /// </summary>
  public DX.OpenXmlElement Element { get; init; }

  /// <summary>
  /// Initializes a new instance of the <see cref="ElementViewModel"/> class.
  /// </summary>
  /// <param name="element"></param>
  public ElementViewModel(DX.OpenXmlElement element)
  {
    Element = element;
  }

  /// <summary>
  /// Access to inner Xml of the element
  /// </summary>
  public string InnerXml => CleanXml(Element.InnerXml);

  /// <summary>
  /// Access to outer Xml of the element
  /// </summary>
  public string OuterXml => CleanXml(Element.OuterXml);

  private string CleanXml(string xml)
  {
    return xml.Replace(" xmlns:w=\"http://schemas.openxmlformats.org/wordprocessingml/2006/main\"", "").Replace("<w:", "<").Replace("</w:", "</");
  }
}
