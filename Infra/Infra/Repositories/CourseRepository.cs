using Domain.Entity;
using Infra.Infra.Contracts;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using System.Threading.Tasks;

namespace Infra.Infra.Repositories
{
    public class CourseRepository : IRepository<Course>
    {
        private string GetConnection() => Settings.ConnectionString;
        public async Task<bool> Create(Course entity)
        {
            var param = new DynamicParameters();
            param.Add("Id", entity.Id);
            param.Add("Title", entity.Title);
            param.Add("Resume", entity.Resume);
            param.Add("Created_Date", entity.Created_Date);
            param.Add("Updated_At", entity.Updated_At);

            var query = $@"INSERT INTO 
                                    Course 
                    (Id, Title, Resume, Created_Date, Updated_At)
                                    VALUES
                    (@Id, @Title, @Resume, @Created_Date, @Updated_At)";
            using(var sql = new SqlConnection(GetConnection()))
            {
               return await sql.ExecuteAsync(query, param: param,
                   commandType: System.Data.CommandType.Text).ConfigureAwait(false) > 0;    
            }
        }

        public Task<bool> Delete(Course entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Course>> GetAll()
        {
            var query = $@"SELECT
                        [Id], [Title], [Resume], [Created_Date], [Updated_At]
                                FROM
                            [Course]";
            using (var sql = new SqlConnection(GetConnection()))
            {
                return await sql.QueryAsync<Course>(query, commandType: System.Data.CommandType.Text).ConfigureAwait(false);
            }
        }

        public Task<Course> GetOne(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Course entity)
        {
            throw new NotImplementedException();
        }
    }
}
