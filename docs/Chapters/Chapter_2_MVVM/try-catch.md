[Contents](README.md)

----

[Prev](loose-coupling.md)

# Handling Exceptions with Try-Catch
It is sometimes said that one of the differences between a robust release product and a proof-of-concept is the way in which errors are handled. There would seem to be a human tendency to make error handling an afterthought, especially when related to handling behaviors that are not central to the business logic (such as network timeouts, invalid/unexpected input etc.).

One of the problems with error handling is it can add a significant amount of complexity to software, especially when it occurs in a deeply nested stack of function calls. The irony of this is that additional complexity increases the potential for further errors.

> A well known example is when errors occur deep inside a library function. How should the code that consumes the library detect and handle such errors?

One method is for functions to return / set flags that indicate success or failure, and pass these back up the function stack where they can be handled. This results in a lot more code and often employs an bespoke protocol (e.g. -1 means error) that may be inconsistent or not be understood by others.

Errors can also happen at highly inconvenient moments such as mid network transaction, or during a file read. In both these cases, it is likely that a critical error means the code cannot proceed, so there needs to be a way to break out and pass the error back up the function stack. In doing so, it is easy to forget to return the system state to a good known state (that is close the network session / any open files).

These are just some examples. The main point is that comprehensive error handling can become very complex and thus creates the temptation to defer or even ignore handling them. 

### Code Examples
Code Examples for this section are provided in the [solution file TryCatch](/code/Chapter2/TryCatch). Open the solution in Visual Studio.

### Sources of Error
The compiler does it's best to prevent developers from performing dangerous operations. However, despite being a type-safe language, there are still many ways to cause a run-time error or even crash.

Some examples include:

- Integer divide by zero
- Dereferencing a null reference
- Out of bounds array access 
- Referencing a non-existing key in a dictionary
- Numerical overflow

You may also have your own critical error which are bespoke to your application:

- Encountering invalid values in some data
- Numerical conditions that prevent a formula being calculated
- Invalid response from a network

> In the code example, open the project `RunTimeCrash` which demonstrates some of these errors. Within `Program.cs` there are a number of examples that cause a run-time exception and crash. Try commenting out each line and note the message that is displayed. Note the word _exception_ that appears.

If you hover the mouse over a method you will see a pop-up summary (see below). One of the sections includes any _exceptions_ that are _thrown_ in the event of a failure.

![PopupDocs](img/editor_popup_exception.png)

## Run-time checks for exceptions
Consider the example of a dictionary. 

```C#
Dictionary<string, uint> lookup = new Dictionary<string, uint>();
lookup.Add("Life", 42);
lookup.Add("Loudest", 10);
lookup.Add("LouderStill", 11);
```

An attempt to get a key-value pair that does not exist can be checked in-place. The following code will successfully read the value `42` and store it in the temporary variable `w`.

```C#
if (lookup.TryGetValue("Life", out uint w))
{
    Console.WriteLine($"The answer..{w}");
}
else
{
    Console.WriteLine($"No such key");
}
```

If the key `Life` did not exist in the dictionary, a false would be returned and "No such key" would be displayed. This fine if you want to handle the error immediately, but what if this was buried deep inside a stack of method calls within a library? You would need to find a way to propagate the error back up the call-stack to where corrective action needs to be implemented.

Note the word _Try_ in the method name. The presence of this in the name often indicates at least two things: 
(i) that the method can fail, and returns a `boolean` to indicate success or failure
(ii) that there is an equivalent method without `Try` in the name (`GetValue` in this example) that instead _throws an exception_ upon failure

## Exception Handling with `try`-`catch`-`finally`
Without dwelling on alternative ways to handle errors, let's dive right into a technique that has (arguably) transformed our ability to catch and process errors as they occur, at any level in our code, and that is exception handling with `try-catch-finally`.

> Build and run the example `NestedErrors`. You might find it helpful to breakpoint the code and step through it line by line. Be sure to step _in_ and not over.

One the first occasion, you should find you stepped into the method `f1`, then `f2` and finally `f3`. ALl went well, and code execution returned to `Program.cs`. 

