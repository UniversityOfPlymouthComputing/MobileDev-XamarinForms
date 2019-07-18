[Back to Table of Contents](/docs/Chapters/Chapter_1_Introduction/README.md)


# Essential C# Part 1
This section is intended to be a quick refresher of some of the C# we will encounter in subsequent sections. 
It is not a comprehensive treatment of C#, but more of a smorgasbord of selected topics.

In this section, all the code will be targeting a console application.
You can use either Visual Studio 2019 or [Visual Studio Code](https://code.visualstudio.com/download).

- Visual Studio 2019. Create a new project, and search on "Console". From the results pick Console App (.NET Core)
- Visual Studio Code. [See these instructions](https://docs.microsoft.com/en-us/dotnet/core/tutorials/with-visual-studio-code) to create and debug an console application.

I will use Visual Studio 2019 for Windows for the sake of consistency (as it's what we use in our teaching labs).

## Hello World Revisited
When you create a new console app, name the project `HelloWorld` and check the source file `Program.cs` is generated as follows:

```C#
using System;

namespace HelloWorld
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

If you feel unsure about the class and static keyword, just make a mental note for now.

> One of the difficulties for educators is that you can't get Hello World on the screen without creating a class and a static method. For educators, it is worth noting that Microsoft recently released access to [Try.net](https://dotnet.microsoft.com/platform/try-dotnet) where you can simply write C#.

Let's examine this code more closely however and pick out some key discussion points (maybe to finish later).

- The entry point for a C# application is `Main`. However, it has the word `static` on the front. Static methods will be covered later, but it's important to make a mental note.
- `Console.WriteLine` invokes a method, but what is `Console`? There is no instance variable called `Console` created, so how come it exists? To understand this, we need to know what me mean by static methods.
- `namespace HelloWorld` appears around the code. You've probably seen such things before, but do you know why is this here?
- `using System` again, it's there, so good to question what it means.

## Creating and Instantiating Classes
Let's wind back and go over a topic you are likely to have covered already - writing a class and instantiating one (or more) with the `new` keyword.

**Task:** Create a new class `RoadVehicle`

- Right click the HelloWorld project (as opposed to the Solution), and choose Add->New Item
- Ensure "Class" is selected
- Set the Name to `RoadVehicle.cs`

The code should look something like this:

```C#
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloWorld
{
    class RoadVehicle
    {
    }
}
```

Let's add a *constructor* method to the `RoadVehicle` class. If you are unsure, a constructor is a method that runs when we create an *instance* of this class. It's often used for initalisation tasks.

```C#
    class RoadVehicle
    {
        public RoadVehicle()
        {
            Console.WriteLine("RoadVehicle Constructor");
        }
    }
```

Back in `Program.cs`, we now create an instance of this class in the `Main`method

```C#
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            RoadVehicle v1 = new RoadVehicle();
        }
```

Now run the code. The string "RoadVehicle Constructor" should have been written to the console window.

> If you are unsure about the sequence, try setting a breakpoint in `Main` and step _in_ to the code (F11), line by line.

You have probably used the `new` keyword before, but do you know _why_ you use it? It's all about how the computer manages memory.
We have actually added three objects to memory:

- An _instance_ of the class. You can have multiple instances of the class, but they are not the same as the class itself. We will see the distinction as we progress. All instances are stored in the area of memory known as the _heap_ using a mechanism known as _dynamic memory allocation_.
- A local variable `v1`. This is stored in the area of memory known as the _function stack_ (where the function is Main). More on this later.
- The `RoadVehicle` class itself (yes, the class can be thought of as a singleton object in its own right). There is only ever **one copy** of the class object in memory, and it is created on first reference. This is also stored in a special area of memory.


### Member Variables vs Static Members
There are two types of data storage associated with a class:

- **instance member** variables (or constants), where each instance (created with `new`) has it's own copy
- **static member** variables (or constants), where the so-called _class object_ has a single unique copy

Update `RoadVehicle.cs` to read as follows:

```C#
    class RoadVehicle
    {
        public static string ProjectVersion = "1.0"; // static member variable
        public int EngineSerialNumber;               // instance member variable

        public RoadVehicle()
        {
            Console.WriteLine("RoadVehicle Constructor");
        }
    }
```

Note the `static` keyword. Let's now create multiple instances of `RoadVehicle` back in `Main`

```C#
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("Code running: Project version " + RoadVehicle.ProjectVersion);

            RoadVehicle v1 = new RoadVehicle();
            v1.EngineSerialNumber = 12345;

            RoadVehicle v2 = new RoadVehicle();
            v2.EngineSerialNumber = 2468;

            //Just to be sure
            if ((v1 == null) || (v2 == null))
            {
                System.Console.WriteLine("That all went badly!");
                return;
            }

            Console.WriteLine("Vehicle 1 serial: " + v1.EngineSerialNumber.ToString());
            Console.WriteLine("Vehicle 2 serial: " + v2.EngineSerialNumber.ToString());
        }
    }
