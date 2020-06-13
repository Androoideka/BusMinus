namespace BusMinus
{
    public class Put
    {
        Veza[] vz;
        Stanica cilj;
        int brVeza;
        internal Put(Stanica c, Veza[] s, int br)
        {
            brVeza = br;
            vz = new Veza[br];
            cilj = c;
            for (int i = 0; i < br; i++)
            {
                vz[i] = s[i];
            }
        }
        private double Duzina
        {
            get
            {
                double duzPuta = 0;
                for (int i = 0; i < brVeza; i++)
                {
                    duzPuta += vz[i].Udalj;
                }
                return duzPuta;
            }
        }
        private int BrojPresedanja
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
        private double Vreme(double maxBrzina)
        {
            double srednjaBrz = maxBrzina * 0.8;
            double s = (Duzina / 1000) / (srednjaBrz);
            s = s * 60;
            if (s == 0)
            {
                return s;
            }
            else
            {
                return s + 1;
            }
        }
        public string Ispis()
        {
            double Duz = Duzina;
            Stanica t = cilj;
            string s = cilj.Ime + " " + Vreme(60) + " min" + " ( ";
            if (Duzina > 1000)
            {
                s += Duz / 1000 + " km )";
            }
            else
            {
                s += Duz + " m )";
            }
            for (int i = brVeza - 1; i >= 0; i--)
            {
                t = vz[i].DrugaStanicaVeze(t);
                s = t.Ime + "( " + vz[i].Linija + ")" + "-->" + s;
            }
            return s;
        }
    }
}
