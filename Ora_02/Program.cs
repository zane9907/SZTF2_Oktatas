namespace Ora_02
{
    class Program
    {
        //ős absztack osztály - nem lehet pédányosítani,
        //minden abstract-al jelölt metódust és tulajdonságot kötelező implementálni a leszármazottban
        //Az abstract osztályok úgymond egy gyűjtő osztályok, a leszármazott osztályokban lévő
        //közös tulajdonságokat és metódusokat (cselekvéseket) gyűjtjük ki egy helyre.
        abstract class Animal
        {
            //publikus tulajdonságok
            public string Name { get; set; }
            public string Species { get; set; }

            //konstruktor
            public Animal(string name, string species)
            {
                Name = name;
                Species = species;
            }

            //Abstract metódusok - kötelező implementálni leszármazottban
            public abstract void MakeSound();

            public abstract void Move();


            //Virtuális metódus - itt kell definiálni de leszármazott osztályokban felül lehet írni a működését
            public virtual void Greeting()
            {
                Console.WriteLine($"I'm a {Species} and my name is {Name}");
            }
        }


        class Dog : Animal
        {

            //public Dog(string name, string species)
            //{
            //    Name = name;
            //    Species = species;
            //}

            //Konstruktor - base kulcszóval meghívjuk az ős osztály konstruktorát és átadjuk neki a szükséges értékeket
            public Dog(string name, string species) : base(name, species)
            {
                
            }

            //Kötelezően implementált abstract metódusok
            //override kulcszóval lehet felülírni őket
            public override void MakeSound()
            {
                Console.WriteLine("Woof");
            }

            public override void Move()
            {
                Console.WriteLine("The dog is walking");
            }
        }

        class Cat : Animal
        {
            //publikus tulajdonság
            public int Age { get; set; }

            
            //Konstruktor - base kulcszóval meghívjuk az ős osztály konstruktorát és átadjuk neki a szükséges értékeket
            public Cat(string name, string species, int age) : base(name, species)
            {
                Age = age;
            }


            //Kötelezően implementált abstract metódusok
            //override kulcszóval lehet felülírni őket
            public override void MakeSound()
            {
                Console.WriteLine("Meow");
            }

            public override void Move()
            {
                Console.WriteLine("The dog is climbing");
            }


            //Felülírt virtuális metódus - Eltérhetünk az ősben lévő működéstől
            //De meghívjatjuk az ősben lévő metódust is a "base" kulcsszó segítségével
            public override void Greeting()
            {
                Console.WriteLine($"My name is {Name} and I'm {Age} years old!");
            }
        }


        static void Main(string[] args)
        {
            //Animal objektumok, Dog és Cat példányként létrehozva
            //Csak azok a metódusok és tulajdonságok érhetőek el, amelyek az Animal osztályban vannak
            //Viszont a felülírt metódusoknál a referencia szerinti (Dog vagy Cat) osztályban levő metódus fog meghívódni
            Animal d = new Dog("dog1", "Dog");
            Animal c = new Cat("cat1", "Cat", 12);


            //Amimal típusú osztály
            //Olyan objektumokat tehetünk bele, amelyek leszármaznak az Animal osztályból
            Animal[] animals = new Animal[2];
            animals[0] = d;
            animals[1] = c;

            for (int i = 0; i < animals.Length; i++)
            {
                animals[i].Greeting();
                animals[i].MakeSound();
                Console.WriteLine($"This anima species: {animals[i].Species}");

                //Megnézzük hogy az animals[i] eleme Cat típusú-e
                if (animals[i] is Cat)
                {
                    //Ha igen akkor kiírjuk az életkorát, de előtte Cat típusúra kell castolni(konvertálni)
                    //Mivel az Age tulajdonság csak a Cat osztályban érhető el
                    Console.WriteLine($"The cats age is: {(animals[i] as Cat).Age}");
                }                
            }

        }
    }
}