```  

The `static` member `ProjectVersion` was accessed before any mention of `new`. This is because `static` members belown to the class object, and not any instance of the class object. The syntax is `Type.Member`, where `Type` is typically a class name, and `Member` is a static member (variable, property or method).

> Static members are often used as global constants associated with a particular type. You don't need to create an instance of a class, you just need the class itself.

From the code above, throughout the duration of the `Main` method, we can identify five additional objects now existing in memory: the two instances of `RoadVehicle`, two local variables `v1` and `v2`, and the class object for `RoadVehicle` (which includes any static members). These all reside in different areas of memory within the application. It is useful to have some understanding about memory organisation, and as it's a topic that can be easily glossed over, I've decided to cover it here.

A simplified representation of an application memory is shown below.

![Application Memory](img/ApplicationMemory.png)

- **Program Code** is where executable instructions for each method are stored. These are held in a distinctly different area to data (variables). It is typically _read-only_ and executable.
- **Stack Memory** holds data such as function parameters and local variables. This region grows downwards (in terms of memory addresses) as you enter a method and contracts back upwards when you leave.
- **Heap Memory** is allocated on request at run-time, typically using the _new_ keyword. When you use _new_, the heap manager attempts to locate a space on the heap that is large enough for the type of object being alocated and stored. In the unfortuante event that it cannot find such a block of memory, a _null_ is returned. Heap memory can be enourmous as it can often utilise page files (also known as swap files) on disks if there is in sufficient physical RAM. The top of the heap typically grows upwards (in terms of memory addresses) as more objects are allocated and down as objects are deallocated. This process is known as dynamic memory allocation. As I hope is now apparent, _allocating and deallocating memory takes effort to manage, so comes with a CPU overhead_. For performance critical code, you should try to avoid very rapid dynamic memory allocation.  
- There are also different regions of the heap. One is the _high frequency heap_, within which static variables are stored and persist, sometimes the lifetime of the application.

Back to the code, we note the following:

- Each instance of `RoadVehicle` has it's own _independent_ copy of the _instance member_ variable `EngineSerialNumber`. The keyword `new` requests that a portion of the _heap memory_ is allocted and reserved to hold all the instance member variables. This is done for each _instance_ of the class. The keyword _new_ returns either a reference to the allocated memory (on sucess) or null (if it fails).
- We did not use `new` to allocate memory for the static member `ProjectVersion`. As soon as there is a reference to the `RoadVehicle` class (via accessing a static or new), a single class object is created in memory. We can view this as a _singleton_ object where only one copy can ever be created. Therefore, there is only ever one copy of the `ProjectVersion` string in memory. As it is public, it is global to the whole application.

Note also that we have two local variables, `v1` and `v2`. These are declared _locally_ within the `Main` function, and are assigned to the reference values return by `new`. Under the hood, `new` returns a reference to some allocated block of memory on the _heap_ (or null if it fails). We therefore say these are _reference types_ (under the hood, reference types related to an actual address in memory). This is in contrast to _value types_ (such as `int` or `bool`), a discussion for later. 

Both `v1` and `v2` _only exist within the scope of the Main function_. The variables themselves are stored in another area of memory known as the *function stack*. This transient region of memory holds data such as function parameters and local variables. 

> `v1` and `v2` are local variables, so only exist within the scope of the `Main` function. They are _automatically_ created (allocated) when asigned a value, and automatically destroyed when we leave the `Main` function.

So for the following code:
```C#
            RoadVehicle v1 = new RoadVehicle();
            v1.EngineSerialNumber = 12345;

            RoadVehicle v2 = new RoadVehicle();
            v2.EngineSerialNumber = 2468;
