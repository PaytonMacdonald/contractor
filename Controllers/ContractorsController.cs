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
    public class ContractorsController : ControllerBase
    {
        private readonly ContractorsService _service;
        private readonly AccountsService _acctService;

        public ContractorsController(ContractorsService service, AccountsService acctsService)
        {
            _service = service;
            _acctService = acctsService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Contractor>> GetAll()
        {
            try
            {
                IEnumerable<Contractor> contractors = _service.GetAll();
                return Ok(contractors);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Contractor> GetById(int id)
        {
            try
            {
                Contractor found = _service.GetById(id);
                return Ok(found);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Contractor>> Create([FromBody] Contractor newContractor)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Account fullAccount = _acctService.GetOrCreateAccount(userInfo);
                newContractor.CreatorId = userInfo.Id;
                Contractor contractor = _service.Create(newContractor);
                contractor.Creator = fullAccount;
                return Ok(contractor);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Contractor>> Update(int id, [FromBody] Contractor update)
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
        public async Task<ActionResult<Contractor>> Delete(int id)
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