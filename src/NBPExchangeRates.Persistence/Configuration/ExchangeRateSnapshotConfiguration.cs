using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NBPExchangeRates.Domain.Entities;

namespace NBPExchangeRates.Persistence.Configuration;

public class ExchangeRateSnapshotConfiguration : IEntityTypeConfiguration<ExchangeRateSnapshot>
{
    public void Configure(EntityTypeBuilder<ExchangeRateSnapshot> builder)
    {
        builder.Property(x => x.Table)
            .HasMaxLength(1)
            .IsRequired();

        builder.Property(x => x.Number)
            .IsRequired();

        builder.Property(x => x.EffectiveDate)
            .HasColumnType("date")
            .IsRequired();
        
        builder.Property(x => x.CreatedAt)
            .HasColumnType("datetimeoffset(7)")
            .IsRequired();
    }
}