namespace DocxControls;

public class DesignTimePropertiesViewModel: PropertiesViewModel
{

  public DesignTimePropertiesViewModel()
  {
    Properties.Add(new PropertyViewModel { Caption = "Title", Name="Title", Type=typeof(string), Value = "Title of the document" });
    Properties.Add(new PropertyViewModel { Caption = "Author", Name = "Creator", Type = typeof(string), Value = "My Name" });
    Properties.Add(new PropertyViewModel { Caption = "Created at", Name = "CreatedAt", Type = typeof(DateTime), Value = DateTime.Now  });
    CanAddAndDeleteProperties = true;
  }
}
