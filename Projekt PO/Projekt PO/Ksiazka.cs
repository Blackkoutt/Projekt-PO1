using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wydawnictwo
{
    abstract class Ksiazka : Publikacje
    {
        public Ksiazka(Autor autor, String tytul) : base(autor, tytul)
        { }
    }
    class Sensacyjne : Ksiazka
    {
        public Sensacyjne(Autor autor, String tytul) : base(autor, tytul)
        { }
    }
    class Albumy : Ksiazka
    {
        public Albumy(Autor autor, String tytul) : base(autor, tytul)
        { }
    }
    class Fantasy : Ksiazka
    {
        public Fantasy(Autor autor, String tytul) : base(autor, tytul)
        { }
    }
    class Kryminalistyczne : Ksiazka
    {
        public Kryminalistyczne(Autor autor, String tytul) : base(autor, tytul)
        { }
    }
    class Romanse : Ksiazka
    {
        public Romanse(Autor autor, String tytul) : base(autor, tytul)
        { }
    }
    class Inne : Ksiazka
    {
        public Inne(Autor autor, String tytul) : base(autor, tytul)
        { }
    }
}
