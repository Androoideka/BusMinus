using System;

namespace BusSharp
{
    abstract class Karta
    {
        protected int cena, vreme;
        protected DateTime poc;
        protected Karta()
        {
            cena = 0;
            vreme = 0;
            poc = DateTime.Now;
        }
        protected Karta(Karta k)
        {
            cena = k.cena;
            vreme = k.vreme;
            poc = k.poc;
        }
        internal static Karta KupiKartu(string tip)
        {
            tip = tip.ToLower();
            Karta k;
            switch (tip)
            {
                case "jedna voznja":
                    k = new JednokratnaKarta();
                    break;
                case "celodnevna voznja":
                    k = new DnevnaKarta();
                    break;
                case "mesecna voznja":
                    k = new MesecnaKarta();
                    break;
                default:
                    k = null;
                    break;
            }
            return k;
        }
        internal int Cena
         {
             get {
                 return cena;
             }
             set {
                 cena = value;
             }
         }
        internal bool Aktivan
        {
            get
            {
                if (vreme != 0)
                {
                    return true;
                }
                return false;
            }
            set
            {
                if (!value)
                {
                    vreme = 0;
                }
            }
        }
        internal double PreostaloVreme 
        {
            get
            {
                double prostSek = vreme - (DateTime.Now - poc).TotalSeconds;
                return prostSek; 
            }
        }
    }

    class JednokratnaKarta : Karta
    {
        internal JednokratnaKarta()
            : base()
        {
            cena = 30;
            vreme = 90 * 60;
        }
    }

    class DnevnaKarta : Karta
    {
        internal DnevnaKarta()
            : base()
        {
            cena = 500;
            vreme = 3600*24;
        }
    }

    class MesecnaKarta : Karta
    {
        internal MesecnaKarta()
            : base()
        {
            cena = 500;
            vreme = 3600 * 24*(DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)-DateTime.Now.Day);
        }
    }
}
