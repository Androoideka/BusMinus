namespace BusMinus
{
    public class Veza
    {
        Stanica stanica1, stanica2;
        int udaljenost, brojLinije;

        public Veza()
        {
            stanica1 = null;
            stanica2 = null;
            udaljenost = 0;
        }
        public Veza(Stanica poc, Stanica kraj, int udalj)
        {
            stanica1 = poc;
            stanica2 = kraj;
            udaljenost = udalj;
        }
        public int BrojLinije
        {
            get
            {
                return brojLinije;
            }
            set
            {
                brojLinije = value;
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
