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
        public Autor(String imie, String nazwisko)
        {
            this.imie = imie;
            this.nazwisko = nazwisko;
            email = "brak maila";
        }

        public Autor()
        {
            this.imie = "Wydawnictwo";
            this.nazwisko = "ePress";
            this.email = "ePress@gmail.com";
        }
        public bool Equals(Autor autor)
        {
            if (autor != null && this.Imie == autor.Imie && this.Nazwisko == autor.Nazwisko)
            { 
                if(email != autor.Email) { email = autor.Email; }//Do nadpisywania maila w przypadku tej samej osoby
                return true; 
            }
            return false;
        }
        public String Imie { get { return imie; } }
        public String Nazwisko { get { return nazwisko; } }
        public String Email { get { return email; } }
        public override string ToString()
        {
            return Imie + " " + Nazwisko;
        }
    }
}
