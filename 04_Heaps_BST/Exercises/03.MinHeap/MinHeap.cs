using System;
using System.Collections.Generic;
using System.Text;

namespace _03.MinHeap
{
    public class MinHeap<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        protected List<T> elements;

        public MinHeap()
        {
            this.elements = new List<T>();
        }

        public int Count => this.elements.Count;

        public void Add(T element)
        {
            this.elements.Add(element);

            this.HeapifyUp(this.elements.Count - 1);
        }

        public T ExtractMin()
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
                && this.elements[index].CompareTo(this.elements[parentIndex]) == -1)
            {
                this.Swap(index, parentIndex);
                index = parentIndex;
                parentIndex = (index - 1) / 2;
            }
        }

        private void HeapifyDown(int index)
        {
            int smallerChildIndex = this.GetSmallerChildIndex(index * 2 + 1, index * 2 + 2);

            while (smallerChildIndex != -1
                && this.IsSmaller(smallerChildIndex, index))
            {
                this.Swap(smallerChildIndex, index);
                index = smallerChildIndex;
                smallerChildIndex = this.GetSmallerChildIndex(index * 2 + 1, index * 2 + 2);
            }
        }

        private int GetSmallerChildIndex(int leftChildIndex, int rightChildIndex)
        {
            if (!this.IsIndexValid(leftChildIndex))
            {
                return -1;
            }

            if (!this.IsIndexValid(rightChildIndex))
            {
                return leftChildIndex;
            }

            return this.IsSmaller(leftChildIndex, rightChildIndex)
                ? leftChildIndex
                : rightChildIndex;
        }

        private bool IsSmaller(int leftChildIndex, int rightChildIndex)
        {
            return this.elements[leftChildIndex].CompareTo(this.elements[rightChildIndex]) == -1;
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
