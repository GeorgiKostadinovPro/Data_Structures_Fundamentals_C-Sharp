using System;
using System.Collections.Generic;

namespace _03.MinHeap
{
    public class PriorityQueue<T> : MinHeap<T> where T : IComparable<T>
    {
        private IDictionary<T, int> indexes;

        public PriorityQueue()
        {
            this.elements = new List<T>();

            this.indexes = new Dictionary<T, int>();
        }

        public void Enqueue(T element)
        {
            this.elements.Add(element);
            this.indexes.Add(element, this.Count - 1);

            this.HeapifyUp(this.elements.Count - 1);
        }

        public T Dequeue()
        {
            if (this.elements.Count == 0)
            {
                throw new InvalidOperationException("The heap is empty!");
            }

            T elementToExtract = this.elements[0];
            this.Swap(0, this.elements.Count - 1);
            this.elements.RemoveAt(this.elements.Count - 1);
            this.indexes.Remove(elementToExtract);
            this.HeapifyDown(0);

            return elementToExtract;
        }

        public void DecreaseKey(T key)
        {
            var index = this.indexes[key];

            this.HeapifyUp(index);
        }

        public void DecreaseKey(T key, T newKey)
        {
            int oldIndex = this.indexes[key];
            this.elements[oldIndex] = newKey;
            this.indexes.Remove(key);
            this.indexes.Add(newKey, oldIndex);

            this.HeapifyUp(oldIndex);
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

            this.indexes[this.elements[index]] = index;
            this.indexes[this.elements[parentIndex]] = parentIndex;
        }

        private bool IsIndexValid(int index)
        {
            return index >= 0 && index < this.elements.Count;
        }
    }
}
