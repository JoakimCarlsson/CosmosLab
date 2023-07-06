namespace CosmosLab.EntityFramework.Persistance.Entities;

public sealed record DbCar(
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
    public static implicit operator DbCar(Car car) => new(
        Guid.NewGuid(),
        car.Make,
        car.Model,
        car.Year,
        car.Color,
        car.Doors,
        car.Wheels,
        car.Windows,
        car.Seats,
        car.Cylinders,
        car.MilesPerGallon,
        car.MilesPerTank,
        car.TankSize,
        car.TopSpeed,
        car.Price,
        car.Horsepower,
        car.Torque,
        car.ZeroToSixty,
        car.QuarterMile,
        car.Weight,
        car.Length,
        car.Width
    );
}