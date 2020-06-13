using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bus_Minus
{
    public class Put
    {
        Stanica[] st;
        int brst;
        int duzinaputa;

        public Put()
        {
            st = new Stanica[1000];
            brst = 0;
            duzinaputa = 0;
        }
        public Put(Put put)
        {
            duzinaputa = put.duzinaputa;
            st = new Stanica[put.brst+1000];
            brst = put.brst;
            for (int i = 0; i < brst; i++)
            {
                st[i] = new Stanica(put.st[i]);
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
			   s+= st[i].Ime;
			}
            s += duzinaputa;
            return s;
        }
    }
}
