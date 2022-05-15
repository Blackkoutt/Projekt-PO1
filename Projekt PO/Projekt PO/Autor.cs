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

        //konstruktor do czasopism żeby miały tego samego "autora" czyli wydawnictwo
        //w publikacjach nie będzie potrzebny drugi konstruktor
        public Autor()
        {
            this.imie = "Wydawnictwo";
            this.nazwisko = "Wydawnictwo";
            this.email = "wydawnictwo@gmail.com";
        }
        
        public bool Equals(Autor autor)
        {
            if(autor != null && this.Imie == autor.Imie && this.Nazwisko == autor.Nazwisko && this.Email == autor.Email)
                { return true; }
            return false;
        }

        public String Imie { get { return imie; } }
        public String Nazwisko { get { return nazwisko; } }
        public String Email { get { return email; } }
    }
}
