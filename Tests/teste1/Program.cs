namespace teste1
{
    class Program
    {
        static double CalcularMedia(int[] numeros)
        {
            if (numeros.Length == 0)
            {
                return 0;
            }

            double soma = numeros.Sum();
            return soma / numeros.Length;
        }

        static void Main()
        {
            Console.WriteLine(CalcularMedia(new int[] { 5, 10, 15, 20 }));
            Console.WriteLine(CalcularMedia(new int[] { }));
        }
    }
}