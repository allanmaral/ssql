using AntlrCSharp.TypeConverter;

namespace AntlrCSharp.Mappers;

public static class ValueTypeMapper
{
    private static readonly Dictionary<Type, ITypeMapper> DefaultMappers = new() { };
    private static readonly Dictionary<Type, ITypeMapper> CustomMappers = new() { };

    public static object Map(object value, Type type)
    {
        if (CustomMappers.TryGetValue(type, out var customMapper))
        {
            return customMapper.Convert(value, type);
        }

        if (DefaultMappers.TryGetValue(type, out var defaultMapper))
        {
            return defaultMapper.Convert(value, type);
        }

        return Convert.ChangeType(value, type);
    }

    public static void RegisterCustomMapper(Type type, ITypeMapper mapper)
    {
        if (CustomMappers.ContainsKey(type))
        {
            throw new ArgumentException($"The is already a custom mapper for the type {type} registered", nameof(type));
        }

        CustomMappers.Add(type, mapper);
    }
}