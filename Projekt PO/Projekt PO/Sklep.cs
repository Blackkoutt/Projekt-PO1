
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

        //Wczytywanie z pliku, na razie jest tutaj pozniej to zmienie
        /*public void WczytajZPlikuKsiazki()
         {
             StreamReader sr = new StreamReader("ksiazki.txt");
             while ((line = sr.ReadLine()) != null)
             {
                 List<String> zakazaneNazwy = new List<String>() { "Autor", "Umowy", "UmowyODzielo", "UmowyOPrace", "DzialHandlu", "Drukarnie", "DzialDruku", "Ksiazka", "Czasopismo", "Publikacje", "Sklep" };

                 string[] s = line.Split(",");
                 Autor autor = new Autor(s[0], s[1]);
                 //Wlasni przez to chce zmienic te wszyskie klasy na rodzaje, bo pozniej ciezko to z pliku wczytac

                 s[4] = DoWielkiej(s[4]);

                 if (zakazaneNazwy.Contains(s[4]))
                 { Console.WriteLine("Niepoprawny rodzaj publikacji\n"); s[4] = "[BezNazwy]"; }

                 String nazwaTypu = "Wydawnictwo." + s[4];
                 Type typ = Type.GetType(nazwaTypu);

                 if (typ == null)
                     typ = Type.GetType("Wydawnictwo.Inne");

                 Publikacje publikacje = Activator.CreateInstance(typ, autor, s[2]) as Publikacje; // Można tu ifa dodać jakiegoś ale nie powinno być w tym miejscu problemu
                 publikacje.setilosc(200); // Proponuję dodać do pliku też ilość bo z 10 pojawi się 200 po restarcie
                 inwentarz.Add(publikacje);
                 //////////////////////////////

                 if (s[4]=="sensacyjne")
                 {

                     Sensacyjne publikacje = new Sensacyjne(autor, s[2]);
                     publikacje.setilosc(200);
                     inwentarz.Add(publikacje);

                 }
                 else if (s[4] == "kryminalistyczne")
                 {

                     Kryminalistyczne publikacje = new Kryminalistyczne(autor, s[2]);
                     publikacje.setilosc(200);
                     inwentarz.Add(publikacje);

                 }
                 else if (s[4] =="fantasy")
                 {

                     Fantasy publikacje = new Fantasy(autor, s[2]);
                     publikacje.setilosc(200);
                     inwentarz.Add(publikacje);

                 }
                 else if (s[4] =="romanse")
                 {

                     Romanse publikacje = new Romanse(autor, s[2]);
                     publikacje.setilosc(200);
                     inwentarz.Add(publikacje);

                 }
                 else if (s[4] =="albumy")
                 {
                     Albumy publikacje = new Albumy(autor, s[2]);
                     publikacje.setilosc(200);
                     inwentarz.Add(publikacje);

                 }
                 else if (s[4]=="inne")
                 {

                     Inne publikacje = new Inne(autor, s[2]);
                     publikacje.setilosc(200);
                     inwentarz.Add(publikacje);

                 }

             }
             sr.Close();

         }*/
        public void ZlecenieKupna(int ilosc, Publikacje publikacje, DzialHandlu DH)
        {
            DH.ZlecenieKupna(ilosc, publikacje);
            /*if (publikacje.Ilosc - ilosc >= 0)
                publikacje.setilosc(publikacje.Ilosc - ilosc);
            else
                Console.WriteLine("Nie ma wystarczającej ilości podanej publikacji, dostępna ilość to: " + publikacje.Ilosc);*/
        }
        /*public void WczytajZPlikuCzasopisma()
        {
            StreamReader sr = new StreamReader("czasopisma.txt");
            while ((line = sr.ReadLine()) != null)
            {
                string[] s = line.Split(",");
                if (s[0] =="tygodnik")
                {
                    //Czasopismo publikacje= new Tygodnik(s[1]);

                }
                if (s[0] =="miesiecznik")
                {
                    //Czasopismo publikacje = new Tygodnik(s[1]);
                }
        
            }
            sr.Close();

        }*/

        public  ArrayList getlista()
        {
            if(inwentarz.Count!=0) return inwentarz;
            throw new PustaListaException("Brak dostepnych pozycji w katalogu");
        }
    }
}