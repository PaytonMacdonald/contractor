using System;
using System.Collections.Generic;
using contractor.Models;
using contractor.Repositories;

namespace contractor.Services
{
    public class JobsService
    {
        private readonly JobsRepository _repo;

        public JobsService(JobsRepository repo)
        {
            _repo = repo;
        }

        internal IEnumerable<Job> GetAll()
        {
            return _repo.GetAll();
        }

        internal Job GetById(int id)
        {
            Job job = _repo.GetById(id);
            if (job == null)
            {
                throw new Exception("Invalid Job Id");
            }
            return job;
        }

        internal IEnumerable<Job> GetByCreatorId(string id)
        {
            return _repo.GetByCreatorId(id);
        }

        internal Job Create(Job newJob)
        {
            return _repo.Create(newJob);
        }
        internal Job Update(int id, Job update)
        {
            Job original = GetById(update.Id);
            original.Title = update.Title.Length > 0 ? update.Title : original.Title;
            original.Body = update.Body.Length > 0 ? update.Body : original.Body;
            original.CreatorId = update.CreatorId.Length > 0 ? update.CreatorId : original.CreatorId;
            if (_repo.Update(id, original))
            {
                return original;
            }
            throw new Exception("Something Went Wrong???");
        }

        internal void Delete(int id, string creatorId)
        {
            Job job = GetById(id);
            if (job.CreatorId != creatorId)
            {
                throw new Exception("You cannot delete another users Job");
            }
            if (!_repo.Delete(id))
            {
                throw new Exception("Something has gone terribly wrong");
            };
        }

    }
}