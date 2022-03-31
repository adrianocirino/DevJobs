using DevJobs.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevJobs.API.Persistence
{
    public class DebJobsContext : DbContext
    {
        public DebJobsContext(DbContextOptions<DebJobsContext> options): base(options) { }

        public DbSet<JobVacancy> JobVacancies { get; set; }
        public DbSet<JobApplication> JobApplication { get; set; }
        public DbSet<JobExperience> JobExperience { get; set; }                       
        protected override void OnModelCreating(ModelBuilder builder)
        {
                builder.Entity<JobVacancy>(e => {
                //e.ToTable("tb_job_vacancies");
                e.HasKey(jv => jv.Id);

                e.HasMany(jv => jv.Applications)
                        .WithOne()
                        .HasForeignKey(ja => ja.IdJobVacancy)
                        .OnDelete(DeleteBehavior.Restrict);
                    
                    e.HasMany(jv => jv.Experiences)
                        .WithOne()
                        .HasForeignKey(je => je.IdJobVacancy)
                        .OnDelete(DeleteBehavior.Restrict);
                });

                builder.Entity<JobApplication>(e => {
                    e.HasKey(ja => ja.Id);	
                });

                builder.Entity<JobExperience>(e => {
                    e.HasKey(je => je.Id);
                });
        }
    }
}