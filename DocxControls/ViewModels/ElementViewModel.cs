﻿using Docx.Automation;

namespace DocxControls.ViewModels;

/// <summary>
/// View model for any DocumentFormat.OpenXml.OpenXmlElement.
/// </summary>
public abstract class ElementViewModel : ObjectViewModel, _Element, DA._Selectable
{

  /// <summary>
  /// Constructor with owner object and modeled element.
  /// </summary>
  /// <param name="owner">owner ViewModel</param>
  /// <param name="modeledElement">Modeled ModeledElement</param>
  protected ElementViewModel(ViewModel? owner, DX.OpenXmlElement? modeledElement) : base(owner, modeledElement)
  {
  }

  /// <summary>
  /// ModeledElement of the document
  /// </summary>
  internal DX.OpenXmlElement? ModeledElement => (DX.OpenXmlElement?)ModeledObject;

  /// <summary>A method to notify that a property has changed.</summary>
  /// <param name="propertyName"></param>
  public new virtual void NotifyPropertyChanged(string propertyName)
  {
    if (propertyName==nameof(DX.OpenXmlElement.OuterXml))
      NotifyPropertyChanged(nameof(DisplayText));
    else
      base.NotifyPropertyChanged(propertyName);
  }

  /// <summary>
  /// Notifies that all properties have changed.
  /// </summary>
  public virtual void NotifyAllPropertiesChanged()
  {
    foreach (var property in GetType().GetProperties().Where(p=>p.CanWrite))
    {
      Debug.WriteLine($"NotifyAllPropertiesChanged Property={property.Name}");
      NotifyPropertyChanged(property.Name);
    }
  }

  #region IElement implementation  -----------------------------------------------------------------------------------------------------------------
  DA.Application DA._Element.Application => Application.Instance;
  /// <summary>
  /// Application object.
  /// </summary>
  public Application Application => Application.Instance;

  object? DA._Element.Parent => Owner;
  /// <summary>
  /// Parent element of the document
  /// </summary>
  public ViewModel? Parent => Owner;

  /// <summary>
  /// Get the ancestor of the specified type.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <returns></returns>
  public T? GetAncestor<T>() where T : class
  {
    if (Owner is T parent)
      return parent;
    return (Owner as ElementViewModel)?.GetAncestor<T>();
  }

  /// <summary>
  /// Removes the element from the document.
  /// </summary>
  public bool Remove()
  {
    if (ModeledElement is null) return false;
    if (ModeledElement.Parent!=null)
      ModeledElement.Remove();
    if (Parent is CompoundElementViewModel compoundParent)
      compoundParent.Elements.Remove(this);
    return true;
  }
  #endregion

  #region Tree navigation ----------------------------------------------------------------------------------------------------------------------------

  /// <summary>
  /// Simple element view model does not have children.
  /// </summary>
  public virtual bool HasChildren => false;

  /// <summary>
  /// Simple element view model does not have children.
  /// </summary>
  public virtual ElementViewModel? FirstChild => null;

  /// <summary>
  /// Simple element view model does not have children.
  /// </summary>
  public virtual ElementViewModel? LastChild => null;

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public virtual ElementViewModel? NextSibling()
  {
    if (Parent is CompoundElementViewModel parent)
    {
      var index = parent.Elements.IndexOf(this);
      if (index < parent.Elements.Count - 1)
        return parent.Elements[index + 1];
    }
    return null;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public virtual ElementViewModel? PreviousSibling()
  {
    if (Parent is CompoundElementViewModel parent)
    {
      var index = parent.Elements.IndexOf(this);
      if (index > 0)
        return parent.Elements[index - 1];
    }
    return null;
  }

  /// <summary>
  /// Checks if the element is the specified type.
  /// </summary>
  /// <param name="type">
  /// Specified type to check the element.
  /// It must be one of the DocumentFormat.OpenXml.ModeledElement type.
  /// It must not be an empty array.
  /// </param>
  /// <returns></returns>
  public bool IsType(Type type)
  {
    if (type == this.GetType())
      return true;
    var baseType = this.GetType().BaseType;
    while (baseType != null)
    {
      if (type == baseType)
        return true;
      baseType = baseType.BaseType;
    }
    return false;
  }

  /// <summary>
  /// Checks if the element is one of the specified types.
  /// </summary>
  /// <param name="types">
  /// A set of element types to check the element.
  /// It must be one of the DocumentFormat.OpenXml.ModeledElement types.
  /// It must not be an empty array.
  /// </param>
  /// <returns></returns>
  public bool HasType(Type[] types)
  {
    if (types.Contains(this.GetType()))
      return true;
    var baseType = this.GetType().BaseType;
    while (baseType != null)
    {
      if (types.Contains(baseType))
        return true;
      baseType = baseType.BaseType;
    }
    return false;
  }

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
      var str = CleanXml(ModeledElement?.OuterXml);
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
