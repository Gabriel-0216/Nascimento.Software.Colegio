using Dapper;
using Domain.Entity;
using Infra.Infra.Contracts;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Infra.Repositories
{
    public class StudentCourseRepository : IRepository<Student_Course>
    {
        private string GetConnection() => Settings.ConnectionString;
        public async Task<bool> Create(Student_Course entity)
        {
            var query = $@"INSERT INTO 
                            [Student_Course]
                        ([Course_Id], [Student_Id], [Created_Date], [Updated_At])
                            values
                        (@CourseId, @StudentId, @Created_Date, @Updated_Date)";
            var param = new DynamicParameters();
            param.Add("CourseId", entity.Course_Id);
            param.Add("StudentId", entity.Student_Id);
            param.Add("Created_Date", entity.Created_Date);
            param.Add("Updated_Date", entity.Updated_At);

            using(var sql = new SqlConnection(GetConnection()))
            {
                return await sql.ExecuteAsync(query, param: param, commandType: System.Data.CommandType.Text).ConfigureAwait(false) > 0;
            }
        }

        public Task<bool> Delete(Student_Course entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Student_Course>> GetAll()
        {
            var query = @"SELECT [Course_Id], [Student_Id], [Created_Date], [Updated_At] FROM [Student_Course]";
            using(var sql = new SqlConnection(GetConnection()))
            {
                return await sql.QueryAsync<Student_Course>(query,
                                            commandType: System.Data.CommandType.Text)
                                            .ConfigureAwait(false);
            }
        }

        public Task<Student_Course> GetOne(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Student_Course entity)
        {
            throw new NotImplementedException();
        }
    }
}