```

We can visualise this as follows:

![v1 and v2 are locals](img/local_reference_to_heap.png)

Note now the referenses (ultimately addresses) are stored in stack variables `v1` and `v2`. You can think of this as making a note of where you left two objects. The note is not the same as the objects. Once execution leaves `Main`, all stack-based objects are automatically deleted,  including `v1` and `v2`. So what happens to the two instances of `RoadVehicle` stored in the heap memory?

> Any object dynamically allocated on the heap with `new` will _persist as long as there is at least one reference to it_. If that reference is removed, the heap object will be automatically deallocated, thus freeing up the memory for other purposes. This process is automated by the .NET [_garbage collector_](https://docs.microsoft.com/en-us/dotnet/standard/garbage-collection/fundamentals). **C#.NET is said to be a _managed_ language**. 
> Lower level _unmanaged languages_ do not do this for you. In languages such as C and C++, it is the developer's responsibility to both allocate _and_ delete heap objects (in C++ you have both _new_ and _delete_ keywords, in C it's the functions `malloc` and `free`). If you forget to delete heap objects, you end up with forgotten objects on the heap that can no longer be reached (referenced). This is known as a _memory leak_. If you keep leaking memory, over time, the heap will fill with very real consequences.

The great news is we rarely have to worry about deallocating memory or memory leakes as it's (mostly) done for us in C#.NET.

### When names collide - namespace to the rescue!
A class name needs to be unique. However, as projects scale, probably involving code written by others (in libraries or additional source), the chance of a name collision can become very real. For example, if you create a class called `Console`, in the absence of namespaces, it could collide with the .NET Console class (we've already met this in the code above). To greatly reduce the chance of a name collision, we enclose our class declatations within a _namespace_. You can think of a namespace as a prefix. In this case, we use the namespace HelloWorld, so the _fully qualfied name_ of our class is _really_ called `HelloWorld.RoadVehicle`. Every type (class, interface, enum) within the enclosing namespace gets the same prefix.

To illustrate this, do the following:

- In `RoadVehicle.cs`, we are going to change the namespace to `DepartmentOfTransport`

```C#
namespace DepartmentOfTransport
```

Back in `Program.cs`, you should now be able to see there are errors indicating the type `RoadVehicle` cannot be found. This is because no such type (a class is a custom type) exists.

One option is to use it's fully qualified name:

```C#
            DepartmentOfTransport.RoadVehicle v1 = new DepartmentOfTransport.RoadVehicle();
            DepartmentOfTransport.RoadVehicle v2 = new DepartmentOfTransport.RoadVehicle();
```            

This is very verbose and not so readable, but as you can see, `DepartmentOfTransport.RoadVehicle` is also unlikely to cause a name collision for most of us (if it does, simply choose another namespace).

Subject to there being no other class called `RoadVehicle` in the project, we can skip the namespace prefix by writing the following line at the top of our source code:

```C#
using DepartmentOfTransport;
```

The compiler now knows to search all the namespaces being used for type names. This is equally true for the `Console` class object.

You can try this if you like. 
- Comment out the line `using System`
- Note the error - to fix, replace `Console` with 'System.Console' 

We will keep the namespace `DepartmentOfTransport` to give us practise in using them.

## Properties
Properties are often related to static or instance variables, but use a function to provide access instead (for many reasons). Let's extend the example above to add some properties to the `RoadVehicle` class.

Change the `RoadVehicle` class to the following:

```C#
    class RoadVehicle
    {
        public static string ProjectVersion = "1.0"; // static member variable
        private int _engineSerialNumber;               // instance member variable
        private int _numberOfWheels;
        private int _carriageCapacity;

        public string Description()
        {
            return string.Format("Road Vehicle. Wheels: {0:d}, Capacity: {1:d} people, serialNo: {2:d}", NumberOfWheels, CarriageCapacity, EngineSerialNumber);
        }
        public RoadVehicle(int EngineSerialNumber, int NumberOfWheels=4, int CarriageCapacity=5)
        {
            _engineSerialNumber = EngineSerialNumber;
            _numberOfWheels = NumberOfWheels;
            _carriageCapacity = CarriageCapacity;
            Console.WriteLine("RoadVehicle Constructor");
        }
    }
