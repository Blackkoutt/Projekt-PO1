using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wydawnictwo
{
    abstract class Publikacje
    {
        protected Autor autor;
        protected String tytul;
        private int ilosc=0;
        public Publikacje(Autor autor, String tytul)
        {
            this.tytul = tytul;
            this.autor = autor;
        }
        //zamiast rozróżniać czy jest z autorem czy bez można podpiąć czasopisma pod autorstwo wydawnictwa
        //dodałem konstruktor w autorze który zamiast nazwiska i imienia daje nazwe wydawnictwa i email
        /*public Publikacje(String tytul)
        {
            this.tytul = tytul;
        }*/

        public bool Equals(Publikacje pub)
        {
            if (this.autor.Equals(pub.autor) && this.Tytul == pub.Tytul)
            { return true; }
            return false;
        }

        public int Ilosc
        {
            get { return ilosc; }
        }
        public String Tytul
        {
            get { return tytul; }
        }
        public void setilosc(int ilosc)
        {
            this.ilosc = ilosc;
        }
        public String getAutor
        {
            get { return tytul + " autorstwa " + autor.Imie + " " + autor.Nazwisko; }
        }
    }
}

