namespace CosmosLab.EntityFramework.Models;

public sealed record Car(
    string Make,
    string Model,
    int Year,
    string Color,
    int Doors
);