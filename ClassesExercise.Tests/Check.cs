using System.Reflection;

namespace ClassesExercise.Tests;

// The tests reach your classes through these helpers, so the project builds
// before you've written them. Each helper fails with a message that says
// what's missing. You don't need to change this file.
public enum Access { None, Public, Private }

public static class Check
{
    static readonly Assembly Exercise = Assembly.Load("ClassesExercise");

    public static Type Class(string name)
    {
        Type? type = Exercise.GetType($"ClassesExercise.{name}");
        Assert.IsNotNull(type, $"Create a public class named {name} in the ClassesExercise project (with namespace ClassesExercise;).");
        return type;
    }

    // set: Public for { get; set; }, Private for { get; private set; }, None for a calculated property (get only).
    public static void Property(string className, string name, Type type, Access set)
    {
        PropertyInfo? property = Class(className).GetProperty(name);
        Assert.IsNotNull(property, $"{className} needs a public property named {name}.");
        Assert.AreEqual(type, property.PropertyType, $"{className}.{name} should be a {TypeName(type)}.");
        Assert.IsTrue(property.GetMethod?.IsPublic == true, $"Anyone should be able to read {className}.{name}: it needs a public get.");

        MethodInfo? setter = property.SetMethod;
        string expected = set switch
        {
            Access.Public => "a public set",
            Access.Private => "a private set",
            _ => "no set at all: it's calculated from other properties",
        };
        bool matches = set switch
        {
            Access.Public => setter?.IsPublic == true,
            Access.Private => setter?.IsPrivate == true,
            _ => setter is null,
        };
        Assert.IsTrue(matches, $"{className}.{name} should have {expected}.");
    }

    public static object New(string className, params object[] arguments)
    {
        Type type = Class(className);
        Type[] types = arguments.Select(a => a.GetType()).ToArray();
        ConstructorInfo? constructor = type.GetConstructor(types);
        string parameters = types.Length == 0 ? "no parameters" : string.Join(", ", types.Select(TypeName));
        Assert.IsNotNull(constructor, $"{className} needs a public constructor that takes {parameters}.");
        return constructor.Invoke(arguments);
    }

    public static T Get<T>(object target, string name)
    {
        PropertyInfo? property = target.GetType().GetProperty(name);
        Assert.IsNotNull(property, $"{target.GetType().Name} needs a public property named {name}.");
        object? value = property.GetValue(target);
        Assert.IsInstanceOfType<T>(value, $"{target.GetType().Name}.{name} should be a {TypeName(typeof(T))}.");
        return (T)value;
    }

    public static void Set(object target, string name, object value)
    {
        PropertyInfo? property = target.GetType().GetProperty(name);
        Assert.IsNotNull(property, $"{target.GetType().Name} needs a public property named {name}.");
        Assert.IsTrue(property.SetMethod?.IsPublic == true, $"{target.GetType().Name}.{name} needs a public set.");
        property.SetValue(target, value);
    }

    public static object? Call(object target, string name, params object[] arguments)
    {
        Type[] types = arguments.Select(a => a.GetType()).ToArray();
        MethodInfo? method = target.GetType().GetMethod(name, types);
        string parameters = types.Length == 0 ? "no parameters" : string.Join(", ", types.Select(TypeName));
        Assert.IsNotNull(method, $"{target.GetType().Name} needs a public method {name} that takes {parameters}.");
        return method.Invoke(target, arguments);
    }

    public static void Returns(string className, string method, Type returnType, params Type[] parameters)
    {
        MethodInfo? info = Class(className).GetMethod(method, parameters);
        string list = parameters.Length == 0 ? "no parameters" : string.Join(", ", parameters.Select(TypeName));
        Assert.IsNotNull(info, $"{className} needs a public method {method} that takes {list}.");
        Assert.AreEqual(returnType, info.ReturnType, $"{className}.{method} should return {TypeName(returnType)}.");
    }

    static string TypeName(Type type) => type == typeof(int) ? "int" : type == typeof(double) ? "double"
        : type == typeof(bool) ? "bool" : type == typeof(string) ? "string" : type == typeof(void) ? "void" : type.Name;
}
