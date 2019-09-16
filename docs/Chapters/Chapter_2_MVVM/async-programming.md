[Contents](README.md)

----

[Prev](anonymous-functions.md)

# Asynchronous Programming
I will try and keep this section shorter than the previous one. Again, this section discusses an important part of the C#.NET language, especially if you wish to work with the Xamarin.Forms APIs.

Let's begin with what this seciton is not about - it is not about multithreaded programming, and for two reasons:

1. Multithreaded programming is a whole topic in it's own right and beyond the scope of this course (shame as it's a personal favourite ;)

1. The approch we are about to meet is in many ways an alternative approach to multi-threaded programming.

For those with a background in multi-threaded programming, this might raise some eyebrows. What about blocking hardware? Well, my own reaction was the same, but C# (liek others) shows us that there is another way, and that way (when it can be applied) is much easier and safer.

> The underlying principle of asynchronous programming is that the slow devices (storage, network interfaces, timers) we often wait for are indepednent electronic devices, and so by their very nature, run in parallel to the CPU. The CPU does not need to stop and wait for such devices, it only needs to know when a device is ready.

The issue we are addressing is the asynchronous nature of the comuter systems and human interaction. 

Consider the user interface for a mobile devices and it's touch screen. It responds to taps or other gestures whenever the human operator decides. It does not know when a user is going to do that (humans input is the epitomy of asynchronous input!) 

- When a user touches a screen, this is detected by the operating system, and via mechanisms we don't see, it generates an _event_. 
    - An event is a record of something that happened together with some registered consequence (such as a method call, also known as an _event handler_)
- Upon receipt, the event is added to a queue (known as an event queue of all things!) by the operting system
- Events are pulled off the queue in turn and their respecitve methods calls (event handlers) are called.
- Events are perform sequentially, _they should be short lived_ and usualy performed in the order they are recieved.
    - Event handlers that are not short lived need to be broken down in to additinal events as we shall see
- When there are no events in the event queue, the operating system can put the application into a low-power state (saving CPU cycles and hence battery). 
    - Another touch event will "wake" the application again (again, handled by the operating system, so not your problem!)


Much of the above is automatic.


[Next - Loose Coupling with Interfaces](loose-coupling.md)

----

[Contents](/docs/README.md)
