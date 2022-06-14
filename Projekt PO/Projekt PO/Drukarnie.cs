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
        //MOZE WYRZUCIC WYJATEK JESLI NIE BEDZIE PLIKU
        public static void AktualizacjaPublikacji(Publikacje publikacja, int ilosc, DzialHandlu DH)
        {
            //DzialHandlu DH=new DzialHandlu();
            if (!DH.PublikacjaNaLiscie(publikacja))
            {
                DH.DodajDoListy(publikacja);
                //zapisywanie do pliku będzie z tego miejsca ciężkie bo trzeba jakoś potem to odczytać i potem połączyć
                //można dodać jakieś ID po którym można potem łączyć te pliki i od razu z niego odczytać jaki rodzaj publikacji
                //albo wszystko do jednego dużego pliku
                Program.Update();
                /*StreamWriter sw = new StreamWriter("Publikacje.txt");
                sw.WriteLine(publikacja.Tytul);
                sw.Write("Ilosc w magazynie: " + publikacja.Ilosc);
                sw.Close();*/
            }
            publikacja.setilosc(publikacja.Ilosc + ilosc);
        }
    }
    class Drukarnie
    {
        public static Boolean DrukujDobrze(int ilosc, Publikacje publikacje, DzialHandlu DH)
        {
            DzialDruku.AktualizacjaPublikacji(publikacje, ilosc, DH);
            return true;
        }
        public static Boolean DrukujNormalnie(int ilosc, Publikacje publikacje, DzialHandlu DH)
        {
            DzialDruku.AktualizacjaPublikacji(publikacje, ilosc, DH);
            return true;
        }
    }
}