```    
Note the following:

- The constructor now has parameters with default values. This makes these parameters optional which can help keep code concise.
- All the instance member variables are now `private` so inaccessible from outside the class. The only way to set them now is via the constructor. This makes sense for most vehicles (not many change the number of wheels!).

Now change `Main` to the following:

```C#
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("Code running: Project version " + RoadVehicle.ProjectVersion);

            RoadVehicle v1 = new RoadVehicle(EngineSerialNumber:12345);
            Console.WriteLine(v1.Description());

            RoadVehicle v2 = new RoadVehicle(EngineSerialNumber:2468);
            Console.WriteLine(v1.Description());
        }
```

Run the code and familiarise yourself. Now add the following to the `RoadVehicle` class :

```C#
        public int EngineSerialNumber
        {
            get
            {
                return _engineSerialNumber;
            }
        }
```

As all this method does is return a single variable, so it can be shortened to:

```C#
        public int EngineSerialNumber => _engineSerialNumber;
```

In `Main`, you can now write (if you wish):

```C#
Console.WriteLine("Serial {0:d}", v1.EngineSerialNumber);
```

Using a property in this way has provided read-only access. The propery name is `EngineSerialNumber`. The syntax to access it looks like you're accessing an instance variable, but in fact you're calling a method. In this case, no setter was written, so you cannot write the following without a compiler error:

```C#
v1.EngineSerialNumber = 12345;
```

Providing limited access is only one benefit. For example, if you wanted to read the serial number as a string and not an integer, we could have equally written

```C#
        public string EngineSerialNumber
        {
            get
            {
                return _engineSerialNumber.ToString();
            }
        }
```
which mashalls an integer to a string representation. From outside the class, it looks like a stored string property. Internally, it's an integer (which is smaller and faster). 

Sticking with the integer version, we can go a stage further and remove the explicit instance variable declaration and use an auto property:

``` C#
    class RoadVehicle
    {
        public static string ProjectVersion = "1.0"; // static member variable
        private int _numberOfWheels;
        private int _carriageCapacity;

        public int EngineSerialNumber { get; }

        public string Description()
        {
            return string.Format("Road Vehicle. Wheels: {0:d}, Capacity: {1:d} people, serialNo: {2:d}", NumberOfWheels, CarriageCapacity, EngineSerialNumber);
        }
        public RoadVehicle(int EngineSerialNumber, int NumerberOfWheels=4, int CarriageCapacity=5)
        {
            this.EngineSerialNumber = EngineSerialNumber;
            _numberOfWheels = NumerberOfWheels;
            _carriageCapacity= CarriageCapacity;
            Console.WriteLine("RoadVehicle Constructor");
        }
    }
```

Observe the line `public int EngineSerialNumber { get; }` which creates a read-only **property**. Behind the scenes there is an integer of course, but as we never need direct access to it, it's hidden away (from harm).

In inspection, we note that the `Description()` function is actually playing the same role as a property. Note this has no backing store as it's what me might call a computed property. We can convert this to a property:

```C#
public string Description => string.Format("Road Vehicle. Wheels: {0:d}, Capacity: {1:d} people", NumberOfWheels, CarriageCapacity);
```

Now we can access it from outside (e.g. in `Main`) without method parenthesis:

```C#
Console.WriteLine(v1.Description);
```

Putting this all together we get:

```C#
    class RoadVehicle
    {
        public static string ProjectVersion = "1.0"; // static member variable

        public int EngineSerialNumber { get; }
        public int NumberOfWheels { get; }
        public int CarriageCapacity { get; }
        public string Description => string.Format("Road Vehicle. Wheels: {0:d}, Capacity: {1:d} people", NumberOfWheels, CarriageCapacity);
        public RoadVehicle(int EngineSerialNumber, int NumberOfWheels=4, int CarriageCapacity=5)
        {
            this.EngineSerialNumber = EngineSerialNumber;
            this.NumberOfWheels = NumberOfWheels;
            this.CarriageCapacity = CarriageCapacity;
            Console.WriteLine("RoadVehicle Constructor");
        }
    }
