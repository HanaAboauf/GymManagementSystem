using GYMManagementDL.Enitities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYMManagementDL.Data.Configrations
{
    internal class PlanConfig : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {

            builder.Property(t => t.Name)
                   .HasColumnType("varchar")
                   .HasMaxLength(50);

            builder.Property(t => t.Description)
                     .HasColumnType("varchar")
                     .HasMaxLength(200);

            builder.Property(t => t.Price)
                   .HasPrecision(10, 2);

            builder.ToTable(t=>t.HasCheckConstraint("CK_Plan_DurationDays", "DurationDays BETWEEN 1 and 365"));
        }
    }
}
