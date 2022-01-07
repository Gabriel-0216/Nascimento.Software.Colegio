using Domain.Entity;
using Infra.Infra.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Infra.Infra.Repositories
{
    public class StudentRepository : IRepository<Student>
    {
        private string GetConnection() => Settings.ConnectionString;
        public async Task<bool> Create(Student entity)
        {
            var param = new DynamicParameters();
            param.Add("Id", entity.Id);
            param.Add("Name", entity.Name);
            param.Add("Phone", entity.Phone);
            param.Add("Email", entity.Email);
            param.Add("Birthdate", entity.Birthdate);
            param.Add("Create_Date", entity.Create_Date);
            param.Add("Updated_Date", entity.Updated_Date);

            var query = $@"INSERT INTO Student (Id,Name,Phone,Email,
                                Birthdate,Create_Date,Updated_Date)
                            VALUES
                        (@Id, @Name, @Phone, @Email, @Birthdate, 
                            @Create_Date, @Updated_Date)";

            using (var sql = new SqlConnection(GetConnection()))
            {
                return await sql.ExecuteAsync(query, param: param,
                    commandType: System.Data.CommandType.Text) > 0;
            }
        }

        public async Task<bool> Delete(Student entity)
        {
            var param = new DynamicParameters();
            param.Add("Id", entity.Id);
            var query = $@"DELETE FROM Student WHERE Id = @Id";

            using(var sql = new SqlConnection(GetConnection()))
            {
                return await sql.ExecuteAsync(query, param: param, 
                    commandType: System.Data.CommandType.Text) > 0;
            }
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            var query = $@"SELECT [Id], [Name], [Phone], [Email], 
            [Birthdate], [Create_Date], [Updated_Date] FROM [Student]";

            using(var sql = new SqlConnection(GetConnection()))
            {
                return await sql.QueryAsync<Student>(query, commandType: System.Data.CommandType.Text).ConfigureAwait(false);
            }
        }

        public async Task<Student> GetOne(string Id)
        {
            var param = new DynamicParameters();
            param.Add("Id", Id);

            var query = $@"SELECT Id, Name, Phone, Email, 
            Birthdate, Create_Date, Updated_Date FROM Student
            WHERE Id = @Id";
            using(var sql = new SqlConnection(GetConnection()))
            {
                return await sql.QueryFirstOrDefaultAsync<Student>(query, param: param, commandType: System.Data.CommandType.Text).ConfigureAwait(false);
            }

        }

        public Task<bool> Update(Student entity)
        {
            throw new NotImplementedException();
        }
    }
}
