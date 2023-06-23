namespace CosmosLab.EntityFramework.Persistance.Entities;

public record DbCar(
    Guid Id,
    string Make,
    string Model,
    int Year,
    string Color,
    int Doors
);