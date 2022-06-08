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
     /*Autor autor = new Autor("AA", "BB", "CC");
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
     */
        //Start programu
        int x=10, a=10, b=20, ilosc=10;
        String imie, nazwisko, mail, tytul;
        Console.WriteLine("Witamy w wydawnictwie Triple S!!");
        System.Threading.Thread.Sleep(2000);
        Console.Clear();
        while (true)
        {
            Console.WriteLine("Jesli chcesz wejsc jako klien wybierz [1]");
            Console.WriteLine("Jesli chcesz wejsc jako dyrektor wydawnictwa wybierz [2]");
            x = int.Parse(Console.ReadLine());
            while (x != 1 && x != 2)
            {
                Console.WriteLine("Nie ma takiej opcji, sprobuj jeszcze raz");
                x = int.Parse(Console.ReadLine());
            }
            System.Threading.Thread.Sleep(1000);
            Console.Clear();

            switch (x)
            {
                case 1:
                    Sklep sklep = new Sklep();
                    sklep.WczytajZPlikuKsiazki("ksiazki.txt");
                    sklep.WczytajZPlikuCzasopisma("czasopisma.txt");
                    ArrayList Inwentarz=new ArrayList();
                    Inwentarz = sklep.getlista();
                    Console.WriteLine("Witamy w naszym sklepie");
                    System.Threading.Thread.Sleep(1000);
                    Console.Clear();
                    Console.WriteLine("Wybierz: ");
                    Console.WriteLine("[1] aby zobaczyc katalog");
                    Console.WriteLine("[2] aby wyjsc ze sklepu i przejsc do panelu zarzadzania");
                    Console.WriteLine("[3] aby zakonczyc program");
                    System.Threading.Thread.Sleep(1000);
                    a = int.Parse(Console.ReadLine());
                    Console.Clear();
                    switch (a)
                    {
                        case 1:


                        break;
                    }

                    break;

                case 2:
                    DzialDruku DD=new DzialDruku();
                    DzialProgramowy DP=new DzialProgramowy();
                    DzialHandlu DH=new DzialHandlu();
                    Drukarnie drukarnie=new Drukarnie();
                    while (true)
                    {
                        Console.WriteLine("[1] dodaj autora");
                        Console.WriteLine("[2] przegladaj i usuwaj dostepnych autorow");
                        Console.WriteLine("[3] podpisz umowe z autorem");
                        Console.WriteLine("[4] zlecenie o przygotowanie konkretnej pozycji");
                        Console.WriteLine("[5] przegladaj podpisane umowy i ewentualne rozwiazanie umowy");
                        Console.WriteLine("[6] przejdz do sklepu");
                        Console.WriteLine("[7] zakoncz program");
                        System.Threading.Thread.Sleep(1000);
                        b = int.Parse(Console.ReadLine());
                        while(b<1||b>7)
                        {
                            Console.WriteLine("NIe ma takiej opcji sprobuj jeszcze raz");
                            b = int.Parse(Console.ReadLine());
                        }
                        System.Threading.Thread.Sleep(1000);
                        Console.Clear();

                        switch (b)
                        {
                            case 1:
                                Console.WriteLine("Podaj imie autora:");
                                imie=Console.ReadLine();
                                Console.WriteLine("Podaj nazwisko autora:");
                                nazwisko=Console.ReadLine();
                                Console.WriteLine("Podaj adres mailowy autora:");
                                mail=Console.ReadLine();
                                Autor autor=new Autor(imie, nazwisko, mail);
                                DP.DodajAutora(autor);
                                System.Threading.Thread.Sleep(1000);
                                Console.Clear();

                                break;
                             
                            case 2:
                                ArrayList autorzy = new ArrayList();
                                autorzy = DP.getAutor();
                                int i = 1;
                                if (autorzy.Count > 0)
                                {
                                    foreach (Autor la in autorzy)
                                    {
                                        Console.WriteLine("[" + i + "] autor: " + la.Imie + " " + la.Nazwisko);
                                        System.Threading.Thread.Sleep(1000);
                                        i++;
                                    }

                                }else                               
                                    Console.WriteLine("Obecnie nie ma zadnych autorow");

                                break;
                            case 3:

                                break;
                            case 4:

                                break;
                            case 5:

                                break;
                            case 6:

                                break;
                            case 7:

                                break;
                          

                        }
                        
                    }
                    break;

            }
        }
    }
}
