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
        //jesli nie to co w klasie wyzej to ew dodac autora do tego konstruktora 
        public Publikacje(String tytul)
        {
            this.tytul = tytul;
        }
        public int Ilosc
        {
            get { return ilosc; }
        }
        public String Tytul
        {
            get
            { return tytul; }
        }
        public String getAutor
        {
            get { return tytul+" autorstwa "+autor.Imie + " " + autor.Nazwisko; }
        }
        public void setilosc(int ilosc)
        {
            this.ilosc = ilosc;
        }
    }
}
