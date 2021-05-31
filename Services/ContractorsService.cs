using System;
using System.Collections.Generic;
using contractor.Models;
using contractor.Repositories;

namespace contractor.Services
{
    public class ContractorsService
    {
        private readonly ContractorsRepository _repo;

        public ContractorsService(ContractorsRepository repo)
        {
            _repo = repo;
        }

        internal IEnumerable<Contractor> GetAll()
        {
            return _repo.GetAll();
        }

        internal Contractor GetById(int id)
        {
            Contractor contractor = _repo.GetById(id);
            if (contractor == null)
            {
                throw new Exception("Invalid Contractor Id");
            }
            return contractor;
        }

        internal IEnumerable<Contractor> GetByCreatorId(string id)
        {
            return _repo.GetByCreatorId(id);
        }

        internal Contractor Create(Contractor newContractor)
        {
            return _repo.Create(newContractor);
        }
        internal Contractor Update(int id, Contractor update)
        {
            Contractor original = GetById(update.Id);
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
            Contractor contractor = GetById(id);
            if (contractor.CreatorId != creatorId)
            {
                throw new Exception("You cannot delete another users Contractor");
            }
            if (!_repo.Delete(id))
            {
                throw new Exception("Something has gone terribly wrong");
            };
        }

    }
}