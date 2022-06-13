using System;
using System.IO;
using Wydawnictwo;
using System.Collections;
using System.Threading;

class Program
{
    public static bool Haslo(string haslo)
    {
        string haslo1 = "1234";
        if (haslo1 == haslo) { return true; }
        throw new HasloException("Niepoprawne haslo");
    }
    /*public static int WyborPublikacji()
    {
        DzialHandlu DH=new DzialHandlu();
        ArrayList publikacje=new ArrayList();
        publikacje = DH.Katalog;
        Console.WriteLine("Wybierz publikacje:");

    }*/
    public static int WyborAutora(ArrayList zdk)
    {
        int ia = 1, wa;
        DzialProgramowy DP=new DzialProgramowy();
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
    public static char WyborKsiazki()
    {
        char wk;
        Console.Clear();
        Console.WriteLine("Jaki gatunek ksiazki:");
        Console.WriteLine("[1] sensacyjne");
        Console.WriteLine("[2] kryminalistyczne");
        Console.WriteLine("[3] fantasy");
        Console.WriteLine("[4] romasnse");
        Console.WriteLine("[5] albumy");
        Console.WriteLine("[6] inne");
        Console.WriteLine("[7] wroc do menu glownego");
        ConsoleKeyInfo key = Console.ReadKey();
        wk = key.KeyChar;
        while (true)
        {
            if (wk == '1' || wk == '2' || wk == '3' || wk == '4' || wk == '5' || wk == '6' || wk == '7') { break; }
            Console.WriteLine("Niepoprawny wybor");
            key = Console.ReadKey();
            wk = key.KeyChar;
        }
        return wk;
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
            Console.WriteLine("3. Zakoncz program");
            ConsoleKeyInfo key = Console.ReadKey();
            wybor_logowania = key.KeyChar;//Console.ReadLine();

            switch (wybor_logowania)
            {
                case '1':
                    {
                        Console.Clear();
                        Console.WriteLine("Pomyslnie zalogowano jako klient");
                        Thread.Sleep(900);
                        Console.Clear();
                        Sklep sklep = new Sklep();
                        sklep.WczytajZPlikuKsiazki();
                        sklep.WczytajZPlikuCzasopisma();
                        ArrayList inwentarz = new ArrayList();
                        while (wybor_logowania == '1')
                        {
                            Console.WriteLine("Witamy w naszym sklepie");
                            Console.WriteLine("Wybierz: ");
                            Console.WriteLine("[1] aby zobaczyc katalog");
                            Console.WriteLine("[2] aby przejsc do strony glownej");
                            Console.WriteLine("[3] aby zakonczyc program");
                            key = Console.ReadKey();
                            wybor_opcji = key.KeyChar;
                            Console.Clear();
                            int np = 1;
                            switch (wybor_opcji)
                            {
                                case '1':
                                    {
                                        try { inwentarz = sklep.getlista(); }
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
                        //Do testow usuwam haslo zeby nie trzeba bylo za kazdym razem wprowadzac
                        /*Console.WriteLine("Wprowadz haslo:");
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
                        }*/
                        Console.WriteLine("Pomyslnie zalogowano jako pracownik");
                        Console.Clear();
                        DzialDruku DD = new DzialDruku();
                        DzialProgramowy DP = new DzialProgramowy();
                        DzialHandlu DH = new DzialHandlu();
                        Drukarnie drukarnie = new Drukarnie();
                        while (wybor_logowania == '2')
                        {
                            Console.WriteLine("Wybierz: ");
                            Console.WriteLine("[1] dodaj autora");
                            Console.WriteLine("[2] przegladaj dostepnych autorow");
                            Console.WriteLine("[3] usun autora");
                            Console.WriteLine("[4] podpisz umowe z autorem");
                            Console.WriteLine("[5] zlecenie o przygotowanie konkretnej pozycji");
                            Console.WriteLine("[6] przegladaj podpisane umowy ");
                            Console.WriteLine("[7] rozwiazanie umowy");
                            Console.WriteLine("[8] wroc do strony logowania");
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
                                        try { DP.DodajAutora(autor); }
                                        catch (AutorJestNaLiscie AJNL)
                                        {
                                            Console.WriteLine(AJNL.Message);
                                            Thread.Sleep(1000);
                                            Console.Clear();
                                            break;
                                        }
                                        Console.WriteLine("Pomyslnie dodano autora");
                                        Thread.Sleep(800);
                                        Console.Clear();
                                        break;
                                    }
                                case '2':
                                    {
                                        ArrayList autorzy = new ArrayList();
                                        int nr = 1;
                                        try { autorzy = DP.getAutor(); }
                                        catch (PustaListaException PL)
                                        {
                                            Console.WriteLine(PL.Message);
                                            Thread.Sleep(700);
                                            Console.Clear();
                                            break;
                                        }
                                        foreach (Autor la in autorzy)
                                        {
                                            Console.WriteLine("[" + nr + "] autor: " + la.Imie + " " + la.Nazwisko);
                                            nr++;
                                        }
                                        Console.WriteLine("Wcisnij dowolny przycisk aby kontunuowac");
                                        Console.ReadLine();
                                        Console.Clear();
                                        break;
                                    }
                                case '3':
                                    ArrayList autorzy1 = new ArrayList();
                                    int i = 1;
                                    int numer_autora;
                                    try { autorzy1 = DP.getAutor(); }
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
                                    numer_autora = int.Parse(Console.ReadLine());
                                    i = 0;
                                    while (numer_autora >= 0 && numer_autora < autorzy1.Count)
                                    {
                                        //BLAD
                                        foreach (Autor autor in autorzy1)
                                        {
                                            if (numer_autora - 1 == i)
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
                                    Console.WriteLine("[3] wroc do menu glownego");
                                    key = Console.ReadKey();
                                    wybor_umowy = key.KeyChar;
                                    while (true)
                                    {
                                        if (wybor_umowy == '1' || wybor_umowy == '2' || wybor_umowy == '3') break;
                                        Console.WriteLine("Niepoprawnie wybrana opcja. Sprobuj jeszcze raz");
                                        key = Console.ReadKey();
                                        wybor_umowy= key.KeyChar;
                                    }
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
                                                try { DP.UmowaOPrace(dlugosc_umowy, autor1); }
                                                catch (AutorMaUmowe AMU)
                                                {
                                                    Console.WriteLine(AMU.Message);
                                                    Thread.Sleep(1000);
                                                    Console.Clear();
                                                    break;
                                                }
                                                Console.WriteLine("Umowa o prace zostala zawarta pomyslnie");
                                                Thread.Sleep(1000);
                                                Console.Clear();
                                                break;
                                            }
                                        case '2':
                                            {
                                                //do poprawy
                                                Console.Clear();
                                                Console.WriteLine("Podaj imie autora");
                                                imie = Console.ReadLine();
                                                Console.WriteLine("Podaj nazwisko autora");
                                                nazwisko = Console.ReadLine();
                                                Console.WriteLine("Podaj adres email autora");
                                                mail = Console.ReadLine();
                                                //metoda zdefiniowana wyzej
                                                wybor_ksiazki = WyborKsiazki();
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
                                                break;
                                            }
                                        case '3':
                                            { Console.Clear(); break; }
                                            //Thread.Sleep(2000);
                                            //Console.Clear();
                                    }
                                    break;
                                case '5':
                                    char wk, zk;
                                    int ia = 1, wa;
                                    Console.WriteLine("Co chcesz zlecic");
                                    Console.WriteLine("[1] ksiazke");
                                    Console.WriteLine("[2] czasopismo");
                                    Console.WriteLine("[3] wroc do poprzedniej strony wyboru");
                                    key = Console.ReadKey();
                                    zk = key.KeyChar;
                                    ArrayList umowy = new ArrayList();
                                    while (true)
                                    {
                                        if (zk == '1' || zk == '2' || zk == '3') break;
                                        Console.WriteLine("Niepoprawnie wybrana opcja. Sprobuj jeszcze raz");
                                        key = Console.ReadKey();
                                        zk = key.KeyChar;
                                    }
                                    if (zk == '3') { Console.Clear(); break; }
                                    else if (zk == '1')
                                    {
                                        try { umowy = DP.getUmowy(); }
                                        catch (PustaListaException PL)
                                        {
                                            Console.WriteLine(PL.Message);
                                            Thread.Sleep(1200);
                                            Console.Clear();
                                            break;
                                        }
                                        //metody zdefiniowana na poczatku
                                        wk = WyborKsiazki();
                                        Console.Clear();
                                        wa = WyborAutora(umowy);
                                        Console.WriteLine("Podaj tytul:");
                                        tytul = Console.ReadLine();
                                        ia = 1;
                                        foreach (Umowy uop in umowy)
                                        {
                                            if (uop is UmowyOPrace)
                                            {
                                                if (wa == ia) 
                                                {
                                                    Console.WriteLine(DP.Zlecenie(wk, uop.Autor, zk, tytul, DH));
                                                    Thread.Sleep(1200);
                                                    Console.Clear();
                                                    break;
                                                }
                                                ia++;
                                            }
                                        }
                                    }
                                    else if (zk == '2')
                                    {
                                        try { umowy = DP.getUmowy(); }
                                        catch (PustaListaException PL)
                                        {
                                            Console.WriteLine(PL.Message);
                                            Thread.Sleep(1200);
                                            Console.Clear();
                                            break;
                                        }
                                        //metody zdefiniowana na poczatku
                                        Console.WriteLine("Jaki magazyn chcesz zlecic: ");
                                        Console.WriteLine("[1] miesiecznik");
                                        Console.WriteLine("[2] tygodnik");
                                        Console.WriteLine("[3] wroc do menu glownego");
                                        key = Console.ReadKey();
                                        wa = key.KeyChar;
                                        while (true)
                                        {
                                            if (zk == '1' || zk == '2' || zk == '3') break;
                                            Console.WriteLine("Niepoprawnie wybrana opcja. Sprobuj jeszcze raz");
                                            key = Console.ReadKey();
                                            zk = key.KeyChar;
                                        }
                                        if (wa == '3') { Console.Clear(); break; }
                                        Console.Clear();
                                        wa = WyborAutora(umowy);
                                        Console.WriteLine("Podaj tytul:");
                                        tytul = Console.ReadLine();
                                        ia = 1;
                                        foreach (Umowy uop in umowy)
                                        {
                                            if (uop is UmowyOPrace)
                                            {
                                                if (wa == ia) 
                                                {
                                                    Console.WriteLine(DP.Zlecenie(wa, uop.Autor, zk, tytul, DH));
                                                    Thread.Sleep(1200);
                                                    Console.Clear();
                                                    break; 
                                                }
                                                ia++;
                                            }
                                        }
                                    }
                                    break;
                                case '6':
                                    try { umowy = DP.getUmowy(); }
                                    catch (PustaListaException PL)
                                    {
                                        Console.WriteLine(PL.Message);
                                        Thread.Sleep(700);
                                        Console.Clear();
                                        break;
                                    }
                                    Console.WriteLine("Autorzy na umowie o prace:");
                                    int number = 1;
                                    foreach (Umowy uop in umowy)
                                    {
                                        if (uop is UmowyOPrace)
                                        {
                                            Console.WriteLine("[" + number + "] autor: " + uop.getAutor);
                                            number++;
                                        }

                                    }
                                    Console.WriteLine("Autorzy na umowie o dzielo:");
                                    number = 1;
                                    foreach (Umowy uop in umowy)
                                    {
                                        if (uop is UmowyODzielo)
                                        {
                                            Console.WriteLine("[" + number + "] autor: " + uop.getAutor);
                                            number++;
                                        }

                                    }
                                    Console.WriteLine("Wcisnij dowolny przycisk aby kontunuowac");
                                    Console.ReadLine();
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
                                            Console.WriteLine("[" + x + "]" + uop.getAutor + " na umowie o prace");
                                        }
                                        else if (uop is UmowyODzielo)
                                        {
                                            Console.WriteLine("[" + x + "]" + uop.getAutor + " na umowie o dzielo ");
                                        }
                                        x++;
                                    }
                                    Console.WriteLine("Z ktorym autorem chcesz rozwiazac umowe: ");
                                    ru = int.Parse(Console.ReadLine());
                                    while (ru < 0 || ru > x)
                                    {
                                        Console.WriteLine("Niepoprawy wybor. Sprobuj jeszcze raz:");
                                        ru = int.Parse(Console.ReadLine());
                                    }
                                    x = 1;
                                    ArrayList rozwiazanie1 = new ArrayList();
                                    try { rozwiazanie1 = DP.getUmowy(); }
                                    catch (PustaListaException PL)
                                    {
                                        Console.WriteLine(PL.Message);
                                        Thread.Sleep(700);
                                        Console.Clear();
                                        break;
                                    }
                                    foreach (Umowy uop in rozwiazanie1)
                                    {
                                        if (ru == x)
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
                                    return;
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
                case '3': { return; }
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