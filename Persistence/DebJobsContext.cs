using DevJobs.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevJobs.API.Persistence
{
    public class DebJobsContext : DbContext
    {
        public DebJobsContext(DbContextOptions<DebJobsContext> options): base(options) { }

        public DbSet<JobVacancy> JobVacancies { get; set; }
        public DbSet<JobApplication> JobApplication { get; set; }
       
       protected override void OnModelCreating(ModelBuilder builder)
       {
            builder.Entity<JobVacancy>(e => {
               //e.ToTable("tb_job_vacancies");
               e.HasKey(jb => jb.Id);

               e.HasMany(jb => jb.Applications)
                    .WithOne()
                    .HasForeignKey(ja => ja.IdJobVacancy)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<JobApplication>(e => {
                e.HasKey(ja => ja.Id);	
            });
       }
    }
}