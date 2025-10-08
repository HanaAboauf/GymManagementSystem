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
    internal class MemberSessionConfig : IEntityTypeConfiguration<MemberSession>
    {
        public void Configure(EntityTypeBuilder<MemberSession> builder)
        {
            builder.HasKey(ms => new {ms.MemberId,ms.SessionId});

            builder.Ignore(ms => ms.Id);

            builder.Property(ms => ms.CreatedAt)
                   .HasColumnName("BookingDate")
                   .HasDefaultValueSql("GETDATE()");

            builder.HasOne(ms => ms.Member)
                .WithMany(m => m.MemberSessions)
                .HasForeignKey(ms => ms.MemberId);

            builder.HasOne(ms => ms.Session)
                .WithMany(s => s.MemberSessions)
                .HasForeignKey(ms => ms.SessionId);
        }
    }
}
