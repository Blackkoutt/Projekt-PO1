using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wydawnictwo
{
    class Autor
    {
        private String imie, nazwisko, email;
        public Autor(String imie, String nazwisko, String email)
        {
            this.imie = imie;
            this.nazwisko = nazwisko;
            this.email = email;
        }
        public String Imie { get { return imie; } }
        public String Nazwisko { get { return nazwisko; } }
        public String Email { get { return email; } }
    }
}
