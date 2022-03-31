namespace DevJobs.API.Controllers
{
    using DevJobs.API.Entities;
    using DevJobs.API.Models;
    using DevJobs.API.Persistence;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/job-vacancies/{id}/applications")]
    [ApiController]
    public class JobApplicationsController : ControllerBase
    {
        private readonly DebJobsContext _devJobsContext;            
        public JobApplicationsController(DebJobsContext devJobsContext)
        {
            _devJobsContext = devJobsContext;
        }
        
        [HttpPost]
        public IActionResult Post(int id, AddJobApplicationInputModel model)
        {
            
            var vacancie = _devJobsContext.JobVacancies.SingleOrDefault(j => j.Id == id);
            if (vacancie == null)
            {
                return NotFound();
            }
            
            var application = new JobApplication(
                model.ApplicantName,
                model.ApplicantEmail,
                model.IdJobVacancy
            );

            _devJobsContext.JobApplication.Add(application);
            _devJobsContext.SaveChanges();
            
            return NoContent();
        }    
    }
}