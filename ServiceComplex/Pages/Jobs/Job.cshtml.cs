using Application.Job;
using Domain.ComplexModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceComplex.Pages.Jobs
{
    public class JobModel : PageModel
    {
        private readonly IJobService _jobService;

        public List<Domain.ComplexModels.Job> JobList;

        public JobModel(IJobService jobService)
        {
            _jobService = jobService;
        }

        public void OnGet()
        {
            JobList = _jobService.GetAll();
        }
    }
}
