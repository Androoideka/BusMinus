using System;

namespace BusMinus
{
    class BusMinus
    {
        Stanica[] stanice = new Stanica[100];
        int brStanica;
        Karta k;

        public BusMinus(string[] niz)
        {
            int brojac = niz.Length;
            voz = new Vozilo[brojac];
            for (int i = 0; i < brojac; i++)
            {
                string[] s0 = niz[i].Split(':');
                string linija = s0[0].ToString();
                string[] s1 = s0[1].Split('-');
                int brojac2 = s1.Length - 2;
                for (int j = 0; j < brojac2; j += 2)
                {
                    Stanica a = this[s1[j]];
                    double udaljenost = Convert.ToDouble(s1[j + 1]);
                    Stanica b = this[s1[j + 2]];
                    Veza v = new Veza(a, b, linija, udaljenost);
                    a.DodajVezu(v);
                    b.DodajVezu(v);
                }
            }
        }
        public string Karta 
        {
            get 
            {
                if(k != null)
                {
                    int t = k.PreostaloVreme;
                    if (t < 86400)
                    {
                        int sat = t / 3600;
                        int minut = t % 3600 / 60;
                        int sekund = t % 60;
                        return sat + ":" + minut + ":" + sekund;
                    }
                    else
                    {
                        return k.PreostaloVreme / 86400 + " Days";
                    }
                }
                else
                {
                    return "Niste kupili kartu";
                }
            }
            set 
            {
                KupiKartu(value);
            }
        }
        public Stanica this[string imeStanice]
        {
            get
            {
                for (int i = 0; i < brStanica; i++)
                {
                    if (stanice[i].Ime == imeStanice)
                    {
                        return stanice[i];
                    }
                }
                if (brStanica >= stanice.Length)
                {
                    Stanica[] t = new Stanica[brStanica + 100];
                    for (int i = 0; i < brStanica; i++)
                    {
                        t[i] = stanice[i];
                    }
                    stanice = t;
                }
                stanice[brStanica] = new Stanica(imeStanice);
                brStanica++;
                return stanice[brStanica-1];
            }
        }
        private void KupiKartu(string tip)
        {
            tip = tip.ToLower();
            switch (tip)
            {
                case"jedna voznja":
                    k = new JednokratnaKarta();
                    break;
                case "celodnevna voznja":
                    k = new DnevnaKarta();
                    break;
                case "mesecna voznja":
                    k = new MesecnaKarta();
                    break;
            }
        }
        public string[] Prikaz()
        {
            string[] p = new string[brStanica];
            for (int i = 0; i < brStanica; i++)
            {
                p[i] = stanice[i].Ime;
            }
            return p;
        }
        #region quicksort
        private void razmeni(ref Put x, ref Put y)
        {
            Put p = x;
            x = y;
            y = p;
        }

        private int podeli(Put[] a, int leva, int desna)
        {
            int k, i;
            k = leva;
            for (i = leva + 1; i <= desna; i++)
                if (a[i] < a[leva])
                {
                    razmeni(ref a[i], ref a[k + 1]);
                    k++;

                }
            razmeni(ref a[leva], ref a[k]);
            return k;

        }

        private void quickSort(Put[] a, int l, int d)
        {
            if (l < d)
            {
                int k = podeli(a, l, d);
                quickSort(a, l, k - 1);
                quickSort(a, k + 1, d);

            }
        }
        private void Sort(Put[] a, int n)
        {
            quickSort(a, 0, n - 1);
        }
        #endregion
        private Put[] Prepisi(Put[] pt, int n)
        {
            Put[] p = new Put[n];
            for (int i = 0; i < n; i++)
            {
                p[i] = pt[i];
            }
            return p;
        }
        private Put[] Izbaci(string poc, string kraj)
        {
            Stanica pocetna = this[poc];
            Stanica krajna = this[kraj];
            if (pocetna == null || krajna == null)
            {
                return null;
            }
            Veza[] st = new Veza[1000];
            int brojac = 0;
            Put[] putevi = new Put[10000];
            pocetna.Put(ref st, 0, ref putevi, ref brojac, krajna);
            int p = k.PreostaloVreme;
            int l = 0;
            for (int i = 0; i < brojac; i++) 
            {
                if (putevi[i].Vreme(60)>p) 
                {
                    l++;
                }
                else
                {
                    putevi[i-l] = putevi[i];
                }
            }
            brojac -= l;
            return Prepisi(putevi, brojac);
        }
        public string[] Ispis(string poc, string kraj)
        {
            Put[] p = Izbaci(poc, kraj);
            Sort(p, p.Length);
            string[] ispis = new string[p.Length];
            for (int i = 0; i < ispis.Length; i++)
            {
                ispis[i] = p[i].Ispis();
            }
            return ispis;
        }
    }
}



