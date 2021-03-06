﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericApp
{
    public class SimpleGeneric<T>
    {
        private T[] values;
        private int index;

        public SimpleGeneric(int len)
        {
            values = new T[len];
            index = 0;
        }

        public void Add(params T[] args)
        {
            foreach (T item in args)
                values[index++] = item;
        }
        public void Print()
        {
            foreach (T item in values)
                Console.Write(item + ", ");
            Console.WriteLine();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            SimpleGeneric<Int32> gIntegers = new SimpleGeneric<Int32>(10); //Int32랑 Int랑 같음
            SimpleGeneric<double> gDoubles = new SimpleGeneric<double>(10);

            gIntegers.Add(1, 2);
            gIntegers.Add(1, 2, 3, 4, 5, 6, 7);
            gIntegers.Add(10);

            gDoubles.Add(10.0, 12.4, 37.5);
            gIntegers.Print();  //제네릭 클래스 하나를 가지고 두개를 호출한다.
            gDoubles.Print();   //제네릭 클래스를 안 만들었다면 int, double 클래스 두 개를 만들어서 호출해야 한다.

        }
    }
}
