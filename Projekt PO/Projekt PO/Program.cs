using System;
using System.IO;
using Wydawnictwo;
using System.Collections;
using System.Threading;

class HasloException : Exception
{
    public HasloException(string message) : base(message) { }
}
class PustaListaException : Exception 
{
    public PustaListaException(string message) : base(message) { }
}
class Program
{
    public static bool Haslo(string haslo)
    {
        string haslo1 = "1234";
        if (haslo1 == haslo) { return true; }
        throw new HasloException("Niepoprawne haslo");
    }
    public static bool Inwentarz(ArrayList Inwentarz)
    {
        if (Inwentarz.Count != 0) return true;
        throw new PustaListaException("Brak dostepnych pozycji w katalogu");
    }
    public static bool Autor(ArrayList autor)
    {
        if(autor.Count != 0) return true;
        throw new PustaListaException("Brak jakichkolwiek autorow na liscie autorow");
    }
    public static int WyborAutora()
    {
        int ia = 1, wa;
        ArrayList zdk = new ArrayList();
        DzialProgramowy DP=new DzialProgramowy();
        /*if (DP.getUmowy().Count == 0)
        throw new PustaListaException("Brak jakichkolwiek autorow na liscie umow");*/
        zdk = DP.getUmowy();
        Console.WriteLine("Wybierz autora:");
        foreach (Umowy uop in zdk)
        {
            if (uop is UmowyOPrace)
            {
                Console.WriteLine("[" + ia + "]" + uop.getAutor);
                ia++;
            }
        }
        wa = int.Parse(Console.ReadLine());
        while (wa < 0 && wa > ia)
        {
            Console.WriteLine("Niepoprawny wybor sprobuj jeszcze raz");
            wa = int.Parse(Console.ReadLine());
        }
        return wa;
    }

