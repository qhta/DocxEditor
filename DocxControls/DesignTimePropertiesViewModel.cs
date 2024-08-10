﻿namespace DocxControls;

/// <summary>
/// Design-time view model for the properties of a document.
/// </summary>
public class DesignTimePropertiesViewModel: PropertiesViewModel
{

  /// <summary>
  /// Default constructor
  /// </summary>
  public DesignTimePropertiesViewModel()
  {
    Properties.Add(new PropertyViewModel { Caption = "Title", Name="Title", PropType=typeof(string), Value = "Title of the document" });
    Properties.Add(new PropertyViewModel { Caption = "Author", Name = "Creator", PropType = typeof(string), Value = "My Name" });
    Properties.Add(new PropertyViewModel { Caption = "Created at", Name = "CreatedAt", PropType = typeof(DateTime), Value = DateTime.Now  });
    CanAddAndDeleteProperties = true;
  }
}
