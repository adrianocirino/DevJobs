namespace DevJobs.API.Controllers
{
    using DevJobs.API.Entities;
    using DevJobs.API.Models;
    using DevJobs.API.Persistence.Repositories;
    using Microsoft.AspNetCore.Mvc;
    using Serilog;

    [Route("api/job-vacancies")]
    [ApiController]
    public class JobVacanciesController : ControllerBase
    {
        private readonly IJobVacancyRepository _repository;
        public JobVacanciesController(IJobVacancyRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var vacancies = _repository.GetAll();

            return Ok(vacancies);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var vacancie = _repository.GetById(id);
            
            if (vacancie == null)
            {
                return NotFound();
            }

            return Ok(vacancie);
        }

        /// <summary>
        /// Cadastrar uma vaga de emprego.
        /// </summary> 
        /// <remarks>
        /// {
        ///     "title": "Dev .NET PL",
        ///     "description": "Descrição vaga Dev .NET PL",
        ///     "company": "string",
        ///     "isRemote": true,
        ///     "salaryRange": "5000-8000"
        /// }
        /// </remarks>
        /// <param name="model">Dados da vaga.</param>
        /// <returns>Objeto recem criado</returns>
        /// <response code="201">Sucesso.</response>
        /// <response code="400">Dados Inválidos</response>
        [HttpPost]
        public IActionResult Post(AddJobVacancyInputModel model)
        {
            Log.Information("POST JobVacancy chamado");

            var vacancie = new JobVacancy(
                model.Title,
                model.Description,
                model.Company,
                model.IsRemote,
                model.SalaryRange
            );

            _repository.Add(vacancie);

            return CreatedAtAction("GetById", new { id = vacancie.Id}, vacancie);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateJobVacancyInputModel model)
        {
            var vacancie = _repository.GetById(id);
            if (vacancie == null)
            {
                return NotFound();
            }

            vacancie.Update(model.Title, model.Description);
            
            _repository.Update(vacancie);
            
            return NoContent();
        }


    }
}