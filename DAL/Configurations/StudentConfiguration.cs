using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations
{
    class StudentConfiguration : EntityTypeConfiguration<Student>
    {
        public StudentConfiguration()
        {
            Ignore(x => x.Id);
            //HasKey(x => x.MyId);

            Property(x => x.LastName).HasMaxLength(64).IsRequired();
        }
    }
}
