[Contents](README.md)

----

[Prev](README.md)

# Anonymous Methods and Lambda
[C# has a bit of a history](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/anonymous-functions#the-evolution-of-delegates-in-c) when it comes to anonymous functions and lambdas. You may have heard the terms `Action`, `Delegate`, `Anonymous Function`, `Func`, `Lambda` or `Closure`. You may encounter any of these terms and it can be quite confusing. As C# evolved, new syntax was introduced to help code become more expressive and concise. The older syntax was kept for backwards compatibility.

[The examples in this section can be found here](/code/Chapter2/AnonymousFunctions)

## Delegates (C# v1)
From the Microsoft documentation:

> A delegate is a type that represents references to methods with a particular parameter list and return type. 

Source: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/delegates/index

To declate a delegate type, you might write something such as:

```C#
delegate int DoMath(int a, int b);
```

In this example, the new type `DoMath` is a reference to _any_ function that takes two integer parameters and returns an integer.

Given we have this new type, we can write another method that takes a parameter of type `DoMath`

```C#
protected void doMathStuff(DoMath f)
{
    int[] xx = { 2, 4, 6, 8 };
    int[] yy = { 1, 3, 5, 7 };
    for (int n = 0; n < xx.Length; n++)
    {
        //Note how the delegate is invoked
        int y = f(xx[n], yy[n]);
        Console.WriteLine(y.ToString());
    }
}
```

Look at the code above carefully, especially the line that reads:

```C#
int y = f(xx[n], yy[n]);
```

Consider the flexibility this brings. The method `doMathStuff` does not need to know anything about the specific function `f`. All it knows is it takes two integer parameters, and returns an integer. What it actually does is decided elsewhere. To illustrate this, consider the following methods that conform to this type:

```C#
int addSomeNumbers(int a, int b)
{
    return a + b;
}
int mulSomeNumbers(int a, int b)
{
    return a * b;
}
```

Both these methods match the prototype of the `DoMath` type. We can assign and store references to these functions as follows:

```C#
DoMath del1_v1 = new DoMath(addSomeNumbers);
DoMath del2_v1 = new DoMath(mulSomeNumbers);
```

Here is the clever part. You can now pass either of these as a parameter

```C#
doMathStuff(del1_v1);
doMathStuff(del2_v1);
```

You might consider `doMathStuff` as a _decorator_ (code that wraps around another).

The syntax of example above is still perfectly valid. The subsequent sections are really just variations on this same theme, with increasingly tidy and concise syntax.

## Delegates (C# v2)
From the Microsoft documentation:

> C# version 2.0 introduced the concept of anonymous methods, which allow code blocks to be passed as parameters in place of a separately defined method. 

Source: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/delegates/index

The main different in v2 is that you don't need to write separate methods with method names (in v1 remember we created `addSomeNumbers` or `mulSomeNumbers`). Instead you can write them inline and _anonymously_ (without a name).

Instead of writing `addSomeNumbers`, you could do the following:

```C#
DoMath del1_v2 = delegate (int a, int b)
{
    return a + b;
};
doMathStuff(del1_v2);
```

Note how the delegate funcion has no name, hence the name _anonymous function_. In this case it is stored in a variable, and is passed as a parameter as before.

You don't have to store the reference first. In the example below, the function is defined in the parameter list itself:

```C#
doMathStuff( delegate (int a, int b)
                {
                    return a * b;
                } );
```

It's arguably a bit messy to read, but it saves creating a function elsewhere. 

## Delegates and Lambdas expressions (C# v3)
From the Microsoft documentation:

> C# 3.0 introduced lambda expressions as a more concise way of writing inline code blocks. Both anonymous methods and lambda expressions (in certain contexts) are compiled to delegate types. Together, these features are now known as anonymous functions. For more information about lambda expressions, see [Lambda expressions](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/lambda-expressions).

Source: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/delegates/index

## Lambdas
Lambda expressions tend to be the most concise and expressive form, and are probably now the most commonly used. Aside from using a slightly different syntax, _type inference_ can be used to shorten them to make them more concise and expressive. 

We define and store a lambda as we did with a delegate
```C#
DoMath del1_v2 = (int a, int b) => { return a + b; };
doMathStuff(del1_v2);
```

Given the type `DoMath` provides all the type information needed, this can be simplied using _type inference_

```C#
DoMath del1_v2 = (a, b) => { return a + b; };
doMathStuff(del1_v2);
```

When you have a _single statement return_ as in this case, you can further simplify the code:

```C#
DoMath del1_v2 = (a, b) => a + b;
doMathStuff(del1_v2);
```

Again, you can also specify the lambda as a parameter:

```C#
doMathStuff((a, b) => a * b);
```

I think you'll agree this is much more concise and expressive than anonymous functions. You might thing there is little left to remove, but there is more... 

## Func and Action Delegates
Common to all the code above was a delegate type declaration

```C#
delegate int DoMath(int a, int b);
```

You can circumvent this by using either `Func<>` or `Action<>` delegate. Consider the code below, especially the parameter type for the method `doMathStuff`

```C#
void doMathStuff(Func<int, int, double> f)
{
    int[] xx = { 2, 4, 6, 8 };
    int[] yy = { 1, 3, 5, 7 };
    double sum = 0.0;
    for (int n = 0; n < xx.Length; n++)
    {
        //Note how the delegate is invoked
        sum += f(xx[n], yy[n]);
    }
    Console.WriteLine($"{sum}");
}
public Funk()
{
    Console.WriteLine("Now using Func<> " +
        "we calculate the sum of products");
    doMathStuff( (a, b) => (double)a * (double)b );
}
```

Let's look closely at the `doMathStuff` method parameter:

```C#
Func<int, int, double> f
```

This represents a function delegate that takes two integer parameters and returns a double. We invoke this method with the line

```C#
doMathStuff( (a, b) => (double)a * (double)b );
```
A lambda is passed and converted to a `Func<int,int,double>` delegate. You can have up to input 16 parameters and you must always have a return type. For delegates without a return type, you use an `Action<>` delegate instead. For example:

```C#
public class ActionClass
{
    List<int> data = new List<int>()
    {
        1,2,3,4,5,6,7,8,9
    };
    protected void ListOutput(Action<int> classify)
    {
        Console.WriteLine("Outputing List");
        foreach (int n in data) {
            classify(n);
        }
    }
    public ActionClass()
    {
        Console.WriteLine("Using Action<int>");
        ListOutput(u => {
            if ((u % 2) == 0) Console.WriteLine($"{u} is even");
        });

        ListOutput(u => { if (u < 5) Console.WriteLine($"{u} is low"); });
    }
}
```

`Action<>`  is the fundamentally the same as `Func<>` except it has no return type.

## Capturing Behaviour
There is one last thing I want to say about delegates and that is capturing behaviour. Capturing is especially useful for _functional programming_. In this course it is not likely this will be used, but you might encounter it or worse, inadvertently capture data and get unexpected results.



## Summary
Much more could be said about delegates, anonymous methods and lambda's. The intention is to give the reader an overview so that at least the concept is clear. Don't be surprised if you need to look up the syntax at first.






[Next - Asynchronous Programming - async and await](async-programming.md)

----

[Contents](/docs/README.md)