```

Note also the following:

- In the constructor, observe how the parameter names were chosen to be the same as the property names. You might think this would result in ambiguity, but it can be resolved using `this` (which can be thought of as _this instance_).
- The constructor can write properties even through no setter accessor was provided. You cannot do this from normal member methods.
- You can also initialise an auto property inline. For example, `public int NumberOfWheels { get; } = 4;`

Sometimes we don't want auto properties, and it makes more sense to back a property with an instance variable. Consider the `Description` property:

```C#
public string Description => string.Format("Road Vehicle. Wheels: {0:d}, Capacity: {1:d} people, serialNo: {2:d}", NumberOfWheels, CarriageCapacity, EngineSerialNumber);
```

Everytime this is accessed, the `FormatString` method is called. This is despite it never changing (it depends on `NumberOfWheels` and `CarriageCapacity`, both of which are fixed once initialised). As the type is `string`, this is a `reference type` which can be set to `null` (you can do the same with value types if you use _optionals_, but we cover that later).

```C#
        private string _description;

        public string Description
        {
            get
            {
                if (_description == null)
                {
                    _description = string.Format("Road Vehicle. Wheels: {0:d}, Capacity: {1:d} people, serialNo: {2:d}", NumberOfWheels, CarriageCapacity, EngineSerialNumber);
                }
                return _description;
            }
        }
```        
Note how `string.Format` is only called the first time the property is read. For subsequent reads, it simply returns the variable-backed value. Note this is only appropriate if you are certain the value will never change.

## Partial Classes
Something we will meet in Xamarin.Forms are _partial classes_. Put simply, it is a way to split a class across separate source files.

Try the following:

- Right click the HelloWorld project (not the solution)
- Choose Add->New Item
- Choose Class
- Set the Name to `RoadVehicleProperties.cs`
- In the new class file, remember to change the namespace to `DepartmentOfTransport`
- In **both** `RoadVehicle.cs` and `RoadVehicleProperties.cs`, change the class declaration to `partial class RoadVehicle` 

Cut all the properties from `Roadvehicle.cs` and paste them into the partial class in `RoadVehicleProperties.cs`

`RoadVehicle.cs` should read as follows:

``` C#
using System;
using System.Collections.Generic;
using System.Text;

namespace DepartmentOfTransport
{
    partial class RoadVehicle
    {

        public RoadVehicle(int EngineSerialNumber, int NumberOfWheels=4, int CarriageCapacity=5)
        {
            this.EngineSerialNumber = EngineSerialNumber;
            this.NumberOfWheels = NumberOfWheels;
            this.CarriageCapacity = CarriageCapacity;
            Console.WriteLine("RoadVehicle Constructor");
        }
    }
}
```

and `RoadVehicleProperties.cs` should read as follows:

```C#
using System;
using System.Collections.Generic;
using System.Text;

namespace DepartmentOfTransport
{
    partial class RoadVehicle
    {
        public static string ProjectVersion = "1.0"; // static member variable
        public int EngineSerialNumber { get; }
        public int NumberOfWheels { get; }
        public int CarriageCapacity { get; }
        private string _description;

        public string Description
        {
            get
            {
                if (_description == null)
                {
                    _description = string.Format("Road Vehicle. Wheels: {0:d}, Capacity: {1:d} people, serialNo: {2:d}", NumberOfWheels, CarriageCapacity, EngineSerialNumber);
                }
                return _description;
            }
        }
    }
}
```

Partial classes are fairly self explainatory, but you might not have known this was possible. As we will learn, this is useful in Xamarin.Forms as it helps split developer edited code from computer generated code.

> All the code from this section [can be found in the folder properties](/code/Chapter1/essential-c-sharp-part1/properties)

## Inheritance
Class inheritance is something that is used throughout .NET, Xamarin.Forms and is fundamental to C#, so it's worth a recap on some important points using the example developed so far.

- Create a new class called `Car` in `Car.cs`
- Change the code to match that shown below

```C#
namespace DepartmentOfTransport
{
    class Car : RoadVehicle
    {
        public bool HasTowBar { get; set; } = false;

