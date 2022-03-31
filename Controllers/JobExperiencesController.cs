namespace DevJobs.API.Controllers
{
    using DevJobs.API.Entities;
    using DevJobs.API.Models;
    using DevJobs.API.Persistence.Repositories;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/job-vacancies/{id}/experiences")]
    [ApiController]
    public class JobExperiencesController : ControllerBase
    {
        private readonly IJobVacancyRepository _repository;
        public JobExperiencesController(IJobVacancyRepository repository)
        {
            _repository = repository;
        }
        
        [HttpPost]
        public IActionResult Post(int id, AddJobExperienceInputModel model)
        {
            
            var vacancie = _repository.GetById(id);
            if (vacancie == null)
            {
                return NotFound();
            }
            
            var experience = new JobExperience(
                model.CompanyName,
                model.Range,
                model.Description,
                id
            );

            _repository.AddExperience(experience);
            
            return NoContent();
        }    
    }
}