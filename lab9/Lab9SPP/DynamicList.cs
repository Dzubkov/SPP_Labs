using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9SPP
{
    public class DynamicList<T> : IListOptions<T>, IEnumerable<T>
    {
        private const int PRIMARY_SIZE = 10;
        private const int MINIMUM = 4;
        public int Count;
        private object[] array;
        public DynamicList()
        {
            Count = 0;
            array = new object[PRIMARY_SIZE];
        }
        public T this[int index]
        {
            get
            {
                if (index < Count || index >= 0)
                    return (T)array[index];
                else
                    throw new IndexOutOfRangeException($"Index is out of range of array: index = {index}, size = {Count}");
                
            }
        }
        public void Add(T elem)
        {
            if (Count == array.Length - 1)
                Resize(array.Length * 2);
            array[Count++] = elem;
        }

        public void Clear()
        {
            for (int i = 0; i < Count; i++)
                array[i] = null;
            Count = 0;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new ListEnum<T>(this);
        }

        public T Remove()
        {
            object remove_obj = array[Count - 1];
            array[Count - 1] = null;
            Count--;
            return (T) remove_obj;
        }

        public void RemoveAt(int index)
        {
            for(int i = index; i<Count-1; i++)
                array[i] = array[i + 1];
            array[Count - 1] = null;
            Count--;
            if (array.Length / MINIMUM > Count)
                Resize(array.Length / 2);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        private void Resize(int newSize)
        {
            object[] newArray = new object[newSize];
            array.CopyTo(newArray, 0);
            array = newArray;
        }
    }
}
