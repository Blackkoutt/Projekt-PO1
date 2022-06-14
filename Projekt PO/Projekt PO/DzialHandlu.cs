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
        public void Zamowienie(int ilosc, Publikacje publikacje, DzialProgramowy DP)
        {
            if (DP.WyborDrukarni(ilosc, publikacje, this))
                ;
        }

        public void ZlecenieKupna(int ilosc, Publikacje publikacje, DzialProgramowy DP)
        {
            
            if (publikacje.Ilosc < 10)
                this.Zamowienie(50, publikacje, DP);
            while (publikacje.Ilosc - ilosc <= 0)
            {
                string kk;
                Console.WriteLine("Nie ma wystarczającej ilości podanej publikacji, dostępna ilość to: " + publikacje.Ilosc);
                kk = Console.ReadLine();
                bool success = int.TryParse(kk, out ilosc);
                while (success == false)
                {                   
                    Console.WriteLine("Podano nieprawidlowa wartosc");
                    kk = Console.ReadLine();
                    success = int.TryParse(kk, out ilosc);
                }
            }
                publikacje.setilosc(publikacje.Ilosc - ilosc);
            if (publikacje.Ilosc < 10)
                this.Zamowienie(50, publikacje, DP);


        }
        

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
