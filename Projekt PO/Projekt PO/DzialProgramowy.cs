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
        private List<Tuple<UmowyODzielo?, UmowyOPrace?>> ListaUmow = new List<Tuple<UmowyODzielo?, UmowyOPrace?>>();

        //zamiana na boola żeby dostać komunikat czy umowa została zawarta (w obu umowach)
        public bool UmowaOPrace(double dlugosc, Autor autor)
        {  
            if (this.UmowaNaLiscie(autor)) 
                { return false; }
            
            if (!this.AutorNaLiscie(autor)) 
                { ListaAutorow.Add(autor); }

            UmowyOPrace umowa = new UmowyOPrace(dlugosc, autor);
            Tuple<UmowyODzielo?, UmowyOPrace?> umowyTyp = new Tuple<UmowyODzielo?, UmowyOPrace?>(null, umowa);
            ListaUmow.Add(umowyTyp);

            return true;
        }
        //jesli autor ma juz umowe to nie dodajemy go do listy autorow i do listy umow
        //tak samo wyzej 

        //po wykonaniu zlecenia umowa powinna zostać rozwiązana czyli teoretycznie jeśli
        //potrzebujemy i tak publikacji do tego to można od razu ją usunąć i tworzenie jest bezsensowne
        //więc albo można ją usuwać ręcznie albo od razu po utworzeniu ewentualnie dodać listę z historią umów
        public bool UmowaODzielo(Autor autor, Publikacje publikacja)
        {
            if (this.UmowaNaLiscie(autor)) 
                { return false; }
            
            if (!this.AutorNaLiscie(autor)) 
                { ListaAutorow.Add(autor); }

            UmowyODzielo umowa = new UmowyODzielo(autor, publikacja);
            Tuple<UmowyODzielo?, UmowyOPrace?> umowyTyp = new Tuple<UmowyODzielo?, UmowyOPrace?>(umowa, null);
            ListaUmow.Add(umowyTyp);

            return true;
        }

        //usuniecie z listy umow obiektu ktory ma danego autora ew usuniecie tez tego obiektu
        public void RozwiazanieUmowy(Autor autor)
        {
            foreach (Tuple<UmowyODzielo?, UmowyOPrace?> umowyTyp in ListaUmow)
            {
                if(umowyTyp.Item1 != null && autor.Equals(umowyTyp.Item1.Autor))
                    { ListaUmow.Remove(umowyTyp); break; }

                if(umowyTyp.Item2 != null && autor.Equals(umowyTyp.Item2.Autor))
                    { ListaUmow.Remove(umowyTyp); break; }
            }

            /*
            foreach (UmowyOPrace umowa in ListaUmow[].Item1)
            {
                if (umowa.Autor == autor)
                { ListaUmow.Remove(umowa); }
            }
            foreach (UmowyODzielo umowa in ListaUmow)
            {
                if (umowa.Autor == autor)
                { ListaUmow.Remove(umowa); }
            }
            */

            foreach (Autor autorr in ListaAutorow)
            {
                if (autor.Equals(autorr)) { ListaAutorow.Remove(autorr); }
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
            /*foreach (UmowyOPrace umowa in ListaUmow)
            {
                if (umowa.Autor == autor)
                {

                }
            }*/
        }
        

        public static Boolean WyborDrukarni(int ilosc, Publikacje publikacje)
        {
            if (publikacje is Albumy)
            {
                return Drukarnie.DrukujDobrze(ilosc, publikacje);
            }
            else
            {
                return Drukarnie.DrukujNormalnie(ilosc, publikacje);
            }
        }

        public List<Tuple<UmowyODzielo?, UmowyOPrace?>> getUmowy() { return ListaUmow; }

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
        
        public bool UmowaNaLiscie(Autor autor)
        {
            foreach (Tuple<UmowyODzielo?, UmowyOPrace?> umowyTyp in ListaUmow)
            {
                if (umowyTyp.Item1 != null && autor.Equals(umowyTyp.Item1.Autor))
                { return true; }

                if (umowyTyp.Item2 != null && autor.Equals(umowyTyp.Item2.Autor))
                { return true; }
            }
            return false;

            /*
            foreach (UmowyODzielo umowa in ListaUmow)
            {
                if (autor.Equals(umowa.Autor)) { return true; }
            }
            return false;*/
        }
        /*
        public bool UmowaOPraceNaLiscie(Autor autor)
        {            
            foreach (UmowyOPrace umowa in ListaUmow)
            {
                if (autor.Equals(umowa.Autor)) { return true; }
            }
            return false;
        }*/

    }
}
