using System;
using System.IO;
using Wydawnictwo;
using System.Collections;
using System.Threading;
//Na początku wczytywania z pliku na liste autorów upewnić się że jest autor wydawnictwo i ma umowę o prace
//inaczej zlecenia czasopism nie powiodą się

//Struktura plików:
//Publikacje.txt - imie,nazwisko,tytul,ilosc,typ publikacji
//Umowy.txt - imie,nazwisko,tytul lub dlugosc,typ umowy
//Autorzy.txt - imie,nazwisko,email

class Program
{
    protected static String NAZWA_PLIKU_PUBLIKACJE = "Publikacje.txt";
    protected static String NAZWA_PLIKU_UMOWY = "Umowy.txt";
    protected static String NAZWA_PLIKU_AUTORZY = "Autorzy.txt";

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

    /*
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
    }*/


    public static Autor? WyborAutora(ArrayList? lista_umow)
    {
        if(lista_umow == null) { return null; }

        int id_autora = 1, wybor_autora;
        ArrayList pomocnicza_lista_autorow = new ArrayList();
        Autor autor;
        Console.WriteLine("Wybierz autora:");
        foreach (Umowy uop in lista_umow)
        {
            if (uop is UmowyOPrace)
            {
                Console.WriteLine("[" + id_autora++ + "]" + uop.Autor);
                pomocnicza_lista_autorow.Add(uop.Autor);
            }
        }
        if(pomocnicza_lista_autorow.Count == 0) { return null; }//brak autorów z umową o prace

        wybor_autora = int.Parse(Console.ReadLine());
        while (wybor_autora < 1 && wybor_autora >= id_autora)
        {
            Console.WriteLine("Niepoprawny wybor sprobuj jeszcze raz");
            wybor_autora = int.Parse(Console.ReadLine());
        }
        autor = (Autor)pomocnicza_lista_autorow[wybor_autora-1];
        return autor;
    }


    public static string DoWielkiej(string nazwa)
    {
        if (nazwa == null) return "";
        if (Char.IsUpper(nazwa[0]))
            return nazwa;
        else
            return Char.ToUpper(nazwa[0]) + nazwa.Substring(1);
    }


    public static void Update(DzialHandlu DH, DzialProgramowy DP)
    {
        StreamWriter zapisPublikacje = new StreamWriter(NAZWA_PLIKU_PUBLIKACJE, false);
        StreamWriter zapisUmowy = new StreamWriter(NAZWA_PLIKU_UMOWY, false);
        StreamWriter zapisAutorzy = new StreamWriter(NAZWA_PLIKU_AUTORZY, false);
        ArrayList listaUmow = new();
        ArrayList listaAutorow = new();

        foreach (Publikacje pub in DH.Katalog)
        {
            zapisPublikacje.WriteLine(pub.Autor.Imie + "," + pub.Autor.Nazwisko + "," + pub.Tytul + "," + pub.Ilosc + "," + pub.GetType().Name);
        }

        try { listaUmow = DP.getUmowy(); }//można to poprawić bo ja nie za bardzo jeszcze ogarniam te wyjątki
        catch(PustaListaException PL)
        {
            listaUmow.Clear();
        }

        foreach (Umowy umowa in listaUmow)
        {
            if(umowa is UmowyODzielo)
            {
                UmowyODzielo umowaODzielo = (UmowyODzielo)umowa;
                zapisUmowy.WriteLine(umowa.Autor.Imie + "," + umowa.Autor.Nazwisko + "," + umowaODzielo.Publikacja.Tytul + ',' + umowa.GetType().Name);
            }
            else if(umowa is UmowyOPrace)
            {
                UmowyOPrace umowaOPrace = (UmowyOPrace)umowa;
                zapisUmowy.WriteLine(umowa.Autor.Imie + "," + umowa.Autor.Nazwisko + "," + umowaOPrace.Dlugosc + ',' + umowa.GetType().Name);
            }
            
        }

        try { listaAutorow = DP.getUmowy(); }//tutaj też
        catch (PustaListaException PL)
        {
            listaAutorow.Clear();
        }

        foreach (Autor autor in DP.getAutor())
        {
            zapisAutorzy.WriteLine(autor.Imie + "," + autor.Nazwisko + "," + autor.Email);
        }

        zapisPublikacje.Close();
        zapisUmowy.Close();
        zapisAutorzy.Close();
    }


