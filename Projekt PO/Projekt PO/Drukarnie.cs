using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wydawnictwo
{
    class DzialDruku
    {
        public static void AktualizacjaPublikacji(Publikacje publikacja, int ilosc, DzialHandlu DH, DzialProgramowy DP)
        {
            if (!DH.PublikacjaNaLiscie(publikacja))
            {
                DH.DodajDoListy(publikacja);
                Program.Update(DH, DP);
            }
            publikacja.setilosc(publikacja.Ilosc + ilosc);
        }
    }
    class Drukarnie
    {
        public static Boolean DrukujDobrze(int ilosc, Publikacje publikacje, DzialHandlu DH, DzialProgramowy DP)
        {
            DzialDruku.AktualizacjaPublikacji(publikacje, ilosc, DH, DP);
            return true;
        }
        public static Boolean DrukujNormalnie(int ilosc, Publikacje publikacje, DzialHandlu DH, DzialProgramowy DP)
        {
            DzialDruku.AktualizacjaPublikacji(publikacje, ilosc, DH, DP);
            return true;
        }
    }
}
