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
    internal class TrainerConfig :GymUserConfig<Trainer> ,IEntityTypeConfiguration<Trainer>
    {
        public new void Configure(EntityTypeBuilder<Trainer> builder)
        {
            builder.Property(t => t.CreatedAt)
                   .HasColumnName("HireDate")
                   .HasDefaultValueSql("GETDATE()");

            base.Configure(builder);


            #region Trainer-Session relationship

            builder.HasMany(t => t.Sessions)
                   .WithOne(s => s.Trainer)
                   .HasForeignKey(s => s.TrainerId);
            #endregion
        }
    }
}
