
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

public class Program
{
    public static void Merge<T>(T[] arr, int start_a, int end_a, int start_b, int end_b) where T: IComparable
    {
        T[] temp = new T[end_b - start_a];
        int current_index = 0;
        int a_index = start_a;
        int b_index = start_b;
        // mezclar hasta que se acabe algun array usando temp como array auxiliar
        while (a_index < end_a && b_index < end_b)
        {
            if (arr[a_index].CompareTo(arr[b_index]) < 0)
            {
                temp[current_index] = arr[a_index];
                a_index++;
            }
            else
            {
                temp[current_index] = arr[b_index];
                b_index++;
            }
            current_index ++;
        }

        // copiar el resto
        if( a_index == end_a)
            Array.Copy(temp,0,arr,start_a,current_index);
        else
        {
            Array.Copy(arr,a_index,temp,current_index,end_a - a_index);
            Array.Copy(temp,0,arr,start_a, end_b - start_a);
        }


    }


    public static void MergeSort<T>(T[] arr) where T : IComparable
    {
        MergeSort(arr,0,arr.Length);
    }

    public static void MergeSort<T>(T[] arr,int start, int end) where T : IComparable
    {
        // Método auxiliar
        if((end - start) <= 1)
            return;

        int start_a = start;
        int end_a = (start + end)/2;
        int start_b = (start + end)/2;
        int end_b = end;

        MergeSort(arr,start_a,end_a);
        MergeSort(arr,start_b,end_b);
        Merge(arr,start_a ,end_a ,start_b ,end_b);
    }

    public static void test(int case_number, int case_len, int min_val, int max_val)
    {
        // Se crean arrays aleatorios y se compara el resultado con el sort de C#
        for (int i = 0; i < case_number; i++)
        {
            // crear un array aleatorio
            Random rnd = new Random();
            int[] arr = Enumerable.Range(0,case_len).Select(x => rnd.Next(min_val, max_val - 1)).ToArray();
            int[] arr_copy = new int[case_len];
            arr.CopyTo(arr_copy, 0);

            MergeSort(arr);
            var temp = new List<int>(arr_copy);
            temp.Sort();
            if(!arr.SequenceEqual(temp))
            {
                Console.WriteLine(string.Concat(arr.Select(x => x.ToString() + ", ")));
                Console.WriteLine(string.Concat(temp.Select(x => x.ToString() + ", ")));
                Console.WriteLine("---------------------------------");
            }

        }

    }

    public static void Main(string[] args)
    {
        test(100,9,0,5);
    }
}