One the second occasion, everything worked well until we got down to `f3` where a _divide by zero_ occurred. For those who may have forgotten the mathematics behind this, the result should be _infinity_ (strictly this is a limit and not a number, but hey, that's not important right now), but integers cannot represent such infinity, so _an exception is raised_.

Let's look at the method `f3`

```C#
uint f3(uint n, uint d)
{
    uint dd = d / 2;
    uint nn = n / dd;
    return nn;
}
```

Note the following:

- The error occurred on the line `uint nn = n / dd;`
- The return statement was never executed
- Instead control was immediately passed up to `Program()` in `Program.cs`
- The code did not crash. 

> **Experiment**. Change the line in `Program()` that reads 
>
> `uint y2 = f1(65536, 4)` 
>
> to
>
> `uint y2 = f1(65536, 2)`. 
> 
> - In which function does the error occur?
> - In both cases, after the error, where does execution resume 

Let's now review the top-level code in `Program.cs`, where `f1` was invoked:

```C#
try
{
    uint y2 = f1(65536, 4);
    Console.WriteLine("Final result: {0}", y2);
}
catch (Exception ex)
{
    Console.WriteLine("Divide by zero caught at the top level");
}
Console.WriteLine("End");
```

Note the code surrounded with a try block contains the invocation of `f1`. If any statement within the try-block throws an exception, and that exception is not already caught at a lower level, then the code in the catch block will run as soon as the exception is thrown. If the exception is not caught, then the code will terminate abruptly. What we mean by _throws an exception_ will be demonstrated in a later example.

> **Experiment** Add a try-catch to the method `f1`. Show that the catch block no longer catches the exception (as it is already caught). What problems do you encounter when trying to do this?

You might have wondered what value to return when the exception is caught. After all, you don't have a valid value to return! In this case, it was easier to handle the exception at the higher level . What if you wanted to do both? The good news is you can, by simply using the keyword `throw`

```C#
uint f1(uint n, uint d)
{
    uint dd = d / 2;
    uint nn = n / dd;
    try
    {
        uint yy = f2(nn, dd);
        return yy;
    }
    catch (Exception)
    {
        Console.WriteLine("Error caught in f1");
        throw; //Pass on up!
    }
}
```

Now the exception is re-thrown using the `throw` command. This is useful where error handling needs to be performed at different levels of granularity. For example, one catch block might "tidy up" by closing files and network sockets, and then pass the exception up the chain to UI code that can inform the user.

## Custom Exceptions
So far we have seen some of the C#.NET exceptions thrown and caught. More often than not, this is sufficient. However, what if your application detected an error that is specific only to your code. For example, readying an unrecognized string or detecting when numerical results might be unreliable (such as a loss of precision). These are not exceptions that will be otherwise thrown.

> Open, build and run the `CustomExcepton` project.

In this example, I've made it an error condition to divide by any value less than 2 (ok, I admit this is completely contrived to keep the example simple). A bespoke divide method is written to throw a custom exception when such a condition is detected:

```C#
private uint div(uint n, uint d)
{
    if (d < 2)
    {
        throw new BadMathException($"You cannot divide by less than 2 in this algorithm. Divisor or value {d} was used.");
    } else
    {
        return n / d;
    }
}
```

Note how a contextually helpful error string is passed as a parameter. The exception class `BadMathException` is simply a subclass of `Exception`. No actual code was added (although it could be). It is the _type_ that is most important.

```C#
    public class BadMathException : Exception
    {
        public BadMathException()
        {
        }

        public BadMathException(string message) : base(message)
        {
        }

        public BadMathException(string message, System.Exception inner) : base(message, inner)
        {
        }
    }
```

The exception is caught in the usual way using `try-catch`. 

```C#
try
{
    uint y1 = f1(65536, 8);
    Console.WriteLine("Final result: {0}", y1);

    uint y2 = f1(65536, 4);
    Console.WriteLine("Final result: {0}", y2);
}
catch (BadMathException bme)
{
    Console.WriteLine(bme.Message);
}
catch (Exception e)
{
    Console.WriteLine("Something went wrong: {0}", e.Message);
}
```

Note how two catch blocks were used, each using different exception _types_. Many methods throw multiple exceptions depending on the specific error. In this example, the specific error is checked first. For all other exception types, the general catch block is used. 

## The `finally` block
One of the potential problems with exceptions, whether caught or not, is that they can leave a system in an adverse state. Examples might include data inconsistency (data partially updated), remaining temporary files and files or network sockets being left open.

What you may sometimes want is the idea of tidying up (closing files, etc.) whether an exception is caught or not. A `finally` block can be added to do just this.

The following code is from the project `AndFinally` in the same solution as above. 

> **Experiment**. Run this code and there should be no exception raised. Note which strings are written to the console and in which order.

```C#
public Program()
{
    uint p = 10, q=2;
    uint y;
    try
    {
        Console.WriteLine("Attempting the division");
        y = p / q;
        //return
    }
    catch (Exception e)
    {
        Console.WriteLine("Exception caught: {0} ", e.Message);
        //return;
    }
    finally
    {
        Console.WriteLine("Tidy Up - close files and network sockets");
    }

    Console.WriteLine("End of the code");
}
```

Once the `try` block completes, the `finally` block is run before the code resumes.

> **Experiment**. Set `q=0` and run this code again. This will force an exception. Again note which strings are written to the console and in which order.
>
> Now uncomment the `return` statement in the catch block and repeat.
>
> Finally, set `q=2`again and uncomment the `return` statement in the `try` block 

The key observation is that the `finally` block will run either once the `try` block has successfully completed or (in the case of an exception) the `catch` block has completed. Even if you perform an early return (in either), the `finally` block will still run. This is useful as it means the code only has to be written in one place.

## Other Reading
Good coverage of this topic is also available from the Microsoft documentation on [Exception Handling](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/exceptions/exception-handling)


[Next - Part 1 - Start with Familiar Code](mvvm-1.md)

----

[Contents](/docs/README.md)