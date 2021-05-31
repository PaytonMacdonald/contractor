using System;
using System.Collections.Generic;
using System.Linq;
using contractor.Models;
using contractor.Repositories;

namespace contractor.Services
{
    public class JobsContractorsService
    {
        private readonly JobsContractorsRepository _jobsContractorsRepo;

        public JobsContractorsService(JobsRepository warehousesRepo, JobsContractorsRepository jobsContractorsRepo)
        {
            _jobsContractorsRepo = jobsContractorsRepo;
        }

        public JobContractor CreateJobContractor(JobContractor jc)
        {
            return _jobsContractorsRepo.Create(jc);
        }

        internal JobContractor UpdateJobContractor(JobContractor jc)
        {
            throw new NotImplementedException();
        }
    }
}