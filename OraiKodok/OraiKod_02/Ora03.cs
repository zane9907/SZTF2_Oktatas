namespace Ora03
{
    abstract class Vehicle
    {
        public string Brand { get; set; }

        public abstract string FuelType();

        public void Drive()
        {
            Console.WriteLine($"Driving a {Brand} vehicle!");
        }
    }

    interface IPassengerCarrier
    {
        void BoardPassenger();
    }

    interface ICargoCarrier
    {
        void LoadCargo();
    }

    class Car : Vehicle, IPassengerCarrier
    {
        public void BoardPassenger()
        {
            Console.WriteLine("Boarding a passenger into the car.");
        }

        public override string FuelType()
        {
            return "Petrol";
        }
    }

    class Truck : Vehicle, ICargoCarrier
    {
        public override string FuelType()
        {
            return "Diesel";
        }

        public void LoadCargo()
        {
            Console.WriteLine("Loading cargo into the truck");
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Vehicle car = new Car();
            car.Brand = "Suzuki";
            Vehicle truck = new Truck();
            truck.Brand = "Mercedes";

            DescribeVehicle(car);
            DescribeVehicle(truck);
        }

        static void DescribeVehicle(Vehicle vehicle)
        {
            Console.WriteLine($"Brand: {vehicle.Brand}");
            Console.WriteLine($"Fuel type: {vehicle.FuelType()}");

            if (vehicle is ICargoCarrier)
            {
                (vehicle as ICargoCarrier).LoadCargo();
            }

            if (vehicle is IPassengerCarrier car)
            {
                //(vehicle as IPassengerCarrier).BoardPassenger();
                car.BoardPassenger();
            }            
        }
    }
}