using ComputerCare.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComputerCare.Infrastructure.Configurations;

public class RepairRequestConfiguration : IEntityTypeConfiguration<RepairRequest>
{
    public void Configure(EntityTypeBuilder<RepairRequest> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.DeviceType)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(r => r.IssueDescription)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(r => r.TotalCost)
            .HasColumnType("decimal(18,2)");

        builder.Property(r => r.Notes)
            .HasMaxLength(2000);

        builder.HasOne(r => r.Customer)
            .WithMany(c => c.RepairRequests)
            .HasForeignKey(r => r.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.AssignedEmployee)
            .WithMany(e => e.AssignedRepairRequests)
            .HasForeignKey(r => r.AssignedEmployeeId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(r => r.CustomerId);
        builder.HasIndex(r => r.Status);
        builder.HasIndex(r => r.CreatedDate);
    }
}
