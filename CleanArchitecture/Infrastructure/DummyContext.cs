using Infrastructure.Configuration;

namespace Infrastructure
{
    /// <summary>
    /// Class defining the Dummy context
    /// </summary>
    public class DummyContext : DbContext
    {
        public virtual DbSet<WeatherForecast> WeatherForecasts { get; set; } = null!;

        public DummyContext(DbContextOptions<DummyContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new WeatherForecastConfiguration());
        }
    }
}
