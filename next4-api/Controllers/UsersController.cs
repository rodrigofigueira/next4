using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using next4_api.Models.DTO.User;
using next4_api.Models;
using next4_api.Data;
using next4_api.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Web;
using next4_api.Interfaces;
using next4_api.Repository;

namespace next4_api.Controllers
{
    public class UsersController : BaseApiController
    {

        private IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository){
            _userRepository = userRepository;
        }

        ///<summary>
        ///Cria novo usuário
        ///</summary>
        /// <param name="user">objeto User com name, email e password</param>
        /// <returns>UserToken</returns>
        [HttpPost]
        // [Authorize]
        public async Task<ActionResult<UserToken>> Post([FromBody] UserPost user){

            var _user = await _userRepository.Post(new User{
                Email = user.Email,
                Name = user.Name,
                Password = user.Password
            });

            return Ok(new UserToken{
                Id = _user.Id,
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

            User _user = await _userRepository.GetByUsernameAndPassword(user.Name, user.Password);

            if(_user == null) return BadRequest("Usuário não encontrado");

            return Ok(new UserToken{
                Id = _user.Id,
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

            User _user = await _userRepository.GetByEmailAndPassword(user.Email, user.Password);

            if(_user == null) return BadRequest("Usuário não encontrado");

            return Ok(new UserToken{
                Id = _user.Id,
                Name = _user.Name,
                Token = new TokenService().CreateToken(_user.Name)
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
            User user = await _userRepository.GetById(id);
            if(user == null) return BadRequest("Usuário não encontrado");
            return Ok(new UserGet{
                Id = user.Id,
                CreatedAt = user.CreatedAt,
                Email = user.Email,
                Name = user.Name,
                UpdatedAt = user.UpdatedAt
            });
        }

        ///<summary>
        ///Pesquisa usuários por username
        ///</summary>
        /// <param name="name">Texto para realizar a pesquisa</param>
        /// <returns>Lista de User</returns>
        [Authorize]
        [HttpGet]
        [Route("getbyname/{name}")]
        public async Task<ActionResult<List<UserGet>>> GetListByNameStartsWith([FromRoute] string name){
            
            var users = await _userRepository.GetListByNameStartsWith(name);
            if(users == null || users.Count == 0) return BadRequest("Nenhum usuário encontrado");

            var usersGet = new List<UserGet>();

            foreach(var user in users){
                usersGet.Add(new UserGet{
                    CreatedAt = user.CreatedAt,
                    Email = user.Email,
                    Id = user.Id,
                    Name = user.Name,
                    UpdatedAt = user.UpdatedAt
                });
            }

            return Ok(usersGet);
        }

        ///<summary>
        ///Pesquisa usuários por email
        ///</summary>
        /// <param name="email">Texto para realizar a pesquisa</param>
        /// <returns>Lista de User</returns>
        [Authorize]
        [HttpGet]
        [Route("getbyemail/{email}")]
        public async Task<ActionResult<List<UserGet>>> GetListByEmailStartsWith([FromRoute] string email){
            
            var users = await _userRepository.GetListByEmailStartsWith(email);
            
            if(users == null || users.Count == 0) return BadRequest("Nenhum usuário encontrado");

            var usersGet = new List<UserGet>();

            foreach(var user in users){
                usersGet.Add(new UserGet{
                    CreatedAt = user.CreatedAt,
                    Email = user.Email,
                    Id = user.Id,
                    Name = user.Name,
                    UpdatedAt = user.UpdatedAt
                });
            }

            return Ok(usersGet);
        }

        // ///<summary>
        // ///Atualiza a senha do usuário
        // ///</summary>
        // /// <param name="user">objeto User com login e password</param>
        // /// <returns>UserToken</returns>
        // [Authorize]
        // [HttpPut]
        // [Route("password")]
        // public async Task<ActionResult> UpdatePassword([FromBody] UserPutPassword user){

        //     bool passwordUpdated = await new UserDAO().UpdatePassword(user);

        //     if(passwordUpdated == true) return Ok("Senha atualizada com sucesso.");

        //     return BadRequest("Não foi possível realizar a atualização da senha.");

        // }

        // ///<summary>
        // ///Update Usuário
        // ///</summary>
        // /// <param name="user">objeto User com todos os campos, exceto password</param>
        // /// <returns>Apenas mensagem de sucesso ou erro</returns>        
        // [Authorize]
        // [HttpPut]
        // public async Task<ActionResult> Update([FromBody] UserPut user){

        //     UserDAO userDAO = new UserDAO();

        //     if(await userDAO.EmailExistsWithIdNotEqualsTo(user.Email, user.Id))
        //         return BadRequest("Email já existe");

        //     if(await userDAO.NameExistsWithIdNotEqualsTo(user.Name, user.Id))
        //         return BadRequest("Nome já existe");

        //     bool userUpdated = await userDAO.Update(user);

        //     if(userUpdated == false) return BadRequest("Não foi possível realizar a atualização");

        //     return Ok("Usuário atualizado com sucesso.");
        // }

        // ///<summary>
        // ///Deleta Usuário
        // ///</summary>
        // /// <param name="id">Id do usuário que será deletado</param>
        // /// <returns>Apenas mensagem de sucesso ou erro</returns>        
        // [Authorize]
        // [HttpDelete]
        // [Route("{id}")]
        // public async Task<ActionResult> Delete([FromRoute] int id){
        //     bool userUpdated = await new UserDAO().Delete(id);

        //     if(userUpdated == false) return BadRequest("Não foi possível realizar a deleção");

        //     return Ok("Usuário deletado.");
        // }

    }

}