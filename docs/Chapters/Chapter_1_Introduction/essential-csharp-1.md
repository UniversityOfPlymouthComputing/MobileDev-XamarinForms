# Essential C# Part 1
This section is intended to be a quick refresher of some of the C# we will encounter in subsequent sections. 
It is not a comprehensive treatment of C#.

In this section, all the code will be targeting a console application.
You can use either Visual Studio 2019 or [Visual Studio Code](https://code.visualstudio.com/download).

- Visual Studio 2019. Create a new project, and search on "Console". From the results pick Console App (.NET Core)
- Visual Studio Code. [See these instructions](https://docs.microsoft.com/en-us/dotnet/core/tutorials/with-visual-studio-code) to create and debug an console application.

I will use Visual Studio for the sake of consistency.

## Hello World Revisited
When you create a new Console app, name the project `HelloWorld` and check the source file `Program.cs` is generated as follows:

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

One of the disadvantages to this approach is that you can't get Hello World on the screen without creating a class and a static method. There is another way however. Microsoft recently released access to [Try.net](https://dotnet.microsoft.com/platform/try-dotnet) where you can create a console in the browser.

Let's examine this code more closely however and pick out some key discussion points (maybe to finish later).

- The entry point for a C# application is `Main`. However, it has the word `static` on the front. Static classess will be covered below, but it's important to note.
- `Console.WriteLine` invokes a method, but what is Console? There is no instance variable called Console created, so how come it exists? Again, the clue is in the word `static`. To understand this, we need to know what me mean by a _class_ and an _instance of a class_.
- `namespace HelloWorld` appears around the code. Why is this here?
- `using System` again, it's there, so good to question what it means

## Create and Instantiating a class
Let's wind back and go over a topic you are likely to have covered already - writing a class and instantiating one (with `new`).

**Task:** Create a new class `RoadVehicle`

- Right click the HelloWorld project (as opposed to the Solution), and choose Add->New Item
- Ensure Class is selected
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

Let's add a *constructor* method to the `RoadVehicle` class. This is the method that runs when we create an *instance* of this class.

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

You have probably used the `new` keyword before, but do you know _why_ you use it? It's all about how the computer manages memory.
We have actually added two objects to memory:

- The `RoadVehicle` class itself (yes, the class can be thought of as a singleton object in its own right). There is only ever **one copy** of the class object in memory, and it is created on first reference.
- An _instance_ of the class. You can have multiple instances of the class, but they are not the same as the class itself. We will see the distinction as we progress.

### Member Variables vs Static Members
There are two types of data storage associated with a class

- **instance member** variable, where each instance has it's own copy
- **static member** variable, where the class object has a single unique copy

Update RoadVehicle.cs to read as follows:

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

We can identify five additional objects now existing in memory: the two instances of `RoadVehicle`, two local variables `v1` and `v2`, and the class object for `RoadVehicle`. These all reside in different areas of memory within the application.

A simplified representation of an application memory is shown below.

![Application Memory](img/ApplicationMemory.png)

- **Program Code** is where executable instructions for each method are stored. These are held in a distinctly different area to data (variables). It is typically _read-only_ and executable.
- **Stack Memory** holds data such as function parameters and local variables. This region grows downwards (in terms of memory address) as you enter a method and contracts back upwards when you leave.
- **Heap Memory** is allocated on request at run-time, typically using the _new_ keyword. When you use _new_, the heap manager attempts to locate a space on the heap that is large enough for the type of object being alocated and stored. In the (hopefully unlikely) event that it cannot find such a block of memory, a _null_ is returned. Heap memory can be enourmous as it can often utilise page files (also known as swap files) on disks if there is in sufficient RAM. The top of the heap typically grows upwards (in terms of memory address) as more objects are allocated and down as objects are deallocated. 
- There are different regions of the heap. One is the _high frequency heap_, within which static variables are stored and persist.

Back to the code, we note the following:

- Each instance of `RoadVehicle` has it's own _independent_ copy of `EngineSerialNumber`. The keyword `new` requests that a portion of the _heap memory_ is allocted and reserved to hold all the member variables for each _instance_ of the class `Roadvehicle`. _new_ returns either a reference to the allocated memory (on sucess) or null (if it fails).
- We did not use `new` to access the static member `ProjectVersion`. As soon as there is a reference to the `RoadVehicle` class (via accessing a static or new), a single class object is created in memory. We can view this as a _singleton_ object where only one copy can ever be created. Therefore there is only ever one copy of the `ProjectVersion` string in memory. As it is public, it is global to the whole application.

Note also that we have two local variables, `v1` and `v2`. These are declared locally within the `Main` function, and are assigned to the reference value return by `new`. Under the hood, `new` returns a reference to some allocated block of memory on the heap (or null if it fails). We therefore say these are _reference types_ (under the hood, reference types related to an actual address in memory). This is in contrast to _value types_ (such as `int` or `bool`). 

Both `v1` and `v2` _only exist within the Main function_. The variables themselves are stored in another area of memory known as the *function stack*. This transient region of memory holds data such as function parameters and local variables. 

> `v1` and `v2` are local variables, so only exist within the context of the `Main` function

So for the following code:
```C#
            RoadVehicle v1 = new RoadVehicle();
            v1.EngineSerialNumber = 12345;

            RoadVehicle v2 = new RoadVehicle();
            v2.EngineSerialNumber = 2468;
```

We can visualise this as follows:

![v1 and v2 are locals](img/local_reference_to_heap.png)

Once execution leaves `Main`, all stack-based objects are automatically deleted,  including `v1` and `v2`. So what happens to the two instances of `RoadVehicle` stored in the heap memory?

> Any object dynamically allocated on the heap with `new` will _persist as long as there is at least one reference to it_. If that reference is removed, the heap object will be automatically deallocated, thus freeing up the memory for other purposes. This process is automated by the .NET [_garbage collector_](https://docs.microsoft.com/en-us/dotnet/standard/garbage-collection/fundamentals)

The great news is we rarely have to worry about deallocating memory as it's done for us.

### When names collide - namespace to the rescue!
A class name needs to be unique. However, as projects become large and complex, probably involving code written by others (in libraries or additional source), there chance of a name collision is very real. Therefore, we enclose our class declatations within a namespace. In this case, we use the namespace HelloWorld.

To illustrate this, do the following:

- In RoadVehicle.cs, change the namespace to DepartmentOfTransport

```C#
namespace DepartmentOfTransport
```

Back in `Program.cs`, you should be able to see there are errors indicating the type `RoadVehicle` cannot be found. This is because no such type (a class is a custom type) exists.

One option is to give it it's fully qualified name:

```C#
            DepartmentOfTransport.RoadVehicle v1 = new DepartmentOfTransport.RoadVehicle();
            DepartmentOfTransport.RoadVehicle v2 = new DepartmentOfTransport.RoadVehicle();
```            

This is very verbose and not so readable, but as you can see, `DepartmentOfTransport.RoadVehicle` is also unlikely to cause a name collision (if it does, choose another namespace).

Subject to there being no other class called RoadVehicle in the project, we can skip the namespace prefix by writing the following line at the top of our source code:

```C#
using DepartmentOfTransport;
```

The compiler now knows to search all the namespaces being used for type names. This is equally true for the `Console` class object.

You can try this if you like. 
- Comment out the line `using System`
- Note the error - to fix, replace `Console` with 'System.Console' 

## Properties
Properties are often related to variables, but use a function to provide controlled access, Let's extend the example above to add some properties to the `RoadVehicle` class.

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
            return string.Format("Road Vehicle. Wheels: {0:d}, Capacity: {1:d} people", _numberOfWheels, _carriageCapacity);
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

- The constructor now has parameters with default values.
- Member variables are now `private` so inaccessible from outside the class. The only way to set them is via the constructor. This makes sense for most vehicles (not many change the number of wheels!).

Now change Main to the following:

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

As all this does is return a single variable, this can be shortened to:

```C#
        public int EngineSerialNumber => _engineSerialNumber;
```

In `Main`, you can now write (if you wish):

```C#
Console.WriteLine("Serial {0:d}", v1.EngineSerialNumber);
```

This has provided read access via the public property `EngineSerialNumber` which may be a good thing. No setter was written, so you cannot write the following by accident:

```C#
v1.EngineSerialNumber = 12345;
```

Providing limited access is only one benefit. If you wanted to read the serial number as a string we could have equally written

```C#
        public string EngineSerialNumber
        {
            get
            {
                return _engineSerialNumber.ToString();
            }
        }
```
which mashalls an integer to a string representation.  

Sticking with the integer version, we can go a stage further and remove the explicit instance variable and use an auto property:

``` C#
    class RoadVehicle
    {
        public static string ProjectVersion = "1.0"; // static member variable
        private int _numberOfWheels;
        private int _carriageCapacity;

        public int EngineSerialNumber { get; }

        public string Description()
        {
            return string.Format("Road Vehicle. Wheels: {0:d}, Capacity: {1:d} people", _numberOfWheels, _carriageCapacity);
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

Observe the line `public int EngineSerialNumber { get; }` which creates a read-only **property**. 

The Description function is actually playing the same role as a property, so we can change that also:

```C#
public string Description => string.Format("Road Vehicle. Wheels: {0:d}, Capacity: {1:d} people", NumberOfWheels, CarriageCapacity);
```

Now we access it from outside (e.g. in `Main`) without method parenthesis

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

- In the constructor how the parameter names were the same as the property names. This is resolved using `this`, which is short hane for _this instance_.
- Although no setter was created, the constructor can (uniquely) write even through no setter accessor was provided.
- You can initialise an auto property. For example, `public int NumberOfWheels { get; } = 4;`

Sometimes it makes sense to back a property with an instance variable. Consider the `Description` property:

```C#
public string Description => string.Format("Road Vehicle. Wheels: {0:d}, Capacity: {1:d} people", NumberOfWheels, CarriageCapacity);
```

There are times when auto properties are not desirable and we still make good use of backing instance variables. Consider `Description`. Everytime this is access, the `FormatString` method is called. This is despite it never changing (it depends on `NumberOfWheels` and `CarriageCapacity`, both of which are fixed once initialised).

```C#
        private string _description;

        public string Description
        {
            get
            {
                if (_description == null)
                {
                    _description = string.Format("Road Vehicle. Wheels: {0:d}, Capacity: {1:d} people", NumberOfWheels, CarriageCapacity);
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
- In the new class file, change the namespace to `DepartmentOfTransport`
- In **both** `RoadVehicle.cs` and `RoadVehicleProperties.cs`, Change the class declaration to `partial class RoadVehicle` 

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
                    _description = string.Format("Road Vehicle. Wheels: {0:d}, Capacity: {1:d} people", NumberOfWheels, CarriageCapacity);
                }
                return _description;
            }
        }
    }
}
```

It's fairly self explainatory, but you might not have known this was possible. As we will learn, this is useful in Xamarin.Forms as it helps split developer edited code from computer generated code.

> All the code from this section [can be found in the folder properties](/code/Chapter1/essential-c-sharp-part1/properties/HelloWorld/)

## Inheritance
Class inheritance is something that is used throughout Xamarin.Forms, so it's worth a recap on some important points using the example developed so far.

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

Note how this class inherits from RoadVehicle

```C#
class Car : RoadVehicle
```

Note the following:

- All properties and methods will be inherited
- Only non-private properties of the parent class (`RoadVehicle`) will be accessible in the child class (`Car`)
- You can only  inherit a single class (but implement many interfaces - covered later)
- Note how the constructor first calls the constructor of the parent class before executing it's own. The default would be to call the parameterless constructor

in the child class, you typically do one or both of the following:

- add new functionality (additional properties and methods)
- _override_ functionality

Let's look at examples of both.

### Adding functionality
We've added an extra parameter, the option to attach a tow bar (for connecting and towing trailors). This is something that can change through the lifetime of a car, so this is a propery that can be both written and read.

This property was made public, so it can be changed from outside. We can do this in Main. For example

```C#
    Car PrimaryCar = new Car(EngineSerialNumber: 13579);
    PrimaryCar.HasTowBar = true;
```

### Overriding functionality
In `RoadVehicle` there was a property `Description`. It is public, and therefore is also a public property of Car. We can demonstrate this by adding the following code to `Main`

```C#
    Car PrimaryCar = new Car(EngineSerialNumber: 13579);
    PrimaryCar.HasTowBar = true;
    Console.WriteLine(PrimaryCar.Description);
```            

However, the output seems incomplete. It would be better if there was some indication that this was also a car and it has a towbar attached. We can do this by _overriding_ the property:

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

In `RoadVehicleProperties.cs`, add the keyword `virtual`

```C#
    public virtual string Description
```

Run the code again and you should see additional information because it is a `Car`.

> The final code can be found in [the inheritence folder](/code/Chapter1/essential-c-sharp-part1/inheritance/HelloWorld)

## Polymorphism and virtual methods
Polymorphism is a big word which can result in new developers running for the hills in fear, when in fact it's realtively simple. Consider the example so far:

First let's add a new type, that is `Motorbike`. 

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

