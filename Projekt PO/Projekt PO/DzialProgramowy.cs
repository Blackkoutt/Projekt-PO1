using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Wydawnictwo
{
    class DzialProgramowy
    {
        private ArrayList ListaAutorow = new ArrayList();
        private ArrayList ListaUmow = new ArrayList();
        // private DzialHandlu DH;
        //private Drukarnie DR; //= new Drukarnie();
      // public DzialProgramowy() { DH = new DzialHandlu(); DR = new Drukarnie(); }
        //zamiana na boola żeby dostać komunikat czy umowa została zawarta (w obu umowach)

        public void UmowaOPrace(double dlugosc, Autor autor)
        {
            if (UmowaODzieloNaLiscie(autor) || UmowaOPraceNaLiscie(autor)) throw new AutorMaUmowe("Autor ma juz zawarta umowe o prace lub dzielo");
            if (!AutorNaLiscie(autor))
            { ListaAutorow.Add(autor); }

            UmowyOPrace umowa = new UmowyOPrace(dlugosc, autor);
            ListaUmow.Add(umowa);
        }

        //jesli autor ma juz umowe to nie dodajemy go do listy autorow i do listy umow
        //tak samo wyzej 

        //po wykonaniu zlecenia umowa powinna zostać rozwiązana czyli teoretycznie jeśli
        //potrzebujemy i tak publikacji do tego to można od razu ją usunąć i tworzenie jest bezsensowne
        //więc albo można ją usuwać ręcznie albo od razu po utworzeniu ewentualnie dodać listę z historią umów
        public void UmowaODzielo(Autor autor, Publikacje publikacja)
        {
            if (UmowaODzieloNaLiscie(autor) || UmowaOPraceNaLiscie(autor)) throw new AutorMaUmowe("Autor ma juz zawarta umowe o prace lub dzielo");
            if (!AutorNaLiscie(autor))
            { ListaAutorow.Add(autor); }

            UmowyODzielo umowa = new UmowyODzielo(autor, publikacja);
            ListaUmow.Add(umowa);
        }

        //usuniecie z listy umow obiektu ktory ma danego autora ew usuniecie tez tego obiektu
        public void RozwiazanieUmowy(Autor autor)
        {
            for (int i = 0; i < ListaUmow.Count; i++)
            {
                //jeśli za duży tasiemiec dodać var autorr = ListaUmow.Cast<UmowyOPrace>().ToList()[i].Autor; i drugie dla UmowyODzielo
                if (ListaUmow[i] is UmowyOPrace && ListaUmow.Cast<UmowyOPrace>().ToList()[i].Autor != null && autor.Equals(ListaUmow.Cast<UmowyOPrace>().ToList()[i].Autor))
                { ListaUmow.Remove(ListaUmow[i]); break; }
                //BLAD
                if (ListaUmow[i] is UmowyODzielo && ListaUmow.Cast<UmowyODzielo>().ToList()[i].Autor != null && autor.Equals(ListaUmow.Cast<UmowyODzielo>().ToList()[i].Autor))
                { ListaUmow.Remove(ListaUmow[i]); break; }
            }

            //po rozwiazaniu umowy napisane przez autora książki powinny być dalej sprzedawane więc autor też powinien zostać
            /*foreach (Autor autorr in ListaAutorow)
            {
                if (autor.Equals(autorr)) { ListaAutorow.Remove(autorr); }
            }*/
        }

        // jesli autor ma umowe o prace to moze zrealizowac zlecenie
        // jesli autor nie ma umowy o prace to nie moze zrealizowac zlecenia musi najpiew posiadac umowe
        // jesli autor ma umowe o prace to wywoluje sie konsturktor ale danej publikacji
        // trzeba najpierw sprawdzic typ danej publikacji
        // dodanie do listy publikacji ale najpierw publikacja musi zostac wydrukowana

        // to jest zlecenie napisania książki więc powinno tylko dostawać informacje
        // potrzebne do stworzenia publikacji i dodać ją na liste

        /*public String Zlecenie(int Wybor_Ksiazki, Autor autor,char Wybor_Rodzaju,string tytul,DzialHandlu DH)
        {
            if (Wybor_Rodzaju == '1' || Wybor_Rodzaju == '2')
            {
                switch (Wybor_Rodzaju)
                {
                    case '1':
                        {
                            switch (Wybor_Ksiazki)
                            {
                                case '1':
                                    {
                                        DH.DodajDoListy(new Sensacyjne(autor, tytul));
                                        return "Zlecenie zostalo poprawnie stworzone";
                                    }
                                case '2':
                                    {
                                        DH.DodajDoListy(new Kryminalistyczne(autor, tytul));
                                        return "Zlecenie zostalo poprawnie stworzone";
                                    }
                                case '3':
                                    {
                                        DH.DodajDoListy(new Fantasy(autor, tytul));
                                        return "Zlecenie zostalo poprawnie stworzone";
                                    }
                                case '4':
                                    {
                                        DH.DodajDoListy(new Romanse(autor, tytul));
                                        return "Zlecenie zostalo poprawnie stworzone";
                                    }
                                case '5':
                                    {
                                        DH.DodajDoListy(new Albumy(autor, tytul));
                                        return "Zlecenie zostalo poprawnie stworzone";
                                    }
                                case '6':
                                    {
                                        DH.DodajDoListy(new Inne(autor, tytul));
                                        return "Zlecenie zostalo poprawnie stworzone";
                                    }
                            }
                            break;
                        }
                    case '2':
                        {
                            switch (Wybor_Ksiazki)
                            {
                                case '1':
                                    {
                                        DH.DodajDoListy(new Miesiecznik(tytul));
                                        return "Zlecenie zostalo poprawnie stworzone";
                                    }
                                case '2':
                                    {
                                        DH.DodajDoListy(new Tygodnik(tytul));
                                        return "Zlecenie zostalo poprawnie stworzone";
                                    }

                            }
                            break;
                        }
                }
                return "";
            }
            else return "Wystapil blad przy realizacji zlecenia";
        }*/
        public bool Zlecenie(Autor? autor, String? rodzaj, String? tytul, DzialHandlu DH)
        {
            //Lista nazw klas nieporządanych przy tworzeniu obiektu publikacji
            List<String> zakazaneNazwy = new List<String>() { "Autor", "Umowy", "UmowyODzielo", "UmowyOPrace", "DzialHandlu", "Drukarnie", "DzialDruku", "Ksiazka", "Czasopismo", "Publikacje", "Sklep" };

            if (autor == null) { autor = new Autor(); }
            if (rodzaj == null) rodzaj = "Inne";
            if (tytul == null) tytul = "Brak Tytulu";

            if (zakazaneNazwy.Contains(rodzaj))//można wyjątek ale ja trochę ich nie ogarniam
            { Console.WriteLine("Niepoprawny rodzaj publikacji\n"); return false; }

            if (UmowaOPraceNaLiscie(autor))
            {
                String nazwaTypu = "Wydawnictwo." + rodzaj;
                Type typ = Type.GetType(nazwaTypu);

                if (typ == null)
                    typ = Type.GetType("Wydawnictwo.Inne");

                Publikacje pub = Activator.CreateInstance(typ, autor, tytul) as Publikacje;

                if (pub == null)
                { Console.WriteLine("Nie udało się stworzyć zlecenia\n"); return false; }

                DH.DodajDoListy(pub);
                return true;
            }
            return false;
        }


        public static Boolean WyborDrukarni(int ilosc, Publikacje publikacje, DzialHandlu DH)
        {
            if (publikacje is Albumy)
            { return Drukarnie.DrukujDobrze(ilosc, publikacje, DH); }
            else
            { return Drukarnie.DrukujNormalnie(ilosc, publikacje, DH); }
        }

        public ArrayList getUmowy() 
        {
            if (ListaUmow.Count != 0) { return ListaUmow; }
            else throw new PustaListaException("Lista umow jest pusta");
            
        }

        public ArrayList getAutor() 
        {
            if(ListaAutorow.Count!=0) return ListaAutorow;
            throw new PustaListaException("Brak jakichkolwiek autorow na liscie autorow");
        }

        //dodaj do pliku i dodaj na liste 
        public void DodajAutora(Autor autor)
        {
            if (!AutorNaLiscie(autor)) { ListaAutorow.Add(autor); }
            else throw new AutorJestNaLiscie("Dany autor jest juz na liscie autorow");
        }
        //tu lepiej zeby byla metoda ktora usunie wszystkie wystapienia
        public void UsunAutora(Autor autor)
        {
            ListaAutorow.Remove(autor);
        }

        public bool AutorNaLiscie(Autor autor)
        {
            foreach (Autor autorr in ListaAutorow)
            {
                if (autor.Equals(autorr)) { return true; }
            }
            return false;
        }

        public bool UmowaOPraceNaLiscie(Autor autor)
        {
            for (int i = 0; i < ListaUmow.Count; i++)
            {
                if (ListaUmow[i] is UmowyOPrace && autor.Equals(ListaUmow.Cast<UmowyOPrace>().ToList()[i].Autor)) { return true; }
            }
            return false;
        }

        public bool UmowaODzieloNaLiscie(Autor autor)
        {
            for (int i = 0; i < ListaUmow.Count; i++)
            {
                if (ListaUmow[i] is UmowyODzielo && autor.Equals(ListaUmow.Cast<UmowyODzielo>().ToList()[i].Autor)) { return true; }
            }
            return false;
        }

    }
}