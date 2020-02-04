using System;
using System.Collections.Generic;
using System.Text;

namespace PartialClass
{
class Student : BaseClass
{
    public int StudentNumber { get; protected set; }
    public Student(string name, int number) {
        Name = name;
        StudentNumber = number;
        Console.WriteLine($"Student Constructor: Name={name} SN={number}");
    }
        
    //TASK - CHANGE new TO override
    public new void Hello() {
        Console.WriteLine("Hello Student");
    }
}
}
