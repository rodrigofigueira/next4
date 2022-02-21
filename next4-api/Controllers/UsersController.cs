using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using next4_api.Models.DTO.User;
using next4_api.Models;
using next4_api.Data;
using next4_api.Services;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace next4_api.Controllers
{
    public class UsersController : BaseApiController
    {

        ///<summary>
        ///Cria novo usuário
        ///</summary>
        /// <param name="user">objeto User com name, email e password</param>
        /// <returns>UserToken</returns>
        [HttpPost]
        public async Task<ActionResult<UserToken>> Post([FromBody] UserPost user){

            User _user = new User{
                Name = user.Name,
                Email = user.Email,
                Password = user.Password
            };

            UserDAO userDAO = new UserDAO();
            _user = await userDAO.Post(user);

            return Ok(new UserToken{
                Name = _user.Name,
                Token = new TokenService().CreateToken(_user.Name)
            });
        }

        ///<summary>
        ///Realiza o login por name
        ///</summary>
        /// <param name="user">objeto User com username e password</param>
        /// <returns>UserToken</returns>
        [HttpPost]
        [Route("login/byusername")]
        public async Task<ActionResult<UserToken>> LoginByName([FromBody] UserLoginByName user){

            UserGet _user = await new UserDAO().GetByUsernameAndPassword(user.Name, user.Password);

            if(_user == null) return BadRequest("Usuário não encontrado");

            return Ok(new UserToken{
                Name = user.Name,
                Token = new TokenService().CreateToken(user.Name)
            });

        }

        ///<summary>
        ///Realiza o login por email
        ///</summary>
        /// <param name="user">objeto User com login e password</param>
        /// <returns>UserToken</returns>
        [HttpPost]
        [Route("login/byemail")]
        public async Task<ActionResult<UserToken>> LoginByEmail([FromBody] UserLoginByEmail user){

            UserGet _user = await new UserDAO().GetByEmailAndPassword(user.Email, user.Password);

            if(_user == null) return BadRequest("Usuário não encontrado");

            return Ok(new UserToken{
                Name = user.Email,
                Token = new TokenService().CreateToken(user.Email)
            });

        }

        ///<summary>
        ///Pesquisa o usuário por id
        ///</summary>
        /// <param name="id">Id do usuário</param>
        /// <returns>User</returns>
        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<UserGet>> GetById([FromRoute] int id){
            UserDAO userDao = new UserDAO();
            var user = await userDao.GetById(id);
            if(user == null) return BadRequest("Usuário não encontrado");
            return Ok(user);
        }


        ///<summary>
        ///Pesquisa usuários por username
        ///</summary>
        /// <param name="name">Texto para realizar a pesquisa</param>
        /// <returns>Lista de User</returns>
        [Authorize]
        [HttpGet]
        [Route("getbyname/{name}")]
        public async Task<ActionResult<List<UserGet>>> GetByName([FromRoute] string name){
            var users = await new UserDAO().GetUsersByName(name);
            if(users == null || users.Count == 0) return BadRequest("Usuário não encontrado");
            return Ok(users);
        }

        ///<summary>
        ///Atualiza a senha do usuário
        ///</summary>
        /// <param name="user">objeto User com login e password</param>
        /// <returns>UserToken</returns>
        [Authorize]
        [HttpPut]
        [Route("password")]
        public async Task<ActionResult> UpdatePassword([FromBody] UserPutPassword user){

            bool passwordUpdated = await new UserDAO().UpdatePassword(user);

            if(passwordUpdated == true) return Ok("Senha atualizada com sucesso.");

            return BadRequest("Não foi possível realizar a atualização da senha.");

        }

        ///<summary>
        ///Update Usuário
        ///</summary>
        /// <param name="user">objeto User com todos os campos, exceto password</param>
        /// <returns>Apenas mensagem de sucesso ou erro</returns>        
        [Authorize]
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UserPut user){
            bool userUpdated = await new UserDAO().Update(user);

            if(userUpdated == false) return BadRequest("Não foi possível realizar a atualização");

            return Ok("Usuário atualizado com sucesso.");
        }

        ///<summary>
        ///Deleta Usuário
        ///</summary>
        /// <param name="id">Id do usuário que será deletado</param>
        /// <returns>Apenas mensagem de sucesso ou erro</returns>        
        [Authorize]
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id){
            bool userUpdated = await new UserDAO().Delete(id);

            if(userUpdated == false) return BadRequest("Não foi possível realizar a deleção");

            return Ok("Usuário deletado.");
        }

    }

}