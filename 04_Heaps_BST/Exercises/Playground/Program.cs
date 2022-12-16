using System;
using _03.MinHeap;
using _04.CookiesProblem;

namespace Playground
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var priorityQueue = new PriorityQueue<int>();

            priorityQueue.Enqueue(4);
            priorityQueue.Enqueue(7);
            priorityQueue.Enqueue(2);
            priorityQueue.Enqueue(9);
            priorityQueue.Enqueue(1);
        }
    }
}
