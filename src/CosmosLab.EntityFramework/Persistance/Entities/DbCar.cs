namespace CosmosLab.EntityFramework.Persistance.Entities;

public sealed record DbCar(
    Guid Id,
    string Make,
    string Model,
    int Year,
    string Color,
    int Doors
) {
    public static implicit operator DbCar(Car car) => new(
        Guid.NewGuid(),
        car.Make,
        car.Model,
        car.Year,
        car.Color,
        car.Doors
        );
}