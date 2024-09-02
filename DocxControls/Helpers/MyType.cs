namespace DocxControls;
public class MyType : IComparable<MyType>
{
  public string Name { get; set; }

  public int CompareTo(MyType other)
  {
    if (other == null) return 1;
    return string.Compare(this.Name, other.Name, StringComparison.Ordinal);
  }
}

