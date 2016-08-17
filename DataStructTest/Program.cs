using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructTest
{
    class Program
    {
        static void Main(string[] args)
        {
            CList<int> clist = new CList<int>();
            Vector<int> vector = new Vector<int>();
            Random rnd = new Random();
            for (int i = 0; i < 100; i++)
            {
                int value = rnd.Next(-1000, 1000);
                clist.InsertAsLast(value);
                vector.Add(value);
            }
            
            //clist.Traverse(Output);
            //Console.Write('\n');
            //Console.WriteLine(clist.Size);
            //Console.WriteLine(clist.First.Data);
            ////Console.WriteLine(clist[1]);
            ////Console.WriteLine(clist.Deduplicate());
            ////Console.Write('\n');
            ////clist.Traverse(Console.WriteLine);
            //Console.Write('\n');
            ////Console.WriteLine(clist.Search(9, clist.Size-1, clist.Last).Data);
            ////clist.SelectionSort(clist.First, clist.Size);
            //clist.MergeSort(clist.First, clist.Size);
            //clist.Traverse(Output);
            //Console.Write('\n');
            Console.Write('\n');
            vector.Traverse(Output);
            Console.Write('\n');
            Console.WriteLine(vector.Disordered());
            vector.BubbleSort(0,vector.Size);
            Console.WriteLine(vector.Uniquify());
            //vector.Add(101);
            vector.Traverse(Output);
            Console.Write('\n');
            //vector.Unsort(3, vector.Size);
            //vector.Traverse(Output);
            
            Console.Write('\n');
            Console.WriteLine(Vector<int>.BinSearch(vector, vector[45], 0, vector.Size));
            Console.ReadLine();
        }
        static void Output(int i)
        {
            Console.Write(i);
            Console.Write(' ');
        }
    }
}
