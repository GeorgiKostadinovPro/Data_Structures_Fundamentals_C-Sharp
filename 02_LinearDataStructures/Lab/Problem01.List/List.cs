namespace Problem01.List
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class List<T> : IAbstractList<T>
    {
        private const int DEFAULT_CAPACITY = 4;
        private T[] items;

        public List()
            : this(DEFAULT_CAPACITY) {
        }

        public List(int capacity)
        {
            if (capacity <= 0)
            { 
                throw new ArgumentOutOfRangeException(nameof(capacity));
            }

            this.items = new T[capacity];
        }

        public T this[int index]
        {
            get
            {
                this.ValidateIndex(index);

                return this.items[index];
            }
            set
            {
                this.ValidateIndex(index);

                this.items[index] = value;
            }
        }

        public int Count { get; private set; }

        public void Add(T item)
        {
            if (this.IsArrayForExpanding())
            {
                this.ExpandArray();
            }

            this.items[this.Count] = item;
            this.Count++;
        }

        public bool Contains(T item)
        {
            return this.IndexOf(item) != -1 ? true : false;
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (item.Equals(this.items[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            this.ValidateIndex(index);

            if (this.IsArrayForExpanding())
            {
                this.ExpandArray();
            }

            for (int i = this.Count; i > index; i--)
            {
                this.items[i] = this.items[i - 1];
            }

            this.items[index] = item;
            this.Count++;
        }

        public bool Remove(T item)
        {
            int index = this.IndexOf(item);

            if (index == -1)
            {
                return false;
            }

            this.RemoveAt(index);

            return true;
        }

        public void RemoveAt(int index)
        {
            this.ValidateIndex(index);

            for (int i = index; i < this.Count - 1; i++)
            {
                this.items[i] = this.items[i + 1];
            }

            this.items[this.Count - 1] = default(T);
            this.Count--;

            if (this.IsArrayForShrinking())
            {
                this.ShrinkArray();
            }
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
            {
                yield return this.items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void ValidateIndex(int index)
        {
            if (index < 0 || index >= this.Count)
            {
                throw new IndexOutOfRangeException("Index is outside the bounds of the array!");
            }
        }

        private bool IsArrayForExpanding()
        {
            return this.Count == this.items.Length;
        }

        private bool IsArrayForShrinking()
        {
            return this.Count <= this.items.Length / 2;
        }

        private void ExpandArray()
        {
            T[] itemsCopy = new T[this.items.Length * 2];

            for (int i = 0; i < this.items.Length; i++)
            {
                itemsCopy[i] = this.items[i];
            }

            this.items = itemsCopy;
        }

        private void ShrinkArray()
        {
            T[] itemsCopy = new T[this.items.Length / 2];

            for (int i = 0; i < this.Count; i++)
            {
                itemsCopy[i] = this.items[i];
            }

            this.items = itemsCopy;
        }
    }
}