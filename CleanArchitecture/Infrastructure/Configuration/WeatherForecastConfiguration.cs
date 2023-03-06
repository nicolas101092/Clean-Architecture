using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    internal class WeatherForecastConfiguration : IEntityTypeConfiguration<WeatherForecast>
    {
        public void Configure(EntityTypeBuilder<WeatherForecast> builder)
        {
            builder.ToTable("WEATHERFORECAST");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                   .HasColumnName("ID");

            builder.Property(e => e.Date)
                   .HasColumnName("DATE");

            builder.Property(e => e.TemperatureC)
                   .HasColumnName("TEMPERATURE_C");

            builder.Property(e => e.Summary)
                   .HasMaxLength(450)
                   .HasColumnName("SUMMARY");
        }
    }
}
