﻿using Docx.Automation;

using Qhta.MVVM;

namespace DocxControls;

/// <summary>
/// View model for a body element: paragraph, table, etc.
/// </summary>
public abstract class ElementViewModel : ObjectViewModel, IElement
{

  /// <summary>
  /// Element of the document
  /// </summary>
  public DX.OpenXmlElement? Element
  {
    get => (DX.OpenXmlElement?)ModeledObject;
    set => ModeledObject = value;
  }

  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="parent">parent ViewModel</param>
  /// <param name="element">Modeled OpenXmlElement</param>
  protected ElementViewModel(ViewModel? parent, object element) : base(parent, element)
  {
  }

  /// <summary>
  /// Returns an Application object that represents the DocxControls application.
  /// </summary>
  public DA.Application Application => throw new NotImplementedException();


  /// <summary>
  /// Access to outer Xml of the element
  /// </summary>
  public virtual string? DisplayText
  {
    get
    {
      var str = CleanXml(Element?.OuterXml);
      if (str != null && str.Length > 1000)
        str = str.Substring(0, 996) + " ...";
      return str;
    }
  }

  /// <summary>
  /// Removes unnecessary tags from xml.
  /// </summary>
  /// <param name="xml"></param>
  /// <returns></returns>
  protected string? CleanXml(string? xml)
  {
    return xml?.Replace(" xmlns:w=\"http://schemas.openxmlformats.org/wordprocessingml/2006/main\"", "").Replace("<w:", "<").Replace("</w:", "</");
  }

  /// <summary>
  /// Displayed tooltip with the name of the bookmark
  /// </summary>
  public string? ToolTip => ModeledObject?.GetType().Name;

}
