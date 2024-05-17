using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

using ScheduleApplication.Models.DomainModels;

namespace ScheduleApp.Models.Configuration
{
    public class TeacherConfig : IEntityTypeConfiguration<Teacher>

    {
        public void Configure(EntityTypeBuilder<Teacher> entity)
        {
            entity.HasData(
     new Teacher { TeacherId = 1, FirstName = "Charbel", LastName = "Fares" },
     new Teacher { TeacherId = 2, FirstName = "John", LastName = "Doe" },
     new Teacher { TeacherId = 3, FirstName = "Jane", LastName = "Smith" },
     new Teacher { TeacherId = 4, FirstName = "Michael", LastName = "Johnson" },
     new Teacher { TeacherId = 5, FirstName = "Emily", LastName = "Brown" },
     new Teacher { TeacherId = 6, FirstName = "David", LastName = "Garcia" },
     new Teacher { TeacherId = 7, FirstName = "Jessica", LastName = "Martinez" },
     new Teacher { TeacherId = 8, FirstName = "Daniel", LastName = "Miller" }
 );
        }
    }
}