[Prev - MVVM Navigation](simple-persistance.md)

---

## Factoring out Common Code
We now have a simple but functioning application that at least goes some way to embrace the MVVM pattern. 

We have separation of Model, ViewModel and View objects. 
* The ViewModel does not make reference to it's presenting view, but it does have references to the next view (for navigation purposes). 
* The Model does not make reference to any ViewModel, altjhough extra code had to be added to allow any ViewModel to observe changes.

> What may be apparent is that quite a lot of extra effort had to be made to adopt the MVVM pattern, and even then, it's still not perfect (in the eyes of a purist).

One thing we can do is try to factor out some of the replicated code into parent classes. This includes both Model and ViewModel code. This has a number of benefits:

* Keeps the overall code shorter
* Bugs in common code can be fixed in one place
* It can help prevent us forgetting important details, such as implementing specific interfaces

For example: I often forget how to write `OnPropertyChanged` for example, or how to notify a view model with changes in a model. Much of this can be written into parent classes.

> Open the solution in the folder [MVVM_Navigation-2](/code/Chapter3/NavigationControllers/2-MVVM_Based/MVVM_Navigation-3-baseclass)
>
> Familiarise yourself with the contents of the MVVM folder.

If you look in the MVVM folder within the Xamarin.Forms project, you will see two files:

* `BindableModelBase` - common baseclass for Models used in MVVM based solutions
* 'ViewModelBase` - two baseclasses for ViewModels. One has a model, the other does not. 

I am sure more could be done (if fact I know others who take the idea further), but again, I've kept things simple.

### Model Code
Quite a significant proportion of the model code used is the previous section can be factored out and reused. This includes:

* Basic Serialisation of all public properties
* Basic Deserialisation 
* Implementation of `INotifyPropertyChanged`

#### Baseclass `BindableModelBase`
Let's look at some key elements in the :

The interface `INotifyPropertyChanged` is implemented by adding the property `PropertyChanged`. Therefore, any model object that inherits this class will automtically implement this interface and all it's requirements. 

```C#
public class BindableModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        ...
    }
```

If we wish to instantiate a model using deserialization, we can do this using a static convenience method:

```C#
    public static ModelType Load<ModelType>(string fn) where ModelType : BindableModelBase
    {
        try
        {
            using (FileStream stream = File.OpenRead(fn))
            {
                var serializer = new XmlSerializer(typeof(ModelType));
                ModelType m = serializer.Deserialize(stream) as ModelType;
                return m;
            }
        }
        catch (Exception)
        {
            return null;
        }
    }
```

Note the use of template types here. `ModelType` is the type of your model that has a constraint that must be a subclass of `BindableModelBase`. In the `App` class, we see this being used:

```C#
    PersonDetailsModel m = BindableModelBase.Load<PersonDetailsModel>(path);
    if (m == null)
    {
        //No such file, then create a new model with defaults and save
        m = new PersonDetailsModel("Anon");
        m.Save(path);
    }
```            

> It is likely that over time, more and more functionality can be added to this parent class. For now this is a reasonable start and at least captures the idea.
>
> If you are one of those people who know C# very well, especially the topic of _reflection_, then I'm sure you can do a much more comprehensive job than has been done here.

#### PersonDetailsModel
This is our actual model code that subclasses `BindableModelBase`. Take a close look at this class and you will notice a lot of code has been moved out.

* All the serialisation and deserialization code
* `OnPropertyChanged()`

All that is left are the properties and the constructor. 

### ViewModel Code
The view model code goes a little further. There are in fact two classes in ViewModelBase.cs

* `ViewModelBase` which has no built in reference to a model (for very simple view models)
* `ViewModelBase<DataModelType>` which as a Model property and associated code to go with it. This class inherits from `ViewModelBase`

#### ViewModelBase class
This is a simple base class that includes many of the core features of a ViewModel

* Implementation of `INotifyPropertyChanged`
* Reference to the `Navigation` object

```C#
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //Useful property to reference the navigation page
        protected INavigation Navigation => Application.Current.MainPage.Navigation;

        //Create events when properties change
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
```   

This class can be used as a stand-alone parent class where no data model reference is used (MVVM without a separate model class). 

#### ViewModelBase<DataModelType> class
If we want to specify a separate model class (my own preference), then this baseclass is used. This inherits from `ViewModelBase` and add the following:

1. Model property, with setter and getter
1. A 'requirement' (abstract method) to implement the `OnModelPropertyChanged` event handler to observe changes in the model
1. Code to ensure changes in the model result in `OnModelPropertyChanged` being called

For the first point, we can't predict the concrete type of the Model, so a constrained generic type is used as follows:

```C#
 public abstract class ViewModelBase<DataModelType> : ViewModelBase where DataModelType : BindableModelBase
```

We do know however that the model must subclass `BindableModelBase`, which in turn means it implements the interface `INotifyPropertyChanged`

In terms of the second point, I personally tend to forget the prototype of this method. I could now look at this base class for two things:

Firstly, the method prototype (in fact, by inheriting this class, the Visual Studio editor will write this for me if I allow it). 

