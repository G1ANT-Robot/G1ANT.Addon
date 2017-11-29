using System;
using System.Reflection;

public class Dynamic
{
    public static object CallPrivate(object obj, string name, params object[] parameters)
    {
        Type[] types = new Type[parameters.Length];
        for (int i = 0; i < parameters.Length; i++)
            types[i] = parameters[i].GetType();

        return obj.GetType().GetMethod(name, BindingFlags.Instance | BindingFlags.NonPublic,
            null, types, null).Invoke(obj, parameters);
    }

    public static Y CallPrivate<Y>(object obj, string name, params object[] parameters)
    {
        return (Y)CallPrivate(obj, name, parameters);
    }
}