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
        public static void AktualizacjaPublikacji(Publikacje publikacje, int ilosc) { }
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
