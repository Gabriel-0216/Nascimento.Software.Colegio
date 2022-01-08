using Dapper;
using Domain.Entity;
using Infra.Infra.Contracts;
using System.Data.SqlClient;

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
            using (var sql = new SqlConnection(GetConnection()))
            {
                return await sql.ExecuteAsync(query, param: param,
                    commandType: System.Data.CommandType.Text).ConfigureAwait(false) > 0;
            }
        }

        public async Task<bool> Delete(Course entity)
        {
            var param = new DynamicParameters();
            param.Add("Id", entity.Id);

            var query = @"DELETE FROM 
                            [Course]
                            WHERE
                           Id = @Id";
            using (var sql = new SqlConnection(GetConnection()))
            {
                return await sql
                    .ExecuteAsync(query, param: param, commandType: System.Data.CommandType.Text)
                    .ConfigureAwait(false) > 0;
            }
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

        public async Task<Course> GetOne(string Id)
        {
            var query = @"SELECT [Id], [Title], [Resume], [Created_Date], [Updated_At]
                                        FROM
                                        [Course]
                                        WHERE
                                        [Id] = @Id";

            var param = new DynamicParameters();
            param.Add("Id", Id);
            using (var sql = new SqlConnection(GetConnection()))
            {
                return await sql
                    .QueryFirstOrDefaultAsync<Course>(query, param: param,
                    commandType: System.Data.CommandType.Text)
                    .ConfigureAwait(false);
            }
        }

        public async Task<bool> Update(Course entity)
        {
            var query = @"UPDATE [Course] 
                                SET
                        [Title] = @Title, [Resume] = @Resume, [Updated_At] = @Updated_At
                                WHERE
                        [Id] = @Id";

            var param = new DynamicParameters();
            param.Add("Id", entity.Id);
            param.Add("Title", entity.Title);
            param.Add("Resume", entity.Resume);
            param.Add("Updated_At", entity.Updated_At);

            using (var sql = new SqlConnection(GetConnection()))
            {
                return await sql
                    .ExecuteAsync(query, param: param,
                    commandType: System.Data.CommandType.Text)
                    .ConfigureAwait(false) > 0;
            }
        }
    }
}
