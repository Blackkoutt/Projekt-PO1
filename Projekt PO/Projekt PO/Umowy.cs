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
        
        
        /*public Umowy(double dlugosc, Autor autor)
        {
            this.autor = autor; this.dlugosc = dlugosc;
            //base
        }
        public Umowy(Autor autor, Publikacje publikacja)
        {
            this.autor = autor; this.publikacja = publikacja;
        }*/

        public Umowy(Autor autor)
        {
            this.autor = autor;
        }

        public Autor Autor
        {
            get { return autor; }
        }
    }
    class UmowyOPrace : Umowy
    {
        protected double dlugosc;

        public UmowyOPrace(double dlugosc, Autor autor) : base(autor)
        {
            this.dlugosc = dlugosc;
        }

        //na zapas, jeśli nie będzie potrzebna usunąć
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

        //na zapas, jeśli nie będzie potrzebna usunąć
        public Publikacje Publikacja 
        { 
            get { return publikacja; } 
        }
    }
}
