using Dapper;
using Domain.Users;
using Infra.Infra.Contracts;
using System.Data.SqlClient;

namespace Infra.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private string GetConnection() => Settings.ConnectionString;
        public async Task<bool> DeleteUser(User user)
        {
            var query = @"DELETE FROM Users WHERE [Id] = @Id";
            var param = new DynamicParameters();
            param.Add("Id", user.Id);

            using (var sql = new SqlConnection(GetConnection()))
            {
                return await sql
                    .ExecuteAsync(query, param: param,
                    commandType: System.Data.CommandType.Text)
                    .ConfigureAwait(false) > 0;
            }
        }

        public async Task<User> GetUserByEmail(string Email)
        {
            var query = @"Select 
                        [Id], [Name], [Email], [Phone], [Birthdate]
                            FROM 
                            [Users]
                        WHERE [Email] = @Email";
            var param = new DynamicParameters();
            param.Add("Email", Email);

            using (var sql = new SqlConnection(GetConnection()))
            {
                return await sql.QueryFirstOrDefaultAsync<User>(query,
                    param: param, commandType: System.Data.CommandType.Text)
                    .ConfigureAwait(false);
            }
        }

        public Task<User> GetUserByPhone(string phone)
        {
            throw new NotImplementedException();
        }

        public async Task<User> Login(User user)
        {
            var query = @"SELECT COUNT(*) FROM [Users] 
                                    WHERE
                         [Email] = @Email 
                                    AND
                         [Phone] = @Phone";

            var param = new DynamicParameters();
            param.Add("Email", user.Email);
            param.Add("Phone", user.Phone);

            using (var sql = new SqlConnection(GetConnection()))
            {
                var exists = await sql.ExecuteScalarAsync<int>(query, param: param, commandType: System.Data.CommandType.Text).ConfigureAwait(false);

                if (exists > 0)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<bool> Register(User user)
        {
            var query = @"INSERT INTO [Users]
                            ([Id],[Name], [Email], [Phone], [Birthdate])
                            VALUES
                            (@Id, @Name, @Email, @Phone, @Birthdate)";
            var param = new DynamicParameters();
            param.Add("Id", user.Id);
            param.Add("Name", user.Name);
            param.Add("Email", user.Email);
            param.Add("Phone", user.Phone);
            param.Add("Birthdate", user.Birthdate);

            using (var sql = new SqlConnection(GetConnection()))
            {
                return await sql.ExecuteAsync(query,
                    param: param, commandType: System.Data.CommandType.Text)
                    .ConfigureAwait(false) > 0;
            }
        }
    }
}
