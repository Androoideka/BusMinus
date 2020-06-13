using System;

namespace BusMinus
{
    class Vozilo
    {
        double brzina; // izrazena u kilometrima po casu
        Stanica pocStan;//pocetna stanica
        DateTime pocVrm;//pocetno vreme
        string imeLinije;

        internal double Brizna
        {
            get { return brzina; }
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
            double vreme = DateTime.Now.Second - pocVrm.Second;
            double predjenPut = (brzina / 3.6) * vreme;
            return predjenPut;
        }
        internal double kolikoDoStanice(Stanica stanica)
        {
            //double put = pocStan.DuzinaDoCilja(trenutniPolozajM(), stanica, imeLinije, null);
            double put = pocStan.razdaljina(trenutniPolozajM(), imeLinije, stanica);
            double vreme = put / brzina;
            return vreme;
        }
        public class Autobus : Vozilo
        {

        }
        public class Tramvaj : Vozilo
        {

        }
        public class Kombi : Vozilo
        {

        }
    }
}