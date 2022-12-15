using System;
using System.Diagnostics.CodeAnalysis;
using _02.BinarySearchTree;
using _03.MaxHeap;

namespace Demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var tree = new BinarySearchTree<int>();

            //tree.Insert(8);
            //tree.Insert(4);
            //tree.Insert(2);
            //tree.Insert(6);

            //tree.EachInOrder(n => Console.Write(n + " "));
            //Console.WriteLine();

            //var newTree = tree.Search(6);
            //newTree.Insert(9);

            //newTree.EachInOrder(n => Console.Write(n + " "));
            //Console.WriteLine();
            //tree.EachInOrder(n => Console.Write(n + " "));

            var heap = new MaxHeap<int>();

            heap.Add(4);
            heap.Add(7);
            heap.Add(11);
            heap.Add(18);
            heap.Add(2);
            heap.Add(5);
            heap.Add(8);
            heap.Add(1);
            heap.Add(21);

            Console.WriteLine(heap.ExtractMax());
        }
    }
}