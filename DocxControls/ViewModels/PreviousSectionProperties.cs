using DocumentFormat.OpenXml;

namespace DocxControls.ViewModels;

/// <summary>
/// View model for a section properties
/// </summary>
public class PreviousSectionProperties : ElementViewModel<DXW.PreviousSectionProperties>, DA.PreviousSectionProperties
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="ownerViewModel">Owner view model. Must be <see cref="CompoundElementViewModel"/></param>
  /// <param name="modeledElement">Modeled section modeledElement element</param>
  public PreviousSectionProperties(ElementViewModel ownerViewModel, DXW.PreviousSectionProperties? modeledElement) : base(ownerViewModel, modeledElement)
  {
  }

  string? DA.SectionPropertiesBase.RsidRPr { get => RsidRPr; set => RsidRPr = value; }
  /// <summary>
  /// Specifies a unique identifier used to track the editing session when the physical character representing this section mark was last formatted.
  /// </summary>
  public HexInt? RsidRPr
  {
    get => OpenXmlElement.RsidRPr?.Value;
    set
    {
      if ((int?)value == RsidRPr) return;
      OpenXmlElement.RsidRPr = new HexBinaryValue(value);
      NotifyPropertyChanged(nameof(RsidRPr));
    }
  }

  string? DA.SectionPropertiesBase.RsidDel { get => RsidDel; set => RsidDel = value; }
  /// <summary>
  /// Specifies a unique identifier used to track the editing session when the section mark for this section was deleted from the document.
  /// </summary>
  public HexInt? RsidDel
  {
    get => OpenXmlElement.RsidDel?.Value;
    set
    {
      if ((int?)value == RsidDel) return;
      OpenXmlElement.RsidDel = new HexBinaryValue(value);
      NotifyPropertyChanged(nameof(RsidDel));
    }
  }

  string? DA.SectionPropertiesBase.RsidR { get => RsidR; set => RsidR = value; }
  /// <summary>
  /// Specifies a unique identifier used to track the editing session when the section mark for this section was added to the document.
  /// </summary>
  public HexInt? RsidR
  {
    get => OpenXmlElement.RsidR?.Value;
    set
    {
      if ((int?)value == RsidR) return;
      OpenXmlElement.RsidDel = new HexBinaryValue(value);
      NotifyPropertyChanged(nameof(RsidR));
    }
  }

  /// <summary>
  /// <para>Section Type that describes section break.</para>
  /// </summary>
  public DA.SectionType? SectionType
  {
    get => EnumValuesConverter.Convert<DA.SectionType>(OpenXmlElement.GetSectionType());
    set
    {
      if (value == SectionType) return;
      OpenXmlElement.SetSectionType(EnumValuesConverter.ConvertBack<DXW.SectionMarkValues>(value));
      NotifyPropertyChanged(nameof(SectionType));
    }
  }

  /// <summary>
  /// Specifies that this section shall be presented using a right-to-left page direction.
  /// This property only affects section-level properties, and does not affect the layout of text within the contents of this section. 
  /// </summary>
  public bool? BiDi
  {
    get => OpenXmlElement.GetBiDi();
    set
    {
      if (value == BiDi) return;
      OpenXmlElement.SetBiDi(value);
      NotifyPropertyChanged(nameof(BiDi));
    }
  }


  /// <summary>
  /// Specifies whether the parent section in this document shall have a different header and footer for its first page. 
  /// </summary>
  public bool? TitlePage
  {
    get => OpenXmlElement.GetTitlePage();
    set
    {
      if (value == TitlePage) return;
      OpenXmlElement.SetTitlePage(value);
      NotifyPropertyChanged(nameof(TitlePage));
    }
  }

  /// <summary>
  /// Specifies the direction of the text flow for this section. 
  /// </summary>
  public DA.TextDirection? TextDirection
  {
    get => EnumValuesConverter.Convert<DA.TextDirection>(OpenXmlElement.GetTextDirection());
    set
    {
      if (value == TextDirection) return;
      OpenXmlElement.SetTextDirection(EnumValuesConverter.ConvertBack<DXW.TextDirectionValues>(value));
      NotifyPropertyChanged(nameof(TextDirection));
    }
  }

  /// <summary>
  /// Specifies the vertical alignment for text on pages in the current section, relative to the top and bottom margins in the main document story on each page.
  /// </summary>
  public DA.VerticalJustification? VerticalTextAlignmentOnPage
  {
    get => EnumValuesConverter.Convert<DA.VerticalJustification>(OpenXmlElement.GetVerticalTextAlignmentOnPage());
    set
    {
      if (value == VerticalTextAlignmentOnPage) return;
      OpenXmlElement.SetVerticalTextAlignmentOnPage(
        EnumValuesConverter.ConvertBack<DXW.VerticalJustificationValues>(value));
      NotifyPropertyChanged(nameof(VerticalTextAlignmentOnPage));
    }
  }

  /// <summary>
  /// Specifies that all endnotes in this document shall not  be displayed or printed.
  /// On any section break other than the first section break in the document, it shall be ignored. 
  /// </summary>
  public bool? NoEndnote
  {
    get => OpenXmlElement.GetNoEndnote();
    set
    {
      if (value == NoEndnote) return;
      OpenXmlElement.SetNoEndnote(value);
      NotifyPropertyChanged(nameof(NoEndnote));
    }
  }

  /// <summary>
  /// Specifies that the page gutter shall be placed on the right side of the page for this section only.
  /// The page gutter defines the amount of extra space added to the specified margin, above any existing margin values. 
  /// </summary>
  public bool? GutterOnRight
  {
    get => OpenXmlElement.GetGutterOnRight();
    set
    {
      if (value == GutterOnRight) return;
      OpenXmlElement.SetGutterOnRight(value);
      NotifyPropertyChanged(nameof(GutterOnRight));
    }
  }

  /// <summary>
  /// Specifies that the contents of the current section shall be protected such that they cannot be edited by a user
  /// (if the consumer is displaying the document and allowing the user to make modification)
  /// except for the text contained in any form field or embedded control that is part of the current section. 
  /// </summary>
  public bool? FormProtection
  {
    get => OpenXmlElement.GetFormProtection();
    set
    {
      if (value == FormProtection) return;
      OpenXmlElement.SetFormProtection(value);
      NotifyPropertyChanged(nameof(FormProtection));
    }
  }

  DA.LineNumberType? DA.SectionPropertiesBase.LineNumberType { get => LineNumberType; set => LineNumberType = (LineNumberType?)value; }
  /// <summary>
  /// Specifies the settings for line numbering to be displayed before each column of text in this section in the document. 
  /// </summary>
  public LineNumberType? LineNumberType
  {
    get
    {
      if (_LineNumberType == null)
      {
        var lineNumberTypeElement = OpenXmlElement.GetLineNumberType();
        if (lineNumberTypeElement != null)
          _LineNumberType = new LineNumberType(this, lineNumberTypeElement);
      }
      return _LineNumberType;
    }
    set
    {
      if (value == LineNumberType) return;
      OpenXmlElement.SetLineNumberType(value?.OpenXmlElement);
      _LineNumberType = value;
      NotifyPropertyChanged(nameof(LineNumberType));
    }
  }
  private LineNumberType? _LineNumberType;

  DA.PageNumberType? DA.SectionPropertiesBase.PageNumberType { get => PageNumberType; set => PageNumberType = (PageNumberType?)value; }
  /// <summary>
  /// Specifies the settings for line numbering to be displayed before each column of text in this section in the document. 
  /// </summary>
  public PageNumberType? PageNumberType
  {
    get
    {
      if (_PageNumberType == null)
      {
        var pageNumberTypeElement = OpenXmlElement.GetPageNumberType();
        if (pageNumberTypeElement != null)
          _PageNumberType = new PageNumberType(this, pageNumberTypeElement);
      }
      return _PageNumberType;
    }
    set
    {
      if (value == PageNumberType) return;
      OpenXmlElement.SetPageNumberType(value?.OpenXmlElement);
      _PageNumberType = value;
      NotifyPropertyChanged(nameof(PageNumberType));
    }
  }
  private PageNumberType? _PageNumberType;


  DA.TextColumns? DA.SectionPropertiesBase.Columns { get => Columns; set => Columns = (TextColumns?)value; }
  /// <summary>
  /// Specifies the settings for line numbering to be displayed before each column of text in this section in the document. 
  /// </summary>
  public TextColumns? Columns
  {
    get
    {
      if (_Columns == null)
      {
        var columnsElement = OpenXmlElement.GetColumns();
        if (columnsElement != null)
          _Columns = new TextColumns(this, columnsElement);
      }
      return _Columns;
    }
    set
    {
      if (value == Columns) return;
      OpenXmlElement.SetColumns(value?.OpenXmlElement);
      _Columns = value;
      NotifyPropertyChanged(nameof(PageNumberType));
    }
  }
  private TextColumns? _Columns;


  /// <summary>
  /// Specifies the number of columns of footnotes. This property is available in Office 2013 and above.
  /// </summary>
  public int? FootnoteColumns
  {
    get => OpenXmlElement.GetFootnoteColumns()?.Val?.Value;
    set
    {
      if (value == FootnoteColumns) return;
      OpenXmlElement.SetFootnoteColumns(value == null ? null : new DXO13W.FootnoteColumns { Val = value });
      NotifyPropertyChanged(nameof(FootnoteColumns));
    }
  }

  DA.FootnoteProperties? DA.SectionPropertiesBase.FootnoteProperties { get => FootnoteProperties; set => FootnoteProperties = (FootnoteProperties?)value; }
  /// <summary>
  /// Specifies the properties for footnotes in this section.
  /// </summary>
  public FootnoteProperties? FootnoteProperties
  {
    get
    {
      if (_FootnoteProperties == null)
      {
        var FootnotePropertiesElement = OpenXmlElement.GetFootnoteProperties();
        if (FootnotePropertiesElement != null)
          _FootnoteProperties = new FootnoteProperties(this, FootnotePropertiesElement);
      }
      return _FootnoteProperties;
    }
    set
    {
      if (value == FootnoteProperties) return;
      OpenXmlElement.SetFootnoteProperties(value?.OpenXmlElement);
      _FootnoteProperties = value;
      NotifyPropertyChanged(nameof(FootnoteProperties));
    }
  }
  private FootnoteProperties? _FootnoteProperties;


  DA.EndnoteProperties? DA.SectionPropertiesBase.EndnoteProperties { get => EndnoteProperties; set => EndnoteProperties = (EndnoteProperties?)value; }
  /// <summary>
  /// Specifies the properties for endnotes in this section.
  /// </summary>
  public EndnoteProperties? EndnoteProperties
  {
    get
    {
      if (_EndnoteProperties == null)
      {
        var EndnotePropertiesElement = OpenXmlElement.GetEndnoteProperties();
        if (EndnotePropertiesElement != null)
          _EndnoteProperties = new EndnoteProperties(this, EndnotePropertiesElement);
      }
      return _EndnoteProperties;
    }
    set
    {
      if (value == EndnoteProperties) return;
      OpenXmlElement.SetEndnoteProperties(value?.OpenXmlElement);
      _EndnoteProperties = value;
      NotifyPropertyChanged(nameof(EndnoteProperties));
    }
  }
  private EndnoteProperties? _EndnoteProperties;

  DA.HeadersFooters DA.SectionPropertiesBase.Headers => Headers;
  /// <summary>
  ///  Collection of headers in this section.
  /// </summary>
  public HeadersFooters<Header> Headers
  {
    get
    {
      if (_Headers == null)
      {
        _Headers = new HeadersFooters<Header>(this);
      }
      return _Headers;
    }
  }
  private HeadersFooters<Header>? _Headers;

  DA.HeadersFooters DA.SectionPropertiesBase.Footers => Footers;
  /// <summary>
  ///  Collection of footers in this section.
  /// </summary>
  public HeadersFooters<Footer> Footers
  {
    get
    {
      if (_Footers == null)
      {
        _Footers = new HeadersFooters<Footer>(this);
      }
      return _Footers;
    }
  }
  private HeadersFooters<Footer>? _Footers;

  DA.PageMargin? DA.SectionPropertiesBase.PageMargin { get => PageMargin; set => PageMargin = (PageMargin?)value; }
  /// <summary>
  ///  Specifies the settings for the document grid, which enables precise layout of full-width East Asian language characters.
  /// </summary>
  public PageMargin? PageMargin
  {
    get
    {
      if (_PageMargin == null)
      {
        var PageMarginElement = OpenXmlElement.GetPageMargin();
        if (PageMarginElement != null)
          _PageMargin = new PageMargin(this, PageMarginElement);
      }
      return _PageMargin;
    }
    set
    {
      if (value == PageMargin) return;
      OpenXmlElement.SetPageMargin(value?.OpenXmlElement);
      _PageMargin = value;
      NotifyPropertyChanged(nameof(PageMargin));
    }
  }
  private PageMargin? _PageMargin;

  DA.PageSize? DA.SectionPropertiesBase.PageSize { get => PageSize; set => PageSize = (PageSize?)value; }
  /// <summary>
  ///  Specifies the settings for the document grid, which enables precise layout of full-width East Asian language characters.
  /// </summary>
  public PageSize? PageSize
  {
    get
    {
      if (_PageSize == null)
      {
        var PageSizeElement = OpenXmlElement.GetPageSize();
        if (PageSizeElement != null)
          _PageSize = new PageSize(this, PageSizeElement);
      }
      return _PageSize;
    }
    set
    {
      if (value == PageSize) return;
      OpenXmlElement.SetPageSize(value?.OpenXmlElement);
      _PageSize = value;
      NotifyPropertyChanged(nameof(PageSize));
    }
  }
  private PageSize? _PageSize;

  DA.PaperSource? DA.SectionPropertiesBase.PaperSource { get => PaperSource; set => PaperSource = (PaperSource?)value; }
  /// <summary>
  ///  Specifies the settings for the document grid, which enables precise layout of full-width East Asian language characters.
  /// </summary>
  public PaperSource? PaperSource
  {
    get
    {
      if (_PaperSource == null)
      {
        var PaperSourceElement = OpenXmlElement.GetPaperSource();
        if (PaperSourceElement != null)
          _PaperSource = new PaperSource(this, PaperSourceElement);
      }
      return _PaperSource;
    }
    set
    {
      if (value == PaperSource) return;
      OpenXmlElement.SetPaperSource(value?.OpenXmlElement);
      _PaperSource = value;
      NotifyPropertyChanged(nameof(PaperSource));
    }
  }
  private PaperSource? _PaperSource;

  DA.PrinterSettingsReference? DA.SectionPropertiesBase.PrinterSettingsReference { get => PrinterSettingsReference; set => PrinterSettingsReference = (PrinterSettingsReference?)value; }
  /// <summary>
  ///  Specifies the settings for the document grid, which enables precise layout of full-width East Asian language characters.
  /// </summary>
  public PrinterSettingsReference? PrinterSettingsReference
  {
    get
    {
      if (_PrinterSettingsReference == null)
      {
        var PrinterSettingsReferenceElement = OpenXmlElement.GetPrinterSettingsReference();
        if (PrinterSettingsReferenceElement != null)
          _PrinterSettingsReference = new PrinterSettingsReference(this, PrinterSettingsReferenceElement);
      }
      return _PrinterSettingsReference;
    }
    set
    {
      if (value == PrinterSettingsReference) return;
      OpenXmlElement.SetPrinterSettingsReference(value?.OpenXmlElement);
      _PrinterSettingsReference = value;
      NotifyPropertyChanged(nameof(PrinterSettingsReference));
    }
  }
  private PrinterSettingsReference? _PrinterSettingsReference;


  DA.DocGrid? DA.SectionPropertiesBase.DocGrid { get => DocGrid; set => DocGrid = (DocGrid?)value; }
  /// <summary>
  ///  Specifies the settings for the document grid, which enables precise layout of full-width East Asian language characters.
  /// </summary>
  public DocGrid? DocGrid
  {
    get
    {
      if (_DocGrid == null)
      {
        var DocGridElement = OpenXmlElement.GetFirstChild<DXW.DocGrid>();
        if (DocGridElement != null)
          _DocGrid = new DocGrid(this, DocGridElement);
      }
      return _DocGrid;
    }
    set
    {
      if (value == DocGrid) return;
      var DocGridElement = OpenXmlElement.GetFirstChild<DXW.DocGrid>();
      if (DocGridElement != null)
        OpenXmlElement.RemoveChild(DocGridElement);
      if (value != null)
        OpenXmlElement.AppendChild(value.OpenXmlElement);
      _DocGrid = value;
      NotifyPropertyChanged(nameof(DocGrid));
    }
  }
  private DocGrid? _DocGrid;

}