    public static void Odczyt(DzialHandlu DH, DzialProgramowy DP)
    {
        if(!File.Exists(NAZWA_PLIKU_PUBLIKACJE))
        {
            StreamWriter pustePublikacje = new StreamWriter(NAZWA_PLIKU_PUBLIKACJE);
            pustePublikacje.Close();
        }
        if (!File.Exists(NAZWA_PLIKU_UMOWY))
        {
            StreamWriter pusteUmowy = new StreamWriter(NAZWA_PLIKU_UMOWY);
            pusteUmowy.Close();
        }
        if (!File.Exists(NAZWA_PLIKU_AUTORZY))
        {
            StreamWriter pusteAutorzy = new StreamWriter(NAZWA_PLIKU_AUTORZY);
            pusteAutorzy.Close();
        }

        StreamReader odczytPublikacje = new StreamReader(NAZWA_PLIKU_PUBLIKACJE);
        StreamReader odczytUmowy = new StreamReader(NAZWA_PLIKU_UMOWY);
        StreamReader odczytAutorzy = new StreamReader(NAZWA_PLIKU_AUTORZY);


        string? line;
        while ((line = odczytPublikacje.ReadLine()) != null)
        {
            List<String> zakazaneNazwy = new List<String>() { "Autor", "Umowy", "UmowyODzielo", "UmowyOPrace", "DzialHandlu", "Drukarnie", "DzialDruku", "Ksiazka", "Czasopismo", "Publikacje", "Sklep" };

            string[] s = line.Split(",");
            string imie = s[0];
            string nazwisko = s[1];
            string tytul = s[2];
            int ilosc = Int16.Parse(s[3]);
            string typStr = s[4];
            Autor autor = new Autor(imie, nazwisko);

            typStr = DoWielkiej(typStr);
            if (zakazaneNazwy.Contains(typStr))
            { typStr = "[BezNazwy]"; }

            String nazwaTypu = "Wydawnictwo." + typStr;
            Type? typ = Type.GetType(nazwaTypu);
            if (typ == null)
                typ = Type.GetType("Wydawnictwo.Inne");

            Publikacje publikacje = Activator.CreateInstance(typ, autor, tytul) as Publikacje;
            publikacje.setilosc(ilosc);
            DH.DodajDoListy(publikacje);
        }

        while ((line = odczytUmowy.ReadLine()) != null)
        {
            string[] s = line.Split(",");
            string imie = s[0];
            string nazwisko = s[1];
            string tytul_dlugosc = s[2];
            string typStr = s[3];
            Autor autor = new Autor(imie, nazwisko);

            typStr = DoWielkiej(typStr);
            
            if(typStr == "UmowaODzielo")
            {

                Publikacje? publikacja = DH.SzukajPublikacji(autor, tytul_dlugosc);
                if (publikacja == null)
                    publikacja = new Inne(autor, tytul_dlugosc);

                DP.UmowaODzielo(autor, publikacja, DH);
            }
            else if(typStr == "UmowaOPrace")
            {
                int dlugosc = int.Parse(tytul_dlugosc);
                DP.UmowaOPrace(dlugosc, autor, DH);
            }
        }

        while ((line = odczytAutorzy.ReadLine()) != null)
        {
            string[] s = line.Split(",");
            string imie = s[0];
            string nazwisko = s[1];
            string email = s[2];
            Autor autor = new Autor(imie, nazwisko, email);

            try { DP.DodajAutora(autor); }
            catch (AutorJestNaLiscie) { }//warte uwagi
        }

        odczytPublikacje.Close();
        odczytUmowy.Close();
        odczytAutorzy.Close();
    }

