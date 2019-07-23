[Back to Table of Contents](/docs/Chapters/Chapter_1_Introduction/README.md)


# Essential C# Part 2
This section is intended to be a quick refresher one more of the C# syntax we will encounter in subsequent sections. 
Like before, it is not a comprehensive treatment of C#.

In this section, all the code will be targeting a console application.
You can use either Visual Studio 2019 or [Visual Studio Code](https://code.visualstudio.com/download).

- Visual Studio 2019. Create a new project, and search on "Console". From the results pick Console App (.NET Core)
- Visual Studio Code. [See these instructions](https://docs.microsoft.com/en-us/dotnet/core/tutorials/with-visual-studio-code) to create and debug an console application.

I will use Visual Studio 2019 for Windows for the sake of consistency (as it's what we use in our teaching labs).

## Optional Properties
Consider the following code:

```C#
class MyModel
{
    public string FirstName { get; set; } = "Anon";
    public string KnownAs { get; set; } = "Matey";
    public int Age { get; set; } = 21;
    public uint PhoneNumber { get; set; } = 0;

    public void Display()
    {
        Console.WriteLine($"This is {FirstName}, also known as {KnownAs}, Age {Age}, Phone number {PhoneNumber}");
    }
}
class Program
{
    static void Main(string[] args)
    {
        MyModel m = new MyModel();
        m.Display();
    }
}
```

There is so much about this that is not ideal. Let's consider a few issues:

- the default values are arbitrary but can still be displayed.
- Display assumes all fields are set
- It might be reasonable to expect a first name and age to be recorded, as everyone as these. The phone number is more problematic as it's quite conceivable that someone has no phone number. The familiar `KnownAs` property may also be irrelevant for many people (and might bring back bad memories of school!).

Let's address the issue of default values by forcing the use of a constructor

```C#
...
public MyModel(string FirstName, string KnownAs, int Age, uint PhoneNumber)
{
    this.FirstName = FirstName;
    this.KnownAs = KnownAs;
    this.Age = Age;
    this.PhoneNumber = PhoneNumber;
}
...
```

**TASK** You can also remove the default property values

Now in main, we are forced to provide the information


```C#
static void Main(string[] args)
{
    MyModel m = new MyModel("Arnold", "Ace", 54, 01234123456);
    m.Display();
}
```

Two of the properties, `PhoneNumber` and `KnownAs` may not be known, or this information may wish to be withheld. In this case, we can choose to make them _optional_ it indicate this. The way we achieve this depends on whether the data is a _reference type_ or a _value type_. Either way, what we don't want to do is make up special values to represent _not set_. For example, we could invent a scheme where the string "NONE" is used it indicate a string value has not been provided. This may work, but it's not a great solution. It's moderately costly to compare strings, but worse, if your code were to be reused in another project, this might not mean anything to other people

## Optional Properties for Reference types
If a variable is declared with any of the following, they are a reference type.

- class
- interface
- delegate

There are also some built in reference types:

- dynamic
- object
- string

Note that `string` is one of them!

See [Reference types](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/reference-types) for more details.

You can thing of a reference type as something that _encapsulates the address_ of some data in memory (as discussed in a previous section). This is in contrast to [_value types_](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/value-types) where the variable name refers to the content. The semantic differences won't be dwelled upon here, needless to say there is more explaination to be done.

> A key feature of a reference type is that it can be set to `null`. We can use this to our advantage as `null` can represent _no value_ or _not set_

Note that for value types (int, double, etc..), these **cannot** (by default) be set to `null`.

## Optional Properties for Value Types
If a variable is not declared as a reference type (as explained in the previous section), it must be a [value type](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/value-types)

By default, value types cannot be `null`. Luckily, C# has a special syntax to does allow for this

Change the declaration of the PhoneNumber property to match the following:

```C#
public uint? PhoneNumber { get; set; }
```

The type is now `uint?`. _This is not the same as uint_, but for the most part, we can treat it as an ordinary `uint`.

You can now set `PhoneNumber` to `nill` or a concrete value.

## Putting it all together
The code can now be changed as follows:

```C#
class MyModel
{
    public string FirstName { get; set; }

    public string KnownAs { get; set; }

    public int Age { get; set; }

    public uint? PhoneNumber { get; set; }

    public void Display()
    {
        string Str = $"This is {FirstName}";
        if (KnownAs != null)
        {
            Str += $", also known as {KnownAs}";
        }
        Str += $", Age {Age}";
        if (PhoneNumber != null)
        {
            Str += $", Phone number { PhoneNumber}";
        } 
        Console.WriteLine(Str);
    }

      public MyModel(string FirstName, int Age, string KnownAs=null, uint? PhoneNumber=null)
      {
          this.FirstName = FirstName;
          this.KnownAs = KnownAs;
          this.Age = Age;
          this.PhoneNumber = PhoneNumber;
      }
}
```

Note that for the constructor, the parameters without defaults are now listed first. Now in `Main`, we can write:

```C#
static void Main(string[] args)
{
    MyModel m = new MyModel("Arnold", 54, null, null);
    m.Display();
}
```

As `null` will be the default for the two optional parameters, we can also write:

```C#
MyModel m = new MyModel(FirstName: "Brian", Age: 71);
```

## Computed Properties
Computed properties, or calculated properties as they may also be known, and properties that have no backing store. They are generally calculated on demand. 

Consider the following (poorly written) C# Code

```C#
    class Circle
    {
        private const double PI = 3.1415926541;
        public double Radius { get; set; }
        public double Diameter { get; private set; }
        public double Circumference { get; private set; }
        public double Area { get; private set; }

        public void CalculateArea()
        {
            Area = PI * Radius * Radius;
        }

        public void CalulateCircumference()
        {
            Circumference = PI * Diameter;
        }

        public void CalculateDiameter()
        {
            Diameter = 2.0 * Radius;
        }
        public Circle(double Radius)
        {
            this.Radius = Radius;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Circle c1 = new Circle(3.0);
            c1.CalculateDiameter();
            c1.CalulateCircumference();
            c1.CalculateArea();
            Console.WriteLine($"A circle of radius {c1.Radius} has a diameter of {c1.Diameter}, circumference of {c1.Circumference} and area of {c1.Area}");
        }
    }
```

For this to work, note the following:

- It's critical to calculate the Diameter _before_ the circumference. 
- If the Radius changes, all parameter have to be re-calculated
- Why are setters provided for Diameter, Area and Circumference?

All parameters are a function of Radius, so let's make use of this:

```C#
class Circle
{
    private const double PI = 3.1415926541;
    public double Radius { get; set; }

    public double Diameter { get => 2.0 * Radius; }
    public double Circumference { get => PI * Diameter; }
    public double Area { get => PI * Radius * Radius; }

    public Circle(double Radius)
    {
        this.Radius = Radius;
    }
}
class Program
{
    static void Main(string[] args)
    {
        Circle c1 = new Circle(3.0);
        Console.WriteLine($"A circle of radius {c1.Radius} has a diameter of {c1.Diameter}, circumference of {c1.Circumference} and area of {c1.Area}");
    }
}
```

Note the following:

- The getters calculate their respective values _every time_ they are accessed, and they are always based on the current value of `Radius` 
- The setters are removed from all but the `Radius` property (they are never set directly, at least not here)
- These are auto properties, so we don't need create a backing store. Where there is no setter, then no storage is needed.

This approach is suited to cases _where the calculations are not overly expensive_. Now consider the case where a property is potentially very slow. Examples include complex calculations or where a network transaction is involved. I won't include the detail code here, but instead _mock_ the concept for illustrative and even test purposes.
## Cached Properties
Replace the declaration of the `PI` constant with the following code:

```C#
    private double? _pi;
    private double PI
    {
        get
        {
            if (_pi == null)
            {
                _pi = DoBigLongCalculationOfPi();
            }
            return (double)_pi;
        }
    }

    //Simulate slow calculation of PI
    private double DoBigLongCalculationOfPi()
    {
        Task.Delay(2000);
        return 3.1415926541;
    }
```
Note that PI has a backing store `_pi`. The first time you read the `PI` property, the backing store `_pi` will be null. This will result in value being fully evaluated (taking approx 2 seconds), and the result being returned. For all subsequent reads, the previously calculated value will be returned (very fast).

Note:

- This approach should only be used where it is truely justifed.
- In this example, `_pi` never becomes out of date, so we don't need to concern ourselves with forcing it to be recomputed (setting it to `null` would for it). This is not the general case!
- In the case of 2 second delays, we should consider using `async` and `await` (see below).

**Task** For purely illustrative purposes, try using this approach for the `Area` property. Remember that you only need to calculate Area the first time it is read **unless** the Radius is changed. _Hint: create a setter for Radius._

Caching schemes such as these can greatly improve performance, but can equally become very complicated to test. The fundamental reason for this is that the _sequence_ of operations is critical. 

## Operators

## Enumerated Types

## async and await
