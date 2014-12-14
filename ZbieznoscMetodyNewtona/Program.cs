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
        /*
        * Rozwiązanie papierowe:
        * https://www.dropbox.com/sh/vkv9pqj522bzyqx/AAAD1MctqPYK-hFW18fnNLzsa?dl=0
        */

        private class Macierz2x2
        {
                /*
            |   a11 a12 |
            |   a21 a22 |
                */
            public double a11;
            public double a21;

            public double a12;
            public double a22;

            public void obliczPochodna(double x, double y)
            {   //pochodna
                a11 = ((2 * x)  / (4 * kwad(x) + 2 * y));
                a12 = ((1) / (4 * kwad(x) + 2 * y));
                a21 = ((-2 * y) / (4 * kwad(x) + 2 * y));
                a22 = ((2 * x)  / (4 * kwad(x) + 2 * y));
            }

            public Macierz2x2(double b11, double b12, double b21, double b22)
            {
                a11 = b11;
                a12 = b12;
                a21 = b21;
                a22 = b22;
            }

            public Macierz2x2()
            {

            }

        }

        private class Macierz2x1
        {
            public double a11;
            public double a21;

            public Macierz2x1 ObliczZeWzoru()
            {
                return new Macierz2x1((kwad(a11) - a21 - 1), (2 * a11 * a21 - 5));
            }

            public string ToString()
            {
                return "\r\n\t|x = " + a11 + "\r\n\t|y = " + a21;
            }

            public Macierz2x1(double b11, double b21)
            {
                a11 = b11;
                a21 = b21;
            }
            public Macierz2x1(Macierz2x1 macierz)
            {
                a11 = macierz.a11;
                a21 = macierz.a21;
            }

            public Macierz2x1()
            {

            }

            public static Macierz2x1 operator +(Macierz2x1 ls, Macierz2x1 rs)
            {
                return new Macierz2x1(ls.a11 + rs.a11, ls.a21 + rs.a21);
            }

            public static Macierz2x1 operator -(Macierz2x1 ls, Macierz2x1 rs)
            {
                return new Macierz2x1(ls.a11 - rs.a11, ls.a21 - rs.a21);
            }

            public static Macierz2x1 operator *(Macierz2x1 ls, Macierz2x1 rs)
            {
                return new Macierz2x1(ls.a11 * rs.a11, ls.a21 * rs.a21);
            }

            public static Macierz2x1 operator *(Macierz2x2 qmatrix, Macierz2x1 rs)
            {
                return new Macierz2x1(
                    (qmatrix.a11 * rs.a11) + (qmatrix.a12 * rs.a21), 
                    (qmatrix.a21 * rs.a11) + (qmatrix.a22 * rs.a21)
                    );
            }

        }

        private static Macierz2x1[] niewiadome = new Macierz2x1[100];
        private static double _epsilon = 0;
        private static Macierz2x1 _wynik;
        private static string czyWyjsc = "n";
        private static void Main(string[] args)
        {
            DrukujPowitanie();
            WczytajEpsilon();
            do
            {
                WczytajPunktyStartowe();

                Newton();
                Console.WriteLine("Wynik: " + _wynik.ToString());

                Console.Write("\nCzy zakonczyc dzialanie programu?(t/n): ");
                czyWyjsc = Console.ReadLine();
            } while (czyWyjsc != "t");
        }

        private static void Newton()
        {
            for (int j = 1; j < niewiadome.Length; j++)
                niewiadome[j] = new Macierz2x1();

            int i = 0;
            do
            {
                niewiadome[i + 1] = nastepnyKrok(niewiadome[i]);
                _wynik = niewiadome[i + 1];
                i++;
            } while (
                (i < niewiadome.Length - 1) &&
                (Math.Abs(niewiadome[i].a11 - niewiadome[i - 1].a11) >= _epsilon) &&
                (Math.Abs(niewiadome[i].a21 - niewiadome[i - 1].a21) >= _epsilon)
                );
        }

        private static void DrukujPowitanie()
        {
            Console.WriteLine(
                "Badamy eksperymentalnie zbieżność metody Newtona dla układu\n\tx^2 - y - 1 = 0\n\t2xy - 5 = 0\n");
        }

        private static void WczytajPunktyStartowe()
        {
            Console.WriteLine("\nPodaj nowa wartości punktów startowych x0 i y0");
            Console.Write("x0: ");
            double x0 = Double.Parse(Console.ReadLine());
            Console.Write("y0: ");
            double y0 = Double.Parse(Console.ReadLine());

            niewiadome[0] = new Macierz2x1(x0,y0);
           
            // TODO: jak złe wartości to pętla


        }

        private static void WczytajEpsilon()
        {
            Console.Write("\nPodaj pożądaną wartość przybliżenia\nEpsilon: ");
            _epsilon = Double.Parse(Console.ReadLine());
            while (!(0 < _epsilon && _epsilon < 1))
            {
                Console.WriteLine("Nieprawidłowa wartość. Wpisz liczbę z przedziału 0 ... 1");
                Console.Write("Epsilon: ");
                _epsilon = Double.Parse(Console.ReadLine());
            }
        }

        private static Macierz2x1 nastepnyKrok(Macierz2x1 poprzednieWartosci)
        {
            Macierz2x2 odwroconaPochodna = new Macierz2x2();
            odwroconaPochodna.obliczPochodna(poprzednieWartosci.a11, poprzednieWartosci.a21);

            return new Macierz2x1(poprzednieWartosci - (odwroconaPochodna * poprzednieWartosci.ObliczZeWzoru()));
        }

        private static double kwad(double x)
        {
            return x * x;
        }
    }
}