    static void Main(string[] args)
    {
        List<String> zakazaneNazwy = new List<String>() { "Autor", "Umowy", "UmowyODzielo", "UmowyOPrace", "DzialHandlu", "Drukarnie", "DzialDruku", "Ksiazka", "Czasopismo", "Publikacje", "Sklep" };

        char wybor_logowania, wybor_opcji, wybor_umowy;
        int wybor_ksiazki;
        string imie, nazwisko, mail, tytul;
        int ru, rk, x;
        int dlugosc_umowy;
        Type? typUmowy, typPublikacji;

        //Setup
        DzialHandlu DH = new();
        DzialProgramowy DP = new();
        DzialDruku DD = new();
        Drukarnie Drukarnie = new();

        Odczyt(DH, DP);

        try { DP.DodajAutora(new Autor()); }
        catch (AutorJestNaLiscie) { }
        try { DP.UmowaOPrace(100, new Autor(), DH); }
        catch (AutorMaUmowe) { }

        Program.Update(DH, DP);

        Sklep sklep = new Sklep(DH);


        while(true)
        {
            Console.WriteLine("Witaj w wydawnictwie EPress");
            Console.WriteLine("Wybierz sposob logowania:");
            Console.WriteLine();
            Console.WriteLine("1. Zaloguj sie jako klient");
            Console.WriteLine("2. Zaloguj sie jako pracownik");
            Console.WriteLine("3. Zakoncz program");
            ConsoleKeyInfo key = Console.ReadKey();
            wybor_logowania = key.KeyChar;

            switch (wybor_logowania)
            {
                case '1':
                    {
                        Console.Clear();
                        Console.WriteLine("Pomyslnie zalogowano jako klient");
                        Thread.Sleep(900);
                        Console.Clear();
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
                            int np = 1, IK, KK;
                            string kk;
                            switch (wybor_opcji)
                            {
                                case '1':
                                    {
                                        ArrayList inwentarz;
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
                                            Console.WriteLine("[" + np++ + "]" + p.getAutor + " - " + p.GetType().Name);
                                        }
                                        Console.WriteLine("");
                                        Console.WriteLine("Ktora z ksiazek chcesz kupic?");//SKLEP ZLECENIE!!!!!!!!!                                      
                                        kk = Console.ReadLine();
                                        bool success = int.TryParse(kk, out KK);
                                        while (success==false)
                                        {
                                            Console.WriteLine("Podano nieprawidlowa wartosc");
                                            kk = Console.ReadLine();
                                            success = int.TryParse(kk, out KK);
                                        }
    
                                        while (KK < 0 || KK > np)
                                        {
                                            Console.WriteLine("Nie ma takiej ksiazki na liscie. Sprobuj jeszcze raz");
                                            kk = Console.ReadLine();
                                            success = int.TryParse(kk, out KK);
                                            while (success == false)
                                            {
                                                Console.WriteLine("Podano nieprawidlowa wartosc");
                                                kk = Console.ReadLine();
                                                success = int.TryParse(kk, out KK);
                                            }
                                        }
                                        np = 1;
                                        foreach (Publikacje p in inwentarz)
                                        {
                                            if (KK == np)
                                            {
                                                Console.WriteLine(p.getAutor);
                                                Console.WriteLine("Ile ksiazek chcesz kupic?");
                                                kk = Console.ReadLine();
                                                success = int.TryParse(kk, out IK);
                                                while (success == false)
                                                {
                                                    Console.WriteLine("Podano nieprawidlowa wartosc");
                                                    kk = Console.ReadLine();
                                                    success = int.TryParse(kk, out IK);
                                                }
                                                while (IK < 0)
                                                {
                                                    kk = Console.ReadLine();
                                                    success = int.TryParse(kk, out IK);
                                                    while (success == false)
                                                    {
                                                        Console.WriteLine("Podano nieprawidlowa wartosc");
                                                        kk = Console.ReadLine();
                                                        success = int.TryParse(kk, out KK);
                                                    }
                                                }
                                                DH.ZlecenieKupna(IK, p);
                                                Console.WriteLine("Pomyślnie zakupiono ksiazke");
                                                Thread.Sleep(700);
                                                Console.Clear();
                                            }
                                            np++;
                                        }
                                        
                                        break;
                                    }

                                case '2':
                                    wybor_logowania = '2';
                                    break;
                                case '3':
                                    return;
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
                        Thread.Sleep(1+500);
                        Console.Clear();
                        while (wybor_logowania == '2')
                        {
                            Thread.Sleep(500);
                            Console.Clear();
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
                                        ArrayList autorzy;
                                        int nr = 1;
                                        try { autorzy = DP.getAutor(); }
                                        catch (PustaListaException PL)
                                        {
                                            Console.WriteLine(PL.Message);
                                            Thread.Sleep(700);
                                            Console.Clear();
                                            break;
                                        }
                                        foreach (Autor autor in autorzy)
                                        {
                                            Console.WriteLine("[" + nr + "] autor: " + autor.Imie + " " + autor.Nazwisko);
                                            nr++;
                                        }
                                        Console.WriteLine("Wcisnij dowolny przycisk aby kontunuowac");
                                        Console.ReadLine();
                                        Console.Clear();
                                        break;
                                    }
                                case '3':
                                    ArrayList autorzy1;
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
                                    numer_autora = int.Parse(Console.ReadLine());
                                    i = 0;
                                    while (numer_autora >= 0 && numer_autora < autorzy1.Count)
                                    {
                                        //BLAD
                                        foreach (Autor autor in autorzy1)
                                        {
                                            if (numer_autora - 1 == i)
                                            {
                                                DP.UsunAutora(autor, DH);
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
                                    typPublikacji = null;

                                    while (true)
                                    {
                                        if (wybor_umowy == '1' || wybor_umowy == '2' || wybor_umowy == '3') break;
                                        Console.WriteLine("Niepoprawnie wybrana opcja. Sprobuj jeszcze raz");
                                        key = Console.ReadKey();
                                        wybor_umowy = key.KeyChar;
                                    }

                                    Console.Clear();
                                    Console.WriteLine("Podaj imie autora");
                                    imie = Console.ReadLine();
                                    Console.WriteLine("Podaj nazwisko autora");
                                    nazwisko = Console.ReadLine();
                                    Console.WriteLine("Podaj adres email autora");
                                    mail = Console.ReadLine();


                                    if (wybor_umowy == '1')
                                    { 
                                        Console.WriteLine("Podaj dlugosc umowy (w latach)");
                                        dlugosc_umowy = int.Parse(Console.ReadLine());

                                        try { DP.UmowaOPrace(dlugosc_umowy, new Autor(imie, nazwisko, mail), DH); }
                                        catch (AutorMaUmowe AMU)
                                        {
                                            Console.WriteLine(AMU.Message);
                                            Thread.Sleep(1000);
                                            Console.Clear();
                                            break;
                                        }
                                    }
                                    else if (wybor_umowy == '2')
                                    { 
                                        Console.WriteLine("Podaj rodzaj publikacji");
                                        string nazwa_rodzaju = Console.ReadLine();

                                        Console.WriteLine("Podaj tytul publikacji");
                                        string tytulPublikacji = Console.ReadLine();

                                        if (zakazaneNazwy.Contains(nazwa_rodzaju))
                                        { nazwa_rodzaju = "Inne"; }

                                        nazwa_rodzaju = "Wydawnictwo." + DoWielkiej(nazwa_rodzaju);
                                        typPublikacji = Type.GetType(nazwa_rodzaju);

                                        Publikacje publikacja = Activator.CreateInstance(typPublikacji, new Autor(imie, nazwisko, mail), tytulPublikacji) as Publikacje;

                                        DP.UmowaODzielo(new Autor(imie, nazwisko, mail), publikacja, DH);
                                    }
                                    else
                                    { Console.Clear(); break; }


                                    break;
                                /*
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
                                }*/
                                case '5':
                                    Autor? autor_zlecenia;
                                    ArrayList? umowy;

                                    try { umowy = DP.getUmowy(); }
                                    catch(PustaListaException PL)
                                    { umowy = null; }

                                    Console.WriteLine("Co chcesz zlecic (podaj rodzaj lub wpisz \"x\" by wrocic):");
                                    string rodzaj_zlecenia = Console.ReadLine();
                                    rodzaj_zlecenia = DoWielkiej(rodzaj_zlecenia);
                                    if (rodzaj_zlecenia == "X") break;

                                    if (rodzaj_zlecenia == "Tygodnik" || rodzaj_zlecenia == "Miesiecznik")
                                    { autor_zlecenia = null; }
                                    else
                                    { autor_zlecenia = WyborAutora(umowy); }                                    

                                    Console.WriteLine("Podaj tytul");
                                    string tytul_zlecenia = Console.ReadLine();

                                    DP.Zlecenie(autor_zlecenia, rodzaj_zlecenia, tytul_zlecenia, DH);

                                    break;

                                /*case '5':
                                    char wk, zk;
                                    int ia = 1, wa;
                                    Console.WriteLine("Co chcesz zlecic (podaj rodzaj lub wpisz \"x\" by wrocic):");
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
                                    break;*/
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
                                            DP.RozwiazanieUmowy(uop.Autor, DH);
                                            break;
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