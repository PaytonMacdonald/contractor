using System.Collections.Generic;
using contractor.Models;
using contractor.Services;
using Microsoft.AspNetCore.Mvc;

namespace contractor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobsContractorsController : ControllerBase
    {
        private readonly JobsContractorsService _jobsContractorsService;

        public JobsContractorsController(JobsContractorsService jc)
        {
            _jobsContractorsService = jc;
        }

        [HttpPost]
        public ActionResult<JobContractor> CreateJobContractor([FromBody] JobContractor jc)
        {
            try
            {
                JobContractor newProduct = _jobsContractorsService.CreateJobContractor(wp);
                return Ok(newProduct);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("{id}")]
        public ActionResult<JobContractor> UpdateJobContractor([FromBody] JobContractor wp)
        {
            try
            {
                JobContractor update = _jobsContractorsService.UpdateJobContractor(wp);
                return Ok(update);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}