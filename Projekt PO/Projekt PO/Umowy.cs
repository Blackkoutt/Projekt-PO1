using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wydawnictwo
{
    abstract class Umowy
    {
        
        protected Autor autor;

        public Umowy(Autor autor)
        {
            this.autor = autor;
        }

        public Autor Autor
        {
            get { return autor; }
        }
        public String getAutor
        {
            get { return autor.Imie + " " + autor.Nazwisko; }
        }
    }
    class UmowyOPrace : Umowy
    {
        protected double dlugosc;

        public UmowyOPrace(double dlugosc, Autor autor) : base(autor)
        {
            this.dlugosc = dlugosc;
        }

        
        public double Dlugosc
        {
            get { return dlugosc; }
        }
    }
    class UmowyODzielo : Umowy
    {
        protected Publikacje publikacja;

        public UmowyODzielo(Autor autor, Publikacje publikacja) : base(autor)
        {
            this.publikacja = publikacja;
        }

       
        public Publikacje Publikacja
        {
            get { return publikacja; }
        }
    }
}
