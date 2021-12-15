using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9SPP
{
    public interface IListOptions<T>
    {
        public void Add(T elem);
        public T Remove();
        public void RemoveAt(int index);
        public void Clear();
    }
}
