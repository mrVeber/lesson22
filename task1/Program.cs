using System;
using System.Linq;
using System.Threading.Tasks;

namespace task1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите размерность двухмерного массива:");
            Console.Write("Введите i: ");
            int x = Convert.ToInt32(Console.ReadLine());

            Func<object, int[]> func1 = new Func<object, int[]>(GetArray);
            Task<int[]> task1 = new Task<int[]>(func1, x);

            Func<Task<int[]>, int> func2 = new Func<Task<int[]>, int>(GetSum);
            Task<int> task2 = task1.ContinueWith<int>(func2);

            Func<Task<int[]>, int> func3 = new Func<Task<int[]>, int>(GetMax);
            Task<int> task3 = task1.ContinueWith<int>(func3);

            /*Action<Task<int[]>> action = new Action<Task<int[]>>(GetPrintArray);
            Task task4 = task1.ContinueWith(action);*/

            Action<Task<int>> action1 = new Action<Task<int>>(GetPrint);
            Task task5 = task3.ContinueWith(action1);

            Action<Task<int>> action2 = new Action<Task<int>>(GetPrint);
            Task task6 = task2.ContinueWith(action2);


            task1.Start();
            Console.ReadKey();
        }
        static int[] GetArray(object a)
        {
            int x = (int)a;
            int[] array = new int[x];
            Random rnd = new Random();
            for (int i = 0; i < x; i++)
            {
                array[i] = rnd.Next(0, 50);

            }
            return array;
        }
        static int GetSum(Task<int[]> task)
        {
            int[] array = task.Result;
            int sum = 0;
            for (int i = 0; i < array.Count(); i++)
            {
                sum += array[i];
            }
            return sum;
        }

        static int GetMax(Task<int[]> task)
        {
            int[] array = task.Result;
            int max = 0;
            for (int i = 0; i < array.Count(); i++)
            {
                if (array[i] > max)
                {
                    max = array[i];
                }
            }
            return max;  
        }

        static void GetPrint(Task<int> task)
        {
            int a = task.Result;
            Console.WriteLine($"{a}");
        }

       /* static void GetPrintArray(Task<int[]> task)
        {
            int[] array = task.Result;
            for (int i = 0; i < array.Count(); i++)
            {
                Console.Write($"{array[i]} ");
            }
        }*/
    }
}
