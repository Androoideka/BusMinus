using System;

namespace BusSharp
{
    public class Vozilo
    {
        double brzina; // izrazena u kilometrima po casu
        Stanica pocStan;//pocetna stanica
        DateTime pocVrm;//pocetno vreme
        string imeLinije;

        internal Vozilo(Stanica a, string linija_b)
        {
            pocStan = a;
            imeLinije = linija_b;
            pocVrm = DateTime.Now;
            brzina = 60;
        }

        internal double Brzina
        {
            get { return brzina * 10 / 36; }
            set { brzina = value; }
        }
        internal Stanica PocStan
        {
            get { return pocStan; }
            set { pocStan = value; }
        }
        internal string ImeLinije
        {
            get { return imeLinije; }
            set { imeLinije = value; }
        }
        private DateTime PocVrm
        {
            get { return pocVrm; }
            set { pocVrm = value; }
        }
        internal double trenutniPolozajM() // predjeni put izrazen u metrima
        {
            double vreme = (DateTime.Now - pocVrm).TotalSeconds;
            double predjenPut = Brzina * vreme;
            return predjenPut;
        }
        internal double kolikoDoStanice(Stanica stanica)
        {
            //Gasic
            double put = pocStan.DuzinaDoCilja(trenutniPolozajM(), stanica, imeLinije, null);
            //Milosevic
            //double put = pocStan.razdaljina(trenutniPolozajM(), imeLinije, stanica);
            double vreme = put / Brzina;
            return vreme;
        }
    }
}