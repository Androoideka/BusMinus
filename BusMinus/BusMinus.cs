using System;

namespace Bus_Minus
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
                Stanica a = stanice[FindIndexInArray(s[0])];
                Stanica b = stanice[FindIndexInArray(s[1])];
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
        public int FindIndexInArray(string x)
        {
            for (int i = 0; i < brStanica; i++)
            {
                if (stanice[i].Ime == x)
                    return i;
            }
            return -1;
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
        public void Sortiraj(Put[] pt, int brPt)
        {
            /*Put temp;
            for (int i = 0; i < brPt; i++)
            {
                for (int j = i+1; j < brPt; j++)
                {
                    if (pt[i].Duzina<pt[j].Duzina)
                    {
                        temp =pt[i];
                        pt[i] = pt[j];
                        pt[j] = temp;
                    }
                }
            }*/
        }
        public string[] Ispis(string poc, string kraj)
        {
            Stanica[] st = new Stanica[1000];
            Stanica pocetna = this[poc];
            Stanica krajna = this[kraj];
            int brojac = 0;
            Put[] putevi = new Put[10000];
            pocetna.Put(ref st, 0, 0, ref putevi, ref brojac, krajna);
            string[] ispis = new string[brojac];
            for (int i = 0; i < brojac; i++) 
            {
                ispis[i] = putevi[i].Ispis();
            }
            return ispis;
        }
    }
}



