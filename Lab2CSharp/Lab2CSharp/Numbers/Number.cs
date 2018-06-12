using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2CSharp.Numbers
{
    public abstract class Number
    {
        public dynamic Num { get; set; }

        public abstract Number Add(object x);
        public abstract Number Substract(object x);
        public abstract Number Mult(object x);
        public abstract Number Divide(object x);

        public override bool Equals(object obj)
        {
            return this.ToString() == obj.ToString();
        }

        public static bool operator ==(Number num1, Number num2) 
        {
            return num1.ToString() == num2.ToString();
        }

        public static bool operator !=(Number num1, Number num2)
        {
            return num1.ToString() != num2.ToString();
        }

        public override int GetHashCode()
        {
            return this.Num.GetHashCode() * 10 / 17 + 45 + this.Num.GetHashCode() * 224;
        }

        public abstract object DeepCopy();

        public override string ToString()
        {
            return "num = " + this.Num + " type = " + this.GetType();
        }
        
    }
}
