using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Lab1
{
    public class DynamicList<T> : IEnumerable<T>
    {
        private T[] _array;

        public T[] Items => _array;

        public int Count => Items.Length;

        public DynamicList()
        {
            _array = new T[0];
        }

        public T this[int index]
        {
            set => _array[index] = value;

            get => _array[index];
        }

        public void Add(T element)
        {
            Array.Resize(ref _array, _array.Length + 1);
            _array[_array.Length - 1] = element;
        }

        public void Remove(T element)
        {
            _array = _array.Where(x => !x.Equals(element)).ToArray();
        }

        public void RemoveAt(int removeIndex)
        {
            _array = _array.Where((source, index) => index != removeIndex).ToArray();
        }

        public void Clear()
        {
            _array = new T[0];
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var obj in _array)
            {
                yield return obj;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
