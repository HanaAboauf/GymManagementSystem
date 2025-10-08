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
    internal class SessionConfig : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.ToTable(t =>
            {
                t.HasCheckConstraint("CK_Session_Time", "EndTime > StartTime");
                t.HasCheckConstraint("CK_Session_Capacity", "Capacity between 1 and 25");
            });

            #region Relationships

            #region Session-Category relationship

            builder.HasOne(s => s.Category)
                   .WithMany(c => c.Sessions)
                   .HasForeignKey(s => s.CategoryId);
            #endregion

            #region Session-Trainer relationship

           
        #endregion
        #endregion
    }
    }
}
