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
    [Route("[controller]")]
    // TODO[epic=Auth] Adds authguard to all routes on the whole controller
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly AccountsService _service;
        private readonly ContractorsService _contractorService;
        private readonly JobsService _jobService;

        public AccountController(AccountsService service, ContractorsService contractorService, JobsService jobService)
        {
            _service = service;
            _contractorService = contractorService;
            _jobService = jobService;
        }

        [HttpGet]
        public async Task<ActionResult<Account>> Get()
        {
            try
            {
                // TODO[epic=Auth] Replaces req.userinfo
                // IF YOU EVER NEED THE ACTIVE USERS INFO THIS IS HOW YOU DO IT (FROM AUTH0)
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Account currentUser = _service.GetOrCreateAccount(userInfo);
                return Ok(currentUser);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpGet("contractors")]
        public async Task<ActionResult<IEnumerable<Contractor>>> GetMyContractors()
        {
            // TODO[epic=Auth] Replaces req.userinfo
            // IF YOU EVER NEED THE ACTIVE USERS INFO THIS IS HOW YOU DO IT (FROM AUTH0)
            Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
            IEnumerable<Contractor> contractors = _contractorService.GetByCreatorId(userInfo.Id);
            return Ok(contractors);

        }

        [HttpGet("jobs")]
        public async Task<ActionResult<IEnumerable<Job>>> GetMyJobs()
        {
            // TODO[epic=Auth] Replaces req.userinfo
            // IF YOU EVER NEED THE ACTIVE USERS INFO THIS IS HOW YOU DO IT (FROM AUTH0)
            Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
            IEnumerable<Job> jobs = _jobService.GetByCreatorId(userInfo.Id);
            return Ok(jobs);

        }




    }
}