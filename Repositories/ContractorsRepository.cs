using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using contractor.Models;
using Dapper;


namespace contractor.Repositories
{
    public class ContractorsRepository
    {

        private readonly IDbConnection _db;
        public ContractorsRepository(IDbConnection db)
        {
            _db = db;
        }

        public IEnumerable<Contractor> GetAll()
        {
            string sql = @"
              SELECT
              c.*,
              a.*
              FROM contractors c
              JOIN accounts a ON c.creatorId = a.id;";
            return _db.Query<Contractor, Account, Contractor>(sql, (contractor, account) =>
            {
                contractor.Creator = account;
                return contractor;
            }, splitOn: "id");
        }

        public Contractor GetById(int id)
        {
            string sql = @"
              SELECT 
                c.*,
                a.* 
              FROM contractors c
              JOIN accounts a ON c.creatorId = a.id
              WHERE id = @id";
            return _db.Query<Contractor, Account, Contractor>(sql, (contractor, account) =>
            {
                contractor.Creator = account;
                return contractor;
            }
            , new { id }, splitOn: "id").FirstOrDefault();
        }

        public IEnumerable<Contractor> GetByCreatorId(string id)
        {
            string sql = @"
              SELECT 
                c.*,
                a.* 
              FROM contractors c
              JOIN accounts a ON c.creatorId = a.id
              WHERE creatorId = @id";
            return _db.Query<Contractor, Account, Contractor>(sql, (contractor, account) =>
            {
                contractor.Creator = account;
                return contractor;
            }
            , new { id }, splitOn: "id");
        }

        public Contractor Create(Contractor newContractor)
        {
            string sql = @"
              INSERT INTO contractors
              (creatorId, make, model, year, price, imgUrl, description)
              VALUES
              (@CreatorId, @Make, @Model, @Year, @Price, @ImgUrl, @Description);
              SELECT LAST_INSERT_ID()";
            newContractor.Id = _db.ExecuteScalar<int>(sql, newContractor);
            return newContractor;
        }

        internal bool Update(int id, Contractor original)
        {
            string sql = @"
              UPDATE contractors
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
            string sql = "DELETE FROM contractors WHERE id = @id LIMIT 1";
            int affectedRows = _db.Execute(sql, new { id });
            return affectedRows == 1;
        }

    }
}
