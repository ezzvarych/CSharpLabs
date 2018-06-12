using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab2CSharp.Numbers;

namespace Lab2CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Integer a = new Integer(10);
            Integer b = new Integer(5);
            b = (Integer)b.Mult(2);
            Console.WriteLine("a.Num = " + a.Num);
            Console.WriteLine("b.Num = " + b.Num);
            Real c = new Real(8);
            Real d = new Real(4);
            c = (Real)c.Divide(0);
            Console.WriteLine("c.Num = " + c.Num);
            Console.WriteLine("a.Equals(b) == " + a.Equals(b) + ", because a.Num == b.Num - " + (a.Num == b.Num));
            Console.WriteLine("a.GetHashCode() = " + a.GetHashCode() + ", b.GetHashCode() = " + b.GetHashCode());
            Console.WriteLine("a.ToString(): " + a.ToString());
            Series ser = new Series();
            ser.EventAdd += ShowMessage;
            ser.EventSet += ShowMessage;
            ser.EventInsert += ShowMessage;
            ser.EventDelete += ShowMessage;
            ser.Add(a);
            ser.Add(b);
            ser.Add(c);
            ser.Add(d);
            ser.Set(1, new Real(6.2));
            ser.Insert(2, new Integer(3));
           // ser.Delete(1);
            Console.WriteLine(ser.ToString());
            Console.ReadKey();
        }

        static void ShowMessage(CollectionEventsArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
