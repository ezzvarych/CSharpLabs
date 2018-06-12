using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab2CSharp.Numbers;

namespace Lab2CSharp
{
    class Series
    {
        List<Number> lst;

        public delegate void CollectionStateHandler(CollectionEventsArgs e);

        public event CollectionStateHandler EventAdd;
        public event CollectionStateHandler EventInsert;
        public event CollectionStateHandler EventDelete;
        public event CollectionStateHandler EventSet;

        public Series()
        {
            lst = new List<Number>();
        }

        public void Add(Number numb)
        {
            lst.Add(numb);
            if (EventAdd != null)
            {
                EventAdd(new CollectionEventsArgs("Element was added to collection"));
            }
        }

        public void Set(int index, Number numb)
        {
            try
            {
                lst[index] = numb;
                if (EventSet != null)
                {
                    EventSet(new CollectionEventsArgs(String.Format("You set new value for element with index {0}", index)));
                }
            }
            catch (ArgumentOutOfRangeException e) 
            {
                Console.WriteLine("Catched exception: " + e.Message);
            }
        }

        public void Insert(int index, Number numb)
        {
            try
            {
                lst.Insert(index, numb);
                if (EventInsert != null)
                {
                    EventInsert(new CollectionEventsArgs(String.Format("You insert new value in index {0}", index)));
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("Catched exception: " + e.Message);
            }
        }

        public Number Delete(int index)
        {
            Number numb = new Integer(0);
            try
            {
                numb = lst[index];
                lst.RemoveAt(index);
                if (EventDelete != null)
                {
                    EventDelete(new CollectionEventsArgs(String.Format("You delete value in index {0}", index)));
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("Catched exception: " + e.Message);
            }
            return numb;
        }

        public override string ToString()
        {
            string str = "\nCollection:\n";
            foreach (Number elem in lst)
            {
                str += "Type: " + elem.GetType() + ", value: " + elem.Num + "\n";
            }
            str += "\n";
            return str;
        }
    }

    class CollectionEventsArgs 
    {
        public string Message { get; set; }

        public CollectionEventsArgs(string message) 
        {
            Message = message;
        }
    }
}
