using next4_api.Models;
using next4_api.Models.DTO.User;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Data;
using System.Collections.Generic;

namespace next4_api.Data
{
    public class UserDAO : Connection
    {       
        public async Task<User> Post(UserPost userPost){

            await connection.OpenAsync();

            var command = new SqlCommand(@"insert into snna_users(name_view, email, password, dt_created, dt_update) OUTPUT INSERTED.id
                                            values (@name, @email, 
                                            CONVERT(NVARCHAR(256), HASHBYTES('SHA2_256', @password), 2), 
                                            @created, @updated)", connection);

            DateTime dataBasis = DateTime.Now;
            int idInserido = 0;

            try{
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@name", userPost.Name);
                command.Parameters.AddWithValue("@email", userPost.Email);
                command.Parameters.Add("@password", SqlDbType.VarChar);
                command.Parameters["@password"].Value = userPost.Password;
                command.Parameters.AddWithValue("@created", dataBasis.ToString("dd/MM/yyyy HH:mm:ss"));
                command.Parameters.AddWithValue("@updated", dataBasis.ToString("dd/MM/yyyy HH:mm:ss"));
                idInserido = Convert.ToInt32(await command.ExecuteScalarAsync());
            }
            catch(Exception ex){
                throw ex;
            }
            finally{
                await connection.CloseAsync();
            }
            
            return new User{
                Id = idInserido,
                Name = userPost.Name,
                CreatedAt = dataBasis,
                UpdatedAt = dataBasis,
                Email = userPost.Email                
            };

        }

        public async Task<UserGet> GetById(int id){

            await connection.OpenAsync();

            string query = @"SELECT [id]
                                ,[name_view]
                                ,[email]
                                ,[dt_created]
                                ,[dt_update] FROM [next4].[dbo].[snna_users] where id = @id";

            var command = new SqlCommand(query, connection);
            UserGet user = null;

            try{
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader userFromDB = await command.ExecuteReaderAsync();

                if (userFromDB.HasRows)
                {
                    userFromDB.Read();

                    user = new UserGet();
                    user.Id = Convert.ToInt32(userFromDB["id"]);
                    user.Name = Convert.ToString(userFromDB["name_view"]);
                    user.Email = Convert.ToString(userFromDB["email"]);
                    user.CreatedAt = Convert.ToDateTime(userFromDB["dt_created"]);
                    user.UpdatedAt = Convert.ToDateTime(userFromDB["dt_update"]);
                }


            }
            catch(Exception ex){
                throw ex;
            }
            finally{
                await connection.CloseAsync();
            }
            
            return user;

        }        

        public async Task<UserGet> GetByUsernameAndPassword(string name, string password){

            await connection.OpenAsync();

            string query = @"SELECT [id]
                                ,[name_view]
                                ,[email]
                                ,[dt_created]
                                ,[dt_update] FROM [next4].[dbo].[snna_users] 
                                where name_view = @name
                                and password = CONVERT(NVARCHAR(256), HASHBYTES('SHA2_256', @password), 2)";

            var command = new SqlCommand(query, connection);
            UserGet user = null;

            try{
                command.Parameters.Clear();
                
                command.Parameters.Add("@name", SqlDbType.VarChar);
                command.Parameters["@name"].Value = name;

                command.Parameters.Add("@password", SqlDbType.VarChar);
                command.Parameters["@password"].Value = password;
                
                SqlDataReader userFromDB = await command.ExecuteReaderAsync();

                if (userFromDB.HasRows)
                {
                    userFromDB.Read();

                    user = new UserGet();
                    user.Id = Convert.ToInt32(userFromDB["id"]);
                    user.Name = Convert.ToString(userFromDB["name_view"]);
                    user.Email = Convert.ToString(userFromDB["email"]);
                    user.CreatedAt = Convert.ToDateTime(userFromDB["dt_created"]);
                    user.UpdatedAt = Convert.ToDateTime(userFromDB["dt_update"]);
                }

            }
            catch(Exception ex){
                throw ex;
            }
            finally{
                await connection.CloseAsync();
            }
            
            return user;

        }

        public async Task<UserGet> GetByEmailAndPassword(string email, string password){

            await connection.OpenAsync();

            string query = @"SELECT [id]
                                ,[name_view]
                                ,[email]
                                ,[dt_created]
                                ,[dt_update] FROM [next4].[dbo].[snna_users] 
                                where email = @email
                                and password = CONVERT(NVARCHAR(256), HASHBYTES('SHA2_256', @password), 2)";

            var command = new SqlCommand(query, connection);
            UserGet user = null;

            try{
                command.Parameters.Clear();
                
                command.Parameters.Add("@email", SqlDbType.VarChar);
                command.Parameters["@email"].Value = email;

                command.Parameters.Add("@password", SqlDbType.VarChar);
                command.Parameters["@password"].Value = password;
                
                SqlDataReader userFromDB = await command.ExecuteReaderAsync();

                if (userFromDB.HasRows)
                {
                    userFromDB.Read();

                    user = new UserGet();
                    user.Id = Convert.ToInt32(userFromDB["id"]);
                    user.Name = Convert.ToString(userFromDB["name_view"]);
                    user.Email = Convert.ToString(userFromDB["email"]);
                    user.CreatedAt = Convert.ToDateTime(userFromDB["dt_created"]);
                    user.UpdatedAt = Convert.ToDateTime(userFromDB["dt_update"]);
                }

            }
            catch(Exception ex){
                throw ex;
            }
            finally{
                await connection.CloseAsync();
            }
            
            return user;

        }

        public async Task<List<UserGet>> GetListByNameStartsWith(string name){

            await connection.OpenAsync();

            string query = @"SELECT [id]
                                ,[name_view]
                                ,[email]
                                ,[dt_created]
                                ,[dt_update] FROM [next4].[dbo].[snna_users] 
                                where name_view like @name";

            var command = new SqlCommand(query, connection);
            List<UserGet> users = new List<UserGet>();

            try{
                command.Parameters.Clear();
                
                command.Parameters.Add("@name", SqlDbType.VarChar);
                command.Parameters["@name"].Value = name + "%";

                SqlDataReader userFromDB = await command.ExecuteReaderAsync();

                if (userFromDB.HasRows == false) return users;

                while(userFromDB.Read()){
                    UserGet user = new UserGet();
                    user.Id = Convert.ToInt32(userFromDB["id"]);
                    user.Name = Convert.ToString(userFromDB["name_view"]);
                    user.Email = Convert.ToString(userFromDB["email"]);
                    user.CreatedAt = Convert.ToDateTime(userFromDB["dt_created"]);
                    user.UpdatedAt = Convert.ToDateTime(userFromDB["dt_update"]);
                    users.Add(user);
                }

            }
            catch(Exception ex){
                throw ex;
            }
            finally{
                await connection.CloseAsync();
            }
            
            return users;

        }

        public async Task<bool> UpdatePassword(UserPutPassword user){

            await connection.OpenAsync();

            var command = new SqlCommand(@"update 
                                            snna_users 
                                            set 
                                            password = CONVERT(NVARCHAR(256), HASHBYTES('SHA2_256', @new_password), 2)                                            
                                            ,dt_update = @updated where 
                                            id = @id 
                                            and password = CONVERT(NVARCHAR(256), HASHBYTES('SHA2_256', @old_password), 2)", connection);

            DateTime dataBasis = DateTime.Now;
            int rowsAffecteds = 0;

            try{
                command.Parameters.Clear();
                command.Parameters.Add("@old_password", SqlDbType.VarChar);
                command.Parameters["@old_password"].Value = user.OldPassword;
                command.Parameters.Add("@new_password", SqlDbType.VarChar);
                command.Parameters["@new_password"].Value = user.NewPassword;
                command.Parameters.AddWithValue("@id", user.Id);
                command.Parameters.AddWithValue("@updated", dataBasis.ToString("dd/MM/yyyy HH:mm:ss"));

                rowsAffecteds = Convert.ToInt32(await command.ExecuteNonQueryAsync());
                
            }
            catch(Exception ex){
                throw ex;
            }
            finally{
                await connection.CloseAsync();
            }

            if(rowsAffecteds == 0) return false;
            return true;

        }

        public async Task<bool> Update(UserPut user){

            await connection.OpenAsync();

            var command = new SqlCommand(@"update 
                                            snna_users 
                                            set 
                                            name_view = @name
                                            ,email = @email                                            
                                            ,dt_update = @updated where 
                                            id = @id", connection);

            DateTime dataBasis = DateTime.Now;
            int rowsAffecteds = 0;

            try{
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", user.Id);
                command.Parameters.AddWithValue("@name", user.Name);
                command.Parameters.AddWithValue("@email", user.Email);
                command.Parameters.AddWithValue("@updated", dataBasis.ToString("dd/MM/yyyy HH:mm:ss"));

                rowsAffecteds = Convert.ToInt32(await command.ExecuteNonQueryAsync());
                
            }
            catch(Exception ex){
                throw ex;
            }
            finally{
                await connection.CloseAsync();
            }

            if(rowsAffecteds == 0) return false;            
            return true;

        }

        public async Task<bool> Delete(int id){
            await connection.OpenAsync();

            var command = new SqlCommand(@"delete snna_users where id = @id", connection);

            DateTime dataBasis = DateTime.Now;
            int rowsAffecteds = 0;

            try{
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", id);
                rowsAffecteds = Convert.ToInt32(await command.ExecuteNonQueryAsync());                
            }
            catch(Exception ex){
                throw ex;
            }
            finally{
                await connection.CloseAsync();
            }

            if(rowsAffecteds == 0) return false;            
            return true;

        }

        public async Task<bool> EmailExists(string email){
            
            await connection.OpenAsync();
            var command = new SqlCommand(@"SELECT count(*) FROM snna_users WHERE email = @email", connection);
            int total;
            try{
                
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@email", email);
                total = Convert.ToInt32(await command.ExecuteScalarAsync());                                                
            }
            catch(Exception ex){
                throw ex;
            }
            finally{
                await connection.CloseAsync();
            }

            if(total > 0) return true;

            return false;

        }

        public async Task<bool> NameExists(string name){
            
            await connection.OpenAsync();
            var command = new SqlCommand(@"SELECT count(*) FROM snna_users WHERE name_view = @name", connection);
            int total;
            try{
                
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@name", name);
                total = Convert.ToInt32(await command.ExecuteScalarAsync());                                                
            }
            catch(Exception ex){
                throw ex;
            }
            finally{
                await connection.CloseAsync();
            }

            if(total > 0) return true;

            return false;

        }

        public async Task<List<UserGet>> GetListByEmailStartsWith(string email){
           
            await connection.OpenAsync();

            string query = @"SELECT [id]
                                ,[name_view]
                                ,[email]
                                ,[dt_created]
                                ,[dt_update] FROM [next4].[dbo].[snna_users] 
                                where email like @email";

            var command = new SqlCommand(query, connection);
            List<UserGet> users = new List<UserGet>();

            try{
                command.Parameters.Clear();
                
                command.Parameters.Add("@email", SqlDbType.VarChar);
                command.Parameters["@email"].Value = email + "%";

                SqlDataReader userFromDB = await command.ExecuteReaderAsync();

                if (userFromDB.HasRows == false) return users;

                while(userFromDB.Read()){
                    UserGet user = new UserGet();
                    user.Id = Convert.ToInt32(userFromDB["id"]);
                    user.Name = Convert.ToString(userFromDB["name_view"]);
                    user.Email = Convert.ToString(userFromDB["email"]);
                    user.CreatedAt = Convert.ToDateTime(userFromDB["dt_created"]);
                    user.UpdatedAt = Convert.ToDateTime(userFromDB["dt_update"]);
                    users.Add(user);
                }

            }
            catch(Exception ex){
                throw ex;
            }
            finally{
                await connection.CloseAsync();
            }
            
            return users;

        }

        public async Task<bool> NameExistsWithIdNotEqualsTo(string name, int id){

            await connection.OpenAsync();
            var command = new SqlCommand(@"SELECT count(*) FROM snna_users WHERE name_view = @name and id <> @id", connection);
            int total;
            try{
                
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@id", id);
                total = Convert.ToInt32(await command.ExecuteScalarAsync());                                                
            }
            catch(Exception ex){
                throw ex;
            }
            finally{
                await connection.CloseAsync();
            }

            if(total > 0) return true;

            return false;

        }

        public async Task<bool> EmailExistsWithIdNotEqualsTo(string email, int id){

            await connection.OpenAsync();
            var command = new SqlCommand(@"SELECT count(*) FROM snna_users WHERE email = @email and id <> @id", connection);
            int total;
            try{
                
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@id", id);
                total = Convert.ToInt32(await command.ExecuteScalarAsync());                                                
            }
            catch(Exception ex){
                throw ex;
            }
            finally{
                await connection.CloseAsync();
            }

            if(total > 0) return true;

            return false;

        }

    }
}