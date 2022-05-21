using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wydawnictwo
{
    class DzialHandlu
    {
        private static List<Publikacje> ListaPublikacji = new List<Publikacje>();

        private static String NazwaPlikuPublikacje = "Publikacje.txt";

        public static void ZlecenieDruku(int ilosc, Publikacje publikacje)
        {
            if (DzialProgramowy.WyborDrukarni(ilosc, publikacje))
            {
                Console.WriteLine("Pomyslnie wydrukowano " + publikacje.Tytul + " w nastepujacej liczbie egzemplarzy: " + ilosc);
                if (!PublikacjaNaLiscie(publikacje))
                    { DodajDoListy(publikacje); }
            }
        }
        //dodaj do listy publikacji jesli drukarnia zwrocila true po wydrukowaniu
        public static void DodajDoListy(Publikacje publikacje)
        {
            ListaPublikacji.Add(publikacje);
        }
        //ew oddzielna klasa sklep
        public void ZlecenieKupna(int ilosc, Publikacje publikacje)
        {
            if (publikacje.Ilosc - ilosc >= 0)
                publikacje.setilosc(publikacje.Ilosc - ilosc);
            else
                Console.WriteLine("Nie ma wystarczającej ilości podanej publikacji, dostępna ilość to: " + publikacje.Ilosc);
        }
        public List<Publikacje> Katalog
        {
            get { return ListaPublikacji; }
        }

        public static String PlikPublikacje
        {
            get { return NazwaPlikuPublikacje; }
        }

        public static Boolean PublikacjaNaLiscie(Publikacje publikacja)
        {
            foreach (Publikacje publikacje in ListaPublikacji)
            {
                if (publikacja.Equals(publikacje)) { return true; }
            }
            return false;
        }

        public void UsunPublikacje(Publikacje publikacja)
        { 
            ListaPublikacji.Remove(publikacja); 
        }

    }

}
