using DevJobs.API.Entities;

namespace DevJobs.API.Persistence
{
    public class DebJobsContext
    {
        public List<JobVacancy> JobVacancies { get; set; }
        public DebJobsContext()
        {
            JobVacancies = new List<JobVacancy>();
        }
       
    }
}