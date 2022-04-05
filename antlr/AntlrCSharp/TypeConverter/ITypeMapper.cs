namespace AntlrCSharp.TypeConverter;

public interface ITypeMapper
{
    public object Convert(object value, Type propertyType);
}