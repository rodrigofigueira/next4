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

        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<UserGet>> GetById([FromRoute] int id){
            UserDAO userDao = new UserDAO();
            var user = await userDao.GetById(id);
            if(user == null) return BadRequest("Usuário não encontrado");
            return Ok(user);
        }

        [Authorize]
        [HttpGet]
        [Route("getbyname/{name}")]
        public async Task<ActionResult<List<UserGet>>> GetByName([FromRoute] string name){
            var users = await new UserDAO().GetUsersByName(name);
            if(users == null || users.Count == 0) return BadRequest("Usuário não encontrado");
            return Ok(users);
        }

        [Authorize]
        [HttpPut]
        [Route("password")]
        public async Task<ActionResult> UpdatePassword([FromBody] UserPutPassword user){

            bool passwordUpdated = await new UserDAO().UpdatePassword(user);

            if(passwordUpdated == true) return Ok("Senha atualizada com sucesso.");

            return BadRequest("Não foi possível realizar a atualização da senha.");

        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UserPut user){
            bool userUpdated = await new UserDAO().Update(user);

            if(userUpdated == false) return BadRequest("Não foi possível realizar a atualização");

            return Ok("Usuário atualizado com sucesso.");
        }

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