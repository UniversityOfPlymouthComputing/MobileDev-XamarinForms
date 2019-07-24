[Back to Table of Contents](/docs/Chapters/Chapter_1_Introduction/README.md)

----

# Essential C# Part 2
This section is intended to be a quick refresher one more of the C# syntax we will encounter in subsequent sections. 
Like before, it is not a comprehensive treatment of C#.

In this section, all the code will be targeting a console application.
You can use either Visual Studio 2019 or [Visual Studio Code](https://code.visualstudio.com/download).

- Visual Studio 2019. Create a new project, and search on "Console". From the results pick Console App (.NET Core)
- Visual Studio Code. [See these instructions](https://docs.microsoft.com/en-us/dotnet/core/tutorials/with-visual-studio-code) to create and debug an console application.

I will use Visual Studio 2019 for Windows for the sake of consistency (as it's what we use in our teaching labs).

## Nullable Properties
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

Two of the properties, `PhoneNumber` and `KnownAs` may not be known, or this information may wish to be withheld. In this case, we can choose to make them _nullable_ to indicate this. The way we achieve this depends on whether the data is a _reference type_ or a _value type_. Either way, what we don't want to do is make up special values to represent _not set_. For example, we could invent a scheme where the string "NONE" is used it indicate a string value has not been provided. This may work, but it's not a great solution. It's moderately costly to compare strings, but worse, if your code were to be reused in another project, this might not mean anything to other people.

## Nullable Properties and Reference types
If a variable is declared with any of the following, they are consider to be a _reference type_.

- class
- interface
- delegate

There are also some built in reference types:

- dynamic
- object
- string

Note that `string` is one of them!

See [Reference types](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/reference-types) for more details.

You can think of a reference type as something that _encapsulates the address_ of some data in memory (as discussed in a previous section). This is in contrast to [_value types_](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/value-types) where the variable name refers to the actual content. The semantic differences won't be dwelled upon here, needless to say there is more explaination to be done.

> A key feature of a reference type is that it can be set to `null`. We can use this to our advantage as `null` can represent _no value_ or _not set_

Note that for value types (int, double, etc..), these **cannot** (by default) be set to `null`.

## Nullable Properties for Value Types
If a variable is not declared as a reference type (as explained in the previous section), it must be a [value type](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/value-types)

By default, value types cannot be `null`. Luckily, C# has a special syntax to does allow for this

Change the declaration of the PhoneNumber property to match the following:

```C#
public uint? PhoneNumber { get; set; }
```

The type is now `uint?`. _This is not the same as uint_, but for the most part, we can treat it as an ordinary `uint`.

You can now set `PhoneNumber` to `nill` or a concrete value.

> For Swift developers, nullable types can often be used in place of [_optionals_](https://developer.apple.com/documentation/swift/optional) (which uses a similar syntax)

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

## Computed / Calculated Properties
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
        for (uint n = 0; n < uint.MaxValue; n++)
        {

        };
        return 3.1415926541;
    }
```
Note that PI has a backing store `_pi`. The first time you read the `PI` property, the backing store `_pi` will be null. This will result in value being fully evaluated (taking several seconds) and being returned. For all subsequent reads, the previously calculated value will be returned (very fast).

We can try this in `Main

```C#
Circle c1 = new Circle(3.0);
Console.WriteLine($"A circle of radius {c1.Radius} has a diameter of {c1.Diameter}, circumference of {c1.Circumference} and area of {c1.Area}");
c1.Radius = 4.0;
Console.WriteLine($"A circle of radius {c1.Radius} has a diameter of {c1.Diameter}, circumference of {c1.Circumference} and area of {c1.Area}");
```

Note:

- This approach should only be used where it is truely justifed.
- In this example, `_pi` never becomes out of date once it's calculated, so we don't need to concern ourselves with forcing it to be recomputed (setting it to `null` would provoke this). This is not the general case however!
- In the case of multiple second delays, we should consider running this on a backround thread and using `async` and `await` (see below).

**Task** For purely illustrative purposes, try using this approach for the `Area` property. Remember that you only need to calculate Area the first time it is read **unless** the Radius is changed. _Hint: create a setter for Radius._

Caching schemes such as these can greatly improve performance, but can equally become very complicated to test. The fundamental reason for this is that the _sequence_ of operations is critical, and trying to test all possible sequence becomes difficult. 

You may wonder why this topic is relevent to mobile development? Mobile devices are increasingly powerful, but despite this, they are still considered to be resource (ram, storage, cpu, battery) constrained devices. Applications are often connected to a back-end service across a network, and transactions can be very slow in poor signal areas. Users are also sensitive to the lag and an unresponsive UI, as are the host operating systems. Therefore, developers need to be nimble in how they interact with the network and update the UI.

## Tuples (value type)
A realtively recent addition to C# are [value-type _tuples](https://docs.microsoft.com/en-us/dotnet/csharp/tuples)_. This convenient language feature enables data to be encapsulated without the overhead of writing a custom class or structure.

In general, they are written as follows:

```C#
var t = (value, value, ...);
```

Some examples are given below. Read through these and the comment as they are mostly self-explanatory.

```C#
    class Program
    {
        public static (int, int) Flip(int xx, int yy)
        {
            return (yy, xx);
        }
        static void Main(string[] args)
        {
            //Unnamed
            var t1 = (2, 3);
            Console.WriteLine($"Unnamed Tuple t1 has values Item1={t1.Item1} and Item2={t1.Item2}");

            //Named
            var t2 = (x: 2, y: 3);
            Console.WriteLine($"Named tuple t2 has values x={t2.x} and y={t2.y}");

            //Test for equality (where compatible)
            if (t1 == t2)
            {
                Console.WriteLine($"t1 is equal to t2");
            }

            //Projection initializers
            double p = 2.0;
            double q = 3.0;
            double r = 5.0;
            var t3 = (p, q, r);
            Console.WriteLine($"Named tuple t3 has values p={t3.p}, q={t3.q} and r={t3.r}");

            //Named overrides projection
            var t4 = (x: p, y: q, z: r);
            Console.WriteLine($"Named tuple t4 has values x={t4.x}, y={t4.y} and z={t4.z}");

            //Explicit types
            (string, int, bool) t5 = ("Hello", 123, false);
            Console.WriteLine($"Tuple t5 = {t5}");

            (string name, int age, bool smoker) t6 = (name: "Dave", age:51, smoker:false);
            Console.WriteLine($"Tuple t6 = {t6}");

            //Using nullable
            (string name, int age, bool? smoker) t7 = (name: "Fred", age: 54, smoker: null);
            Console.WriteLine($"Tuple t7 = {t7}");

            (string name, int age, bool smoker)? t8 = (name: "Fred", age: 54, smoker: false);
            if (t8.HasValue)
            {
                Console.WriteLine($"Tuple t8 = {t8}");
            }

            //Returning a tuple type from a function
            (int x, int y) t9 = Program.Flip(xx: 2, yy: 4);
            Console.WriteLine($"Tuple t9 = {t9}");

            //Returning a tuple type and unpacking
            (int x, int y) = Program.Flip(xx: 2, yy: 4);
            Console.WriteLine($"x={x} and y={y}");
        }
    }
```  

Tuples are particularly useful for returning multiple values from a method (as shown abopve in the `Flip` method example).

## Operator Overloading
I've added this topic as it features in some of the following examples. C# has some abilty to redefine the meaning of operators (such as +,-) when used with custom types (Classes or Structures) using something known as [operator overloading](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/operator-overloading)

```C#
    class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        //Translate (math speak for add in this case) - returns a new Coordinate that is the sum
        public static Coordinate operator +(Coordinate u, Coordinate v) => new Coordinate(u.X + v.X, u.Y + v.Y);

        // Equality
        public static bool operator ==(Coordinate u, Coordinate v) => (u.X == v.X) && (u.Y == v.Y);
        public static bool operator !=(Coordinate u, Coordinate v) => !((u.X == v.X) && (u.Y == v.Y));

        public override string ToString() => $"x:{X},y:{Y}";
    }    
 
 
class Program
{
    static void Main(string[] args)
    {
        Coordinate p1 = new Coordinate(x: 3, y: 4);
        Coordinate p2 = new Coordinate(x: -1, y: 1);

        Console.WriteLine(p1+p2);

        //You write +, you get this for free!
        p1 += p2;
        Console.WriteLine($"P1 = {p1}");

        //The equality operator
        if (p1 != p2)
        {
            Console.WriteLine($"{p1} and {p2} are not equal");
        }
    }
}
```

In the example above, three opererators were overloaded: `+`, `==` and `!=`. Let's look at these more closely.

```C#
public static Coordinate operator +(Coordinate u, Coordinate v) => new Coordinate(u.X + v.X, u.Y + v.Y);
```

The `+` operator is overloaded, such that `A + B`, returns a _new_ instance of `Coordinate` initialised with the summation of `A` and `B`, and (critically) where `A` and `B` are both of type `Coordinate`.

```C#
    Coordinate p1 = new Coordinate(x: 3, y: 4);
    Coordinate p2 = new Coordinate(x: -1, y: 1);
    Coordinate psum = p1 + p2;
```            

Here, `psum` is a reference to a new instance of `Coordinate`, holding the vector sum of `p1` and `p2`.

- Overloaded operators must be `static` and `public`
- [Only specific operators may be overloaded](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/operator-overloading#overloadable-operators)

Some operators have a requirement that associated operators are also defined. `+` does not, but if you overload `==` you must also overide `!=` (or you get a compiler error)

```C#
   public static bool operator ==(Coordinate u, Coordinate v) => (u.X == v.X) && (u.Y == v.Y);
   public static bool operator !=(Coordinate u, Coordinate v) => !((u.X == v.X) && (u.Y == v.Y));
```        

>> These are essentially static methods with some _syntactic sugaring_. Unlike C++, you cannot access them as explicit static methods on the class.

Having operators can make code very clean to read, for example:

```C#
    if (p1 != p2)
    {
        Console.WriteLine($"{p1} and {p2} are not equal");
    }
```

However, the general advice is to use sparingly and only when the intent is clear. There is a view that operator overloading can lead to ambiguity, and there is some merit to this argument.

## Type Operator Overloading
C# can also perform [user defined type conversions](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/user-defined-conversion-operators) which effectively overrides `=` in some cases)

Let's look at some examples by adding the following code:

Add this to the `Coordinate` class:

```C#
    // Implicit conversion (no typecast) - perform pythagoras when converting to a double
    public static implicit operator double(Coordinate u) => Math.Sqrt(u.X*u.X + u.Y*u.Y);
    public static implicit operator Coordinate(int d) => new Coordinate(d,d);

    // Explicit type conversions (where an explicit type-cast is provided)
    public static explicit operator Coordinate((int x, int y) tuple) => new Coordinate(tuple.x, tuple.y);
```        

In main, you can test this using the following:

```C#
    //Implicit conversion
    double L1 = p1;
    Console.WriteLine($"Length of {p1} is {L1}");

    //This is a C# value-tuple (nice?)
    (int x, int y) t = (x: 3, y: 4);

    //Perform explicit conversion from a tuple to Coordinate
    Coordinate p3 = (Coordinate)t;
    Console.WriteLine($"p3 = {p3}");

    //Implicit conversion from integer to Coordinate
    Coordinate p4 = 0;
    Console.WriteLine($"p4 = {p4}");
```            

Let's consider what has been achieve here:

### Implicit Conversion
Consider the following code:

```C#
    double L1 = p1;
```

Here is the code that performs / defines the meaning of this

```C#
   public static implicit operator double(Coordinate u) => Math.Sqrt(u.X*u.X + u.Y*u.Y);
```        

To someone familiar with the mathematics, they might be able to guess that such a conversion return _the distance of the point from the origin_, but I would suggest in this case _it's very ambiguous_. 

You can argue the same for the following:
```C#
    Coordinate p4 = 0;
```

whereby `p4` becomes a reference to an instance of `Coordinate` with the properties `X` and `Y` set to the literal value 0. This behaviour is defined by the following:

```C#
    public static implicit operator Coordinate(int d) => new Coordinate(d,d);
```

Again, maybe not obvious without inspecting the code more closely. 

### Explicit Conversion
Sometimes it is preferred (or even necessary) to perform an explicit type-cast from one type to another.

```C#
    //This is a C# value-tuple (nice?)
    (int x, int y) t = (x: 3, y: 4);

    //Perform explicit conversion from a tuple to Coordinate
    Coordinate p3 = (Coordinate)t;
```            

The code behind this conversion is as follows:

```C#
   public static explicit operator Coordinate((int x, int y) tuple) => new Coordinate(tuple.x, tuple.y);
```        

In this example, the reader can probably guess the behaviour but more obvious, the explicit type-cast does communicate that there is some type conversion being performed under the hood.

In summary, operator and user-defined conversion operator overloading can be used to make code concise and in one sense more readby, but at risk of also introducing ambiguity. If working in a well understood domain (such as a particular field of mathematics), then operators might communicate their intent by implication through knowledge of the context. However, an explicit function name might be preferrable. It's up to you - use wisely!

## Enumerated Types
[Enumerated types](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/enumeration-types) are very useful for making code both safer and more readable.

Consider the following type declaration:

```C#
    public enum MarketSector
    {
        Omnivore, Vegetarian, Vegan, Fructarian
    }
```

Under the hood, a variable of type `MarketSector` is an integer. However, it can ONLY take on the values `MarketSector.Omnivore`, `MarketSector.Vegetarian`, `MarketSector.Vegan` and `MarketSector.Fructarian` which (by default) evaluate to 0,1,2 and 3 respectively. 

```C#
    MarketSector s = MarketSector.Vegan;
    Console.WriteLine((int)s);
```
 would display the value 2. You can override the encoded values. For example,
 
 ```C#
    public enum MarketSector
    {
        Omnivore=1, Vegetarian=2, Vegan=Vegetarian+10, Fructarian=Vegetarian+100
    }
    
    MarketSector s = MarketSector.Vegan;
    Console.WriteLine((int)s);
```    

would display 12. Note that you cannot assign `s` to a numerical value. The benefit of this include:

- The code is more readable
- The compiler can enforce that only permitted values can be assigned

Now look at the following example code:

```C#
    public static class Extensions
    {
        public static string Definition(this MarketSector s)
        {
            switch (s)
            {
                case MarketSector.Omnivore:
                    return "Eats both meat and vegetables";
                case MarketSector.Vegetarian:
                    return "A person who does not eat meat or fish, and sometimes other animal products, especially for moral, religious, or health reasons.";
                case MarketSector.Vegan:
                    return "A person who does not eat or use animal products.";
                case MarketSector.Fruitarian:
                    return "A person who eats only fruit, and possibly nuts and seeds.";
                default:
                    return "Error - did you add another category without updating the code?";
            }
        }
    }
    public enum MarketSector
    {
        Omnivore=1, Vegetarian=2, Vegan=Vegetarian+10, Fruitarian = Vegetarian +100
    }

    [Flags]
    public enum IngredientsContain
    {
        Wheat = 1,
        Dairy = 2,
        Gluten = 4,
        Nuts = 8
    }

    class Recipe
    {
        public IList<string> Ingredients { get; set; }
        public IngredientsContain Allergens { get; set; }
        public MarketSector TargetMarket;

        public string PackagingNoticeSuitability
        {
            get
            {
                switch (TargetMarket)
                {
                    case MarketSector.Omnivore:
                        return "Contains meat products";
                    case MarketSector.Vegetarian:
                        return "Suitable for Vegetarians";
                    case MarketSector.Vegan:
                        return "Vegan";
                    case MarketSector.Fruitarian:
                        return "Fruitarian Certified";
                    default:
                        return "Note sure - don't eat this!";
                }
            }
        }
```

### [Flags] attribute
By applying the `Flags` attribute, an enumerated value can take on any combination of permitted values. The integer values are typically powers of 2.

```C#
    [Flags]
    public enum IngredientsContain
    {
        Wheat = 1,
        Dairy = 2,
        Gluten = 4,
        Nuts = 8
    }
```

This is why the following line is valid:

```C#
    FavCurry.Allergens = IngredientsContain.Dairy | IngredientsContain.Gluten;
```

The values are combined with a logical-or. These can be tested for using an expression such as:

```C#
    if (FavCurry.Allergens.HasFlag(IngredientsContain.Nuts))
    {
        Console.WriteLine("Warning - this product may be banned from certain airlines");
    }
```

Again, note how the code is more readable than if numerical values were used. There is more to be said for enumerated types. You can read more in the [Microsoft documentation](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/enumeration-types#enumeration-types-as-bit-flags).

### Extension Methods
In C#, you can even extend an enumerated type to include methods. Remembering that an enumerated type is fundamentally an integer, then such mehods will operate on the contained value. The example above is highlighted here:

```C#
      public static string Definition(this MarketSector s)
        {
            switch (s)
            {
                case MarketSector.Omnivore:
                    return "Eats both meat and vegetables";
                case MarketSector.Vegetarian:
                    return "A person who does not eat meat or fish, and sometimes other animal products, especially for moral, religious, or health reasons.";
                case MarketSector.Vegan:
                    return "A person who does not eat or use animal products.";
                case MarketSector.Fruitarian:
                    return "A person who eats only fruit, and possibly nuts and seeds.";
                default:
                    return "Error - did you add another category without updating the code?";
            }
        }
    }
    public enum MarketSector
    {
        Omnivore=1, Vegetarian=2, Vegan=Vegetarian+10, Fruitarian = Vegetarian +100
    }
```

This code add the method `Definition` such that it can be applied to an instance (ultimately an integer), so that it can be invoked as follows:

```C#
    Console.WriteLine(FavCurry.TargetMarket.Definition());
```

Although it is a static method, like unary overloaded operators, some syntactic sugaring is used to allow this method to be invoked against an instance of the enumerated type.

See [Extension Methods](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods) for a more comprehensive discussion.

----

[Back to Table of Contents](/docs/Chapters/Chapter_1_Introduction/README.md)
