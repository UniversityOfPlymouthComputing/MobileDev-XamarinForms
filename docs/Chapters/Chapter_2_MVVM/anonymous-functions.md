[Contents](README.md)

----

[Prev](README.md)

# Delegates, Anonymous Functions and Lambdas
[C# has a bit of history](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/anonymous-functions#the-evolution-of-delegates-in-c) when it comes to delegates, anonymous functions and lambda's. You may have heard the terms `Action`, `Delegate`, `Anonymous Function`, `Func`, `Lambda` or `Closure`. These are all closely related (even logically equivalent), but you may encounter any of these terms and it can be quite confusing. 
As C# evolved, new syntax was introduced to help code become more expressive and concise. The older syntax was kept presumably for backwards compatibility.

## CODE EXAMPLES
[The examples in this section can be found here](/code/Chapter2/AnonymousFunctions)

## Delegates (C# v1)
We begin with a quote from the Microsoft documentation:

> A delegate is a type that represents references to methods with a particular parameter list and return type. 

Source: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/delegates/index

If you're a C or C++ programmer, this is somewhat akin to function pointers only with extra safety and a few additional features. Put simply, they are a way to pass **blocks of code** (plus associated data) to and from different parts of your application, as if you were moving data. 

You can even consider such blocks of code (delegate type) as a data structure and treat it like any other. You can assign it to a property or variable, pass to a method or return from a method.

Why would you want to do this? That would probably need a very long answer but I'll try and give some examples here.

First of all, to declare a delegate _type_, you might write something such as:

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

Consider the flexibility this brings. The method `doMathStuff` does not need to know anything about the specific function `f`. All it knows (from the type information) is that it takes two integer parameters, and returns an integer. What it actually does is defined elsewhere. To illustrate this, consider the following methods that conform to this type:

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

You might consider `doMathStuff` as a _decorator_ (code that wraps statements around another).

The syntax of example above is perfectly valid and may still be used. The subsequent sections are really just variations on this same theme, with increasingly tidy and concise syntax.

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

Note how the delegate function has no name, hence the name _anonymous function_. In this case it is stored in a variable, and is passed as a parameter as before.

You don't have to store the reference first. In the example below, the function is defined _inline_, within the parameter list itself:

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
Lambda expressions tend to be the most concise and expressive form of delegate, and are probably now the most commonly used. Aside from using a slightly different syntax, _type inference_ can be used to shorten them to make them more concise and expressive. 

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

Again, you can also write the lambda inline as a parameter:

```C#
doMathStuff((a, b) => a * b);
```

I think you'll agree this is much more concise and expressive than anonymous functions. You might think there is little left to remove, but there is more... 

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
The output from the `Funk` method is as follows:

```
Now using Func<> we calculate the sum of products
100
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
        Console.WriteLine("Outputting List");
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
The output from invoking `ActionClass` is as follows:
```
Using Action<int>
Outputting List
2 is even
4 is even
6 is even
8 is even
Outputting List
1 is low
2 is low
3 is low
4 is low
```

`Action<>`  is the fundamentally the same as `Func<>` except it has no return type.

## Capturing Behavior
There is one last thing I want to say about delegates and that is capturing behavior. Capturing is especially useful for _functional programming_. In this course it is not likely this will be used, but you might encounter it or maybe, inadvertently capture data and get unexpected results.

Consider the following code with a particular focus on the order of each line.

```C#
int scale = 10;

//The following delegate 'captures' the variable scale
Func<int,int> f1 = (u) => scale * u;

int y1 = f1(5);
Console.WriteLine($"{scale} * 5 = {y1}");

scale = 2;
int y2 = f1(5);
Console.WriteLine($"{scale} * 5 = {y2}");
```

The results are as follows:

```
10 * 5 = 50
2 * 5 = 10
```

Note the following:

* delegate function `f1` makes reference to the variable `scale` currently in scope - it is said to _capture_ `scale` 
* `f1` is defined after `scale`
* When `scale` is later updated, this is reflected in the result of `f1` when it is subsequently invoked.

Logically, we might draw the conclusion that `f1` captures `scale` _by reference_ (even through it is a _value type_), and this seems to be a fairly reasonably way to look at it. However, there is more to capturing behavior than this. Now consider a second example where a method _returns_ a delegate function (this may seem a little weird at first):

```C#
Func<double> CreateCounter(double initValue, double inc)
{
    double sum = initValue;
    return () => { sum += inc; return sum; };
}
```

The return type of `CreateCounter` is a delegate function. So this code does not return a normal value (such as an integer or double), but a delegate function.

Note also that the delegate function being returned _captures_ the local variable `sum` and the parameter `inc`. You might be wondering if this is going to break? After all, both a local variable and a parameter would normally be inaccessible once the function ends. With capturing behavior, both will live on! To illustrate, let's now look at where the returned delegate is used.

```C#
public Capturing()
{
    Func<double> acc1 = CreateCounter(0.0, 1.0);
    Func<double> acc2 = CreateCounter(10.0, 2.0);

    Console.WriteLine("Counter 1: " + acc1().ToString());
    Console.WriteLine("Counter 1: " + acc1().ToString());

    Console.WriteLine("Counter 2: " + acc2().ToString());
    Console.WriteLine("Counter 2: " + acc2().ToString());
}
```
Here is the output:

```
Counter 1: 1
Counter 1: 2
Counter 2: 12
Counter 2: 14
```

`acc1` and `acc2` each store a reference a delegate function. Remember that within the `CreateCounter` method, this delegate function captures _the local variable_ `sum` and parameter `inc` before being returned.

`ac1` has captured 0.0 and 1.0 for `sum` and `inc`, whereas `ac2` has captured 10.0 and 2.0 respectively. These captured values are now part of the delegate. Where we invoke `acc1()` and `acc2()`, they are parameterless as they have all the information captured within to perform their task.

> If you are curious, you might want to read up on _currying_, a functional programming technique that is related to the above.

To reflect:

* Each time `CreateCounter` is invoked, parameter `inc` and `initValue` are passed. A unique local variable `sum` is created (typically on the function stack).
* `sum` and `inc` are both referenced inside the delegate, so both are _captured_ and thus persists (somewhere) in memory even after `CreateCounter` exits.
* The captured `sum` and `inc` will persist as long as the delegate function persists.
* The returned delegate includes both code and all captured variables.

Each instance`acc1` and `acc2` have their own `sum` and `inc` captured within them. When delegate functions are returned from another function, you will get  code and possibly some form of captured state contained within.


## Summary
Much more could be said about delegates, anonymous methods, lambda's and their applications. The intention is to give the reader an overview so that at least the concept is familiar. Don't be surprised if you need to look up the syntax at first (I still do).

There is much more that could be said about passing and returning delegates. In the past I've included functional programming examples, but I found this left readers confused and even overwhelmed. The problem is it can be fun (well I think it's fun), but as an educator, this risks also becoming self-indulgent.

Where you are likely to use delegates is with networking and UI interfaces. Completion handlers are often short and passed as a parameter. This could be the completion of a network transaction or an animation. What you might find is that this approach benefits from keeping related code together in one place, arguably making it easier to maintain.

[Next - Asynchronous Programming - async and await](async-programming.md)

----

[Contents](/docs/README.md)
