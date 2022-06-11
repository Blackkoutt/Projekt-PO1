
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

        private ArrayList inwentarz = new ArrayList();
        private string line;

        //Wczytywanie z pliku, na razie jest tutaj pozniej to zmienie
        public void WczytajZPlikuKsiazki()
        {
            StreamReader sr = new StreamReader("ksiazki.txt");
            while ((line = sr.ReadLine()) != null)
            {
                string[] s = line.Split(",");
                Autor autor = new Autor(s[0], s[1]);
                //Wlasni przez to chce zmienic te wszyskie klasy na rodzaje, bo pozniej ciezko to z pliku wczytac
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

        }
        public void ZlecenieKupna(int ilosc, Publikacje publikacje)
        {
            if (publikacje.Ilosc - ilosc >= 0)
                publikacje.setilosc(publikacje.Ilosc - ilosc);
            else
                Console.WriteLine("Nie ma wystarczającej ilości podanej publikacji, dostępna ilość to: " + publikacje.Ilosc);
        }
        public void WczytajZPlikuCzasopisma()
        {
            StreamReader sr = new StreamReader("czasopisma.txt");
            while ((line = sr.ReadLine()) != null)
            {
                string[] s = line.Split(",");
                Autor autor = new Autor();
                if (s[0] =="tygodnik")
                {
                    Czasopismo publikacje= new Tygodnik(autor, s[1]);

                }
                if (s[0] =="miesiecznik")
                {
                    Czasopismo publikacje = new Tygodnik(autor, s[1]);
                }
        
            }
            sr.Close();

        }

        public  ArrayList getlista()
        {
            return inwentarz;
        }
    }
}