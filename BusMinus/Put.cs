namespace BusSharp
{
    class Put
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
        private Stanica pocetna
        {
            get
            {
                Stanica p = cilj;
                for (int i = brVeza-1; i >= 0; i--)
                {
                    p = vz[i].DrugaStanicaVeze(p);
                }
                return p;
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
        internal double Vreme(Vozilo[] a)
        {
            double vreme = 0;
            double rastojanje = 0;
            string ln = "";
            Vozilo v = null;
            Stanica t = pocetna;
            for (int i = 0; i < brVeza; i++)
            {
                if(ln != vz[i].Linija)
                {
                    ln = vz[i].Linija;
                    if (v != null)
                    {
                        vreme = vreme + rastojanje / v.Brzina;
                    }
                    for (int j = 0; j < a.Length; j++)
                    {
                        if(a[j].ImeLinije == vz[i].Linija)
                        {
                            v = a[j];
                        }
                    }
                    vreme += v.kolikoDoStanice(t);
                }
                rastojanje += vz[i].Udalj;
                t = vz[i].DrugaStanicaVeze(t);
            }
            if (v != null)
            {
                vreme = vreme + rastojanje / v.Brzina;
            }
            return vreme;
        }
        protected string IspisVeza()
        {
            string s = cilj.Ime;
            Stanica t = cilj;
            for (int i = brVeza-1; i >= 0; i--)
            {
                s = vz[i].DrugaStanicaVeze(t).Ime + "-" + vz[i].Udalj + "-" + s;
                t = vz[i].DrugaStanicaVeze(t);
            }
            return s;
        }
        public virtual string Ispis(Vozilo[] a)
        {
            double Duz = Duzina;
            Stanica t = cilj;
            string s = cilj.Ime + " " + (int)(Vreme(a)/60) + " min" + " (";
            if (Duzina > 1000)
            {
                s += Duz / 1000 + " km)";
            }
            else
            {
                s += Duz + " m)";
            }
            for (int i = brVeza - 1; i >= 0; i--)
            {
                t = vz[i].DrugaStanicaVeze(t);
                s = t.Ime + "( " + vz[i].Linija + ")" + "-->" + s;
            }
            return s;
        }
        #region komentari
        /*public virtual string Ispis()
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
        internal double Vreme(double maxBrzina)
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
        }*/
        #endregion
    }
    class Linija : Put
    {
        string linija;
        internal Linija(Stanica c, Veza[] s, int br) : base(c, s, br)
        {
            linija = s[0].Linija;
        }
        public string Ispis()
        {
            string s = IspisVeza();
            s = linija + ":" + s;
            return s;
        }
    }
}
