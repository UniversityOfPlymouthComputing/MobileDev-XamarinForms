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
Code Examples for this section are provided in the [solution file TryCatch](/code/Chapter2/TryCatch)

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

## Exception Handling with `try`-`catch`-`finally`
Without dwelling on alternative ways to handle errors, let's dive right into a technique that has (arguably) transformed our ability to catch and process errors as they occur, namely exception handling with `try-catch-finally`.



## Other Reading
Good coverage of this topic is also available from the Microsoft documentation on [Exception Handling](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/exceptions/exception-handling)


[Next - Part 1 - Start with Familiar Code](mvvm-1.md)

----

[Contents](/docs/README.md)