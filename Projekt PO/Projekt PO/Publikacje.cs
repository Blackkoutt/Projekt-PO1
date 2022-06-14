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
        public Publikacje(String tytul) 
        {
            this.tytul = tytul;
            autor=new Autor();
        }

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

        public Autor Autor
        {
            get { return autor; }
        }

        public String getAutor
        {
            get { return tytul + " autorstwa " + autor.Imie + " " + autor.Nazwisko; }
        }
    }
}

