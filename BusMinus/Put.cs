namespace BusMinus
{
    public class Put
    {
        Veza[] vz;
        Stanica cilj;
        int brVeza;
        public Put(Stanica c, Veza[] s, int br)
        {
            brVeza = br;
            vz = new Veza[br];
            cilj = c;
            for (int i = 0; i < br; i++)
            {
                vz[i] = s[i];
            }
        }
        public int Duzina
        {
            get
            {
                int duzPuta = 0;
                for (int i = 0; i < brVeza; i++)
                {
                    duzPuta += vz[i].Udalj;
                }
                return duzPuta;
            }
        }
        public int BrojPresedanja
        {
            get
            {
                int brPresedanja = 0;
                for (int i = 1; i < brVeza; i++)
                {
                    if(!vz[i].Linija.Equals(vz[i-1].Linija))
                    {
                        brPresedanja++;
                    }
                }
                return brPresedanja;
            }
        }
        public static bool operator >(Put a, Put b)
        {
            if (a == null || b == null || a.Duzina <= b.Duzina)
            {
                return false;
            }
            return true;
        }
        public static bool operator <(Put a, Put b)
        {
            if (a == null || b == null || a.Duzina >= b.Duzina)
            {
                return false;
            }
            return true;
        }
        public string Ispis()
        {
            string s = cilj.Ime + "=" + Duzina;
            Stanica t = cilj;
            for (int i = brVeza-1; i >= 0; i--)
            {
                t = vz[i].DrugaStanicaVeze(t);
                s = t.Ime + "--->" + s;
            }
            return s;
        }
    }
}
