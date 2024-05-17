using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
 using ScheduleApplication.Models.DomainModels;

namespace ScheduleApp.Models.Configuration
{
    public class ClassConfig : IEntityTypeConfiguration<Class>
    {
        public void Configure(EntityTypeBuilder<Class> entity)
        {
            entity.HasOne(c => c.Teacher)
                  .WithMany(t => t.Classes)
                  .HasForeignKey(c => c.TeacherId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasData(
                new Class { ClassId = 1, Title = "Intro to C", Number = 100, TeacherId = 1, DayId = 1, MilitaryTime = "0800" },
                new Class { ClassId = 2, Title = "Data Structures", Number = 101, TeacherId = 1, DayId = 2, MilitaryTime = "0930" },
                new Class { ClassId = 3, Title = "Machine Learning", Number = 102, TeacherId = 1, DayId = 3, MilitaryTime = "1100" },
                new Class { ClassId = 4, Title = "Artificial Intelligence", Number = 103, TeacherId = 1, DayId = 4, MilitaryTime = "1230" },
                new Class { ClassId = 5, Title = "Database Management", Number = 104, TeacherId = 2, DayId = 5, MilitaryTime = "1400" },
                new Class { ClassId = 6, Title = "Software Engineering", Number = 105, TeacherId = 3, DayId = 1, MilitaryTime = "1530" },
                new Class { ClassId = 7, Title = "Web Development", Number = 106, TeacherId = 4, DayId = 2, MilitaryTime = "1700" },
                new Class { ClassId = 8, Title = "Computer Networks", Number = 107, TeacherId = 5, DayId = 3, MilitaryTime = "1830" }
            );
        }
    }
}

