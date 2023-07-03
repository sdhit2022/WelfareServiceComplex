using Application.Interfaces.Context;
using Application.Interfaces;
using Application.Product;
using AutoMapper;
using Domain.ComplexModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;

namespace Application.BaseInfo
{
    public interface IJobService
    {
        List<Job> GetAll();
        Job GetJob(int id);
        ResultDto Insert(Job job);
        ResultDto Update(Job job);
        ResultDto Remove(int id);

        void saveChanges();

        bool GetJobByName(string name);
        bool CheckJobNameExists(string name, int id);
    }


    public class JobService : IJobService
    {
        private readonly IAuthHelper _authHelper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ILogger<ProductService> _logger;
        private readonly IMapper _mapper;
        private readonly IComplexContext _complexContext;

        public JobService(IAuthHelper authHelper, IHttpContextAccessor contextAccessor, ILogger<ProductService> logger, IMapper mapper, IComplexContext complexContext)
        {
            _authHelper = authHelper;
            _contextAccessor = contextAccessor;
            _logger = logger;
            _mapper = mapper;
            _complexContext = complexContext;
        }

        public List<Job> GetAll()
        {
            return _complexContext.Jobs.ToList();
        }

        public Job GetJob(int id)
        {
            return _complexContext.Jobs.Find(id);
        }

        public ResultDto Insert(Job job)
        {
            var result = new ResultDto();
            try
            {
                _complexContext.Jobs.Add(job);
                saveChanges();
                return result.Succeeded();
            }
            catch (Exception ex)
            {
                _logger.LogError($"حین ذخیره شغل خطای {ex} رخ داد");
                return result.Failed("ذخیره سازی شغل ناموفق بود!");
            }
        }

        public void saveChanges()
        {
            _complexContext.SaveChanges();
        }

        public ResultDto Update(Job job)
        {
            var result = new ResultDto();
            var oldjob = _complexContext.Jobs.Find(job.JobId);
            if (oldjob != null)
            {
                oldjob.JobName = job.JobName;
                try
                {
                    _complexContext.Jobs.Update(oldjob);
                    saveChanges();
                    return result.Succeeded();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"حین به روز رسانی شغل با آیدی {oldjob.JobId} خطای زیر رخ داد {ex}");
                    return result.Failed($"{ex}");
                }
            }
            return result.Failed("شغل پیدا نشد!");
        }

        public ResultDto Remove(int id)
        {
            var result = new ResultDto();
            Job job = _complexContext.Jobs.Find(id);
            if (job != null)
            {
                try
                {
                    _complexContext.Jobs.Remove(job);
                    saveChanges();
                    return result.Succeeded();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"حین حذف شغل با آیدی {id} خطای زیر رخ داد {ex}");
                    return result.Failed("عملیات با خطا مواجه شد");
                }
            }
            _logger.LogError($"Don't Any Record width Id {id} of Table Job");
            return result.Failed("شغل وجود ندارد");
        }
        public bool CheckJobNameExists(string name, int id)
        {
            var result = _complexContext.Jobs.Any(u => u.JobName == name && u.JobId != id);
            return result;
        }
        public bool GetJobByName(string name)
        {
            var result = _complexContext.Jobs.Any(u => u.JobName == name);
            return result;

        }

    }
}
