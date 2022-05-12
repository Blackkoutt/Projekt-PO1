using System;
using System.IO;
using Wydawnictwo;
using Dzialy;
using RodzajeUmow;
using Drukarnia;
// w programie powinno byc zapisywanie do pliku i odczytywanie z pliku na liste (tam gdzie sa uzyte jakiekolwiek listy)
class Autor
{
    private String Imie, Nazwisko, email;
    public Autor(String Imie, String Nazwisko, String email) { this.Imie = Imie; this.Nazwisko = Nazwisko; this.email = email; }
    //dodac konstruktor bezparametrowy ew aby nie bylo problemu w konstruktorze Publikacje (string)
}
namespace Wydawnictwo
{
    abstract class Publikacje
    {
        protected Autor autor;
        protected String tytul;
        private int ilosc;
        public Publikacje(Autor autor, String tytul) { this.tytul = tytul; this.autor = autor; }
        //jesli nie to co w klasie wyzej to ew dodac autora do tego konstruktora 
        public Publikacje(String tytul) { this.tytul = tytul; }
        public int Ilosc { get { return ilosc; } }
        public String Tytul { get { return tytul; } }
        public void setilosc(int ilosc) { this.ilosc = ilosc; }
    }
    abstract class Ksiazka : Publikacje
    {
        public Ksiazka(Autor autor, String tytul) : base(autor, tytul) { }
    }
    class Sensacyjne : Ksiazka { public Sensacyjne(Autor autor, String tytul) : base(autor, tytul) { } }
    class Albumy : Ksiazka { public Albumy(Autor autor, String tytul) : base(autor, tytul) { } }
    class Fantasy : Ksiazka { public Fantasy(Autor autor, String tytul) : base(autor, tytul) { } }
    class Kryminalistyczne : Ksiazka { public Kryminalistyczne(Autor autor, String tytul) : base(autor, tytul) { } }
    class Romanse : Ksiazka { public Romanse(Autor autor, String tytul) : base(autor, tytul) { } }
    class Inne : Ksiazka { public Inne(Autor autor, String tytul) : base(autor, tytul) { } }
    abstract class Czasopismo : Publikacje
    {
        public Czasopismo(String tytul) : base(tytul) { }
    }
    class Miesiecznik : Czasopismo { public Miesiecznik(Autor autor, String tytul) : base(tytul) { } }
    class Tygodnik : Czasopismo { public Tygodnik(Autor autor, String tytul) : base(tytul) { } }
}
namespace Dzialy
{
    class DzialProgramowy
    {
        private List<Autor> ListaAutorow = new List<Autor>();
        private List<Umowy> ListaUmow = new List<Umowy>();
        public void UmowaOPrace(double dlugosc, Autor autor)
        {
            Umowy umowa = new UmowyOPrace(dlugosc, autor);
            ListaUmow.Add(umowa);
            ListaAutorow.Add(autor);
        }
        //jesli autor ma juz umowe to nie dodajemy go do listy autorow i do listy umow
        //tak samo wyzej 
        public void UmowaODzielo(Autor autor, Publikacje publikacja)
        {
            Umowy umowa = new UmowyODzielo(autor, publikacja);
            ListaUmow.Add(umowa);
            ListaAutorow.Add(autor);
        }
        //usuniecie z listy umow obiektu ktory ma danego autora ew usuniecie tez tego obiektu
        public void RozwiazanieUmowy(Autor autor)
        {
            foreach (UmowyOPrace umowa in ListaUmow) 
            {
                if (umowa.Autor == autor) { ListaUmow.Remove(umowa); }
            }
            foreach (UmowyODzielo umowa in ListaUmow)
            {
                if (umowa.Autor == autor) { ListaUmow.Remove(umowa); }
            }
            foreach (Autor autorr in ListaAutorow)
            {
                if (autorr == autor) {ListaAutorow.Remove(autorr);}
            }
        }
        //tu powinien byc uzyty tylko konstruktor jednoparametrowy publikacja(tytul)
        // jesli autor ma umowe o prace to moze zrealizowac zlecenie
        // jesli autor nie ma umowy o prace to nie moze zrealizowac zlecenia musi najpiew posiadac umowe
        // jesli autor ma umowe o prace to wywoluje sie konsturktor ale danej publikacji
        // trzeba najpierw sprawdzic typ danej publikacji
        // dodanie do listy publikacji ale najpierw publikacja musi zostac wydrukowana
        public void Zlecenie(Autor autor, Publikacje publikacje) 
        {
            foreach (UmowyOPrace umowa in ListaUmow)
            {
                if (umowa.Autor == autor) 
                {

                }
            }
        }
        public List<Umowy> Umowy { get { return ListaUmow; } }
        public static Boolean WyborDrukarni(int ilosc, Publikacje publikacje) 
        {
            if (publikacje is Albumy) { return Drukarnie.DrukujDobrze(ilosc, publikacje); }
            else { return Drukarnie.DrukujNormalnie(ilosc, publikacje); }
        }
        public List<Autor> Autor { get { return ListaAutorow; } }
        //dodaj do pilku i dodaj na liste 
        public void DodajAutora(Autor autor) { ListaAutorow.Add(autor); }
        //tu lepiej zeby byla metoda ktora usunie wszystkie wystapienia
        public void UsunAutora(Autor autor) { ListaAutorow.Remove(autor); }
    }
    class DzialHandlu
    {
        private List<Publikacje> ListaPublikacji = new List<Publikacje>();
        public static void ZlecenieDruku(int ilosc, Publikacje publikacje) 
        {
            if (DzialProgramowy.WyborDrukarni(ilosc, publikacje))
                Console.WriteLine("Pomyslnie wydrukowano " + publikacje.Tytul+" w nastepujacej liczbie egzemplarzy: " + ilosc);
        }
        //dodaj do listy publikacji jesli drukarnia zwrocila true po wydrukowaniu
        public void DodajDoListy(Publikacje publikacje) { ListaPublikacji.Add(publikacje); }
        //ew oddzielna klasa sklep
        public void ZlecenieKupna(int ilosc, Publikacje publikacje) { }
        public List<Publikacje> Katalog { get { return ListaPublikacji; } }
    }
    class DzialDruku
    {
        //pisanie do pliku
        //jesli publikacja jest juz na liscie to zwiekszenie jej ilosci 
        //jesli publikacja nie jest na liscie to zapisanie do pliku i 
        public static void AktualizacjaPublikacji(Publikacje publikacje,int ilosc ) { }
    }
}
namespace RodzajeUmow
{
    abstract class Umowy
    {
        //dodac cos do konstruktorow
        protected Autor autor;
        protected double dlugosc;
        protected Publikacje publikacja;
        public Umowy(double dlugosc, Autor autor) { this.autor = autor; this.dlugosc = dlugosc; }
        public Umowy(Autor autor, Publikacje publikacja) { this.autor = autor; this.publikacja = publikacja; }
        public Autor Autor { get { return autor; } }
    }
    class UmowyOPrace : Umowy { public UmowyOPrace(double dlugosc, Autor autor) : base(dlugosc, autor) { } }
    class UmowyODzielo : Umowy { public UmowyODzielo(Autor autor, Publikacje publikacja) : base(autor, publikacja) { } }
}
namespace Drukarnia
{
    class Drukarnie
    {
        public static Boolean DrukujDobrze(int ilosc, Publikacje publikacje) 
        {
            DzialDruku.AktualizacjaPublikacji(publikacje,ilosc);
            return true;
        }
        public static Boolean DrukujNormalnie(int ilosc, Publikacje publikacje) 
        {
            DzialDruku.AktualizacjaPublikacji(publikacje,ilosc);
            return true;
        }
    }
}
