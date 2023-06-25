namespace CosmosLab.Functions.Features.CosmosSDK.Models;

internal sealed record Car(
    Guid Id,
    string Make,
    string Model,
    int Year,
    string Color,
    int Doors
);