```C#
protected abstract void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e);
```

Secondly, an example implementation is included for me to copy and modify (commented out)

```C#
    protected override void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        //Flag changes to the view-viewmodel binding layer -  very simple pass-through in this example
        if (e.PropertyName.Equals(nameof(Model.BirthYear)))
        {
            OnPropertyChanged(nameof(BirthYear));
        }
        else if (e.PropertyName.Equals(nameof(Model.Name)))
        {
            OnPropertyChanged(nameof(Name));
        }
    }
```

For point 3. we should look at the setter for the `Model` property itself: 

```C#
        public DataModelType Model
        {
            get => model;
            set
            {
                if (model != value)
                {
                    model = value;
                    OnPropertyChanged();
                    if (model != null)
                    {
                        model.PropertyChanged += OnModelPropertyChanged;
                    }
                }
            }
        }
```

Notice how the change event is hooked up for you:

```C#
    model.PropertyChanged += OnModelPropertyChanged;
```

This works becasue the model type is constrained (it must have a `event` type property `PropertyChanged`). This is one of those steps that is otherwise easy to forget!

### ViewModels
Take a look at the ViewModel classes and you should find they are shorter and simpler than before.

#### FirstPageViewModel
The `FirstPageViewModel` only has to display model data, so is particularly simple. It has two properties exposed to the View-ViewModel binding layer which simple map through read-only data:

```C#
    public string Name => Model.Name;
    public int BirthYear => Model.BirthYear;
```        

We are forced by the compiler to implement the abstract method `OnModelPropertyChanged`. This ensures any changes to the model properties are intercepted in the ViewModel. This is one of the common behaviours of MVVM

What you do in response depends on the application of course. In this somewhat contrived example, we simply update the View-ViewModel bindings by calling `OnPropertyChanged`. This forces the binding layer to re-read the properties (which in turn read the model properties).

```C#
    protected override void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        //Flag changes to the view-viewmodel binding layer -  very simple pass-through in this example
        if (e.PropertyName.Equals(nameof(Model.BirthYear)))
        {
            OnPropertyChanged(nameof(BirthYear));
        }
        else if (e.PropertyName.Equals(nameof(Model.Name)))
        {
            OnPropertyChanged(nameof(Name));
        }
    }
```        

In this view, which is simply reading the data for display, this might seems unnecessary. However, it is not.

> **Experiment**: Put a breakpoint inside `OnModelPropertyChanged` and try to find out when it gets called. 
>
> If you comment out the code inside `OnModelPropertyChanged` what happens?

Using this approach, any changes to the model will ensure the View-ViewModel binding layer is updated, irrespective of the origin (this ViewModel, the Model itself or another ViewModel). 

#### YearEditPageViewModel
In the `YearEditPageViewModel` we have two properties exposed to the View-ViewModel binding as before. However, the `BirthYear` property can be modified by the View (a slider).

Again, as this is such a simple application, we pass the changes directly through to the model object in the setter: 

```C#
    public int BirthYear                //Read / Write
    {
        get => Model.BirthYear;
        set
        {
            if (value != Model.BirthYear)
            {
                Model.BirthYear = value;
            }
        }
    }
```        

Note that `OnPropertyChanged()` is not called (we could, but there is no need). This is because it is called in `OnModelPropertyChanged` (which picks up any changes applied to the model).

#### NameEditPageViewModel
Finally there is the `NameEditPageViewModel` which is slightly different from the previous two.

Firstly, it has it's own model data (a simple string property `Name`) and no separate model object. Therefore, it subclasses `ViewModelBase` as opposed to ` ViewModelBase<PersonDetailsModel>`

There is no `OnModelPropertyChanged` method, so we must remember to call `OnPropertyChanged()` ourselves in order to update the View-ViewModel bindings.

```C#
    public string Name
    {
        get => name;
        set
        {
            if (name != value)
            {
                name = value;
                OnPropertyChanged();
                ((Command)ButtonCommand)?.ChangeCanExecute();
            }
        }
    }
```        

## A Reflection on the MVVM Approach Presented Here
The first thing I want to stress here is that the previous sections are only intended to communicate one possible approach to implementing MVVM. This was done with simplicity in mind and is very much the authors "hand-rolled" MVVM implementation. 

Certainly don't take the approach presented here to be an authority, and by all means, "roll your own" solution if you like.

Before anyone emails me with a list of reasons why it's not fit for purpose, please remember the purpose: Educational

* This is aimed at new adopters and fairly inexpeierenced developers
* Many will be updating their OOP and C# skills as we go (so are not going to enjoy advanced concepts such as reflection at this stage)
* To appreciate more sophisticated approaches (written by much more experienced developers), we first need to understand the problems and limitations those approaches elegantly overcome.


Alternatively, you might prefer to invest in learning one of the third-party MVVM frameworks out there. In fact, if you are going to write anything other than experimental code, maybe that is a good idea. If you work for a company, there may even be a policy in place, especially where unit testing of ViewModels is a requirement.

---

[Contents](README.md)