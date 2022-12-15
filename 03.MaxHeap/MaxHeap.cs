namespace _03.MaxHeap
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.ExceptionServices;

    public class MaxHeap<T> : IAbstractHeap<T> where T : IComparable<T>
    {
        private readonly IList<T> elements;

        public MaxHeap()
        {
            this.elements = new List<T>();
        }

        public int Size => this.elements.Count;

        public void Add(T element)
        {
            this.elements.Add(element);

            this.HeapifyUp(this.elements.Count - 1);
        }

        public T ExtractMax()
        {
            if (this.elements.Count == 0)
            {
                throw new InvalidOperationException("The heap is empty!");
            }

            T elementToExtract = this.elements[0];
            this.Swap(0, this.elements.Count - 1);
            this.elements.RemoveAt(this.elements.Count - 1);
            this.HeapifyDown(0);

            return elementToExtract;
        }

        public T Peek()
        {
            if (this.elements.Count == 0)
            {
                throw new InvalidOperationException("The heap is empty!");
            }

            return this.elements[0];
        }
        
        private void HeapifyUp(int index)
        {
            int parentIndex = (index - 1) / 2;

            while (index > 0 
                && this.elements[index].CompareTo(this.elements[parentIndex]) == 1)
            {
                this.Swap(index, parentIndex);
                index = parentIndex;
                parentIndex = (index - 1) / 2;
            }
        }

        private void HeapifyDown(int index)
        {
            int biggerChildIndex = this.GetBiggerChildIndex(index);

            while(this.IsIndexValid(biggerChildIndex)
                && this.elements[index].CompareTo(this.elements[biggerChildIndex]) == -1)
            {
                this.Swap(biggerChildIndex, index);

                index = biggerChildIndex;
                biggerChildIndex = this.GetBiggerChildIndex(index);
            }
        }

        private int GetBiggerChildIndex(int index)
        {
            int leftChildIndex = index * 2 + 1;
            int rightChildIndex = index * 2 + 1;

            if (rightChildIndex < this.elements.Count)
            {
                if (leftChildIndex > rightChildIndex)
                {
                    return leftChildIndex;
                }

                return rightChildIndex;
            }
            else if (leftChildIndex < this.elements.Count)
            {
                return leftChildIndex;
            }

            return -1;
        }

        private void Swap(int index, int parentIndex)
        {
            T temp = this.elements[index];
            this.elements[index] = this.elements[parentIndex];
            this.elements[parentIndex] = temp;
        }

        private bool IsIndexValid(int index)
        {
            return index >= 0 && index < this.elements.Count;
        }
    }
}
