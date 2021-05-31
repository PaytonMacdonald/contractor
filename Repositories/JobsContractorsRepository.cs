
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using contractor.Models;
using contractor.Interfaces;
using Dapper;

namespace contractor.Repositories
{
    public class JobsContractorsRepository
    {
        private readonly IDbConnection _db;

        public JobsContractorsRepository(IDbConnection db)
        {
            _db = db;
        }

        public JobContractor Create(JobContractor wp)
        {
            string sql = @"INSERT INTO 
            jobs_contractors(jobId, contractorId)
            VALUES (@JobId, @ContractorId);
            SELECT LAST_INSERT_ID();
            ";

            wp.Id = _db.ExecuteScalar<int>(sql, wp);
            return wp;
        }

        internal List<JobContractorViewModel> GetProductByWarehouseId(int warehouseId)
        {
            string sql = @"
                SELECT
                c.*,
                j.location,
                jc.id as jobContractorId,
                jc.productId as productId,
                jc.warehouseId as warehouseId
                FROM
                jobs_contractors jc
                JOIN warehouses j ON w.id = jc.warehouseId
                JOIN products c ON p.id = jc.productId
                WHERE
                jc.warehouseId = @warehouseId;
            ";
            return _db.Query<JobContractorViewModel>(sql, new { warehouseId }).ToList();
        }
    }
}