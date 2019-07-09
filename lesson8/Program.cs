using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace lesson8
{
    class Program
    {
        static int quickCount = 0;
        //Создание массива
        static int[] MyArray(int n, int min, int max)
        {
            int[] array = new int[n];
            Random rnd = new Random();
            for (int i = 0; i < n; i++)
                array[i] = rnd.Next(min, max);
            return array;
        }

        static void swapXOR(ref int a, ref int b)
        {
            //int t = a;
            //a = b;
            //b = t;
            a = a ^ b;
            b = b ^ a;
            a = a ^ b;
        }

        /* Поменять элементы местами */
        static void swap(int[] myint, int i, int j) //swap функция обмена
        {
            int t = myint[i];
            myint[i] = myint[j];
            myint[j] = t;
        }

        //  Сортируем методом пузырька
        static long BubbleSort(int[] myarray)
        {
            int f = 0;
            long count = 0;
            for (int i = 0; i < myarray.Length; i++)
            {
                f = 0;
                count++;
                for (int j = 0; j < myarray.Length - 1; j++)
                    if (myarray[j] > myarray[j + 1])//Сравниваем соседние элементы
                    {
                        //  Обмениваем элементы местами
                        //int t = a[j];
                        //a[j] = a[j + 1];
                        //a[j + 1] = t;
                        f = 1;
                        swapXOR(ref myarray[j], ref myarray[j + 1]);
                        count++;

                    }
                if (f == 0) break; //оптимизировали пузырьковую сортировку Если сортировать нечего выходим из цикла
            }
            return count;
        }

        // Шейкер-сортировка
        static long ShakerSort(int[] myarray)
        {
            int left = 0, //левая граница
                right = myarray.Length - 1, //правая граница
                count = 0;
            int f = 0;
            while (left <= right)
            {
                f = 0;
                for (int i = left; i < right; i++)  //Сдвигаем к концу массива "тяжелые элементы"
                {                    
                    if (myarray[i] > myarray[i + 1])
                    {
                        swap(myarray, i, i + 1); //swap функция обмена
                        f = 1;
                        count++;
                    }
                }
                right--;// уменьшаем правую границу

                for (int i = right; i > left; i--) //Сдвигаем к началу массива "легкие элементы"
                {                    
                    if (myarray[i - 1] > myarray[i])
                    {
                        swap(myarray, i - 1, i);//swap функция обмена
                        f = 1;
                        count++;
                    }
                }
                left++; // увеличиваем левую границу
                if (f == 0) break;//оптимизировали шейкерную сортировку
            }
            return count;
        }

        static long simpleCountingSort(int[] A, int k) // где k – длина массива А, 
                                          // а 1000 – его максимальное значение 
        {
            int[] C = new int[k];
            int i,j;
            int count = 0;
            //for (i = 0; i < k; i++)
            //    C[i] = 0;
            for (i = 0; i < k; i++)
            {
                count++;
                C[A[i]]++;
            }
            int b = 0;
            for (j = 0; j < k; j++)
                for (i = 0; i < C[j] - 1; i++)
                {
                    count++;
                    A[b++] = j;
                }
            return count;
        }

        static void quickSort(int[] array, int first, int last)
        {
            int i = first, j = last, x = array[(first + last) / 2];

            do
            {
                while (array[i] < x)
                    i++;
                while (array[j] > x)
                    j--;

                if (i <= j)
                {
                    if (array[i] > array[j])
                    {
                        swap(array, i, j);
                        quickCount++;
                    }
                    i++;
                    j--;
                }
            } while (i <= j);

            if (i < last)
                quickSort(array, i, last);
            if (first < j)
                quickSort(array, first, j);
            
        }

        static int countSort(int[] arr, int len)
        {
            int count = 0; // Счётчик обращений к элементам
            int [] values = new int[len];
            
            for (int i = 0; i < len; ++i)
            {
                ++values[arr[i]];
                count++;
            }

            int k = 0;
            for (int i = 0; i < len; ++i)
            {
                for (int j = 0; j < values[i]; ++j)
                {
                    arr[k++] = i;
                    count++;
                }
            }            

            return count;
        }

        static void Main(string[] args)
        {            
            int n = 20000; //размер массива
            int[] a = MyArray(n, 0, n);//создание массива
            int[] b = new int[n];
            int[] c = new int[n];
            int[] d = new int[n];
            int[] e = new int[n];
            int[] f = new int[n];
            long count = 0;
            Array.Copy(a, b, n);
            Array.Copy(a, c, n);
            Array.Copy(a, d, n);
            Array.Copy(a, e, n);
            Array.Copy(a, f, n);
            var t= DateTime.Now;

            Console.WriteLine("Сортировка пузырьком:");
            t = DateTime.Now;
            count = BubbleSort(b);
            Console.WriteLine($"Кол-во операций:{count} Время в миллисекундах:{(DateTime.Now - t).TotalMilliseconds}");

            Console.WriteLine("Шейкерная сортировка:");

            count = ShakerSort(c);
            Console.WriteLine($"Кол-во операций:{count} Время в миллисекундах:{(DateTime.Now - t).TotalMilliseconds}");

            Console.WriteLine("Cортировка подсчётом:");
            t = DateTime.Now;
            count = simpleCountingSort(d, n);
            Console.WriteLine($"Кол-во операций:{count} Время в миллисекундах:{(DateTime.Now - t).TotalMilliseconds}");

            Console.WriteLine("Быстрая сортировка:");
            t = DateTime.Now;
            quickSort(e,a[0], a[n-1]);
            Console.WriteLine($"Кол-во операций:{quickCount} Время в миллисекундах:{(DateTime.Now - t).TotalMilliseconds}");

            Console.WriteLine("Cортировка слиянием:");
            t = DateTime.Now;
            count = countSort(f, n);
            Console.WriteLine($"Кол-во операций:{count} Время в миллисекундах:{(DateTime.Now - t).TotalMilliseconds}");

            Console.ReadKey();

        }
    }
}
