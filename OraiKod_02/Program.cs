namespace OraiKod_02
{
    abstract class Konyv
    {
        public string Id { get; set; }
        public string Szerzo { get; set; }
        public string Cim { get; set; }

        public Konyv(string id, string szerzo, string cim)
        {
            Id = id;
            Szerzo = szerzo;
            Cim = cim;
        }

        public abstract string KonvyTipusa();

        public virtual void Informacio()
        {
            Console.WriteLine("Id: " + Id);
            Console.WriteLine("Szerző: " + Szerzo);
            Console.WriteLine("Cím: " + Cim);
        }
    }


    class Regeny : Konyv
    {
        public string Mufaj { get; set; }

        public Regeny(string id, string szerzo, string cim, string mufaj) : base(id, szerzo, cim)
        {
            Mufaj = mufaj;
        }

        public override string KonvyTipusa()
        {
            return "Regény";
        }

        public override void Informacio()
        {
            base.Informacio();
            Console.WriteLine("Műfaj: " + Mufaj);
        }
    }

    class Tankonyv : Konyv
    {
        public Tankonyv(string id, string szerzo, string cim) : base(id, szerzo, cim)
        {
        }

        public override string KonvyTipusa()
        {
            return "Tankönyv";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Konyv[] konyvek = new Konyv[2];
            konyvek[0] = new Regeny("asd", "én", "nem tudom", "te");
            konyvek[1] = new Tankonyv("jld", "Yes", "Bukás");

            for (int i = 0; i < konyvek.Length; i++)
            {
                konyvek[i].Informacio();
                if (konyvek[i].KonvyTipusa() == "Regény")
                {
                    Console.WriteLine("Ez a könyv egy regény olvasd nyugodtan.");
                }
                else if (konyvek[i].KonvyTipusa() == "Tankönyv")
                {
                    Console.WriteLine("EZ EGY TANKÖNYV! ÉGESD EL!");
                }
                Console.WriteLine("--------------------------------------");
            }
        }
    }
}