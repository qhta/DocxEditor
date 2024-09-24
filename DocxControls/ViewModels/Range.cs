using Docx.Automation;

using Qhta.MVVM;

namespace DocxControls.ViewModels;

/// <summary>
/// Represents a contiguous area in a document. Each Range object is defined by a starting and ending element.
/// </summary>
public class Range : ViewModel, DA.Range
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  public Range(object parent, object start, object end)
  {
    Parent = parent;
    Start = start;
    End = end;
  }

  #region DA.Range properties implementation -----------------------------------------------------------------------------------
  /// <summary>
  /// Element that starts the range.
  /// </summary>
  public object Start { get; private set; }

  /// <summary>
  /// Element that ends the range.
  /// </summary>
  public object End { get; private set; }

  DA.Application DA.Range.Application => Application;

  /// <summary>
  /// Returns an object that represents the DocxControls application.
  /// </summary>
  public Application Application => DocxControls.Application.Instance;

  /// <summary>
  /// Returns the parent object for the specified object.
  /// </summary>
  public object? Parent { get; }
  #endregion

  #region DA.Range methods implementation -----------------------------------------------------------------------------------

  /// <summary>
  /// Collapses a range or selection to the starting or ending position. After a range or selection is collapsed, the starting and ending points are equal.
  /// </summary>
  /// <param name="direction">
  /// The direction in which to collapse the range or selection. Can be either of the following MoveDirection constants: Forward or Backward. The default value is Backward.
  /// </param>
  public void Collapse(MoveDirection? direction = MoveDirection.Backward)
  {
    if (direction == MoveDirection.Backward)
    {
      End = Start;
    }
    else
    {
      Start = End;
    }
  }

  /// <summary>
  /// Collapses the specified range to its start or end position and then moves the collapsed object by the specified number of units.
  /// </summary>
  /// <param name="unit"> The unit by which to move the range.</param>
  /// <param name="count">
  /// The number of units by which the specified range is to be moved.
  /// If Count is a positive number, the object is collapsed to its end position and moved backward in the document by the specified number of units.
  /// If Count is a negative number, the object is collapsed to its start position and moved forward by the specified number of units.
  /// The default value is 1.
  /// You can also control the collapse direction by using the Collapse method before using the Move method.
  /// If the range is in the middle of a unit or isn't collapsed, moving it to the beginning or end of the unit counts as moving it one full unit.
  /// </param>
  /// <result>
  /// This method returns an integer that indicates the number of units by which the object was actually moved, or it returns 0 (zero) if the move was unsuccessful.
  /// </result>
  public int Move(MoveUnits? unit, int? count = 1)
  {
    if (count > 0)
    {
      Collapse(MoveDirection.Forward);
      return MoveEnd(unit, -count);
    }
    else
    {
      Collapse(MoveDirection.Backward);
      return MoveStart(unit, -count);
    }
  }


  /// <summary>
  /// Moves the ending position of a range or selection.
  /// </summary>
  /// <param name="unit">The unit by which to move the ending character position.</param>
  /// <param name="count">
  /// The number of units to move.
  /// If this number is positive, the ending character position is moved forward in the document.
  /// If this number is negative, the end is moved backward.
  /// If the ending position overtakes the starting position, the range collapses and both character positions move together.
  /// The default value is 1.</param>
  /// <returns>
  /// This method returns an integer that indicates the number of units the range actually moved, or it returns 0 (zero) if the move was unsuccessful.
  /// </returns>
  public int MoveEnd(MoveUnits? unit = MoveUnits.Element, int? count = 1)
  {
    if (End is DX.OpenXmlElement element)
    {
      var moveStartAlso = End == Start;
      if (count > 0)
      {
        int n = 0;
        for (int i = 0; i < count; i++)
        {
          if (!MoveElementForward(ref element, unit))
            break;
          End = element;
          if (moveStartAlso)
            Start = element;
          moveStartAlso = End == Start;
          n++;
        }
        return n;
      }
    }
    return 0;
  }


  /// <summary>
  /// Moves the start position of the specified range.
  /// </summary>
  /// <param name="unit">The unit by which start position of the specified range is to be moved.</param>
  /// <param name="count">
  /// The maximum number of units by which the specified range is to be moved.
  /// If Count is a positive number, the start position of the range is moved forward in the document.
  /// If it is a negative number, the start position is moved backward.
  /// If the start position is moved forward to a position beyond the end position, the range is collapsed and both the start and end positions move together.
  /// The default value is 1.
  /// </param>
  /// <returns>
  /// This method returns an integer that indicates the number of units by which the start position or the range actually moved,
  /// or it returns 0 (zero) if the move was unsuccessful.
  /// </returns>
  public int MoveStart(MoveUnits? unit = MoveUnits.Element, int? count = 1)
  {
    if (Start is DX.OpenXmlElement element)
    {
      var moveEndAlso = End == Start;
      if (count > 0)
      {
        int n = 0;
        for (int i = 0; i < count; i++)
        {
          if (!MoveElementBackward(ref element, unit))
            break;
          End = element;
          if (moveEndAlso)
            End = element;
          moveEndAlso = End == Start;
          n++;
        }
        return n;
      }
    }
    return 0;
  }

  /// <summary>
  /// Moves the end position of the specified range until any of the specified types elements are found in the document. If the movement is forward in the document, the range is expanded.
  /// </summary>
  /// <param name="types">
  /// A set of element types to move the end position of the specified range until one of them is found in the document.
  /// It must be one of the DocumentFormat.OpenXml.OpenXmlElement types.
  /// It must not be an empty array.
  /// </param>
  /// <param name="direction">
  /// The direction in which to move the range or selection. Can be either of the following MoveDirection constants: Forward or Backward. The default value is Backward.
  /// </param>
  /// <param name="Count">
  /// The maximum number of elements by which the specified range is to be moved. The default value is int.MaxValue (unlimited).
  /// If the ending position overtakes the starting position, the range collapses and both positions move together.
  /// </param>
  public int MoveEndUntil(Type[] types, MoveDirection? direction = MoveDirection.Forward, int? Count = int.MaxValue)
  {
    if (End is DX.OpenXmlElement element)
    {
      var moveStartAlso = End == Start;
      int n = 0;
      for (int i = 0; i < Count; i++)
      {

        if (direction == MoveDirection.Forward)
        {
          if (!MoveElementForward(ref element, MoveUnits.Element))
            break;
        }
        else
        {
          if (!MoveElementBackward(ref element, MoveUnits.Element))
            break;
        }
        End = element;
        if (moveStartAlso)
          Start = element;
        moveStartAlso = End == Start;
        n++;
        if (element.HasType(types))
          break;
      }
      return n;
    }
    return 0;
  }

  /// <summary>
  /// Moves the start position of the specified range until one of the specified types elements is found in the document.
  /// </summary>
  /// <param name="types">
  /// A set of element types to move the end position of the specified range until one of them is found in the document.
  /// It must be one of the DocumentFormat.OpenXml.OpenXmlElement types.
  /// It must not be an empty array.
  /// </param>
  /// <param name="direction">
  /// The direction in which to move the range or selection. Can be either of the following MoveDirection constants: Forward or Backward. The default value is Backward.
  /// </param>
  /// <param name="Count">
  /// The maximum number of elements by which the specified range is to be moved. The default value is int.MaxValue (unlimited).
  ///  If the start position is moved forward to a position beyond the end position, the range is collapsed and both the start and end positions move together.
  /// </param>
  /// <returns>
  /// This method returns the number of element by which the start position of the specified range moved.
  /// </returns>
  public int MoveStartUntil(Type[] types, MoveDirection? direction = MoveDirection.Forward, int? Count = int.MaxValue)
  {
    if (Start is DX.OpenXmlElement element)
    {
      var moveEndAlso = End == Start;
      int n = 0;
      for (int i = 0; i < Count; i++)
      {
        if (direction == MoveDirection.Forward)
        {
          if (!MoveElementForward(ref element, MoveUnits.Element))
            break;
        }
        else
        {
          if (!MoveElementBackward(ref element, MoveUnits.Element))
            break;
        }
        Start = element;
        if (moveEndAlso)
          End = element;
        moveEndAlso = End == Start;
        n++;
        if (element.HasType(types))
          break;
      }
      return n;
    }
    return 0;
  }

  /// <summary>
  /// Moves the end position of the specified range while any of the specified types elements are found in the document. If the movement is forward in the document, the range is expanded.
  /// </summary>
  /// <param name="types">
  /// A set of element types to move the end position of the specified range while one of them is found in the document.
  /// It must be one of the DocumentFormat.OpenXml.OpenXmlElement types.
  /// It must not be an empty array.
  /// </param>
  /// <param name="direction">
  /// The direction in which to move the range or selection. Can be either of the following MoveDirection constants: Forward or Backward. The default value is Backward.
  /// </param>
  /// <param name="Count">
  /// The maximum number of elements by which the specified range is to be moved. The default value is int.MaxValue (unlimited).
  /// If the ending position overtakes the starting position, the range collapses and both positions move together.
  /// </param>
  public int MoveEndWhile(Type[] types, MoveDirection? direction = MoveDirection.Forward, int? Count = int.MaxValue)
  {
    if (End is DX.OpenXmlElement element)
    {
      var moveStartAlso = End == Start;
      int n = 0;
      for (int i = 0; i < Count; i++)
      {
        if (!element.HasType(types))
          break;
        if (direction == MoveDirection.Forward)
        {
          if (!MoveElementForward(ref element, MoveUnits.Element))
            break;
        }
        else
        {
          if (!MoveElementBackward(ref element, MoveUnits.Element))
            break;
        }
        End = element;
        if (moveStartAlso)
          Start = element;
        moveStartAlso = End == Start;
        n++;
      }
      return n;
    }
    return 0;
  }

  /// <summary>
  /// Moves the start position of the specified range while one of the specified types elements is found in the document.
  /// </summary>
  /// <param name="types">
  /// A set of element types to move the end position of the specified range while one of them is found in the document.
  /// It must be one of the DocumentFormat.OpenXml.OpenXmlElement types.
  /// It must not be an empty array.
  /// </param>
  /// <param name="direction">
  /// The direction in which to move the range or selection. Can be either of the following MoveDirection constants: Forward or Backward. The default value is Backward.
  /// </param>
  /// <param name="Count">
  /// The maximum number of elements by which the specified range is to be moved. The default value is int.MaxValue (unlimited).
  ///  If the start position is moved forward to a position beyond the end position, the range is collapsed and both the start and end positions move together.
  /// </param>
  /// <returns>
  /// This method returns the number of element by which the start position of the specified range moved.
  /// </returns>
  public int MoveStartWhile(Type[] types, MoveDirection? direction = MoveDirection.Forward, int? Count = int.MaxValue)
  {
    if (Start is DX.OpenXmlElement element)
    {
      var moveEndAlso = End == Start;
      int n = 0;
      for (int i = 0; i < Count; i++)
      {
        if (!element.HasType(types))
          break;
        if (direction == MoveDirection.Forward)
        {
          if (!MoveElementForward(ref element, MoveUnits.Element))
            break;
        }
        else
        {
          if (!MoveElementBackward(ref element, MoveUnits.Element))
            break;
        }
        Start = element;
        if (moveEndAlso)
          End = element;
        moveEndAlso = End == Start;
        n++;
      }
      return n;
    }
    return 0;
  }

  /// <summary>
  /// Collapses the range and moves the range until any of the specified types elements are found in the document.
  /// </summary>
  /// <param name="types">
  /// A set of element types to move the end position of the specified range until one of them is found in the document.
  /// It must be one of the DocumentFormat.OpenXml.OpenXmlElement types.
  /// It must not be an empty array.
  /// </param>
  /// <param name="direction">
  /// The direction in which to move the range or selection. Can be either of the following MoveDirection constants: Forward or Backward. The default value is Forward.
  /// </param>
  /// <param name="Count">
  /// The maximum number of elements by which the specified range is to be moved. The default value is int.MaxValue (unlimited).
  /// </param>
  public int MoveUntil(Type[] types, MoveDirection? direction = MoveDirection.Forward, int? Count = int.MaxValue)
  {
    Collapse(direction);
    if (direction == MoveDirection.Forward)
      return MoveStartUntil(types, direction, Count);
    else
      return MoveEndUntil(types, direction, Count);
  }

  /// <summary>
  /// Collapses the range and moves the range while any of the specified types elements are found in the document.
  /// </summary>
  /// <param name="types">
  /// A set of element types to move the end position of the specified range while one of them is found in the document.
  /// It must be one of the DocumentFormat.OpenXml.OpenXmlElement types.
  /// It must not be an empty array.
  /// </param>
  /// <param name="direction">
  /// The direction in which to move the range or selection. Can be either of the following MoveDirection constants: Forward or Backward. The default value is Forward.
  /// </param>
  /// <param name="Count">
  /// The maximum number of elements by which the specified range is to be moved. The default value is int.MaxValue (unlimited).
  /// </param>
  public int MoveWhile(Type[] types, MoveDirection? direction = MoveDirection.Forward, int? Count = int.MaxValue)
  {
    Collapse(direction);
    if (direction == MoveDirection.Forward)
      return MoveStartWhile(types, direction, Count);
    else
      return MoveEndWhile(types, direction, Count);
  }

  /// <summary>
  /// Moves the reference of the OpenXmlElement forward by one unit.
  /// </summary>
  /// <param name="element">move element</param>
  /// <param name="unit"></param>
  /// <returns>true if move was successful, false otherwise</returns>
  private bool MoveElementForward(ref DX.OpenXmlElement element, MoveUnits? unit)
  {
    DX.OpenXmlElement? newElement;
    switch (unit)
    {
      case MoveUnits.Element:
        newElement = element.FirstChild;
        if (newElement == null)
          newElement = element.NextSibling();
        if (newElement == null)
        {
          newElement = element.Parent;
          if (newElement != null)
          {
            if (!MoveElementForward(ref newElement, MoveUnits.Sibling))
              return false;
          }
        }
        if (newElement == null)
          return false;
        element = newElement;
        return true;

      case MoveUnits.Sibling:
        newElement = element.NextSibling();
        if (newElement == null)
          return false;
        element = newElement;
        return true;

      case MoveUnits.SiblingOrParentSibling:
        newElement = element.PreviousSibling();
        if (newElement == null)
        {
          newElement = element.Parent;
          if (newElement != null)
          {
            if (!MoveElementForward(ref newElement, MoveUnits.Sibling))
              return false;
          }
        }
        if (newElement == null)
          return false;
        element = newElement;
        return true;

    }
    return false;
  }

  /// <summary>
  /// Moves the reference of the OpenXmlElement backward by one unit.
  /// </summary>
  /// <param name="element">move element</param>
  /// <param name="unit"></param>
  /// <returns>true if move was successful, false otherwise</returns>
  private bool MoveElementBackward(ref DX.OpenXmlElement element, MoveUnits? unit)
  {
    DX.OpenXmlElement? newElement;
    switch (unit)
    {
      case MoveUnits.Element:
        newElement = element.PreviousSibling();
        if (newElement == null)
        {
          newElement = element.Parent;
          if (newElement != null)
          {
            if (newElement.HasChildren)
              newElement = newElement.LastChild;
            else
              return false;
          }
        }
        if (newElement == null)
          return false;
        element = newElement;
        return true;

      case MoveUnits.Sibling:
        newElement = element.PreviousSibling();
        if (newElement == null)
          return false;
        element = newElement;
        return true;

      case MoveUnits.SiblingOrParentSibling:
        newElement = element.PreviousSibling();
        if (newElement == null)
        {
          newElement = element.Parent;
        }
        if (newElement == null)
          return false;
        element = newElement;
        return true;

    }
    return false;
  }

  #endregion
}