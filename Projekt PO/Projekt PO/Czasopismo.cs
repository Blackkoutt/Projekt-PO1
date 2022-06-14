using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wydawnictwo
{
    abstract class Czasopismo : Publikacje
    {
        public Czasopismo(Autor autor, String tytul) : base(autor, tytul) 
        { }
    }
    class Miesiecznik : Czasopismo
    {
        public Miesiecznik(Autor autor, String tytul) : base(autor, tytul)
        { }
    }
    class Tygodnik : Czasopismo
    {
        public Tygodnik(Autor autor, String tytul) : base(autor, tytul)
        { }
    }
}
