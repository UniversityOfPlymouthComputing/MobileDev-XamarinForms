[Contents](../README.md)

----

# C# Generics
The code examples for this section are to be [found in this folder](/code/Chapter3/Templates)

> Generics introduce the concept of type parameters to the .NET Framework, which make it possible to design classes and methods that defer the specification of one or more types until the class or method is declared and instantiated by client code. For example, by using a generic type parameter T, you can write a single class that other client code can use without incurring the cost or risk of runtime casts or boxing operations
>
> [Generics (C# Programming Guide)](https://docs.microsoft.com/dotnet/csharp/programming-guide/generics/)

> For the term _boxing_, [see this link](https://docs.microsoft.com/dotnet/csharp/programming-guide/types/boxing-and-unboxing)

Generics feature in many languages, include C++, Swift and Java. They are a powerful language feature that can promote code reuse. At the same time, they can be confusing, especially at first.

We are not going to dwell on generics too much. The philosophy here is "just enough knowledge" to get the general idea. The Microsoft documentation contains far more detail if you wish to pursue it more deeply.

> For this section, two example projects will be featured. Both of these are console applications, and both are contained within a single Visual Studio Solution [Class Templates](/code/Chapter3/Templates/ClassTemplates)

## Example 1 - An Unbounded Generic Class
Open the solution file and make "ClassTemplatesA-unbounded" the startup project.

* Build and run this code to see what it does
* Review the code in [Program.cs](/code/Chapter3/Templates/ClassTemplates/ClassTemplates/Program.cs)

There are four classes in total, of which three are of interest:

### Class A
This is a simple class with a single integer property `Data`. 

Although no explicit parent class is provided, all objects ultimately inherit from `System.Object` (a quick Internet search on C# System.Object will take you to the documentation for this).

One of the methods in `System.Object` is the `ToString()` method. Any attempt to write an object to the console using `Console.WriteLine` will employ `ToString()`. You are free to override this method to output a custom string as shown in the class below.

```C#
public class ClassA
{
    protected int _data;
    public int Data { get => _data; set => _data = value; }

    public ClassA(int data)
    {
        Data = data;
    }

    // ToString() is to be found in System.Object
    public override string ToString() => "Hi there from class A!";
}
```

### Class B
The next class inherits from `ClassA`. It too overrides `ToString()`.

In addition, it overrides the method `Equals` from `System.Object`. This allows us to write a custom equality test. In this case, the following criteria are used:

* The types must match
* The data properties must match.

This is different from `ClassA` where the default `Equals` method was inherited from `System.Object`

> **Note** - because `Equals` is overridden, the compiler also insists that `GetHashCode` is also overridden. In this case, I've simply combined the base class version and the hash code of the data. _This is not a great example. Generating a unique hash is a complex subject._
This is not the focus of this topic.

```C#
public class ClassB : ClassA
{
    public ClassB(int data) : base(data) { }

    // Equals() is to be found in System.Object
    public override bool Equals(object obj)
    {
        //Different type?
        if (obj.GetType() != this.GetType()) return false;

        //Same type with different data?
        ClassB other = (ClassB)obj;
        if (other.Data != Data) return false;
            
        return true;
    }

    public override string ToString() => "Greetings from class B";
    //Not so great hash code
    public override int GetHashCode()
    {
        return base.GetHashCode() + 27 * Data.GetHashCode();
    }
}
```

Now let's look at how these classes are instantiated and how their behaviour differs.

### The Unconstrained Generic Class `MyClass<T>`
Consider the class below. The first thing that should be apparent is the inclusion of `<T>`. You can think of `T` as an additional parameter, only instead of a value, it's a _Type_.

```C#
public class MyClass<T>
{
    T SomeObject1 { get; set; }
    T SomeObject2 { get; set; }
    public MyClass(T obj1, T obj2)
    {
        SomeObject1 = obj1;
        SomeObject2 = obj2;
        Ident();
        Comp();
        Console.WriteLine("-----------------");
    }
    private void Ident()
    {
        Console.WriteLine(SomeObject1.ToString());
        Console.WriteLine(SomeObject2.ToString());
    }
    private void Comp()
    {
        if (SomeObject1.Equals(SomeObject2))
        {
            Console.WriteLine("We are the same!");
        }
        else
        {
            Console.WriteLine("We are similar, but not the same object!");
        }
    }
}
```

Note in particular the class declaration 

```C# 
public class MyClass<T>
```

The `T` is a placeholder for an as yet unspecified **type**. This _generic type_ can then used in the body of the class definition. 
* Whenever an instance of this class is created or referenced, a type must be provided. 
* In this case, it could absolutely any type. 
* In such cases, we say `T` is unconstrained.

For example, 
```C# 
var m = new MyClass<ClassA>(objA1, objA2);
```

where `T` is specified (at compile time) as the type `ClassA`. Returning to the source, note how the type `T` is used for properties within the class. 

```C#
T SomeObject1 { get; set; }
T SomeObject2 { get; set; }
```

Again, to reiterate, `T` could be **any** type, but as all classes derive from a common base-class `System.Object`, we do know at least something about it.

> All we currently know about the generic type `T` is that is is a subclass of `System.Object`.

Therefore, we can safely invoke methods from `System.Object` on these properties. It is probably worth having a look at the [documentation](https://docs.microsoft.com/dotnet/api/system.object?view=netstandard-2.1) to see which methods are included in `System.Object`

This example makes use of the `Equals` and `ToString` methods::

&ensp; &ensp; In the `Ident()` method, `ToString()` is explicitly invoked:

```C#
Console.WriteLine(SomeObject1.ToString());
```

&ensp; &ensp; In the `Comp()` method, the `Equals` method is invoked:

```C#
if (SomeObject1.Equals(SomeObject2)) {
    ...
}
```

When we run this code, the following is performed as a comparison:

```C#
ClassA objA1 = new ClassA(10);
ClassA objA2 = new ClassA(10);
_ = new MyClass<ClassA>(objA1, objA2);

ClassB objB1 = new ClassB(10);
ClassB objB2 = new ClassB(10);
_ = new MyClass<ClassB>(objB1, objB2);
```

The output is as follows:

```
Unbounded generics demo
Hi there from class A!
Hi there from class A!
We are similar, but not the same object!
-----------------
Greetings from class B
Greetings from class B
We are the same!
-----------------
```

> TASK - Read the code. Can you work out why the outputs are different?

Key to this is understanding the default `Equals` method:

* For the two objects `objA1` and `objA2` (of type `ClassA`), the method `Equals` (from `System.Object`) returns a `false`. This is because they are reference types and refer to separate objects. In this sense, they are not equal as they are not the exact same object.

* For the two objects `objB1` and `objB2` (of type `ClassB`), the method `Equals` was overridden and redefined to return a `true` if both objects had the same type and data value.

The class `MyClass<T>` is unchanged for both cases. It is said to be _generic_

A limitation of the above is that little could be assumed about the type `T` at compile time because it could simply be anything. If we can limit the choice of `T` to a subset of types (including `System.Object`), then we gain more flexibility.

## Example 2 - A Constrained Generic Class
As we saw above, a generic type can be used to allow a class definition greater flexibility. However, in the previous example, the generic type was unconstrained. In this next example we will add further flexibility by adding constraints to the generic type.

Let's look at an example, [which can be found here](/code/Chapter3/Templates/ClassTemplates/ClassTemplatesB/Program.cs)

* Study the code example in the second project `ClassTemplatesB-constrained`
* Run the code to see what it does

The output should be as follows:

```
Listing all objects in order of increasing magnitude
ClassXY: |(0.300,0.400)| = 0.500
ClassXY: |(3.000,4.000)| = 5.000
ClassXY: |(1.000,10.000)| = 10.050
```

Let's now look at some key points for each class

### Starting at the top : the `Program` class
In the `Program` class, we see the following lines:

```C#
SortedCollectionClass<ClassXY> container = new SortedCollectionClass<ClassXY>();
container?.AddObject(new ClassXY(3.0,4.0));
container?.AddObject(new ClassXY(1.0, 10.0));
container?.AddObject(new ClassXY(0.3, 0.4));
container?.ListAll();
```

> The objective of the application is to create a list of (x,y) coordinates and sort them in order of line length (aka Magnitude).

Note that the class `SortedCollectionClass<T>` is _generic_. In this example, the generic type `T` is defined as `ClassXY`. You could use others of course.

> The class `ClassXY` encapsulates a pair of cartesian coordinates (`xx`,`yy`). It has a single public property `Magnitude` which is the length of the line from the origin (0,0) to point (`xx`, `yy`). The mathematical details are unimportant for the purpose of this discussion.

Let's now drill down deeper to see some of the detailed implementation.

### The class `SortedCollectionClass<T>`
Consider the source for `SortedCollectionClass<T>` below
```C#
public class SortedCollectionClass<T> where T : IComparable
{
    public List<T> Objects { get; } = new List<T>();

    public void AddObject(T obj)
    {
        Objects.Add(obj);
        Objects.Sort();
    }

    public void ListAll()
    {
        Console.WriteLine("Listing all objects in order of increasing magnitude");
        foreach (T obj in Objects)
        {
            Console.WriteLine(obj); //Calls ToString
        }
    }
}
```

Some key points:

1. The class declaration shows us how to apply a **constraint** to a generic type using the `where` keyword. The _constraint_ is that T **must** be of type `IComparible`.
```C#
   public class SortedCollectionClass<T> where T : IComparable
```

2. The `Objects` property. This is of type `List<T>`, where `T` is the type of object in the list. [See here for more details about generic lists](https://docs.microsoft.com/dotnet/api/system.collections.generic.list-1?view=netframework-4.8)

We can now state the following:

* The class `ClassXY` indeed implements the interface `IComparible`, so can also be considered to be of type `IComparible` as well (as discussed previously).
* Knowing `T` is of type `IComparible` guarantees all the methods and properties in that interface are implemented. In this case, this is limited to the method `int CompareTo(object)`
* `T` is said to be _constrained_ to type `IComparible` (and `System.Object`). If `ClassXY` did not implement `IComparible` the compiler would generate an error.

For more details, see the [Microsoft documentation](https://docs.microsoft.com/dotnet/csharp/programming-guide/generics/constraints-on-type-parameters)

### The class `ClassXY`
Following a point above, we can look at `ClassXY`. The class declaration implements the interface `IComparable`. 

```C#
public class ClassXY : IComparable
{
    ...
}
```
This in turn forces the developer to implement the only method in `IComparable` which is `CompareTo`. This method is used for overriding how objects are sorted. The implementation is shown below.

```C#
//Used in sorting - Return +1 when this precedes obj, 0 for same, -1 for obj precedes this
public int CompareTo(object obj)
{
    // other = (ClassXY)obj IF obj is of type ClassXY
    // for details on patttern matching with is, see https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/is
    if (obj is ClassXY other)
    {
        return (Magnitude > other.Magnitude) ? 1 : -1;
    }
    //Two different types have the same sort position (weird I know)
    return 0;
}
```
### A slight aside - pattern matching with `is`
At risk od being distracted, this code also demonstrates a form of _pattern matching_ in C# using [the `is` keyword ](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/is).

```C#
    if (obj is ClassXY other)
    {
        return (Magnitude > other.Magnitude) ? 1 : -1;
    }
```
It works something along the following lines:

* The run-time check is made to see if `obj` is of type `ClassXY`
   * If true, a local variable `other` (type `ClassXY`) is created,  assigned to `obj` and made available inside the code block.
   * If false, the local `other` is not created.

This is a safe and concise alternative to a simple type-cast.

> **TASKS** - Read the comments in the constructor of `Program.cs`. There are two tasks.
>
> For Task 1, uncomment the code, build and run. Examine the `Binary32` class to see how this works. Can you modify the `Binary32` class such that it is sorted in the opposite order.
>
> For Task 2, when you uncomment the code you get a compiler error. Why is this?

For the first task, look at the `CompareTo` method closely. For the second, consider the constraint on `T`.

## Summary
Generic types can make a Class more reusable and flexible. We will see how this helps us with the MVVM pattern later in this section.

Using constrained generic types actually widens the reuse and flexibility

> It may seem odd that by adding _constraints_, we _gain_ more flexibility. Linguistically this can seem confusing at first. 
>
>It is important to remember that C# is a type-safe language, so requires guarantees of which methods and properties are implemented on any given object. In loosely typed languages, this is not checked, and an attempt to invoke a method that does not exist would typically result in run-time error. Type-safe languages try to prevent this at compile time.
>
>Unbound generics (with no constraints) can be _any_ type of object, leaving the compiler with few guarantees about the implemented methods and properties. The only guarantee is that all objects inherit from `System.Object`. By _constraining_ the type, what we actually do is to _provide the compiler with more specific information about the object type_, and hence widen the methods and properties available. 

Generics require some getting used to.

---
[Next - Value and Reference Semantics](ValueRefSemantics.md)
[Next - Navigation Controllers](Chapter_3_Navigation/NavControllers.md)