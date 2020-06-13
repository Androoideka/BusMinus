namespace BusSharp
{
    class Stanica
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
        private int this[string linija]
        {
            get
            {
                int brojac = 0;
                for (int i = 0; i < brveza; i++)
                {
                    if(veze[i].Linija == linija)
                    {
                        brojac++;
                    }
                }
                return brojac;
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
        internal Linija PutUIstojLiniji(string ln, ref Veza[] vz, int n)
        {
            if (n != 0 && this[ln] == 1)
            {
                return new Linija(this, vz, n);
            }
            Linija s = null;
            posecena = true;
            for (int i = 0; i < brveza; i++)
            {
                Stanica t = veze[i].DrugaStanicaVeze(this);
                if (!t.posecena && n == 0 || (veze[i] != vz[n-1] && veze[i].Linija == vz[n-1].Linija))
                {
                    vz[n] = veze[i];
                    s = t.PutUIstojLiniji(ln, ref vz, n + 1);
                }
            }
            posecena = false;
            return s;
        }
        internal double DuzinaDoCilja(double daljina, Stanica cilj, string linija, Veza td)
        {
            if (daljina <= 0 && this == cilj)
            {
                return -daljina;
            }
            else
            {
                double duzina = 0;
                for (int i = 0; i < brveza; i++)
                {
                    if (linija == veze[i].Linija && (veze[i] != td || this[linija] == 1))
                    {
                        duzina += veze[i].DrugaStanicaVeze(this).DuzinaDoCilja(daljina - veze[i].Udalj, cilj, linija, veze[i]);
                    }
                }
                return duzina;
            }
        }
        private bool postoji(string[] p, int n, string p_2)
        {
            for (int i = 0; i < n; i++)
            {
                if (p[i] == p_2)
                {
                    return true;
                }
            }
            return false;
        }
        internal string[] Poziv(Vozilo[] voz, int brVozila)
        {
            string[] nizImena = new string[1000];
            int brimena = 0;
            for (int i = 0; i < brveza; i++)
            {
                if (!postoji(nizImena, brimena, veze[i].Linija))
                {
                    nizImena[brimena] = veze[i].Linija;
                    brimena++;
                }
            }
            string[] ispis = new string[brimena];
            int brojac = 0;
            for (int i = 0; i < brimena; i++)
            {
                for (int j = 0; j < brVozila; j++)
                {
                    if (voz[j].ImeLinije == nizImena[i])
                    {
                        ispis[brojac] = nizImena[brojac] + " " + voz[j].kolikoDoStanice(this);
                        brojac++;
                    }
                }
            }
            return ispis;
        }
        #region komentari
        /*internal string[] LinijeKojePocinju()
        {
            string[] pocetak = new string[brveza];
            int brojac = 0;
            for (int i = 0; i < brveza; i++)
            {
                bool dodati = true;
                for (int j = 0; j < brveza; j++)
                {
                    if(i != j && veze[i].Linija == veze[j].Linija)
                    {
                        dodati = false;
                    }
                }
                if(dodati)
                {
                    pocetak[brojac] = veze[i].Linija;
                    brojac++;
                }
            }
            return pocetak;
        }
        internal Linija[] LinijeKojePocinju()
        {
            Linija[] ln = new Linija[brveza];
            int n = 0;
            for (int i = 0; i < brveza; i++)
            {
                if(this[veze[i].Linija] == 1)
                {
                    Veza[] vz = new Veza[10000];
                    ln[n] = PutUIstojLiniji(veze[i].Linija, ref vz, 0);
                    n++;
                }
            }
            return ln;
        }*/
        #endregion
        #region MilosevicProffesionalac
        /*internal double pocetnaRaz(string imelinije, double razdaljina, out Stanica zadnja)
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
        }*/
        #endregion
    }
}
