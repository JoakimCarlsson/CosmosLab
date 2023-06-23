namespace CosmosLab.EntityFramework.Persistance.EntityTypeConfiguration;

public class DbCarEntityTypeConfiguration : IEntityTypeConfiguration<DbCar>
{
    public void Configure(EntityTypeBuilder<DbCar> builder)
    {
        builder.ToContainer("Cars");
        builder.HasNoDiscriminator();
        builder.HasKey(x => x.Id);
        builder.HasPartitionKey(x => x.Make);
    }
}