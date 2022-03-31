namespace DevJobs.API.Entities
{
    public class JobExperience
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Range { get; set; }
        public string Description { get; set; }
        public int IdJobVacancy { get; set; }

        public JobExperience(string companyName, string range, string description, int idJobVacancy)
        {
            CompanyName = companyName;
            Range = range;
            Description = description;
            IdJobVacancy = idJobVacancy;
        }                            
    }
}