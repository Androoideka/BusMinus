using System;

namespace BusSharp
{
    public class BusSharp
    {
        Linija[] ln;
        int brLn;
        Stanica[] stanice = new Stanica[100];
        int brStanica;
        Karta k;
        Vozilo[] vozilo;
        int brVoz = 0;

        public BusSharp(string[] niz)
        {
            int brojac = niz.Length;
            ln = new Linija[brojac];
            vozilo = new Vozilo[brojac];
            for (int i = 0; i < brojac; i++)
            {
                string[] s0 = niz[i].Split(':');
                string linija = s0[0].ToString();
                string[] s1 = s0[1].Split('-');
                int brojac2 = s1.Length - 2;
                ln[brLn] = NapraviLiniju(linija, s1, brojac2 + 2);
                brLn++;
                Vozilo v1 = new Vozilo(this[s1[0]], linija);
                vozilo[brVoz] = v1;
                brVoz++;
            }
        }
        public string[] Pozovi(string imeStanice)
        {
            string[] temp = this[imeStanice].Poziv(vozilo, brVoz);
            for(int i = 0; i < temp.Length; i++)
            {
                string[] a = temp[i].Split(' ');
                temp[i] = a[0] + " " + (int)Convert.ToDouble(a[1])/60 + " minuta";
            }
            return temp;
        }
        public string Kartica 
        {
            get 
            {
                if(k != null)
                {
                    int t = (int)k.PreostaloVreme;
                    if (t < 86400)
                    {
                        int sat = t / 3600;
                        int minut = t % 3600 / 60;
                        int sekund = t % 60;
                        return sat + ":" + minut + ":" + sekund;
                    }
                    else
                    {
                        return (int)k.PreostaloVreme / 86400 + " dana";
                    }
                }
                else
                {
                    return "Niste kupili kartu";
                }
            }
            set 
            {
                k = Karta.KupiKartu(value);
            }
        }
        private Stanica this[string imeStanice]
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
        private void Razmeni(ref Put x, ref Put y)
        {
            Put p = x;
            x = y;
            y = p;
        }

        private int Podeli(Put[] a, int leva, int desna)
        {
            int k, i;
            k = leva;
            for (i = leva + 1; i <= desna; i++)
            {
                if(a[i].Vreme(vozilo)<a[leva].Vreme(vozilo))
                {
                    Razmeni(ref a[i], ref a[k + 1]);
                    k++;
                }
            }
            Razmeni(ref a[leva], ref a[k]);
            return k;
        }

        private void QuickSort(Put[] a, int l, int d)
        {
            if (l < d)
            {
                int k = Podeli(a, l, d);
                QuickSort(a, l, k - 1);
                QuickSort(a, k + 1, d);
            }
        }
        private void Sort(Put[] a, int n)
        {
            QuickSort(a, 0, n - 1);
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
            putevi = Prepisi(putevi, brojac);
            Sort(putevi, putevi.Length);
            return putevi;
        }
        public string[] Ispis(string poc, string kraj)
        {
            Put[] p = Izbaci(poc, kraj);
            string[] ispis = new string[Math.Min(3, p.Length)];
            for (int i = 0; i < ispis.Length; i++)
            {
                ispis[i] = p[i].Ispis(vozilo);
            }
            return ispis;
        }
        private Linija NapraviLiniju(string linija, string[] podaci, int n)
        {
            Veza[] veze = new Veza[n - 1];
            for (int i = 0; i < n / 2; i++)
            {
                veze[i] = new Veza(this[podaci[i*2]], this[podaci[i*2 + 2]], linija, Convert.ToDouble(podaci[i*2 + 1]));
            }
            return new Linija(this[podaci[n-1]], veze, n / 2);
        }
        private Linija NapraviLiniju(string linija, string[] stanice, double[] rastojanja, int n)
        {
            Veza[] veze = new Veza[n-1];
            for (int i = 0; i < n-1; i++)
            {
                veze[i] = new Veza(this[stanice[i]], this[stanice[i+1]], linija, rastojanja[i]);
            }
            return new Linija(this[stanice[n-1]], veze, n-1);
        }
        public void DodajUNizLinija(string linija, string[] stanice, double[] rastojanja, int n)
        {
            if(brLn >= ln.Length)
            {
                Linija[] temp = new Linija[brLn + 100];
                for (int i = 0; i < brLn; i++)
                {
                    temp[i] = ln[i];
                }
                ln = temp;
            }
            ln[brLn] = NapraviLiniju(linija, stanice, rastojanja, n);
            brLn++;
        }
        public void IzmeniLiniju(string linija, string[] stanice, double[] rastojanja, int n, int i)
        {
            ln[i] = NapraviLiniju(linija, stanice, rastojanja, n);
        }
        public void IzvuciIzNizaLinija(int[] indeksi)
        {
            int k = 0;
            for(int i = 0; i < brLn; i++)
            {
                ln[i-k] = ln[i];
                if(Proveri(indeksi, i))
                {
                    k++;
                }
            }
        }
        private bool Proveri(int[] indeksi, int x)
        {
            for(int i = 0; i < brLn; i++)
            {
                if(indeksi[i] == x)
                {
                    return true;
                }
            }
            return false;
        }
        public string[] Linije
        {
            get
            {
                string[] ispisLinija = new string[brLn];
                for (int i = 0; i < brLn; i++)
                {
                    ispisLinija[i] = ln[i].Ispis();
                }
                return ispisLinija;
            }
        }
        #region komentari
        /*public BusMinus(string[] niz)
        {
            int brojac = niz.Length;
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
        public Linija[] NadjiLinije()
        {
            Linija[] ln = new Linija[brStanica];
            int n = 0;
            for (int i = 0; i < brStanica; i++)
            {
                Linija[] tln = stanice[i].LinijeKojePocinju();
                if(ln)
            }
            /*Linija[] ln = new Linija[100];
            int n = 0;
            for (int i = 0; i < brStanica; i++)
            {
                string[] lns = stanice[i].LinijeKojePocinju();
                for (int j = 0; lns[j] != ""; j++)
                {
                    for (int k = 0; k < n; k++)
                    {
                        if()
                    }
                }
            }
        }*/
        #endregion
    }
}



