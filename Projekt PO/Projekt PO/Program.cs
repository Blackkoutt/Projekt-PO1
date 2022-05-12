using System;
using System.IO;
using Wydawnictwo;
using System.Collections;

// w programie powinno byc zapisywanie do pliku i odczytywanie z pliku na liste (tam gdzie sa uzyte jakiekolwiek listy)
class Program
{
    static void Main(string[] args)
    {

        //TESTY PROGRAMU
        Autor autor = new Autor("AA", "BB", "CC");
        Autor autor1 = new Autor("A1", "B2", "C3");
        Sensacyjne sen = new Sensacyjne(autor, "TYTUL");
        //zwraca tytul ksiazki
        Console.WriteLine(sen.Tytul);
        DzialProgramowy DP=new DzialProgramowy();
        //po zawarciu umowy o prace z konkretnym autorem dodawany jest on automatycznie do listy autorow
        DP.UmowaOPrace(2, autor);
        DP.UmowaOPrace(3, autor1);
        ArrayList lista = new ArrayList();
        lista = DP.getAutor();
        //zwraca cala liste autorow
        for (int i = 0; i < lista.Count; i++)
        {
            //rzutowanie elementow kolekcji na typ Autor i wypisanie
            Autor autorr = (Autor)lista[i];
            Console.WriteLine(autorr.Imie+" "+autorr.Nazwisko+" "+autorr.Email);
        }
    }
}
