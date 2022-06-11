using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wydawnictwo
{
    abstract class Czasopismo : Publikacje
    {
        public Czasopismo(String tytul) : base(tytul)
        { }
    }
    class Miesiecznik : Czasopismo
    {
        public Miesiecznik(Autor autor, String tytul) : base(tytul)
        { }
    }
    class Tygodnik : Czasopismo
    {
        public Tygodnik(Autor autor, String tytul) : base(tytul)
        { }
    }
}
