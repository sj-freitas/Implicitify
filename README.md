# Implicitify
A C# library that allows classes to implement interfaces implicitly

## Motivation
When adding abstractions on top of well-known C# classes such as `FileInfo`, `HttpClient` or even any external nuget that one may wish to extend. There's always the complicated extra step of having to create an interface for these types that add the methods and then creating a Wrapper. However this is too much work, although it guarantees type-safety.

## Inspiration
In languages such as Go interfaces are always implemented implicitly, this allows for greater abstractions that the implementations aren't aware of. However in both C# and Java this isn't a feature.

## The Solution
### Creating a wrapper dynamically
Solves this issue, by requiring the programmer just to create the interface with the specific methods or properties to abstract.

### Using a Proxy
Allows for an interceptor to call the real instance's methods from the interface declaration.

