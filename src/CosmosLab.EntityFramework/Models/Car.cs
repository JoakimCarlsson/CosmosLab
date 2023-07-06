namespace CosmosLab.EntityFramework.Models;

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
);