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
    

        public void UmowaOPrace(double dlugosc, Autor autor, DzialHandlu DH)
        {
            if (UmowaODzieloNaLiscie(autor) || UmowaOPraceNaLiscie(autor)) throw new AutorMaUmowe("Autor ma juz zawarta umowe o prace lub dzielo");
            if (!AutorNaLiscie(autor))
            { ListaAutorow.Add(autor); }

            UmowyOPrace umowa = new UmowyOPrace(dlugosc, autor);
            ListaUmow.Add(umowa);
            Program.Update(DH, this);
        }

        
        public void UmowaODzielo(Autor autor, Publikacje publikacja, DzialHandlu DH)
        {
            if (UmowaODzieloNaLiscie(autor) || UmowaOPraceNaLiscie(autor)) throw new AutorMaUmowe("Autor ma juz zawarta umowe o prace lub dzielo");
            if (!AutorNaLiscie(autor))
            { ListaAutorow.Add(autor); }

            UmowyODzielo umowa = new UmowyODzielo(autor, publikacja);
            ListaUmow.Add(umowa);
            Program.Update(DH, this);
        }

      
        public void RozwiazanieUmowy(Autor autor, DzialHandlu DH)
        {
            for (int i = 0; i < ListaUmow.Count; i++)
            {
                if (ListaUmow[i] is UmowyOPrace && ListaUmow.Cast<UmowyOPrace>().ToList()[i].Autor != null && autor.Equals(ListaUmow.Cast<UmowyOPrace>().ToList()[i].Autor))
                { ListaUmow.Remove(ListaUmow[i]); break; }

                if (ListaUmow[i] is UmowyODzielo && ListaUmow.Cast<UmowyODzielo>().ToList()[i].Autor != null && autor.Equals(ListaUmow.Cast<UmowyODzielo>().ToList()[i].Autor))
                { ListaUmow.Remove(ListaUmow[i]); break; }
            }
            Program.Update(DH, this);
        }

        public bool Zlecenie(Autor? autor, String? rodzaj, String? tytul, DzialHandlu DH)
        {
            
            List<String> zakazaneNazwy = new List<String>() { "Autor", "Umowy", "UmowyODzielo", "UmowyOPrace", "DzialHandlu", "Drukarnie", "DzialDruku", "Ksiazka", "Czasopismo", "Publikacje", "Sklep" };

            if (autor == null) { autor = new Autor(); }
            if (rodzaj == null) rodzaj = "Inne";
            if (tytul == null) tytul = "Brak Tytulu";

            if (zakazaneNazwy.Contains(rodzaj))
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
                DH.ZlecenieDruku(200, pub, this);
                Program.Update(DH, this);
                return true;
            }
            return false;
        }


        public Boolean WyborDrukarni(int ilosc, Publikacje publikacje, DzialHandlu DH)
        {
            if (publikacje is Albumy)
            { return Drukarnie.DrukujDobrze(ilosc, publikacje, DH, this); }
            else
            { return Drukarnie.DrukujNormalnie(ilosc, publikacje, DH, this); }
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

         
        public void DodajAutora(Autor autor)
        {
            if (!AutorNaLiscie(autor)) { ListaAutorow.Add(autor); }
            else throw new AutorJestNaLiscie("Dany autor jest juz na liscie autorow");
        }

        public void UsunAutora(int numer_autora, DzialHandlu DH)
        {
            for (int i = 0; i < ListaAutorow.Count; i++)
            {
                if (i + 1 == numer_autora)
                {
                    ListaAutorow.Remove(ListaAutorow[i]);
                    Program.Update(DH, this);
                    break;
                }
            }
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