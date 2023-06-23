namespace CosmosLab.EntityFramework.Persistance;

public class CosmosLabDbContext : DbContext
{
    public DbSet<DbCar> Cars => Set<DbCar>();
    
    public CosmosLabDbContext(DbContextOptions<CosmosLabDbContext> options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}