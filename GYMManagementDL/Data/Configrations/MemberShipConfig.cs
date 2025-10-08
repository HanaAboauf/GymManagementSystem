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
    internal class MemberShipConfig : IEntityTypeConfiguration<MemberShip>
    {
        public void Configure(EntityTypeBuilder<MemberShip> builder)
        {
            builder.HasKey(ms => new{ ms.MemberId,ms.PlanId});

            builder.Ignore(ms=>ms.Id);

            builder.Property(ms=>ms.CreatedAt)
                   .HasColumnName("StartDate")
                   .HasDefaultValueSql("GETDATE()");



            builder.HasOne(ms => ms.Member)
                   .WithMany(m => m.MemberShips)
                   .HasForeignKey(ms => ms.MemberId);

            builder.HasOne(ms => ms.Plan)
                   .WithMany(p => p.MemberShips)
                   .HasForeignKey(ms => ms.PlanId);
        }
    }
}
