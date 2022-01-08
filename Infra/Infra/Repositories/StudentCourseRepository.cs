using Dapper;
using Domain.Entity;
using Infra.Infra.Contracts;
using System.Data.SqlClient;

namespace Infra.Infra.Repositories
{
    public class StudentCourseRepository : IStudentCourseRepository
    {
        private string GetConnection() => Settings.ConnectionString;
        public async Task<bool> Create(Student_Course entity)
        {
            var query = $@"INSERT INTO 
                            [Student_Course]
                        ([Course_Id], [Student_Id], [Active], [Created_Date], [Updated_At])
                            values
                        (@CourseId, @StudentId, @Active, @Created_Date, @Updated_Date)";
            var param = new DynamicParameters();
            param.Add("CourseId", entity.Course_Id);
            param.Add("StudentId", entity.Student_Id);
            param.Add("Created_Date", entity.Created_Date);
            param.Add("Active", entity.Active);
            param.Add("Updated_Date", entity.Updated_At);

            using (var sql = new SqlConnection(GetConnection()))
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
            var query = @"SELECT [Course_Id], [Student_Id], [Active], [Created_Date], [Updated_At] FROM [Student_Course]";
            using (var sql = new SqlConnection(GetConnection()))
            {
                return await sql.QueryAsync<Student_Course>(query,
                                            commandType: System.Data.CommandType.Text)
                                            .ConfigureAwait(false);
            }
        }

        public async Task<Student_Course> GetOne(string Id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(Student_Course entity)
        {
            var param = new DynamicParameters();
            param.Add("Course_Id", entity.Course_Id);
            param.Add("Student_Id", entity.Student_Id);
            param.Add("Active", entity.Active);
            param.Add("Updated_At", entity.Updated_At);

            var query = $@"UPDATE 
                            Student_Course 
                        SET Active = @Active, Updated_At = @Updated_At 
                            WHERE
                            Course_Id = @Course_Id AND Student_Id = @Student_Id";

            using (var sql = new SqlConnection(GetConnection()))
            {
                return await sql.ExecuteAsync(query, param: param,
                    commandType: System.Data.CommandType.Text)
                    .ConfigureAwait(false) > 0;
            }
        }

        public async Task<Student_Course> GetOne(string Course_Id, string Student_Id)
        {
            var query = $@"SELECT [Course_Id], [Student_Id], [Active],
                                    [Created_Date], [Updated_At]
                                           FROM Student_Course
                                            WHERE 
                                    [Course_Id] = @Course_Id and [Student_Id] = @Student_Id";
            var param = new DynamicParameters();
            param.Add("Course_Id", Course_Id);
            param.Add("Student_Id", Student_Id);

            using (var sql = new SqlConnection(GetConnection()))
            {
                return await sql
                    .QueryFirstOrDefaultAsync<Student_Course>(query, param: param,
                        commandType: System.Data.CommandType.Text)
                        .ConfigureAwait(false);
            }

        }
    }
}
