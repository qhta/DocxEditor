﻿using Docx.Automation;

using Qhta.MVVM;

namespace DocxControls.ViewModels;

/// <summary>
/// View model for a body element: paragraph, table, etc.
/// </summary>
public class ElementViewModel : ObjectViewModel, IElement, DA.ISelectable
{
  /// <summary>
  /// Default constructor.
  /// </summary>
  public ElementViewModel() : base()
  {
  }

  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="element">Modeled OpenXmlElement</param>
  public ElementViewModel(object element) : base(element)
  {
  }

  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="owner">owner ViewModel</param>
  /// <param name="element">Modeled OpenXmlElement</param>
  public ElementViewModel(ViewModel owner, object element) : base(owner, element)
  {
  }

  /// <summary>
  /// Element of the document
  /// </summary>
  public DX.OpenXmlElement? Element
  {
    get => (DX.OpenXmlElement?)ModeledObject;
    set => ModeledObject = value;
  }


  #region IElement implementation  -----------------------------------------------------------------------------------------------------------------
  DA.Application DA.IElement.Application => Application.Instance;
  object? DA.IElement.Parent => Owner;
  #endregion


  #region ISelectable implementation ----------------------------------------------------------------------------------------------------------------

  /// <summary>
  /// Selects the object.
  /// </summary>
  public virtual void Select()
  {
    IsSelected = true;
  }

  /// <summary>
  /// Selects the object. In a composite object, selects all children.
  /// </summary>
  public virtual void SelectAll()
  {
    Select();
  }
  #endregion

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
