using Microsoft.EntityFrameworkCore;

namespace LibrusProject.Model
{
    public class StudyContext : DbContext
    {
        public DbSet<Student> students { get; set; }

        public DbSet<Grade> grades { get; set; }

        public StudyContext(DbContextOptions options) : base(options) 
        {
        
        }
    }
}
