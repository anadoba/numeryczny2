using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ZbieznoscMetodyNewtona
{
    internal class Program
    {
        private static ParaNiewiadomych[] paraNiewiadomych = new ParaNiewiadomych[100];
        private static double _epsilon = 0;

        /*
        * Rozwiązanie papierowe:
        * https://www.dropbox.com/sh/vkv9pqj522bzyqx/AAAD1MctqPYK-hFW18fnNLzsa?dl=0
        */

        private class ParaNiewiadomych
        {
            public double x;
            public double y;

            public ParaNiewiadomych(double x, double y)
            {
                this.x = x;
                this.y = y;
            }
        }

        private static void Main(string[] args)
        {
            DrukujPowitanie();
            WczytajDane();

            Console.ReadLine();
        }

        private static void DrukujPowitanie()
        {
            Console.WriteLine(
                "Badamy eksperymentalnie zbieżność metody Newtona dla układu\n\tx^2 - y - 1 = 0\n\t2xy - 5 = 0\n");
        }

        private static void WczytajDane()
        {
            Console.WriteLine("\nPodaj wartości punktów startowych x0 i y0");
            Console.Write("x0: ");
            double x0 = Double.Parse(Console.ReadLine());
            Console.Write("y0: ");
            double y0 = Double.Parse(Console.ReadLine());
            paraNiewiadomych[0] = new ParaNiewiadomych(x0, y0);
            // TODO: jak złe wartości to pętla

            Console.WriteLine("\nPodaj pożądaną wartość przybliżenia\nEpsilon: ");
            _epsilon = Double.Parse(Console.ReadLine());
            while (! (0 < _epsilon && _epsilon < 1))
            {
                Console.WriteLine("Nieprawidłowa wartość. Wpisz liczbę z przedziału 0 ... 1");
                Console.WriteLine("Epsilon: ");
                _epsilon = Double.Parse(Console.ReadLine());
            }
        }
    }
}