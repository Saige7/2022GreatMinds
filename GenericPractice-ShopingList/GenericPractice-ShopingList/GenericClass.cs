using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericPractice_ShopingList
{
    internal class GenericClass<T>
    {
        public T test;

        public GenericClass()
        {

        }

        public void DisplayItem(T val)
        {
            Console.WriteLine(val);
        }
    }
}
