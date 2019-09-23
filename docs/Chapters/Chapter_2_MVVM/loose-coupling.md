[Contents](README.md)

----

[Prev](async-programming.md)

# Loose Coupling with Interfaces
[Interfaces](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/interfaces/) are one of those topics in Object Orientated Programming (OOP) that might have left you wondering "what's the point"?

You may have encountered the term 'design pattern', typically a collection of known solutions to problems in OOP, within which you will likely encounter interfaces. In fact, some people can have quite strong views about interfaces.

The focus of this course needs is not on design patterns, but on cross platform development using Xamarin.Forms. To take it all on is going to overload the learner. However, we cannot completely ignore topics such as design patterns, although I will try to keep it to a minimum.

## Interfaces and Classes
You're probably familiar with a class. A class is a data type that encapsulates data and methods. Therefore, any given class offers certain guaranteed functionality (i.e. performs spefic tasks through methods. Because C# is a compiled and type-safe language, we know that when we instantiate a class type, we know the behaviour (methods) we are getting. 

> If we instantiate a class, and try and invoke a non-existing method in our code, the compiler will report this as an error and we won't even get to run the code. This avoids the embarrasment of run-time errors.

This is known as type-safety.

A class can also inherit diretly from one (and only one) parent class. In doing this, it also _inherits_ some (non private) of those behavious as well. _It is assumed you already know about class inheritance, method overrides and poymorphism_.

You can make a class _abstract_. Such a class is incomplete, and can only be used as a parent class. Typically, some default behavior is included, and the child classes override the missing behaviour.

An interface contains no concrete code. 

## Why use interfaces
- C# only supports single inheritance. For multiple inheritance, you need to use interfaces
- A struct cannot inherit a parent, but can implement any number of interfaces

[Next - Handling Exceptions with Try-Catch](try-catch.md)

----

[Contents](/docs/README.md)