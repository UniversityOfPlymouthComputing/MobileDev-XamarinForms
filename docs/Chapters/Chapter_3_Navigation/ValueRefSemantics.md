[Contents](readme.md)

---

The code for this section can be found in the folder [ValueReference](/code/Chapter3/ValueReference)

Open the solution file and note there are a number of projects (one for each section below).

## Value Types and Semantics
Build the project `ValueReference1` and using the debugger, step through line by line to follow the logic.

In this example we first define an integer variables `a` and initialise its value to 10.

```C#
int a = 10;         
```

Next we create a second variable `b`, and initialize it with the _value_ stored in `a`

```C#
int b = a;
```

Under the hood, the following logical operations are performed:

* A unique space for `b` is created in memory
* The value of `a` is read from memory and copied into the memory location for `b`

Both `a` and `b` are separate independent variables that exist in memory. They just happen to hold the same value.

> When one value type variable is assigned to another, then the value of one is copied into the other. Both variables remain independent.

We can see this when we change one of the variables `a`

```C#
a+=1;
```

The value of `a` is now 11, but `b` remains at 10. The variables `a` and `b` exist at separate memory locations.

These behaviors are also referred to as __value semantics__. The simple types (`char`, `short`, `int`, `long`, `float`, `double` etc.) all use value semantics.


## Using reference semantics with value types
The next example is in the project `ValueReference-2`. Build and step through the code, making note of how the variables change.

We start with a simple value type `a`

```C#
int a = 10;
```

Next, we create another variable, only this time we tell the compiler to use _reference semantics_

```C#
ref int b = ref a;
```

The variable `b` is now just another name for variable `a`. If we imagine `a` and `b` as variables in memory, then each has the same address.

If we now modify `a`, we see the same change reflected in `b`. The following line:

```C#
a += 1;
```

will result in BOTH `a` and `b` being equal to `11`. Similarly, if we modify `b`:

```C#
b += 10;
```

Now both `a` and `b` return the value `21`.

This example also demonstrates how this can be used to perform in-line modification. The following function takes two parameters:

* a reference to an integer (that's a fancy pointer to C and C++ programmers!)
* a value `delta`

```C#
public void updateInplace(ref int u, int delta)
{
    u += delta;
}
```

When we invoke this function, we can see how reference semantics differ from value.

```C#
int a = 10;
updateInplace(ref a, 10);
```

A reference to `a` is passed as the first parameter. A copy of the literal value 10 as the second.

When the function returns, `a` will now equal `20`.

> Passing by reference can be confusing / ambiguous for others, so use sparingly.

## Classes and Structures
Build the project `ValueReference-3` and step through the code with the debugger.

Consider the following class and structure:

```C#
public class MyObj 
{
    public int a;
    public int b;
    public MyObj(int aa, int bb) => (a, b) = (aa, bb);
    public override string ToString() => $"a={a}, b={b}";
}

public struct MyStruct
{
    public int a;
    public int b;
    public MyStruct(int aa, int bb) => (a, b) = (aa, bb);
    public override string ToString() => $"a={a}, b={b}";
}
```

They look almost identical, but there is an important difference.

> Instances of a **class** (objects) are **reference types**
>
> Instances of a **structure** are **value types** (although they may encapsulate reference types within them)

We can see this behavior demonstrated in the code.

```C#
MyObj r1 = new MyObj(2, 3);
MyObj r2 = new MyObj(10, 20);
MyObj r3 = r1;
```

The reference `r3` is identical to `r1`. They both reference the same object in memory, so any change to one is reflected in the other.

Contrast this with the structure:

```C#
MyStruct s1 = new MyStruct(2, 3);
MyStruct s2 = s1;
```

The structure `s2` is first created, then all the elements of `s1` are _copied_ into `s2`. From this point onwards, both are entirely independent data structures.

> A change in `s1` will have no impact on `s2` and vice-versa.

This is demonstrated in the code.

Note how we get both the copy behavior and independence we saw with simple value types (`int`, `float` etc..). Contrast this with a class where no new objects are created and therefore no data needs to be copied.


## Equivalence
Build and step through the project `ValueReference-4`.

We sometimes want to compare objects for equivalence. We need to be clear about what we mean by equivalence. 

For reference types, we could mean either of the following:

* both references are identical, in that they reference the same data object in memory
* the references may be different, they may be equivalent in some other sense, such as equal properties and/or type etc.

For value types, two variables cannot reference the same object in memory (one of the key features of value types). However, as with reference types, they may be equivalent in terms of having equal properties and/or type.

I this example, we define a custom `Equals` method by implementing the interface `IEquatable<>`

```C#
public class MyObj : IEquatable<MyObj>
{
    public int a;
    public int b;
    public MyObj(int aa, int bb) => (a, b) = (aa, bb);

    //To perform a bespoke member by member comparison
    public bool Equals([AllowNull] MyObj other)
    {
        if (other == null) return false;
        return ((other.a == a) && (other.b == b));
    }

    public override string ToString() => $"a={a}, b={b}";
}
```

### The `==` operator
We need to be careful in our assumptions about testing for equality.

> From the [Microsoft documentation](https://docs.microsoft.com/dotnet/csharp/language-reference/operators/equality-operators), we learn that:
>
>  _By default, two reference-type operands are equal if they refer to the same object
> ...
> However, a reference type can overload the == operator. If a reference type overloads the == operator, use the `Object.ReferenceEquals` method to check if two references of that type refer to the same object._

For our own classes, we can simply use the `==` operator to check if two variables reference the same object. If we wish to be certain, then use the `Object.ReferenceEquals` method`.

Two instances of this class can be compared in the following way:

* Using the == operator, which will test if the references are the same (same object in memory)
   * If it's possible that `==` has been overridden, then use the `Object.ReferenceEquals` method`.

* Using the `Equals` method. 
   * There is also a default implementation in [`System.Object`](https://docs.microsoft.com/dotnet/api/system.object.equals?view=netframework-4.8) 

You need to be careful when testing for equality: make sure you are clear what you mean by equality and whether your code really tests this.

For structures, we get a bit more help:

```C#
public struct MyStruct
{
    public int a;
    public int b;
    public MyStruct(int aa, int bb) => (a, b) = (aa, bb);
    public override string ToString() => $"a={a}, b={b}";
}
```

We cannot compare with `==` as this would always return false (unless we override it).

We can use the default `Equals` method however:

```C#
MyStruct s1 = new MyStruct(10, 20);
MyStruct s2 = new MyStruct(10, 20);
//if (s1 == s2) does not compile
if (s1.Equals(s2))
{
    Console.WriteLine("s1 and s2 are the same!");
}
else
{
    Console.WriteLine("s1 and s2 are not the same!");
}
```

This results in a positive result (they are considered equal). This performs an element-by-element comparison which can be convenient (assuming this is what you want).

From the [Microsoft documentation](https://docs.microsoft.com/dotnet/api/system.valuetype.equals?view=netframework-4.8):

> The `ValueType.Equals(Object)` method overrides `Object.Equals(Object)` and provides the default implementation of value equality for all value types in the .NET Framework.
>
> The default implementation calls `Object.Equals(Object)` on each field of the current instance and obj and returns true if all fields are equal.

Again, if you want to fully define the meaning of `Equals`, override and write one yourself.

## Immutable Reference Types
For this section, build and step through the project `ValueReference-5`

The key point in this example is a simple one:

> Careful what you assume!

We are going to focus on a fairly well known reference type, the `System.String` class, also known as `string`

> The type `string` is a class, but it _behaves_ as a value type!

Consider the first line:

```C#
string s1 = "Hello";
```

You can consider literal strings are instances of `string` objects. We could have equally written

```C#
string s1 = new string("Hello"); 
```

Next, we create a second reference `s2`

```C#
string s2 = s1;
```

At this point, `s2` is simply a reference to `s1`. These are reference types, so no data needs to be copied (so it's fast). If we are curious, we can test this using `Object.ReferenceEquals(s1,s2)`

Next, `s1` is modified:

```C#
s1 = "World";
```

You might expect that `s2` is also updated, as after all `string` is reference type. However, remember that a literal string is a new instance of `string`. We could have written:

```C#
s1 = new string ("World");
```




---

[Next - Navigation Controllers](Chapter_3_Navigation/NavControllers.md)