[Prev - MVVM Navigation](mvvm_navigation_1.md)

---

## Simple Persistance
In the previous examples, we saw how we can navigate between pages, passing data forward and back and we traverse the hierarchy. When the debug session was closed (or the app was closed), all edits were lost.

> What is lacking is any form of data persistance. When you restart your device, all ram based data is lost. Only data saved to local flash storage or network storage can be persisted.

We will come to network storage much later. For now, let's focus on local storage which is all ultimately backed by flash memory on the device. There are a number of options here. One of the simplest is to write your data to a file. As data is usually structured, we need to find a way to also preserve that structure. For that, we need a _file format_. Popular file formats include XML and JSON. JSON is more compact, but as XML formatting is intrinsic to .NET and Xamarin.Forms, we will use it here. Furthermore, it does not require any third-party libraries. It is also very simple to implement, and we like simple :)

## Serialising Data to an XML file
Open the solution in the folder [MVVM_Navigation-2](/code/Chapter3/NavigationControllers/2-MVVM_Based/MVVM_Navigation-2-persist)

* Start this code, edit both the year and Name.
* Stop the debug session
* Restart the debug session and check the data values are what they were before

The main change in this code are the `Save` and `Load` methods in `PersonDetailsModel.cs`. Let's look at the save first:

```C#
    public void Save(string fn)
    {
        this.Filename = fn;
        using (var writer = new System.IO.StreamWriter(fn))
        {
            var serializer = new XmlSerializer(this.GetType());
            serializer.Serialize(writer, this);
            writer.Flush();
        }
    }
```

The key to this is `System.Xml.Serialization.XmlSerializer`.

> XML serialization is the process of converting an object's public properties and fields to a serial format (in this case, XML) for storage or transport. Deserialization re-creates the object in its original state from the XML output.
> 
> [Microsoft documentation on XmlSerializer](https://docs.microsoft.com/dotnet/api/system.xml.serialization.xmlserializer?view=netframework-4.8)

Here is a sample of what the XML output looks like:

```XML
<?xml version="1.0" encoding="utf-8"?>
<PersonDetailsModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Filename>C:\Users\noutram\AppData\Local\Temp\userdetails.xml</Filename>
  <Name>Anon</Name>
  <BirthYear>1970</BirthYear>
</PersonDetailsModel>
```

> Note the filepath in the <Filename> elemenent will be different on each machine and platform. This was derived from a unit test on my own machine.

In simple terms, the binary data in the model class is converted to a text format that is both human and machine intelligable stream of XML (text). This process is known as **serialisation**. The reverse, deserialisation, is to read and parse the XML text file, and recreate the original binary object in memory.

The good news is this is mostly automatic and done for you. If you read the documentation, you can be much more prescriptive about what is serialised to XML and what is not.

The destination for this text is a file with full path `fn` (pass by parameter). `System.IO.StreamWriter` is used to open/create and write files. 
Again, [see the documentation for more details](https://docs.microsoft.com/dotnet/api/system.io.streamwriter?view=netframework-4.8)

Note the use of `using`. This ensures the memory alloacted by `System.IO.StreamWriter` to manage file handles is released once the file is written, even if an exception occurs.

[See the documentation for more details](https://docs.microsoft.com/dotnet/standard/garbage-collection/using-objects)

## Deserialising Data from an XML file
To instantiate an object from a serialised file, the process is fairly straightforward:

```C#
    //Deserialise an XML file to a new instance of this type
    public static PersonDetailsModel Load(string fn)
    {
        try
        {
            using (FileStream stream = File.OpenRead(fn))
            {
                var serializer = new XmlSerializer(typeof(PersonDetailsModel));
                PersonDetailsModel m = serializer.Deserialize(stream) as PersonDetailsModel;
                return m;
            }
        }
        catch (Exception)
        {
            return null;
        }
    }
```

The key line is this one:

```C#
    PersonDetailsModel m = serializer.Deserialize(stream) as PersonDetailsModel;
```

> The `Deserialize` method actually instantiates an object in memory using the infomormation in the XML file.

## Instantiating the Model
If we now look back at the entry point of our Xamarin.Forms application in the `App` class constructor, we see this the model being instantiated using deserialisation (if the file exists):

```C#
    public App()
    {
        ...
        //Try and load from persistent storage
        string mainDir = FileSystem.AppDataDirectory;
        string path = System.IO.Path.Combine(mainDir, "userdetails.xml");
        var m = PersonDetailsModel.Load(path);
        if (m == null)
        {
            //No such file, then create a new model with defaults and save
            m = new PersonDetailsModel("Anon");
            m.Save(path);
        }
        ...
    }
```        

## Calling the `Save` method
We must be careful to not call `Save` too often as writing to flash memory is relatively expensive. 

* When we edit the name, is is sensible to persist the data each time it changes.
* When we move the slider to edit the year, the slider value is may be updated hundres times as the slider moves. 

Luckily this is easily resolved as the slider also supports a command for when the slider stops moving. We see this in the XAML for the `YearEditPage`:

```XML
    <Slider 
            x:Name="YearSlider"
            Maximum="2100"
            Minimum="1900"
            Value="{Binding BirthYear}"
            DragCompletedCommand="{Binding BirthYearSliderCommand}"
            MinimumTrackColor="Blue"
            MaximumTrackColor="Red"
    />
```            

In the ViewModel, we see the command being set up in the constructor:

```C#
    public YearEditPageViewModel(PersonDetailsModel model = null)
    {
        ...

        //Command property - save the model only when the user stops moving the slider
        BirthYearSliderCommand = new Command(execute: () => Model.Save());
    }
```        

## Other approaches
This has only begun to look at persistence. 

---

[Next - Factoring Common Code into Parent Classes]()
