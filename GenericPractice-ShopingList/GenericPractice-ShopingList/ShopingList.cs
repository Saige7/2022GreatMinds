using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericPractice_ShopingList
{
    internal class ShopingList<T>
    {
        public List<T> shopingList = new List<T>();

        public void Add(T item)
        {
            shopingList.Add(item);
        }

        public void Remove(T item)
        {
            shopingList.Remove(item);
        }

        public void View()
        {
            for(int i = 0; i < shopingList.Count; i++)
            {
                Console.WriteLine(shopingList[i]);
            }
        }
    }
}
