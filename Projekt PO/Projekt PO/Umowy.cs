using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wydawnictwo
{
    abstract class Umowy
    {
        //dodac cos do konstruktorow
        protected Autor autor;
        protected double dlugosc;
        protected Publikacje publikacja;
        public Umowy(double dlugosc, Autor autor)
        {
            this.autor = autor; this.dlugosc = dlugosc;
        }
        public Umowy(Autor autor, Publikacje publikacja)
        {
            this.autor = autor; this.publikacja = publikacja;
        }
        public Autor Autor
        {
            get { return autor; }
        }
    }
    class UmowyOPrace : Umowy
    {
        public UmowyOPrace(double dlugosc, Autor autor) : base(dlugosc, autor)
        { }
    }
    class UmowyODzielo : Umowy
    {
        public UmowyODzielo(Autor autor, Publikacje publikacja) : base(autor, publikacja)
        { }
    }
}
