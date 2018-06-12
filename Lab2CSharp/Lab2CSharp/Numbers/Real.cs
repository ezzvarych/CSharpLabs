using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2CSharp.Numbers
{
    class Real : Number
    {
        
        public Real(object num)
        {
            if (num is double || num is int)
            {
                this.Num = num;
            }
            else throw new ArgumentException("You must input real number");
        }

        public override Number Add(dynamic x)
        {
            if (x is Number)
            {
                this.Num += x.Num;
            }
            else if (x is int || x is double)
            {
                this.Num += x;
            }
            else throw new InvalidCastException("No match with needed type of arguments");

            return this;
        }

        public override Number Substract(dynamic x)
        {
            if (x is Number)
            {
                this.Num -= x.Num;
            }
            else if (x is int || x is double)
            {
                this.Num -= x;
            }
            else throw new InvalidCastException("No match with needed type of arguments");

            return this;
        }

        public override Number Mult(dynamic x)
        {
            if (x is Number)
            {
                this.Num *= x.Num;
            }
            else if (x is int || x is double)
            {
                this.Num *= x;
            }
            else throw new InvalidCastException("No match with needed type of arguments");

            return this;
        }

        public override Number Divide(dynamic x)
        {
            try
            {
                if (x is Number)
                {
                    this.Num /= x.Num;
                }
                else if (x is int || x is double)
                {
                    this.Num /= x;
                }
                else throw new InvalidCastException("No match with needed type of arguments");
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine(e.Message);
            }
            return this;
        }

        public override object DeepCopy()
        {
            return new Real(this.Num);
        }
    }
}