    static void Main(string[] args)
    {
        char wybor_logowania, wybor_opcji, wybor_umowy;
        int wybor_ksiazki;
        string imie, nazwisko, mail, tytul;
        int ru, rk, x;
        double dlugosc_umowy;

        while(true)
        {
            Console.WriteLine("Witaj w wydawnictwie EPress");
            Console.WriteLine("Wybierz sposob logowania:");
            Console.WriteLine();
            Console.WriteLine("1. Zaloguj sie jako klient");
            Console.WriteLine("2. Zaloguj sie jako pracownik");
            ConsoleKeyInfo key = Console.ReadKey();
            wybor_logowania = key.KeyChar;//Console.ReadLine();

            switch (wybor_logowania)
            {

                case '1':
                
                    {

                        Console.Clear();
                        Console.WriteLine("Pomyslnie zalogowano jako klient");
                        Thread.Sleep(1500);
                        Console.Clear();
                        Sklep sklep = new Sklep();
                        sklep.WczytajZPlikuKsiazki();
                        sklep.WczytajZPlikuCzasopisma();
                        ArrayList inwentarz = new ArrayList();
                        inwentarz = sklep.getlista();

                        while (wybor_logowania=='1')
                        {
                            Console.WriteLine("Witamy w naszym sklepie");
                            Console.WriteLine("Wybierz: ");
                            Console.WriteLine("[1] aby zobaczyc katalog");
                            Console.WriteLine("[2] aby przejsc do strony glownej");
                            Console.WriteLine("[3] aby zakonczyc program");
                            key = Console.ReadKey();
                            wybor_opcji = key.KeyChar;
                            Console.Clear();
                            int np=1;
                            switch (wybor_opcji)
                            {
                                case '1':
                                    {
                                        try { Inwentarz(inwentarz); }
                                        catch (PustaListaException PL) 
                                        {
                                            Console.WriteLine(PL.Message);
                                            Thread.Sleep(700);
                                            Console.Clear();
                                           break;
                                        }
                                            foreach (Publikacje p in inwentarz)
                                            {
                                                //nie wypisuje konkretnych pozycji
                                                Console.WriteLine("[" + np++ + "]" + p.getAutor + p.GetType());
                                            }
                                            Console.WriteLine("Ktora z ksiazek chcesz kupic?");
                                            Thread.Sleep(500);
                                        break;
                                    }
                                case '2':
                                    wybor_logowania = '2';
                                break;
                                case '3':
                                    return;
                                //break;

                            }
                        }
                        break;
                    }

                case '2':
                    {
                        Console.Clear();
                        string haslo;
                        Console.WriteLine("Wprowadz haslo:");
                        haslo = Console.ReadLine();
                        for (; haslo != "1234";)
                        {
                            try { Haslo(haslo); }
                            catch (HasloException h)
                            {
                                Console.WriteLine(h.Message); ;
                                Thread.Sleep(800);
                                Console.Clear();
                            }
                            Console.WriteLine("Wprowadz haslo:");
                            haslo = Console.ReadLine();
                        }
                        Console.WriteLine("Pomyslnie zalogowano jako pracownik");
                        Console.Clear();
                        DzialDruku DD = new DzialDruku();
                        DzialProgramowy DP = new DzialProgramowy();
                        DzialHandlu DH = new DzialHandlu();
                        Drukarnie drukarnie = new Drukarnie();
                        while (wybor_logowania=='2')
                        {
                            Console.WriteLine("Wybierz: ");
                            Console.WriteLine("[1] dodaj autora");
                            Console.WriteLine("[2] przegladaj dostepnych autorow");
                            Console.WriteLine("[3] usun autora");
                            Console.WriteLine("[4] podpisz umowe z autorem");
                            Console.WriteLine("[5] zlecenie o przygotowanie konkretnej pozycji");
                            Console.WriteLine("[6] przegladaj podpisane umowy ");
                            Console.WriteLine("[7] rozwiazanie umowy");
                            Console.WriteLine("[8] przejdz do strony glownej");
                            Console.WriteLine("[9] zakoncz program");
                            Thread.Sleep(1000);
                            key = Console.ReadKey();
                            wybor_opcji = key.KeyChar;
                            Console.Clear();
                            switch (wybor_opcji)
                            {
                                case '1':
                                    {
                                        Console.WriteLine("Podaj imie autora:");
                                        imie = Console.ReadLine();
                                        Console.WriteLine("Podaj nazwisko autora:");
                                        nazwisko = Console.ReadLine();
                                        Console.WriteLine("Podaj adres mailowy autora:");
                                        mail = Console.ReadLine();
                                        Autor autor = new Autor(imie, nazwisko, mail);
                                        DP.DodajAutora(autor);
                                        Thread.Sleep(1000);
                                        Console.Clear();
                                        break;
                                    }
                                case '2':
                                    {
                                        ArrayList autorzy = new ArrayList();
                                        autorzy = DP.getAutor();
                                        int number = 1;
                                        try { Autor(autorzy); }
                                        catch (PustaListaException PL)
                                        {
                                            Console.WriteLine(PL.Message);
                                            Thread.Sleep(700);
                                            Console.Clear();
                                            break;
                                        }
                                        foreach (Autor la in autorzy)
                                        {
                                            Console.WriteLine("[" + number + "] autor: " + la.Imie + " " + la.Nazwisko);
                                            number++;
                                        }
                                        Thread.Sleep(1500);
                                        Console.Clear();
                                        break;
                                    }
                                case '3':
                                    ArrayList autorzy1 = new ArrayList();
                                    autorzy1 = DP.getAutor();
                                    int i=1;
                                    int numer_autora;
                                    try { Autor(autorzy1); }
                                    catch (PustaListaException PL)
                                    {
                                        Console.WriteLine(PL.Message);
                                        Thread.Sleep(700);
                                        Console.Clear();
                                        break;
                                    }
                                    foreach (Autor la in autorzy1)
                                    {
                                        Console.WriteLine("[" + i + "] autor: " + la.Imie + " " + la.Nazwisko);
                                        i++;
                                    }
                                    Console.WriteLine("Ktorego autora chcesz usunac? (Podaj numer)");
                                    //tu moze byc key zamist console readline
                                    numer_autora=int.Parse(Console.ReadLine());
                                    i = 0;
                                    while (numer_autora >= 0 && numer_autora < autorzy1.Count)
                                    {
                                        //BLAD
                                        foreach (Autor autor in autorzy1)
                                        {
                                            if (numer_autora-1 == i)
                                            {
                                                DP.UsunAutora(autor);
                                                numer_autora = -1;
                                                break;
                                            }
                                            i++;
                                        }
                                    }
                                    Console.Clear();
                                    break;
                                case '4':
                                    Console.WriteLine("Jaka umowe chcesz podpisac?");
                                    Console.WriteLine("[1] umowa o prace");
                                    Console.WriteLine("[2] umowa o dzielo");
                                    key = Console.ReadKey();
                                    wybor_umowy = key.KeyChar;
                                    switch (wybor_umowy)
                                    {
                                        case '1':
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Podaj imie autora");
                                        imie = Console.ReadLine();
                                        Console.WriteLine("Podaj nazwisko autora");
                                        nazwisko = Console.ReadLine();
                                        Console.WriteLine("Podaj adres email autora");
                                        mail = Console.ReadLine();
                                        Console.WriteLine("Podaj dlugosc umowy (w latach):");
                                        dlugosc_umowy = double.Parse(Console.ReadLine());
                                        Autor autor1 = new Autor(imie, nazwisko, mail);
                                        if(DP.UmowaOPrace(dlugosc_umowy, autor1))
                                        Console.WriteLine("Umowa o prace zostala zawarta pomyslnie");
                                        Thread.Sleep(1000);
                                        Console.Clear();
                                        break;
                                    }
                                        case'2':
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Podaj imie autora");
                                        imie = Console.ReadLine();
                                        Console.WriteLine("Podaj nazwisko autora");
                                        nazwisko = Console.ReadLine();
                                        Console.WriteLine("Podaj adres email autora");
                                        mail = Console.ReadLine();
                                        Console.Clear();
                                            Console.WriteLine("Jaki gatunek ksiazki:");
                                            Console.WriteLine("[1] sensacyjne");
                                            Console.WriteLine("[2] kryminalistyczne");
                                            Console.WriteLine("[3] fantasy");
                                            Console.WriteLine("[4] romasnse");
                                            Console.WriteLine("[5] albumy");
                                            Console.WriteLine("[6] inne");
                                            wybor_ksiazki = int.Parse(Console.ReadLine());
                                            while(wybor_ksiazki<1||wybor_ksiazki>6)
                                                {
                                                    Console.WriteLine("Niepoprawny wybor");
                                                    Thread.Sleep(700);
                                                }
                                            switch (wybor_ksiazki)
                                            {
                                                case '1':
                                                {
                                                    Console.WriteLine("Podaj tytul ksiazki");
                                                    tytul = Console.ReadLine();
                                                    Autor autor2 = new Autor(imie, nazwisko, mail);
                                                    Sensacyjne publikacje = new Sensacyjne(autor2, tytul);
                                                    DP.UmowaODzielo(autor2, publikacje);
                                                    break;
                                                }
                                                case '2':
                                                {
                                                    Console.WriteLine("Podaj tytul ksiazki");
                                                    tytul = Console.ReadLine();
                                                    Autor autor2 = new Autor(imie, nazwisko, mail);
                                                    Kryminalistyczne publikacje = new Kryminalistyczne(autor2, tytul);
                                                    DP.UmowaODzielo(autor2, publikacje);
                                                    break;
                                                }
                                                case '3':
                                                {
                                                    Console.WriteLine("Podaj tytul ksiazki");
                                                    tytul = Console.ReadLine();
                                                    Autor autor2 = new Autor(imie, nazwisko, mail);
                                                    Fantasy publikacje = new Fantasy(autor2, tytul);
                                                    DP.UmowaODzielo(autor2, publikacje);
                                                    break;
                                                }
                                                case '4':
                                                {
                                                    Console.WriteLine("Podaj tytul ksiazki");
                                                    tytul = Console.ReadLine();
                                                    Autor autor2 = new Autor(imie, nazwisko, mail);
                                                    Romanse publikacje = new Romanse(autor2, tytul);
                                                    DP.UmowaODzielo(autor2, publikacje);
                                                    break;
                                                }
                                                case '5':
                                                {
                                                    Console.WriteLine("Podaj tytul ksiazki");
                                                    tytul = Console.ReadLine();
                                                    Autor autor2 = new Autor(imie, nazwisko, mail);
                                                    Albumy publikacje = new Albumy(autor2, tytul);
                                                    DP.UmowaODzielo(autor2, publikacje);
                                                    break;
                                                }
                                                case '6':
                                                {
                                                    Console.WriteLine("Podaj tytul ksiazki");
                                                    tytul = Console.ReadLine();
                                                    Autor autor2 = new Autor(imie, nazwisko, mail);
                                                    Inne publikacje = new Inne(autor2, tytul);
                                                    DP.UmowaODzielo(autor2, publikacje);
                                                    break;
                                                }
                                            }
                                        }
                                        Thread.Sleep(2000);
                                        Console.Clear();
                                        break;
                                            //poprawic
                                        /*default:
                                            {
                                                Console.WriteLine("Niepoprawny wybor. Sprobuj jeszcze raz");
                                                key = Console.ReadKey();
                                                wybor_ = key.KeyChar;
                                                Console.Clear();
                                                break;
                                            }*/
                                    }
                                    break;
                                case '5':
                                    int zk,wk;
                                    //String wk;
                                    int ia = 1, wa; 
                                    Console.WriteLine("Co chcesz wydrukowac");
                                    Console.WriteLine("[1] ksiazke");
                                    Console.WriteLine("[2] czasopismo");
                                    zk = int.Parse(Console.ReadLine());
                                    while (zk < 0 || zk > 2)
                                    {
                                        Console.WriteLine("Niepoprawnie wybrana opcja. Sprobuj jeszcze raz");
                                        zk = int.Parse(Console.ReadLine());
                                    }
                                    if (zk == 1)
                                    {

                                        Console.WriteLine("Jaki gatunek ksiazki:");
                                        Console.WriteLine("[1] sensacyjne");
                                        Console.WriteLine("[2] kryminalistyczne");
                                        Console.WriteLine("[3] fantasy");
                                        Console.WriteLine("[4] romasnse");
                                        Console.WriteLine("[5] albumy");
                                        Console.WriteLine("[6] inne");
                                        wk = int.Parse(Console.ReadLine());
                                        while (wk < 1 || wk > 6)
                                        {
                                            Console.WriteLine("Niepoprawny wybor");
                                            Thread.Sleep(700);
                                        }
                                      //  wk = Console.ReadLine();
                                        switch (wk)
                                        {
                                            case '1':

                                                try { wa = WyborAutora(); }
                                                catch(PustaListaException PL)
                                                {
                                                    Console.WriteLine(PL.Message);
                                                    Thread.Sleep(700);
                                                    Console.Clear();
                                                    break;
                                                }
                                                Console.WriteLine("Podaj tytul:");
                                                tytul=Console.ReadLine();
                                                ia = 1;
                                                foreach (Umowy uop in DP.getUmowy())
                                                {
                                                    if (uop is UmowyOPrace)
                                                    {
                                                        if (wa == ia)
                                                        {
                                                            Publikacje ZD = new Sensacyjne(uop.Autor, tytul);
                                                            DH.ZlecenieDruku(200,ZD);
                                                        }
                                                        ia++;
                                                    }
                                                }

                                                break;
                                            case '2':
                                                try { wa = WyborAutora(); }
                                                catch (PustaListaException PL)
                                                {
                                                    Console.WriteLine(PL.Message);
                                                    Thread.Sleep(700);
                                                    Console.Clear();
                                                    break;
                                                }
                                                Console.WriteLine("Podaj tytul:");
                                                tytul = Console.ReadLine();
                                                ia = 1;
                                                foreach (Umowy uop in DP.getUmowy())
                                                {
                                                    if (uop is UmowyOPrace)
                                                    {
                                                        if (wa == ia)
                                                        {
                                                            Publikacje ZD = new Kryminalistyczne(uop.Autor, tytul);
                                                            DH.ZlecenieDruku(200, ZD);
                                                        }
                                                        ia++;
                                                    }
                                                }

                                                break;
                                            case '3':
                                                try { wa = WyborAutora(); }
                                                catch (PustaListaException PL)
                                                {
                                                    Console.WriteLine(PL.Message);
                                                    Thread.Sleep(700);
                                                    Console.Clear();
                                                    break;
                                                }
                                                Console.WriteLine("Podaj tytul:");
                                                tytul = Console.ReadLine();
                                                ia = 1;
                                                foreach (Umowy uop in DP.getUmowy())
                                                {
                                                    if (uop is UmowyOPrace)
                                                    {
                                                        if (wa == ia)
                                                        {
                                                            Publikacje ZD = new Fantasy(uop.Autor, tytul);
                                                            DH.ZlecenieDruku(200, ZD);
                                                        }
                                                        ia++;
                                                    }
                                                }

                                                break;
                                            case '4':
                                                try { wa = WyborAutora(); }
                                                catch (PustaListaException PL)
                                                {
                                                    Console.WriteLine(PL.Message);
                                                    Thread.Sleep(700);
                                                    Console.Clear();
                                                    break;
                                                }
                                                Console.WriteLine("Podaj tytul:");
                                                tytul = Console.ReadLine();
                                                ia = 1;
                                                foreach (Umowy uop in DP.getUmowy())
                                                {
                                                    if (uop is UmowyOPrace)
                                                    {
                                                        if (wa == ia)
                                                        {
                                                            Publikacje ZD = new Romanse(uop.Autor, tytul);
                                                            DH.ZlecenieDruku(200, ZD);
                                                        }
                                                        ia++;
                                                    }
                                                }

                                                break;
                                            case '5':
                                                try { wa = WyborAutora(); }
                                                catch (PustaListaException PL)
                                                {
                                                    Console.WriteLine(PL.Message);
                                                    Thread.Sleep(700);
                                                    Console.Clear();
                                                    break;
                                                }
                                                tytul = Console.ReadLine();
                                                ia = 1;
                                                foreach (Umowy uop in DP.getUmowy())
                                                {
                                                    if (uop is UmowyOPrace)
                                                    {
                                                        if (wa == ia)
                                                        {
                                                            Publikacje ZD = new Albumy(uop.Autor, tytul);
                                                            DH.ZlecenieDruku(200, ZD);
                                                        }
                                                        ia++;
                                                    }
                                                }

                                                break;
                                            case '6':
                                                try { wa = WyborAutora(); }
                                                catch (PustaListaException PL)
                                                {
                                                    Console.WriteLine(PL.Message);
                                                    Thread.Sleep(700);
                                                    Console.Clear();
                                                    break;
                                                }
                                                Console.WriteLine("Podaj tytul:");
                                                tytul = Console.ReadLine();
                                                ia = 1;
                                                foreach (Umowy uop in DP.getUmowy())
                                                {
                                                    if (uop is UmowyOPrace)
                                                    {
                                                        if (wa == ia)
                                                        {
                                                            Publikacje ZD = new Inne(uop.Autor, tytul);
                                                            DH.ZlecenieDruku(200, ZD);
                                                        }
                                                        ia++;
                                                    }
                                                }
                                                break;
                                            default:
                                                {
                                                    Console.WriteLine("Wybierz jedna z podanych opcji");
                                                    Thread.Sleep(800);
                                                    break;
                                                }


                                        }

                                    }
                                    else if(zk==2)
                                    {
                                        Console.WriteLine("Jaki magazyn chcesz wydrukowac: ");
                                        Console.WriteLine("[1] miesiecznik");
                                        Console.WriteLine("[2] tygodnik");
                                        wa=int.Parse(Console.ReadLine());
                                        int ilosc;
                                        while(wa < 0 && wa > 2)
                                        {
                                            Console.WriteLine("Niepoprawny wybor sprobuj jeszcze raz");
                                            wa=int.Parse(Console.ReadLine());
                                        }
                                        if(wa==1)
                                        {
                                            Autor redakcja = new Autor();
                                            Console.WriteLine("Podaj tytul magazynu: ");
                                            tytul = Console.ReadLine();
                                            Publikacje czasopismo = new Miesiecznik(redakcja, tytul);
                                            Console.WriteLine("Podaj ilosc:");
                                            ilosc= int.Parse(Console.ReadLine());
                                            DH.ZlecenieDruku(ilosc, czasopismo);
                                            Thread.Sleep(800);
                                            Console.Clear();
                                        }
                                        else if (wa == 2)
                                        {
                                            Autor redakcja = new Autor();
                                            Console.WriteLine("Podaj tytul magazynu: ");
                                            tytul = Console.ReadLine();
                                            Publikacje czasopismo = new Miesiecznik(redakcja, tytul);
                                            Console.WriteLine("Podaj ilosc:");
                                            ilosc = int.Parse(Console.ReadLine());
                                            DH.ZlecenieDruku(ilosc, czasopismo);
                                            Thread.Sleep(800);
                                            Console.Clear();
                                        }
                                    }
                                    break;
                                case '6':
                                    ArrayList umowy = new ArrayList();
                                    try { umowy = DP.getUmowy(); }
                                    catch (PustaListaException PL)
                                    {
                                        Console.WriteLine(PL.Message);
                                        Thread.Sleep(700);
                                        Console.Clear();
                                        break;
                                    }
                                    Console.WriteLine("Autorzy na umowie o prace:");
                                    foreach (Umowy uop in umowy)
                                    {
                                        if (uop is UmowyOPrace)
                                        {
                                            Console.WriteLine(uop.getAutor );
                                        }

                                    }
                                    Console.WriteLine("Autorzy na umowie o dzielo:");
                                    foreach (Umowy uop in umowy)
                                    {
                                        if (uop is UmowyODzielo)
                                        {
                                            Console.WriteLine(uop.getAutor);
                                        }

                                    }
                                    Thread.Sleep(1500);
                                    Console.Clear();
                                    break;

                                case '7':
                                    ArrayList rozwiazanie = new ArrayList();
                                    try { rozwiazanie = DP.getUmowy(); }
                                    catch (PustaListaException PL)
                                    {
                                        Console.WriteLine(PL.Message);
                                        Thread.Sleep(700);
                                        Console.Clear();
                                        break;
                                    }
                                    x = 1;
                                    foreach (Umowy uop in rozwiazanie)
                                    {
                                        if (uop is UmowyOPrace)
                                        {
                                            Console.WriteLine("["+x+ "]"+uop.getAutor+" na umowie o prace");
                                        }
                                        else if (uop is UmowyODzielo)
                                        {
                                            Console.WriteLine("["+x+"]"+uop.getAutor+" na umowie o dzielo ");
                                        }
                                        x++;
                                    }
                                    Console.WriteLine("Z ktorym autorem chcesz rozwiazac umowe: ");
                                    ru=int.Parse(Console.ReadLine());
                                    while(ru<0||ru>x)
                                    {
                                        Console.WriteLine("Niepoprawy wybor. Sprobuj jeszcze raz:");
                                        ru = int.Parse(Console.ReadLine());
                                    }
                                    x = 1;
                                    foreach (Umowy uop in rozwiazanie)
                                    {
                                        if(ru==x)
                                        {
                                            DP.RozwiazanieUmowy(uop.Autor);
                                        }
                                        x++;
                                    }
                                        break;
                                case '8':
                                    wybor_logowania = '1';
                                    break;

                                case '9':
                                    return ;
                                default:
                                    {
                                        Console.WriteLine("Wybierz jedna z podanych opcji");
                                        Thread.Sleep(800);
                                        Console.Clear();
                                        break;
                                    }
                            }

                        }

                    }
                    break;
                default:
                    {
                        Console.WriteLine("Wybierz jedna z podanych opcji");
                        Thread.Sleep(800);
                        Console.Clear();
                        break;
                    }
            }
        }
    }
}