using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NBPExchangeRates.Domain.Entities;

namespace NBPExchangeRates.Persistence.Configuration;

public class ExchangeRateConfiguration : IEntityTypeConfiguration<ExchangeRate>
{
    public void Configure(EntityTypeBuilder<ExchangeRate> builder)
    {
        builder.Property(x => x.Mid)
            .HasColumnType("decimal(10,6)");

        builder.Property(x => x.Bid)
            .HasColumnType("decimal(10,6)");

        builder.Property(x => x.Ask)
            .HasColumnType("decimal(10,6)");

        builder.HasOne(x => x.ExchangeRateSnapshot)
            .WithMany(d => d.ExchangeRates)
            .HasForeignKey(x => x.ExchangeRateSnapshotId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}