using Bogus;

namespace CosmosLab.Shared.Models;

public sealed record Car(
    Guid Id,
    string Make,
    string Model,
    int Year,
    string Color,
    int Doors,
    int Wheels,
    int Windows,
    int Seats,
    int Cylinders,
    int MilesPerGallon,
    int MilesPerTank,
    int TankSize,
    int TopSpeed,
    int Price,
    int Horsepower,
    int Torque,
    int ZeroToSixty,
    int QuarterMile,
    int Weight,
    int Length,
    int Width
)
{
    public static Car GenerateRandomCar()
    {
        return new Faker<Car>()
            .CustomInstantiator(f =>
                new Car(
                    Guid.NewGuid(),
                    f.Vehicle.Manufacturer(),
                    f.Vehicle.Model(),
                    f.Random.Number(1900, 2021),
                    f.Commerce.Color(),
                    f.Random.Number(2, 4),
                    f.Random.Number(1, 10),
                    f.Random.Number(1, 10),
                    f.Random.Number(1, 10),
                    f.Random.Number(1, 10),
                    f.Random.Number(1, 10),
                    f.Random.Number(1, 10),
                    f.Random.Number(1, 10),
                    f.Random.Number(1, 10),
                    f.Random.Number(1, 10),
                    f.Random.Number(1, 10),
                    f.Random.Number(1, 10),
                    f.Random.Number(1, 10),
                    f.Random.Number(1, 10),
                    f.Random.Number(1, 100000),
                    f.Random.Number(1, 100000),
                    f.Random.Number(1, 1000000)
                )
            ).Generate();
    }
};