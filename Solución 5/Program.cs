
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

public class Program
{

    public static int findShortestList<T>(List<List<T>> list_of_lists)
    {
        // Buscar la lista con menos elementos
        int min_len = int.MaxValue;
        int min_index = -1;
        int current_index = 0;
        foreach (var sub_list in list_of_lists)
        {
            if (sub_list.Count < min_len)
            {
                min_len = sub_list.Count;
                min_index = current_index;
            }
            current_index++;
        }
        return min_index;
    }


    public static void RemoveNonContainedItems<T>(List<T> min_sublist, List<List<T>> list_of_lists)
    {
        //Buscar cual de los elementos de la lista con menos elementos pertenece a todas las listas
        int current_index = 0;
        foreach (var sub_list in list_of_lists)
        {
            current_index = 0;
            List<int> index_to_remove = new();
            foreach (var common_item in min_sublist)
            {
                // si existe un elemento igual que el common item entonces no se remueve
                bool is_not = true;
                foreach (var item in sub_list)
                {
                    if (common_item.Equals(item))
                    {
                        is_not = false;
                        break;
                    }
                }
                // si no está en esa lista se añade a la lista a remover
                if (is_not)
                    index_to_remove.Add(current_index);
                current_index++;
            }

            // Removiendo los elementos afuera porque los enumerables no se pueden modificar mientras se recorren
            current_index = 0;
            foreach (var index in index_to_remove)
            {
                min_sublist.RemoveAt(index - current_index);
                current_index++;
            }
        }

    }

    public static List<T> findCommonElements<T>(List<List<T>> list_of_lists) where T : IEquatable<T>
    {
        // Buscar la lista con la menor cantidad de elementos y remover los que no pertenecen a alguna lista
        int min_index = findShortestList(list_of_lists);

        List<T> min_sublist = list_of_lists[min_index];

        list_of_lists.RemoveAt(min_index);

        RemoveNonContainedItems(min_sublist, list_of_lists);

        return min_sublist;
    }

    public static void Shuffle(IList<int> list)
    {
        Random rnd = new();
        int i = list.Count;
        while (i > 1)
        {
            i--;
            int k = rnd.Next(i + 1);
            int value = list[k];
            list[k] = list[i];
            list[i] = value;
        }
    }

    public static void test(int case_number, int list_number, int min_len, int max_len, int common_len)
    {
        // Se generan casos con valores comunes conocidos
        Random rnd = new();
        // valores comunes a todas las listas
        IEnumerable<int> common_list = Enumerable.Range(0, common_len);
        int err_count = 0;
        for (int i = 0; i < case_number; i++)
        {
            List<List<int>> list_of_lists = new(list_number);
            int last_step = common_len;

            //creando las las sublistas
            for (int j = 0; j < list_number; j++)
            {
                int len = rnd.Next(min_len - common_len, max_len + 1 - common_len);
                List<int> current_list = new(len);
                // annadiendo valores comunes
                current_list.AddRange(common_list);
                // annadiendo valores distintos
                current_list.AddRange(Enumerable.Range(last_step, len));
                // Shuffle
                Shuffle(current_list);
                list_of_lists.Add(current_list);

                last_step += len;
            }
            List<int> common_elements = findCommonElements(list_of_lists);
            common_elements.Sort();
            if (!common_elements.SequenceEqual(common_list))
            {
                Console.WriteLine(String.Concat(common_elements.Select(x => x.ToString() + ", ")));
                Console.WriteLine(String.Concat(common_list.Select(x => x.ToString() + ", ")));
                Console.WriteLine('\n');
                foreach (var sublist in list_of_lists)
                    Console.WriteLine(String.Concat(sublist.Select(x => x.ToString() + ", ")));
                Console.WriteLine("-------------");
                err_count++;
            }
        }
        Console.WriteLine(err_count);
    }

    public static void Main(string[] args)
    {
        test(10, 10, 3, 20, 0);
    }
}

