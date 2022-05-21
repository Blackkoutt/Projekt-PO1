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

        //zamiana na boola żeby dostać komunikat czy umowa została zawarta (w obu umowach)
        public bool UmowaOPrace(double dlugosc, Autor autor)
        {  
            if (this.UmowaODzieloNaLiscie(autor) || this.UmowaOPraceNaLiscie(autor)) 
                { return false; }
            
            if (!this.AutorNaLiscie(autor)) 
                { ListaAutorow.Add(autor); }

            UmowyOPrace umowa = new UmowyOPrace(dlugosc, autor);
            ListaUmow.Add(umowa);

            return true;
        }
        //jesli autor ma juz umowe to nie dodajemy go do listy autorow i do listy umow
        //tak samo wyzej 

        //po wykonaniu zlecenia umowa powinna zostać rozwiązana czyli teoretycznie jeśli
        //potrzebujemy i tak publikacji do tego to można od razu ją usunąć i tworzenie jest bezsensowne
        //więc albo można ją usuwać ręcznie albo od razu po utworzeniu ewentualnie dodać listę z historią umów
        public bool UmowaODzielo(Autor autor, Publikacje publikacja)
        {
            if (this.UmowaODzieloNaLiscie(autor) || this.UmowaOPraceNaLiscie(autor)) 
                { return false; }
            
            if (!this.AutorNaLiscie(autor)) 
                { ListaAutorow.Add(autor); }

            UmowyODzielo umowa = new UmowyODzielo(autor, publikacja);
            ListaUmow.Add(umowa);

            return true;
        }

        //usuniecie z listy umow obiektu ktory ma danego autora ew usuniecie tez tego obiektu
        public void RozwiazanieUmowy(Autor autor)
        {
            for (int i = 0; i < ListaUmow.Count; i++)
            {
                //jeśli za duży tasiemiec dodać var autorr = ListaUmow.Cast<UmowyOPrace>().ToList()[i].Autor; i drugie dla UmowyODzielo
                if (ListaUmow[i] is UmowyOPrace && ListaUmow.Cast<UmowyOPrace>().ToList()[i].Autor != null && autor.Equals(ListaUmow.Cast<UmowyOPrace>().ToList()[i].Autor)) 
                    { ListaUmow.Remove(ListaUmow[i]); }

                if (ListaUmow[i] is UmowyODzielo && ListaUmow.Cast<UmowyODzielo>().ToList()[i].Autor != null && autor.Equals(ListaUmow.Cast<UmowyODzielo>().ToList()[i].Autor)) 
                    { ListaUmow.Remove(ListaUmow[i]); }
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
        public bool Zlecenie(Autor autor, String rodzaj/*Publikacje publikacja*/)
        {
            //Niestety jeśli wszędzie mamy namespace Wydawnictwo
            //to jeśli jakiś idiota będzie chciał napisać książkę w kategorii Autor
            //wywali błąd bo autor to nie publikacja
            List<String> zakazaneNazwy = new List<String>() { "Autor", "Umowy", "UmowyODzielo", "UmowyOPrace", "DzialHandlu", "Drukarnie", "DzialDruku" };
            
            if(zakazaneNazwy.Contains(rodzaj))
                { Console.WriteLine("Niepoprawny rodzaj publikacji\n"); return false; }

            if(UmowaOPraceNaLiscie(autor)) 
            {
                String nazwaTypu = "Wydawnictwo." + rodzaj;
                Type typ = Type.GetType(nazwaTypu);
                
                if (typ == null)
                    typ = Type.GetType("Wydawnictwo.Inne");

                Publikacje pub = Activator.CreateInstance(typ, autor, "tytul") as Publikacje;

                if (pub == null)
                    { Console.WriteLine("Nie udało się stworzyć zlecenia\n"); return false; }

                DzialHandlu.DodajDoListy(pub);
                return true; 
            }
            return false;
        }
        

        public static Boolean WyborDrukarni(int ilosc, Publikacje publikacje)
        {
            if (publikacje is Albumy) 
                { return Drukarnie.DrukujDobrze(ilosc, publikacje); }
            else
                { return Drukarnie.DrukujNormalnie(ilosc, publikacje); }
        }

        public ArrayList getUmowy() { return ListaUmow; }

        public ArrayList getAutor() { return ListaAutorow; }

        //dodaj do pliku i dodaj na liste 
        public void DodajAutora(Autor autor)
        {
            ListaAutorow.Add(autor);
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
