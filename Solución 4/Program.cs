

public class Letters
{
    public static char[] all_letters = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'ñ', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'];
}

public class Program
{
    static bool IsAnagram(string a, string b)
    {
        // Usar un diccionario para contar las letras de la primera palabra e ir restandolas con la segunda
        IDictionary<char,int> letters = new Dictionary<char,int>();
        for(int i = 0; i < a.Length; i++)
        {
            if (letters.TryGetValue(a[i],out int val))
                letters[a[i]] = val + 1;
            else
            {
                letters.Add(a[i],1);
            }
        }

        for (int i = 0; i < b.Length; i++)
        {
            if (letters.TryGetValue(b[i], out int val))
                letters[b[i]] = val - 1;
            else
                return false;
        }

        return letters.Values.Sum() == 0;
    }

    public static void Shuffle<T>(T[] list)
    {
        Random rnd = new();
        int i = list.Length;
        while (i > 1)
        {
            i--;
            int k = rnd.Next(i + 1);
            T value = list[k];
            list[k] = list[i];
            list[i] = value;
        }
    }

    static bool IsAnagramSlow(string a, string b)
    {
        // todos los anagramas ordenados son la misma palabra
        string ord_a = string.Concat(a.OrderBy(c => c));
        string ord_b = string.Concat(b.OrderBy(c => c));
        return ord_a == ord_b;
    }

    static void Test(int case_number,int a_len, int b_len)
    {
        // Tester que se basa en que la implementación de IsAnagramSlow es correcta
        // Los casos tienen la misma probabilidad de ser anagramas que de no serlos
        Random rnd = new ();
        int err_count = 0;
        int anagrams = 0;
        for (int i = 0; i < case_number; i++)
        {
            // String aleatorio
            var a = string.Concat(Letters.all_letters.OrderBy(x => rnd.Next(0,Letters.all_letters.Length)).Take(a_len));
            // String que puede ser una permutación de a u otro string aleatorio
            string b;
            if (rnd.Next(0,2) == 0)
            {
                var a_arr = a.ToArray();
                Shuffle(a_arr);
                b = string.Concat(a_arr);
            }
            else
                b = string.Concat(Letters.all_letters.OrderBy(x => rnd.Next(0,Letters.all_letters.Length)).Take(b_len));
            bool val = IsAnagram(a,b);
            bool true_val = IsAnagramSlow(a,b);
            if(true_val)
                anagrams++;
            if(val ^ true_val )
            {
                Console.WriteLine("a: " + a.ToString());
                Console.WriteLine("b: " + b.ToString());
                Console.WriteLine("Mine: " + val.ToString());
                Console.WriteLine("Slow: " + true_val.ToString());
                Console.WriteLine("----------------");

                err_count++;
            }
        }
        Console.WriteLine(err_count.ToString() + '/' + case_number.ToString());
        Console.WriteLine(anagrams.ToString() + '/' + case_number.ToString());
    }

    public static void Main(string[] args)
    {
        Test(100,15,15);
    }
}

