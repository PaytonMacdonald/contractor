using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using contractor.Models;
using Dapper;


namespace contractor.Repositories
{
    public class JobsRepository
    {

        private readonly IDbConnection _db;
        public JobsRepository(IDbConnection db)
        {
            _db = db;
        }

        public IEnumerable<Job> GetAll()
        {
            string sql = @"
              SELECT
              c.*,
              a.*
              FROM jobs c
              JOIN accounts a ON c.creatorId = a.id;";
            return _db.Query<Job, Account, Job>(sql, (job, account) =>
            {
                job.Creator = account;
                return job;
            }, splitOn: "id");
        }

        public Job GetById(int id)
        {
            string sql = @"
              SELECT 
                c.*,
                a.* 
              FROM jobs c
              JOIN accounts a ON c.creatorId = a.id
              WHERE id = @id";
            return _db.Query<Job, Account, Job>(sql, (job, account) =>
            {
                job.Creator = account;
                return job;
            }
            , new { id }, splitOn: "id").FirstOrDefault();
        }

        public IEnumerable<Job> GetByCreatorId(string id)
        {
            string sql = @"
              SELECT 
                c.*,
                a.* 
              FROM jobs c
              JOIN accounts a ON c.creatorId = a.id
              WHERE creatorId = @id";
            return _db.Query<Job, Account, Job>(sql, (job, account) =>
            {
                job.Creator = account;
                return job;
            }
            , new { id }, splitOn: "id");
        }

        public Job Create(Job newJob)
        {
            string sql = @"
              INSERT INTO jobs
              (creatorId, make, model, year, price, imgUrl, description)
              VALUES
              (@CreatorId, @Make, @Model, @Year, @Price, @ImgUrl, @Description);
              SELECT LAST_INSERT_ID()";
            newJob.Id = _db.ExecuteScalar<int>(sql, newJob);
            return newJob;
        }

        internal bool Update(int id, Job original)
        {
            string sql = @"
              UPDATE jobs
              SET
                title = @Title
                body = @Body
                creatorId = @CreatorId,
              WHERE id=@Id
              ";
            int affectedRows = _db.Execute(sql, original);
            return affectedRows == 1;
        }

        public bool Delete(int id)
        {
            string sql = "DELETE FROM jobs WHERE id = @id LIMIT 1";
            int affectedRows = _db.Execute(sql, new { id });
            return affectedRows == 1;
        }

    }
}