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
            foreach (Umowy u in ListaUmow)
            {
                if (u.Autor == autor)
                { ListaUmow.Remove(u); }
            }
            /*
             foreach (UmowyODzielo umowa in ListaUmow)
            {
                if (umowa.Autor == autor)
                { ListaUmow.Remove(umowa); }
            }*/
            foreach (Autor autorr in ListaAutorow)
            {
                if (autorr == autor) { ListaAutorow.Remove(autorr); }
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
        public ArrayList getUmowy() { return ListaUmow; }
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
        public ArrayList getAutor()
        {
            return ListaAutorow;
        }
        //dodaj do pilku i dodaj na liste 
        public void DodajAutora(Autor autor)
        {
            ListaAutorow.Add(autor);
        }
        //tu lepiej zeby byla metoda ktora usunie wszystkie wystapienia
        public void UsunAutora(Autor autor)
        {
            ListaAutorow.Remove(autor);
        }
    }
}
