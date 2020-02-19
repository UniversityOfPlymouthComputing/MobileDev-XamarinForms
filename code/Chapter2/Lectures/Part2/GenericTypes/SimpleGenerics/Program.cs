using System;

namespace SimpleGenerics
{
    class Program
    {
        public class MyData<T> 
        {
            private T[] array;
            public MyData(int size, Func<int,T> f) 
            {
                array = new T[size];
                for (int n=0; n<size; n++) {
                    array[n] = f(n);
                }
            }
            public void ListToConsole()
            {
                foreach (T element in array)
                {
                    Console.WriteLine(element);
                }
            }
        }
        static void Main(string[] args)
        {
            MyData<int> d1 = new MyData<int>(10, (int index) => index);
            d1.ListToConsole();
            MyData<double> d2 = new MyData<double>(size: 5, (int index) => ((double)index)/5.0);
            d2.ListToConsole();
        }
    }
}
