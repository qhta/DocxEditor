namespace DocxControls.Helpers;
internal struct ViewModelTypeDescriptor
{
  public Type type;
  public ConstructorInfo constructorInfo;
  public PropertyInfo[] properties;

  /// <summary>
  /// Constructor for ViewModelTypeDescriptor
  /// </summary>
  /// <param name="type"></param>
  /// <param name="constructorInfo"></param>
  /// <param name="properties"></param>
  public ViewModelTypeDescriptor(Type type, ConstructorInfo constructorInfo, PropertyInfo[] properties)
  {
    this.type = type;
    this.constructorInfo = constructorInfo;
    this.properties = properties;
  }
}
