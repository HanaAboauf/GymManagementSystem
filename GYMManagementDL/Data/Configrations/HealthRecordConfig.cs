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
    internal class HealthRecordConfig : IEntityTypeConfiguration<HealthRecord>
    {
        public void Configure(EntityTypeBuilder<HealthRecord> builder)
        {
            builder.ToTable("Members");

            builder.HasOne<Member>()
                   .WithOne(m => m.HealthRecord)
                   .HasForeignKey<HealthRecord>(hr => hr.Id);

            builder.Ignore(b => b.CreatedAt);
            builder.Ignore(b => b.UpdatedAt);
        }
    }
}