        public Car(int EngineSerialNumber, int NumberOfWheels = 4, int CarriageCapacity = 5, bool HasTowBarFitted = false) : base(EngineSerialNumber, NumberOfWheels, CarriageCapacity)
        {
            HasTowBar = HasTowBarFitted;
            Console.WriteLine("Car Constructor: type " + this.GetType().ToString());
        }
    }
}
```

This class **inherits** from `RoadVehicle` using the following syntax:

```C#
class Car : RoadVehicle
```

There is some terminology around this.
- This is known as _subclassing_. 
- In this example, `RoadVehicle` might be referred to as the _parent_ class or _base class_.
- The `Car` class is referred to as and the _child_ or _subclass_.

Note the following key points:

- All properties and methods of the parent will be inherited by the child
- Only non-private properties of the parent class will be accessible in the child class
- In C#, you can only inherit a single parent class (but implement many interfaces - covered later) unlike in C++ where you can inherit many.
- The child can reference itself using the keyword `this` and it's parent using `base` 
- The constructor in the child first calls the constructor of the parent. Initialisation is perform top to bottom. If not specified, the default would be to automatically call the parameterless constructor of the parent.
- Note the syntax for calling a specific parent constuctor.
- The base class `RoadVehicle` also inherits from [`System.Object`](https://docs.microsoft.com/en-us/dotnet/api/system.object?view=netcore-2.1) by default.

> You might want to use the debugger to step into the code and see the sequence in which constructors are called. The general rule is parent, then child.

In the child class, you typically do one or both of the following:

- add new functionality (additional properties and methods)
- _override_ functionality

Let's look at examples of both.

### Adding functionality
We've added an extra property `HasTowBar` (for connecting and towing trailors) to the `Car` class that was not present in the parent. This makes sense as not all Road Vehicles can have a tow bar fitted. For a car, the fitting of a tow bar is something that can change through the lifetime of a car, so this is a propery that can be both written and read.

This property was made public, so it can be changed from outside. We can do this in `Main`. For example

```C#
    Car PrimaryCar = new Car(EngineSerialNumber: 13579);
    PrimaryCar.HasTowBar = true;
```

It should be stresses that the `HasTowBar` property is only present in `Car`, and not `RoadVehicle`. Extending behaviour in this way is probably the most common thing to do when subclassing. 

What is often more intriguing is when we wish to modify (either by replacing or  extending) the behaviour of a parent class using a process known as _overriding_.

### Overriding functionality
In `RoadVehicle` there was a property `Description`. It is public, and therefore is also a public property of `Car`. We can demonstrate this by adding the following code to `Main`

```C#
    Car PrimaryCar = new Car(EngineSerialNumber: 13579);
    PrimaryCar.HasTowBar = true;
    Console.WriteLine(PrimaryCar.Description);
```            

However, the output seems incomplete and indistinguishable from a generic road vehicle. It is useful that the information is retined from the parent, but it would be better if there was some indication that this was also a car and it has a towbar attached. We can do this by _overriding_ (and in this case extending) the property:

In `Car.cs`
```C#
    public override string Description
    {
        get
        {
            return base.Description + ": Is of type Car" + (HasTowBar ? " with towbar attached" : ".");
        }
    }
