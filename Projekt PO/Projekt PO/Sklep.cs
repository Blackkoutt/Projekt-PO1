
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
        public void WczytajZPlikuKsiazki(string np)
        {
            StreamReader sr = new StreamReader(np);
            while ((line = sr.ReadLine()) != null)
            {
                string[] s = line.Split(",");
                Autor autor = new Autor(s[0], s[1]);
                //Wlasni przez to chce zmienic te wszyskie klasy na rodzaje, bo pozniej ciezko to z pliku wczytac
                Sensacyjne publikacja = new Sensacyjne(autor, s[3]);
                inwentarz.Add(publikacja);
            }
            sr.Close();

        }
        public void WczytajZPlikuCzasopisma(string adres)
        {
            string[] dane = File.ReadAllLines(adres);
            foreach (string s in dane)
            {
                string[] informacje = s.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                Autor autor = new Autor();

            }

        }
        public  ArrayList getlista()
        {
            return inwentarz;
        }
    }
}