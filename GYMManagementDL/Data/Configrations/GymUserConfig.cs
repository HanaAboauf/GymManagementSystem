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
    internal class GymUserConfig<T> : IEntityTypeConfiguration<T> where T : GymUser
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {

            builder.Property(m =>m.Name)
                   .IsRequired()
                   .HasColumnType("varchar")
                   .HasMaxLength(50);

            builder.Property(m => m.Email)
                   .IsRequired()
                   .HasColumnType("varchar")
                   .HasMaxLength(100);

            builder.HasIndex(m => m.Email).IsUnique();

            builder.Property(m => m.PhoneNumber)
                   .IsRequired()
                   .HasColumnType("varchar")
                   .HasMaxLength(11);
            builder.HasIndex(m => m.PhoneNumber).IsUnique();

            builder.OwnsOne(m => m.Address, ab => {
                 ab.Property(a => a.Street)
                  .HasColumnName("Street")
                  .HasColumnType("varchar")
                  .HasMaxLength(30);

                ab.Property(a => a.City)
                .HasColumnName("City")
                .HasColumnType("varchar")
                .HasMaxLength(30);

            });

            builder.ToTable(t=>
            {
                t.HasCheckConstraint("CK_Member_Email_Format", "Email LIKE '%_@__%.__%'");
                t.HasCheckConstraint("CK_Member_Phone_Format", "PhoneNumber LIKE '01%' AND PhoneNumber NOT LIKE '%[^0-9]%'");
            });

        }
    }
}
