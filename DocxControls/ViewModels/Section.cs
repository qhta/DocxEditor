using System.Runtime.InteropServices.Marshalling;

using Qhta.MVVM;

namespace DocxControls.ViewModels;

/// <summary>
/// Represents a section in a document.
/// </summary>
public class Section : ElementViewModel, DA.Section
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="owner"></param>
  public Section(Body owner) : base(owner)
  {
  }

  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="owner"></param>
  /// <param name="properties"></param>
  public Section(Body owner, SectionProperties properties) : base(owner, properties.SectionPropertiesElement)
  {
    SectionProperties = properties;
  }

  /// <summary>
  /// SectionProperties view model.
  /// </summary>
  public SectionProperties? SectionProperties { get; }

  /// <summary>
  /// OpenXml SectionPropertiesElement element.
  /// </summary>
  public DXW.SectionProperties? SectionPropertiesElement => (DXW.SectionProperties?)OpenXmlElement;

  //private Paragraph? OwnerParagraph => (Paragraph?)Parent;

  //IEnumerable<DA.Block> DA.Section.Blocks => Blocks;
  ///// <summary>
  ///// Get the blocks of the section.
  ///// </summary>
  //public IEnumerable<Block> Blocks
  //{
  //  get
  //  {
  //    var firstElement = OwnerParagraph as ElementViewModel;
  //    while (firstElement != null)
  //    {
  //      if (firstElement != OwnerParagraph && (firstElement is Paragraph paragraph) && (paragraph.OpenXmlElement as DXW.Paragraph)?.Elements<DXW.SectionPropertiesElement>()?.Any() == true)
  //      {
  //       break;
  //      }
  //      firstElement = firstElement.PreviousSibling();
  //    }
  //    while (firstElement != null && firstElement is Block block)
  //    {
  //      yield return block;
  //      if (firstElement == OwnerParagraph)
  //        break;
  //      firstElement = firstElement.NextSibling();
  //    }
  //  }
  //}
}
