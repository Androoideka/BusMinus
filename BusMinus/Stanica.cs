namespace Bus_Minus
{
    public class Stanica
    {
        string ime;
        Veza[] veze;
        int brveza;
        bool posecena = false;
        public Stanica()
        {
            ime = "";
            veze = new Veza[1000];
            brveza = 0;
        }
        public Stanica(string s)
        {
            ime = s;
            veze = new Veza[1000];
            brveza = 0;
        }
        public Stanica(Stanica s)
        {
            ime=s.ime; 
        }
        public string Ime
        {
            get {
                return ime;
            }
            set
            {
                ime = value;
            }
        }
        public void DodajVezu(Veza v)
        {
            veze[brveza] = v;
            brveza++;
        }
        public void Put(ref Stanica[] st, int duzinaPuta, int n, ref Put[] pt, ref int brojac, Stanica cilj)
        {
            st[n] = this;
            if (this == cilj)
            {
                pt[brojac] = new Put(st, n+1, duzinaPuta);
                brojac++;
                return;
            }
            posecena = true;
            for (int i = 0; i < brveza; i++)
            {
                Stanica t = veze[i].DrugaStanicaVeze(this);
                if (!t.posecena)
                {
                    t.Put(ref st, duzinaPuta + veze[i].Udalj, n + 1, ref pt, ref brojac, cilj);
                }
            }
        }
    }
}
