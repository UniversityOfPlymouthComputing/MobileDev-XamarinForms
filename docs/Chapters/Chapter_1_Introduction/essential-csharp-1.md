# Essential C# Part 1
This section is intended to be a quick refresher of some of the C# we will encounter in subsequent sections. 
It is not a comprehensive treatment of C#.

In this section, all the code will be targeting a console application.
You can use either Visual Studio 2019 or [Visual Studio Code](https://code.visualstudio.com/download).

- Visual Studio 2019. Create a new project, and search on "Console". From the results pick Console App (.NET Core)
- Visual Studio Code. [See these instructions](https://docs.microsoft.com/en-us/dotnet/core/tutorials/with-visual-studio-code) to create and debug an console application.

I will use Visual Studio Code as I like the built in terminal.

## Hello World Revisited
When you create a new Console app, check the code is as follows:

```C#
using System;

namespace c_sharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
```

One of the disadvantages to this approach is that you can't get Hello World on the screen without creating a class and a static method. There is another way however. Microsoft recently released access to [Try.net](https://dotnet.microsoft.com/platform/try-dotnet) where you can create a console in the browser.

Let's examine this code more closely however and pick out some key discussion points (maybe to finish later).

- The entry point for a C# application is `Main`. However, it has the word `static` on the front. Static classess will be covered below, but it's important to note.
- `Console.WriteLine` invokes a method, but what is Console? There is no instance variable called Console created, so how come it exists? Again, the clue is in the word `static`. To understand this, we need to know what me mean by a _class_ and an _instance of a class_.
- `namespace c_sharp` appears around the code. Why is this here?
- `using System` again, it's there, so good to question what it means

## Create and Instantiating a class
Let's wind back and go over a topic you are likely to have covered already - writing a class and instantiating one (with `new`).

### When names collide - namespace to the rescue!

## Properties

## Partial Classes

## Inheritance and constructors

## Polymorphism and the big-bad-mouse

## Static Classes