```

Note how we've _extended_ the functionality here. `base.Description` will return the description string from the parent. Appended to this is an addition string relating to the car (in C# you can append strings with the `+` operator).

> Aside: The code that reads `(HasTowBar ? " with towbar attached" : "."` is an inline conditional statement of the form `<condition> ? <value if true> : <value if false>`. 
> `HasTowBar` is tested. If true, then `" with towbar attached"` is used otherwise its a full stop `"."`

In `RoadVehicleProperties.cs`, to allow a baseclass method to be overridden in a child class, you also need to add the keyword [`virtual`](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/virtual)

```C#
    public virtual string Description
```

Run the code again and you should see additional information because it is a `Car`.

> The final code can be found in [the inheritence folder](/code/Chapter1/essential-c-sharp-part1/inheritance)

## Polymorphism and virtual methods
Polymorphism is a big word which can result in new developers running for the hills in fear, when in fact it's realtively simple. They key is in the keywords `virtual` and `override`.

From the Microsoft documentation:

> When a virtual method is invoked, the run-time type of the object is checked for an overriding member. The overriding member in the most derived class is called, which might be the original member, if no derived class has overridden the member. (from https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/virtual, accessed 18/07/2019)

Consider the following three classes:

```C#
    public class Entity
    {
        public virtual void IdentifyYourself() => Console.WriteLine("I am Base");
    };

    public class TypeA : Entity
    {
        public override void IdentifyYourself() => Console.WriteLine("I am TypeA");
        public void JumpOverFences() => Console.WriteLine("Fence!");
    };

    public class TypeB : Entity
    {
        public new void IdentifyYourself() => Console.WriteLine("I am TypeB");
        public void JumpThroughHoops() => Console.WriteLine("Hoop!");
    };
```
Note that the subclass `TypeA` uses `override` whereas `TypeB` uses `new`. Let's now observe 

```C#
    Entity child;

    bool choice = FlipACoin(); // returns true or false
    if (choice == true)
    {
        child = new TypeA();
    }
    else
    {
        child = new TypeB();
    }

    child.IdentifyYourself();
```

First we notice that the type for child is that of a parent class `Entity`. At _run time_, depending on the outcome of the `FlipACoid()` method, `child` is assigned a reference to _either_ `TypeA` or `TypeB`. This is perfectly legal C# because `TypeA` and `TypeB` have all the atttributes of type `Entity`. What is interesting is when you invoke `IdentifyYourself()`.

- if `TypeA` was chosen, the output is "I am TypeA" which is the implementation in the derived class. 
   - This is interesting as at _compile time_, `child` is of type `Entity` which has it's own implementation of `IdentifyYourself()`
   - Note that in the child class, the keyword `override` is used.
- if `TypeB` was chosen, the output is "I am Base" which is the implementation in the base class.
   - The difference is that the keyword `new` was used instead of `override`

How does this work? In the case of `TypeA`, something often known as _late binding_ is used. The compiler notes two things: 

(i) `IdentifyYourself` is virtual in the base class
(ii) `IdentifyYourself` is overridden in `TypeA`

Normally when you invoke a particular (non virtual) method in code, the build tools (at some point in the cycle) uses the object type to resolve a fixed address in memory for that specific method. Code does not move around in memory, so this makes sense and results in efficient code, especially if the method is called multiple times. This process is known sometimes known as _early binding_ and it relies heavily on object types. We sometimes say C# is a strongly typed language.

However, in the case of _overridden virutal methods_ (or properties), as in the case of `IdentifyYourself()`, the address is **not** resolved at build time, and instead, code is added to _look it up at run time_. When it encounters the method call, it will traverse down the object heirarchy to find (and cache) the last (overridden) implementation and call that. _This is polymorphism_, and for a small performance hit, it enables nimble and concise programming techniques.

As a final point, `child` is still considered to be of type `Entity`. You cannot invoke `JumpOverFences()` as this is specific to `TypeA`. If you wanted to do this, you would need to add your own run-time check:

```C#
    if (child is TypeA)
    {
        //First Cast to TypeA, then invoke
        ((TypeA)child).JumpOverFences();
    }
```

And if you get this wrong, expect a run-time exception!

Now we return to our example code and apply this concept. First let's add a new type, that is `Motorbike`. 

- Motorbikes don't have towbars (although I'd like to see someone try)
- Some Motorbikes can have sidecars

With this in mind:

- Add a new class `Motorbike` to `Motorbike.cs`
- Paste in the code below

```C#
using System;
using System.Collections.Generic;
using System.Text;

namespace DepartmentOfTransport
{
    class Motorbike : RoadVehicle
    {
        public bool HasSideCar { get; set; } = false;

        public Motorbike(int EngineSerialNumber, int NumberOfWheels = 2, int CarriageCapacity = 2, bool HasSideCarFitted = false) : base(EngineSerialNumber, NumberOfWheels:2, CarriageCapacity)
        {
            HasSideCar = HasSideCarFitted;
            Console.WriteLine("Motorbike Constructor: type " + this.GetType().ToString());
        }

        public override string Description
        {
            get
            {
                return base.Description + ": Is of type Motorbike" + (HasSideCar ? " with sidecar attached" : ".");
            }
        }
    }
}
```

In `Main`, we can test this out by adding the following code:

```C#
    Motorbike bike = new Motorbike(EngineSerialNumber: 333444);
    bike.HasSideCar = true;
    Console.WriteLine(bike.Description);
```

Let's think about the logic behind the class relationships:

- A `RoadVehicle` is general, and not of any particular vehicle type. It has the common properties shared among more specific types, such as Cars and Motorbikes.
- A Car is a type of `RoadVehicle`
- A `Motorbike` is also a type of `RoadVehicle`

Now consider a driver, who commutes to work every day. That driver has a primary mode of transport. For illustrative purposes, we can going to provide two concrete options: `Car` or `Motorbike`. However, each person is different. Let's now create the Driver class:

```C#
using System;
using System.Collections.Generic;
using System.Text;

namespace DepartmentOfTransport
{

    class Driver
    {
        public string Name { get; set; }
        public RoadVehicle PrimaryModeOfTransport { get; set; }

        Driver(string Name, RoadVehicle PrimaryModeOfTransport = null)
        {
            this.Name = Name;
            if (PrimaryModeOfTransport != null)
            {
                this.PrimaryModeOfTransport = PrimaryModeOfTransport;
            }
        }

        public string Description
        {
            get
            {
                if (PrimaryModeOfTransport == null)
                {
                    return Name + ", has no primary vehicle";
                }
                else
                {
                    return Name + ": Primary mode of transport is " + PrimaryModeOfTransport.Description;
                }
            }
        }
    }
}
```

Note that `PrimaryModeOfTransport` is of type `RoadVehicle`. We don't yet know what actual type each driver will use, so the common base class `RoadVehicle` is used as the type. To see this in action, let's clean up Main to keep things easier to read

```C#
        static void Main(string[] args)
        {
            Driver commuter1 = new Driver(Name: "Regular Dave", new Car(EngineSerialNumber:12345,HasTowBarFitted:true));
            Driver commuter2 = new Driver(Name: "Risky Dave", new Motorbike(EngineSerialNumber:333555));
            Driver commuter3 = new Driver(Name: "Green Dave", null);

            Console.WriteLine(commuter1.Description);
            Console.WriteLine(commuter2.Description);
            Console.WriteLine(commuter3.Description);
        }
```        

Note that concrete instances of `Car` and `Motorbike` are passed into the second parameter of commuter1 and commuter2 respectively. 
Run the code. Look closely at the output

```
RoadVehicle Constructor
Car Constructor: type DepartmentOfTransport.Car
RoadVehicle Constructor
Motorbike Constructor: type DepartmentOfTransport.Motorbike
Regular Dave: Primary mode of transport is Road Vehicle. Wheels: 4, Capacity: 5 people: Is of type Car with towbar attached
Risky Dave: Primary mode of transport is Road Vehicle. Wheels: 2, Capacity: 2 people: Is of type Motorbike.
Green Dave, has no primary vehicle
```

Note the console output for the first two, where we observe the _the correct description of the vehicle is displayed_ (for `Car` and `Motorbike` respectively).
Lookimg back at the source for `Driver`, let's remind ourselves of the following key points:

- `PrimaryModeOfTransport` is of type `RoadVehicle`
- Despite this, `PrimaryModeOfTransport.Description` somehow knows to use the property from the child type (`Car` and `Motorbike` respectively)

This is Polymorphism and it is a run-time facility. At run time:

- An instance of a class can be assigned to a variable with a _parent type_
- Introspection is performed on the `PrimaryModeOfTransport` property to establish it's actual type.
- The concrete property for `Description` in the child type was found and called.

**Experiment**
In RoadVehicleProperties.cs, try removing the word `virtual` from the line that reads `public virtual string Description`

**virtual** functions are functions that can be overridden and support polymorphic behaviour. This adds an extra run-time cost, but also allows for much more expressive code to be written.

Try changing the keyword **override** to **new** (which is the default). Contrast the results.

The code in this section can be found in the [polymorph folder](/code/Chapter1/essential-c-sharp-part1/polymorph)

## Static Classes
Before we finnish this section, let's take a quick clook at static classes. Consider the following example:

```C#
    static public class MathTools
    {
        static readonly double Pi = 3.1415926541;
        public static double Scale { get; set; } = 1.0;

        public static double AreaOfCircle(double radius)
        {
            double r = radius * Scale;
            return Pi * r * r;
        }

        public static double CircumferenceOfCircle(double radius)
        {
            double r = radius * Scale;
            return Pi * r * 2.0;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("Area of a circle radius 10m is " + MathTools.AreaOfCircle(10.0));
            Console.WriteLine("Circumference of a circle radius 10m is " + MathTools.CircumferenceOfCircle(10.0));
            MathTools.Scale = 0.01;
            Console.WriteLine("Area of a circle radius 10mm is " + MathTools.AreaOfCircle(10.0));
            Console.WriteLine("Circumference of a circle radius 10mm is " + MathTools.CircumferenceOfCircle(10.0));
        }
    }
```
    
The class `MathTools` conly contains static members. You cannot have instance members in a static class. You cannot / do not need to ever use `new` in relation to a static class. As soon as you make a reference to it, it will exist. Note how `MathTools.Scale` can be used as a global property.

This is not something you are likely to use often, but it's good to know.

-----

[Back to Table of Contents](/docs/Chapters/Chapter_1_Introduction/README.md)
