using System;

namespace BusMinus
{
    class BusMinus
    {
        Stanica[] stanice;
        int brStanica;
        public BusMinus(string[] niz)
        {
            brStanica = Convert.ToInt32(niz[0]);
            stanice = new Stanica[100 + brStanica];
            for (int i = 0; i < brStanica; i++)
            {
                stanice[i] = new Stanica(niz[i + 1]);
            }
            for (int i = brStanica + 1; i < niz.Length; i++)
            {
                string[] s = niz[i].Split('-');
                Stanica a = this[s[0]];
                Stanica b = this[s[1]];
                Veza v = new Veza(a, b, Convert.ToInt32(s[2]));
                a.DodajVezu(v);
                b.DodajVezu(v);
            }
        }
        public Stanica this[string imeStanice] 
        {
            get {
                for (int i = 0; i < brStanica; i++) {
                    if (stanice[i].Ime==imeStanice) {
                        return stanice[i];
                    }
                }
                return null;
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
        public void Sortiraj(Put[] a, int n)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = i+1; j < n; j++)
                {
                    if (a[i] > a[j])
                    {
                        Put temp = a[i];
                        a[i] = a[j];
                        a[j] = temp;
                    }
                }
            }
        }
       /* private void quickSort(Put[] a, int levo, int desno)
        {
            if (levo < desno)
            {
                int k = podeli2(a, levo, desno);
                quickSort(a, levo, k - 1);
                quickSort(a, k + 1, desno);
            }
        }

        private int podeli2(Put[] a, int levo, int desno)
        {
            Put[] b = new Put[desno - levo + 1];
            Put k = a[levo];
            int nlevo = 0;
            int ndesno = desno - levo;
            for (int i = levo; i <= desno; i++)
            {
                if (k < a[i])
                {
                    b[ndesno] = a[i];
                    ndesno--;
                }
                else
                {
                    b[nlevo] = a[i];
                    nlevo++;
                }
            }
            b[ndesno] = k;
            for (int i = 0; i < desno - levo + 1; i++)
            {
                a[levo + i] = b[i];
            }
            return ndesno + levo;
        }*/

        public string[] Ispis(string poc, string kraj)
        {
            Veza[] st = new Veza[1000];
            Stanica pocetna = this[poc];
            Stanica krajna = this[kraj];
            int brojac = 0;
            Put[] putevi = new Put[10000];
            pocetna.Put(ref st, 0, ref putevi, ref brojac, krajna);
            Sortiraj(putevi, brojac);
            //quickSort(putevi, 0, brojac);
            string[] ispis = new string[brojac];
            for (int i = 0; i < brojac; i++) 
            {
                ispis[i] = putevi[i].Ispis();
            }
            return ispis;
        }
    }
}



