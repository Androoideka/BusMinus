namespace BusMinus
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
        public string Ime
        {
            get
            {
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
        public void Put(ref Veza[] vz, int n, ref Put[] pt, ref int brojac, Stanica cilj)
        {
            if (this == cilj)
            {
                pt[brojac] = new Put(cilj, vz, n);
                brojac++;
                return;
            }
            posecena = true;
            for (int i = 0; i < brveza; i++)
            {
                Stanica t = veze[i].DrugaStanicaVeze(this);
                if (!t.posecena)
                {
                    vz[n] = veze[i];
                    t.Put(ref vz, n + 1, ref pt, ref brojac, cilj);
                }
            }
            posecena = false;
        }
    }
}
