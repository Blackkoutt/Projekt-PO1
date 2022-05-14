using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wydawnictwo
{
    class DzialDruku
    {
        //pisanie do pliku
        //jesli publikacja jest juz na liscie to zwiekszenie jej ilosci 
        //jesli publikacja nie jest na liscie to zapisanie do pliku i 
        public static async void AktualizacjaPublikacji(Publikacje publikacja, int ilosc) 
        {
            if (!DzialHandlu.PublikacjaNaLiscie(publikacja))
            {
                DzialHandlu.DodajDoListy(publikacja);
                //zapisywanie do pliku będzie z tego miejsca ciężkie bo trzeba jakoś potem to odczytać i potem połączyć
                //można dodać jakieś ID po którym można potem łączyć te pliki i od razu z niego odczytać jaki rodzaj publikacji
                //albo wszystko do jednego dużego pliku
            }
            publikacja.setilosc(publikacja.Ilosc + ilosc);
        }
    }
    class Drukarnie
    {
        public static Boolean DrukujDobrze(int ilosc, Publikacje publikacje)
        {
            DzialDruku.AktualizacjaPublikacji(publikacje, ilosc);
            return true;
        }
        public static Boolean DrukujNormalnie(int ilosc, Publikacje publikacje)
        {
            DzialDruku.AktualizacjaPublikacji(publikacje, ilosc);
            return true;
        }
    }
}
