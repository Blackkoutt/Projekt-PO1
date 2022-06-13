using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wydawnictwo
{
    class HasloException : Exception
    {
        public HasloException(string message) : base(message) { }
    }
    class PustaListaException : Exception
    {
        public PustaListaException(string message) : base(message) { }
    }
    class AutorMaUmowe : Exception
    {
        public AutorMaUmowe(string message) : base(message) { }
    }
    class AutorJestNaLiscie : Exception
    {
        public AutorJestNaLiscie(string message) : base(message) { }
    }
}
