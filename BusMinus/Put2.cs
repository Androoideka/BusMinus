namespace BusMinus
{
    class Put2
    {
        Stanica[] st;
        int brst;
        int duzinaputa;

        public Put2()
        {
            st = new Stanica[1000];
            brst = 0;
            duzinaputa = 0;
        }
        public Put2(Put2 put)
        {
            duzinaputa = put.duzinaputa;
            st = new Stanica[put.brst+1000];
            brst = put.brst;
            for (int i = 0; i < brst; i++)
            {
                st[i] = new Stanica(put.st[i]);
            }
        }
        public Put2(Stanica[] s, int br, int duz)
        {
            brst = br;
            duzinaputa = duz;
            st = new Stanica[br];
            for (int i = 0; i < br; i++)
            {
                st[i] = s[i];
            }
        }
        public int Duzina
        {
            get {
                return duzinaputa;
            }
            set {
                duzinaputa = value;
            }
        }
        public int BrojStanica
        {
            get
            {
                return brst;
            }
            set
            {
                brst = value;
            }
        }
        public static bool operator >(Put2 a, Put2 b)
        {
            if (a == null || b == null || a.duzinaputa <= b.duzinaputa)
                return false;
            return true;
        }
        public static bool operator <(Put2 a, Put2 b)
        {
            if (a == null || b == null || a.duzinaputa >= b.duzinaputa)
                return false;
            return true;
        }
        public void DodajStanicu(Stanica v)
        {
            st[brst] = v;
            brst++;
        }
        public string Ispis() 
        {
            string s = " ";
            for (int i = 0; i < brst; i++)
			{
			   s+= st[i].Ime + "-";
			}
            s += duzinaputa;
            return s;
        }
    }
}
