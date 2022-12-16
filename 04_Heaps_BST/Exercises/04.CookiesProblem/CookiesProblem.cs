using System;
using System.Linq;
using _03.MinHeap;
using Wintellect.PowerCollections;

namespace _04.CookiesProblem
{
    public class CookiesProblem
    {
        public int Solve(int minSweetness, int[] cookies)
        {
            var priorityQueue = new MinHeap<int>();

            foreach (var cookie in cookies)
            {
                priorityQueue.Add(cookie);
            }

            int currentMinSweetnessCookie = priorityQueue.Peek();
            int steps = 0;

            while (currentMinSweetnessCookie < minSweetness
                && priorityQueue.Count > 1)
            {
                int leastSweetCookie = priorityQueue.ExtractMin();
                int secondSweetCookie = priorityQueue.ExtractMin();

                int newCookie = leastSweetCookie + 2 * secondSweetCookie;

                priorityQueue.Add(newCookie);
                currentMinSweetnessCookie = priorityQueue.Peek();
                steps++;
            }

            return currentMinSweetnessCookie > minSweetness ? steps : -1;
        }
    }
}
