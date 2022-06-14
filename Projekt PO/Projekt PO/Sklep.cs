
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;



namespace Wydawnictwo
{
    class Sklep
    {
        static ArrayList inwentarz;

        public Sklep(DzialHandlu DH)
        {
            inwentarz = DH.Katalog;
        }

        public static void AktualizujInwentarz(DzialHandlu DH)
        {
            inwentarz = DH.Katalog;
        }

             

        public  ArrayList getlista()
        {
            if(inwentarz.Count!=0) return inwentarz;
            throw new PustaListaException("Brak dostepnych pozycji w katalogu");
        }
    }
}