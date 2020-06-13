using System;

namespace BusMinus
{
    internal abstract class Karta
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
        internal int PreostaloVreme 
        {
            get
            {
                int prostSek = poc.Second + vreme - DateTime.Now.Second;
                return prostSek; 
            }
        }
    }

    internal class JednokratnaKarta : Karta
    {
        internal JednokratnaKarta()
            : base()
        {
            cena = 30;
            vreme = 90 * 60;
        }
    }

    internal class DnevnaKarta : Karta
    {
        internal DnevnaKarta()
            : base()
        {
            cena = 500;
            vreme = 3600*24;
        }
    }

    internal class MesecnaKarta : Karta
    {
        internal MesecnaKarta()
            : base()
        {
            cena = 500;
            vreme = 3600 * 24*(DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)-DateTime.Now.Day);
        }
    }
}
