using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Wydawnictwo
{
    class DzialHandlu
    {
        private ArrayList ListaPublikacji = new ArrayList();
        
        public void ZlecenieDruku(int ilosc, Publikacje publikacje, DzialProgramowy DP)
        {
            if (DP.WyborDrukarni(ilosc, publikacje, this))
            {
                Console.WriteLine("Pomyslnie wydrukowano " + publikacje.Tytul + " w nastepujacej liczbie egzemplarzy: " + ilosc);
            }
        }

        public void ZlecenieKupna(int ilosc, Publikacje publikacje)
        {
            if (publikacje.Ilosc - ilosc >= 0)
                publikacje.setilosc(publikacje.Ilosc - ilosc);
            else
                Console.WriteLine("Nie ma wystarczającej ilości podanej publikacji, dostępna ilość to: " + publikacje.Ilosc);
        }
        //WYJATEK

        public ArrayList  Katalog
        {
           get { return ListaPublikacji; }
        }

        public void DodajDoListy(Publikacje publikacje)
        {
            ListaPublikacji.Add(publikacje);
            Sklep.AktualizujInwentarz(this);
        }

        public void UsunPublikacje(Publikacje publikacja)
        {
            ListaPublikacji.Remove(publikacja);
            Sklep.AktualizujInwentarz(this);
        }

        public Boolean PublikacjaNaLiscie(Publikacje publikacja)
        {
            foreach (Publikacje publikacje in ListaPublikacji)
            {
                if (publikacja.Equals(publikacje)) { return true; }
            }
            return false;
        }

        public Publikacje? SzukajPublikacji(Autor autor, string tytul)
        {
            foreach(Publikacje publikacje in ListaPublikacji)
            {
                if(autor.Equals(publikacje.Autor) && tytul == publikacje.Tytul)
                    return publikacje;
            }
            return null;
        }

    }

}
