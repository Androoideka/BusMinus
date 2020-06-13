namespace BusMinus
{
    internal class Stanica
    {
        string ime;
        Veza[] veze;
        int brveza;
        bool posecena = false;
        internal Stanica()
        {
            ime = "";
            veze = new Veza[1000];
            brveza = 0;
        }
        internal Stanica(string s)
        {
            ime = s;
            veze = new Veza[1000];
            brveza = 0;
        }
        internal string Ime
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
        internal Veza this[string linija]
        {
            get
            {
                for (int i = 0; i < brveza; i++)
                {
                    if(veze[i].Linija == linija)
                    {
                        return veze[i];
                    }
                }
                return null;
            }
        }
        internal void DodajVezu(Veza v)
        {
            veze[brveza] = v;
            brveza++;
        }
        internal void Put(ref Veza[] vz, int n, ref Put[] pt, ref int brojac, Stanica cilj)
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
        internal double DuzinaDoCilja(double daljina, Stanica cilj, string linija, Veza td)
        {
            double duzina = 0;
            if (daljina <= 0 && this == cilj)
            {
                return duzina;
            }
            else
            {
                for (int i = 0; i < brveza; i++)
                {
                    if (linija == veze[i].Linija && veze[i] != td)
                    {
                        if (daljina <= 0)
                        {
                            duzina += -daljina;
                            daljina = 0;
                        }
                        duzina += DuzinaDoCilja(daljina - veze[i].Udalj, cilj, linija, veze[i]);
                    }
                }
                return duzina;
            }
        }
        #region MilosevicProffesionalac
        internal double pocetnaRaz(string imelinije, double razdaljina, out Stanica zadnja)
        {
            if (razdaljina >= 0)
            {
                posecena = true;
                for (int i = 0; i < brveza; i++)
                {
                    if (veze[i].Linija == imelinije && !veze[i].DrugaStanicaVeze(this).posecena)
                    {
                        veze[i].DrugaStanicaVeze(this).pocetnaRaz(imelinije, razdaljina - veze[i].Udalj, out zadnja);
                    }
                }
            }
            zadnja = this;
            return -razdaljina;
        }
        internal double razdaljina(double razdaljina, string imelinije, Stanica mojastanica)
        {
            Stanica zadnja;
            double malarazdaljina = pocetnaRaz(imelinije, razdaljina, out zadnja);
            return mojastanica.Put2(0, zadnja, imelinije) + malarazdaljina;
        }
        internal double Put2(double velikarazdaljina, Stanica zadnja, string imelinije)
        {
            if (this == zadnja)
            {
                return velikarazdaljina;
            }
            else
            {
                posecena = true;
                for (int i = 0; i < brveza; i++)
                {
                    Stanica t = veze[i].DrugaStanicaVeze(this);
                    if (!t.posecena && (veze[i].Linija == imelinije))
                    {

                        Put2(velikarazdaljina, zadnja, imelinije);
                        velikarazdaljina = velikarazdaljina + veze[i].Udalj;
                    }
                }
                posecena = false;
            }
            return velikarazdaljina;
        }
        #endregion
    }
}
