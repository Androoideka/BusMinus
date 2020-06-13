namespace Bus_Minus
{
    public class Veza
    {
        Stanica pocetnaSt, krajnjaSt;
        int udaljenost;

        public Veza()
        {
            pocetnaSt = new Stanica();
            krajnjaSt = new Stanica();
            udaljenost = 0;
        }
        public Veza(string poc, string kraj, int udalj)
        {
            pocetnaSt = new Stanica(poc);
            krajnjaSt = new Stanica(kraj);
            udaljenost = udalj;
        }

        public Veza(Stanica poc, Stanica kraj, int udalj)
        {
            pocetnaSt = poc;
            krajnjaSt = kraj;
            udaljenost = udalj;
        }

        public Stanica POCStanica
        {
            get
            {
                return pocetnaSt;
            }
            set
            {
                pocetnaSt = new Stanica(value);
            }
        }
        public Stanica KRAJstanica
        {
            get {
                return krajnjaSt;
            }
            set {
                krajnjaSt = new Stanica(value);
            }
        }
        public int Udalj
        {
            get {
                return udaljenost;
            }
            set {
                udaljenost = value;
            }
        }
        public Stanica DrugaStanicaVeze(Stanica s)
        {
            if (s == pocetnaSt)
            {
                return krajnjaSt;
            }
            else if (s == krajnjaSt)
            {
                return pocetnaSt;
            }
            return null;
        }
    }
}
