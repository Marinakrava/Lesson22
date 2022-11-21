using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson22
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = Convert.ToInt32(Console.ReadLine());

            Func<object, int[]>func1= new Func<object, int[]>(GetArray);
            Task<int[]>task1 = new Task<int[]>(func1, n);

            Func<Task<int[]>,int[]> func2 = new Func<Task<int[]>,int[]>(SummArray);
            Task<int[]> task2 = task1.ContinueWith<int[]>(func2);

            Func<Task<int[]>, int[]> func3 = new Func<Task<int[]>, int[]>(MaxArray);
            Task<int[]> task3 = task2.ContinueWith<int[]>(func2);

            Action<Task<int[]>> action = new Action<Task<int[]>> (PrintArray);
            Task task4 = task3.ContinueWith(action);

            task1.Start();
            Console.ReadKey();
        }

        static int[] GetArray(object a)// метод по формированию массива
        { 
            int n = (int)a; 
            int[] array = new int[n];
            Random random = new Random();
            for (int i = 0; i < n; i++)
            { 
            array[i] = random.Next(0,50);
            }
            return array;
        }

        static int[] SummArray (Task<int[]> task)// метод по нахождения суммы чисел в массиве
        {
            int [] array = task.Result; 
            int sum = 0;    
            for (int i = 0; i < array.Count()-1; i++)
            {              
                sum+= array[i];
            }
            return sum;
        }

        static int MaxArray (int[]array)// метод по нахождению максимального числа в массиве
        {
            int max = array[0];
            foreach (int a in array)
            { 
            if (a > max) 
                    max = a;
            }
            return max;
        }
        static void PrintArray(Task<int[]> task)// метод ввывода на экран
        { 
        int[] array = task.Result;
            for (int i = 0; i < array.Count(); i++)
            { 
            Console.Write($"{array[i]} ,{MaxArray}");
            }
        }
       
       

    }
}
