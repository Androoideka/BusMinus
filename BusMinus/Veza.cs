namespace BusMinus
{
    public class Veza
    {
        Stanica stanica1, stanica2;
        string linija;
        int udaljenost;
        public Veza()
        {
            stanica1 = null;
            stanica2 = null;
            linija = "";
            udaljenost = 0;
        }
        public Veza(Stanica poc, Stanica kraj, int udalj)
        {
            stanica1 = poc;
            stanica2 = kraj;
            linija = "";
            udaljenost = udalj;
        }
        public Veza(Stanica poc, Stanica kraj, string imeln, int udalj)
        {
            stanica1 = poc;
            stanica2 = kraj;
            linija = imeln;
            udaljenost = udalj;
        }
        public string Linija
        {
            get
            {
                return linija;
            }
            set
            {
                linija = value;
            }
        }
        public int Udalj
        {
            get
            {
                return udaljenost;
            }
        }
        public Stanica DrugaStanicaVeze(Stanica s)
        {
            if (s == stanica1)
            {
                return stanica2;
            }
            else if (s == stanica2)
            {
                return stanica1;
            }
            return null;
        }
    }
}
