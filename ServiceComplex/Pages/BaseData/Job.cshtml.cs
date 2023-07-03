using Application.BaseInfo;
using Domain.ComplexModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace ServiceComplex.Pages.BaseData
{
    public class JobModel : PageModel
    {
        private readonly IJobService _jobService;

        public List<Job> JobList;

        public JobModel(IJobService jobService)
        {
            _jobService = jobService;
        }

        public void OnGet()
        {
            JobList = _jobService.GetAll();
        }

        public void OnGetCreate()
        {

        }

        public IActionResult OnPostCreate(string JobName)
        {
            Job newJob = new Job
            {
               JobName = JobName 
            };
            _jobService.Insert(newJob);
            return Redirect("/BaseData/Job");
        }

        public IActionResult OnGetEdit(int JobId)
        {
            var Job = _jobService.GetJob(JobId);
            return new JsonResult(JsonConvert.SerializeObject(Job));
        }

        public IActionResult OnPostEdit(string JobName,int JobId)
        {
            var newJob = new Job()
            {
                JobName = JobName,
                JobId = JobId
            };
            _jobService.Update(newJob);
            return Redirect("/BaseData/Job");

        }

        public IActionResult OnGetRemove(int id)
        {
            return new JsonResult(_jobService.Remove(id));
        }

        public IActionResult OnGetCheckName(string name)
        {
            return new JsonResult(_jobService.GetJobByName(name));
        }
        public IActionResult OnGetCheckJobNameExists(string name, int id)
        {
            return new JsonResult(_jobService.CheckJobNameExists(name, id));
        }
    }
}
