using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations
{
    class InstructorConfiguration : EntityTypeConfiguration<Instructor>
    {
        public InstructorConfiguration()
        {
            Ignore(x => x.Id);

            Property(x => x.LastName).HasMaxLength(64).IsRequired();
            Property(x => x.FirstName).HasMaxLength(32);

        }
    }
}
