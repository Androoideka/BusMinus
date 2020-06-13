namespace BusSharp
{
    class Veza
    {
        Stanica stanica1, stanica2;
        string linija;
        double udaljenost;
        internal Veza()
        {
            stanica1 = null;
            stanica2 = null;
            linija = "";
            udaljenost = 0;
        }
        internal Veza(Stanica poc, Stanica kraj, double udalj)
        {
            stanica1 = poc;
            stanica2 = kraj;
            linija = "";
            udaljenost = udalj;
            poc.DodajVezu(this);
            kraj.DodajVezu(this);
        }
        internal Veza(Stanica poc, Stanica kraj, string imeln, double udalj)
        {
            stanica1 = poc;
            stanica2 = kraj;
            linija = imeln;
            udaljenost = udalj;
            poc.DodajVezu(this);
            kraj.DodajVezu(this);
        }
        internal string Linija
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
        internal double Udalj
        {
            get
            {
                return udaljenost;
            }
        }
        internal Stanica DrugaStanicaVeze(Stanica s)
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
