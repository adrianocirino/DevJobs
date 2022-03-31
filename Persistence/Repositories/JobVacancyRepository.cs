using DevJobs.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevJobs.API.Persistence.Repositories
{
    public class JobVacancyRepository : IJobVacancyRepository
    {
        private readonly DebJobsContext _context;
        public JobVacancyRepository(DebJobsContext context)
        {
            _context = context;
        }

        public List<JobVacancy> GetAll()
        {
            return _context.JobVacancies.Include(ja => ja.Applications).ToList();
        }

        public JobVacancy GetById(int id)
        {
            return _context.JobVacancies.Include(ja => ja.Applications).SingleOrDefault(jv => jv.Id == id);
        }

        public void Add(JobVacancy jobVacancy)
        {
            _context.JobVacancies.Add(jobVacancy);
            _context.SaveChanges();
        }

        public void Update(JobVacancy jobVacancy)
        {
            _context.JobVacancies.Update(jobVacancy);
            _context.SaveChanges();
        }
        public void AddApplication(JobApplication jobApplication)
        {
            _context.JobApplication.Add(jobApplication);
            _context.SaveChanges();
        }        
    }
}