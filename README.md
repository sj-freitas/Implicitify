# Implicitify
A C# library that allows classes to implement interfaces implicitly. Allows for any class to implement any interface as long as the class has the same method and/or property signature.

## Usage
```csharp
using Implicitify;

public interface IDirectoryInfo
{
    string Name { get; }
}

public static class Program
{
    public static void Main()
    {
        // DirectoryInfo is a System.IO class, it does not implement
        // any interface, however, it would be nice to abstract it.
        var test = new System.IO.DirectoryInfo(@"C:\Hello World\");

        // Wrapped is an IDirectoryInfo implementation!
        var wrapped = test.As<IDirectoryInfo>();

        // Will "Hello World"
        Console.WriteLine(directory.Name);
    }
}
```

## Motivation
When adding abstractions on top of well-known C# classes such as `FileInfo`, `HttpClient` or even any external nuget that one may wish to extend. There's always the complicated extra step of having to create an interface for these types that add the methods and then creating a Wrapper. However this is too much work, although it guarantees type-safety.

## Inspiration
In languages such as Go interfaces are always implemented implicitly, this allows for greater abstractions that the implementations aren't aware of. However in both C# and Java this isn't a feature.
