using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using contractor.Models;
using contractor.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace contractor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobsController : ControllerBase
    {
        private readonly JobsService _service;
        private readonly AccountsService _acctService;

        public JobsController(JobsService service, AccountsService acctsService)
        {
            _service = service;
            _acctService = acctsService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Job>> GetAll()
        {
            try
            {
                IEnumerable<Job> jobs = _service.GetAll();
                return Ok(jobs);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Job> GetById(int id)
        {
            try
            {
                Job found = _service.GetById(id);
                return Ok(found);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Job>> Create([FromBody] Job newJob)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Account fullAccount = _acctService.GetOrCreateAccount(userInfo);
                newJob.CreatorId = userInfo.Id;
                Job job = _service.Create(newJob);
                job.Creator = fullAccount;
                return Ok(job);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Job>> Update(int id, [FromBody] Job update)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                _service.Update(id, update);
                return Ok("Updated");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Job>> Delete(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                _service.Delete(id, userInfo.Id);
                return Ok("Delorted");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}