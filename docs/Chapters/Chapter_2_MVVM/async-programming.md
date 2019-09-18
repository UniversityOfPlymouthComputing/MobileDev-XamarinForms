[Contents](README.md)

----

[Prev](anonymous-functions.md)

# Asynchronous Programming
I will try and keep this section shorter than the previous one. Again, this section discusses an important part of the C#.NET language, especially if you wish to work with the Xamarin.Forms APIs.

Let's begin with what this seciton is not about, and that is multithreaded programming, and for two reasons:

1. Multithreaded programming is a whole topic in it's own right and beyond the scope of this course (shame as it's a personal favourite ;)

1. For practical purposes, the approch we are about to meet is in many ways an alternative approach to multi-threaded programming.

For those with a background in multi-threaded programming, this might raise some eyebrows. What about blocking hardware? Well, my own reaction was the same, but C# (like some others) shows us that there is another way, and that way (when it can be applied) is much easier and safer to program.

> The underlying principle of asynchronous programming is that the slow devices (storage, network interfaces, timers) we often wait for are indepednent electronic devices, and so by their very nature, run in parallel to the CPU. The CPU does not need to stop and wait for such devices, it only needs to know when a device is ready.

The issue we are addressing is the asynchronous nature of the comuter systems and human interaction. 

Consider the user interface of a mobile device and it's touch screen. It responds to taps or other gestures _whenever the human operator feels inclined_. The comuter does not know when a user is going to tap the screen (human input is the epitomy of asynchronous input!). So modern devices are reactive, or _event driven_.

- When a user touches a screen, this is detected by the operating system (via hardware mechanisms we don't concern ourselves with), and this in turn it generates an _event_. 
    - An event is a record of something that happened together with some registered consequence (such as a method call, also known as an _event handler_)
- Upon receipt, the event is added to a queue (the event queue) by the operting system
- Events are pulled off the queue in turn and their respecitve methods (event handlers) are called.
- Events are perform sequentially and allowed to complete, _they should be short lived_ and usualy performed in the order they are recieved.
    - Event handlers that are not short lived need to be broken down in to additinal events 
    - As we shall see, this is super easy in C#
- When there are no events in the event queue, the operating system can put the application into a waiting state (saving CPU cycles and hence battery). 
    - Another touch event will "wake" the application again (again, handled by the operating system, so not your problem!)


Almost all of the above turns out to be automatic. Our task is to register and write event handlers and ensure they don't block the CPU for any significant amount of time (we are talking milliseconds here). _Anything that blocks for too will render the user interface unresponsive and could result in our application being kicked by the operating system._

 > Ah ha! (I hear some of you cry!). What if I'm uploading data to a server? That can take seconds! (Don't you just love catching out tutors?)

 Yes indeed, thre are many things that can take seconds, some more obvious than others, including:

 - network transactions (Wifi or Bluetooth)
 - Saving data to internal storage 
 - Animations
 - Waiting on a timer
 - ...More

So how do we perfom any of the above in an event handler? The answer lies in turning a single event handler into a multiple event handler, and for that, we use `await` and `async`

## `await` and `async` in action
As always, I like to use an example, and one that is relevent and simple. We will definately be communicating with a network in following sections, so let's go with that.







[Next - Loose Coupling with Interfaces](loose-coupling.md)

----

[Contents](/docs/README.md)
