namespace DevJobs.API.Controllers
{
    using DevJobs.API.Entities;
    using DevJobs.API.Models;
    using DevJobs.API.Persistence;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Route("api/job-vacancies")]
    [ApiController]
    public class JobVacanciesController : ControllerBase
    {
        private readonly DebJobsContext _devJobsContext;       
        public JobVacanciesController(DebJobsContext devJobsContext)
        {
            _devJobsContext = devJobsContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var vacancies = _devJobsContext.JobVacancies;
              
            return Ok(vacancies);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var vacancie = _devJobsContext.JobVacancies
                .Include(e => e.Applications)
                .SingleOrDefault(j => j.Id == id);
            
            if (vacancie == null)
            {
                return NotFound();
            }

            return Ok(vacancie);
        }

        [HttpPost]
        public IActionResult Post(AddJobVacancyInputModel model)
        {
            var vacancie = new JobVacancy(
                model.Title,
                model.Description,
                model.Company,
                model.IsRemote,
                model.SalaryRange
            );

            _devJobsContext.JobVacancies.Add(vacancie);
            _devJobsContext.SaveChanges();

            return CreatedAtAction("GetById", new { id = vacancie.Id}, vacancie);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateJobVacancyInputModel model)
        {
            var vacancie = _devJobsContext.JobVacancies.SingleOrDefault(j => j.Id == id);
            if (vacancie == null)
            {
                return NotFound();
            }

            vacancie.Update(model.Title, model.Description);
            _devJobsContext.SaveChanges();
            
            return NoContent();
        }


    }
}