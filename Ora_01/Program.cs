namespace SZTF2_Oktatas
{
    class Szemely
    {
        //Statikus adattag - Random szám generáláshoz
        static Random rnd = new Random();

        //Privát adattagok - kisbetűs nevek és privát láthatóságúak
        private string id;
        private string nev;
        private int kor;
        private string emailCim;

        //publikus tulajdonságok - Nagybetűs nevek és publikus láthatóságúak, getter és setter metódusokkal rendelkeznek
        public string Nev
        {
            get
            {
                return nev;
            }

            set
            {
                //A value értéke az "=" jel jobb oldalán lévő érték lesz mindig
                nev = value;
            }
        }

        public int Kor
        {
            get
            {
                return kor;
            }

            set
            {
                kor = value;
            }
        }

        public string EmailCim
        {
            get
            {
                return emailCim;
            }

            set
            {
                if (value.Contains('@'))
                {
                    emailCim = value;
                }
                else
                {
                    emailCim = "";
                }
            }
        }

        public string Id
        {
            get
            {
                return id;
            }

            private set
            {
                //Ha a value üres string ("")
                if (value == "")
                {
                    //Akkor generálunk egy id-t
                    id = nev.Substring(0, 2).ToUpper() + kor + rnd.Next(100,1000);
                }
                else
                {
                    //Ha nem üres akkor beállítjuk az értéket
                    id = value;
                }
            }
        }

        //id opcionális bemeneti paraméter
        public Szemely(string nev, int kor, string emailCim, string id = "")
        {
            //Random rnd = new Random();

            //Értéket adunk a tulajdonságoknak
            Nev = nev;
            Kor = kor;
            EmailCim = emailCim;

            Id = id;

            //Másik módja hogy vizsgáljuk az id értékét, tulajdonság setter metódusában szebb

            //if (id == "")
            //{
            //    this.id = nev.Substring(0, 2).ToUpper() + kor + rnd.Next(100);
            //}
            //else
            //{
            //    Id = id;
            //}
        }


        public void Koszon()
        {
            //Console.WriteLine("Helló, én {0} vagyok, és {1} éves vagyok!", nev, kor);
            //Console.WriteLine("Helló, én" + nev + " vagyok, és " + kor + "éves vagyok!");
            Console.WriteLine($"Helló, én {nev} vagyok, és {kor} éves vagyok!");
        }
    }


    class SzemelyManager
    {
        //publikus tulajdonság - Szemely tömb
        public Szemely[] Szemelyek { get; set; }

        public SzemelyManager()
        {
            //0 elemszámmal hozzuk létre, mivel később majd bővítjük és csökkentjük
            Szemelyek = new Szemely[0];
        }

        public void TombBovites(Szemely uj)
        {
            //Létrehozunk egy új tömböt egyel nagyobb elemszámmal mint az eredeti
            Szemely[] temp = new Szemely[Szemelyek.Length + 1];

            //Átmásoljuk az új tömbbe az eredeti tömbben lévő összes elemet
            for (int i = 0; i < Szemelyek.Length; i++)
            {
                temp[i] = Szemelyek[i];
            }

            //Az új tömb utolsó elemébe elhelyezzük a hozzáadandó Személyt
            temp[temp.Length - 1] = uj;

            //Majd az eredeti tömböt felülírjuk az új tömbbel
            Szemelyek = temp;
        }

        public void TombCsokkentes(string id)
        {
            //Létrehozunk egy új tömböt egyel kisebb elemszámmal mint az eredeti
            Szemely[] temp = new Szemely[Szemelyek.Length - 1];

            //Létrehozunk egy db változót az új tömbben való indexeléshez
            int db = 0;

            //Átmásoljuk az új tömbbe azokat az elemeket,
            //amelyeknek az Id tulajdonsága nem egyezik meg a keresett id-vel
            for (int i = 0; i < Szemelyek.Length; i++)
            {
                if (Szemelyek[i].Id != id)
                {
                    temp[db] = Szemelyek[i];
                    db++;
                }
            }

            //Majd az eredeti tömböt felülírjuk az új tömbbel
            Szemelyek = temp;
        }

        public void Mentes(Szemely sz)
        {
            //Létrehozunk egy 3 elemű string tömböt amiben eltároljuk a Személy adatait
            string[] adatok = new string[3];
            adatok[0] = sz.Nev;
            adatok[1] = sz.Kor.ToString();
            adatok[2] = sz.EmailCim;

            //Létrehozunk egy fájlt aminek a neve a személy Id-je lesz
            File.WriteAllLines(sz.Id + ".txt", adatok);
        }

        public void Betoltes(string[] adatok, string id)
        {
            //A bemeneti paraméterekből létrehozunk egy Személy objektumot, amelyet hozzáadunk a tömbhöz
            Szemely sz = new Szemely(adatok[0], int.Parse(adatok[1]), adatok[2], id);
            TombBovites(sz);
        }

        public void Torles(string id)
        {
            //Csak akkor törlünk ha a Szemelyek tömbben van elem
            if (Szemelyek.Length > 0)
            {
                TombCsokkentes(id);
                File.Delete(id + ".txt");
            }
        }
    }



    class Program
    {
        static void Main(string[] args)
        {
            //Példányosítunk egy SzemelyManager osztályt
            SzemelyManager s = new SzemelyManager();

            //Beolvassuk az összes fájl elérési útját
            //Directory.GetCurrentDirectory() -> bin/Debug mappa (.Net6.0-ban bin/Debug/net6.0)
            string[] fajlok = Directory.GetFiles(Directory.GetCurrentDirectory());

            //Végig iterálunk a fájlokon
            for (int i = 0; i < fajlok.Length; i++)
            {
                //Csak azokat a fájlokat olvassuk be amik .txt kiterjesztésűek
                if (fajlok[i].Contains(".txt"))
                {
                    //Kiszedjük a fájl nevéből a Személy id-jét

                    //string id = fajlok[i].Split("\\")[fajlok[i].Split("\\").Length].Substring(0,7);
                    //string id = fajlok[i].Split("\\")[^1].Substring(0,7);
                    string id = Path.GetFileNameWithoutExtension(fajlok[i]);

                    //Beolvassuk a fájl tartalmát
                    string[] adatok = File.ReadAllLines(fajlok[i]);

                    //Betöltjük a személyt a tömbbe
                    s.Betoltes(adatok, id);
                }
            }


            //Mehhívjuk az összes személy Koszon metódusát
            for (int i = 0; i < s.Szemelyek.Length; i++)
            {
                s.Szemelyek[i].Koszon();
            }


            //Létrehozunk egy személyt id nélkül, majd elmentjük
            Szemely sz1 = new Szemely("sz1", 65, "");
            s.TombBovites(sz1);
            s.Mentes(sz1);

            for (int i = 0; i < s.Szemelyek.Length; i++)
            {
                s.Szemelyek[i].Koszon();
            }


            //Kitörlünk egy adott id-jű személyt
            //s.Torles("SZ6572");

            ;
        }
    }
}