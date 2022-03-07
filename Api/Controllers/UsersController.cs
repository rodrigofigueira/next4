using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Api.Models.DTO.User;
using System.Threading.Tasks;
using System.Collections.Generic;
using Api.Interfaces;
using System;

namespace Api.Controllers
{
    public class UsersController : BaseApiController
    {

        private IUserRepository _userRepository;
        private IUserService _userService;

        public UsersController(IUserRepository userRepository, IUserService userService){
            _userRepository = userRepository;
            _userService = userService;
        }

        ///<summary>
        ///Cria novo usuário
        ///</summary>
        /// <param name="user">objeto User com name, email e password</param>
        /// <returns>UserToken</returns>
        [HttpPost]
        // [Authorize]
        public async Task<ActionResult<UserToken>> Post([FromBody] UserPost user){

            bool nameExists = await _userService.NameExists(user.Name);
            if(nameExists) return BadRequest("Nome já existe");

            bool emailExists = await _userService.EmailExists(user.Email);
            if(emailExists) return BadRequest("Email já existe");

            return Ok(await _userService.Post(user));
        }

        ///<summary>
        ///Realiza o login por name
        ///</summary>
        /// <param name="user">objeto User com username e password</param>
        /// <returns>UserToken</returns>
        [HttpPost]
        [Route("login/byusername")]
        public async Task<ActionResult<UserToken>> LoginByName([FromBody] UserLoginByName user)
        {
            UserToken _user = await _userService.LoginByName(user);
            if (_user == null) return BadRequest("Usuário não encontrado");
            return Ok(_user);
        }

        ///<summary>
        ///Realiza o login por email
        ///</summary>
        /// <param name="user">objeto User com login e password</param>
        /// <returns>UserToken</returns>
        [HttpPost]
        [Route("login/byemail")]
        public async Task<ActionResult<UserToken>> LoginByEmail([FromBody] UserLoginByEmail user)
        {
            UserToken _user = await _userService.LoginByEmail(user);
            if (_user == null) return BadRequest("Usuário não encontrado");
            return Ok(_user);
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
            var user = await _userService.GetById(id);
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
        public async Task<ActionResult<List<UserGet>>> GetListByNameStartsWith([FromRoute] string name){
            var users = await _userService.GetByName(name);
            if(users == null || users.Count == 0) return BadRequest("Nenhum usuário encontrado");
            return Ok(users);
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
            
            var users = await _userService.GetByEmail(email);            
            if(users == null || users.Count == 0) return BadRequest("Nenhum usuário encontrado");
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
        public async Task<ActionResult<string>> UpdatePassword([FromBody] UserPutPassword user)
        {
            bool passwordUpdated = await _userService.UpdatePassword(user);
            if (passwordUpdated == true) return Ok("Senha atualizada com sucesso.");
            return BadRequest("Não foi possível atualizar a senha.");
        }

        ///<summary>
        ///Deleta Usuário
        ///</summary>
        /// <param name="id">Id do usuário que será deletado</param>
        /// <returns>Apenas mensagem de sucesso ou erro</returns>        
        [Authorize]
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<string>> Delete([FromRoute] int id)
        {
            bool userDeleted = await _userService.Delete(id);
            if (userDeleted == false) return BadRequest("Não foi possível realizar a deleção");
            return Ok("Usuário deletado.");
        }

        ///<summary>
        ///Update Usuário
        ///</summary>
        /// <param name="user">objeto User com todos os campos, exceto password</param>
        /// <returns>Apenas mensagem de sucesso ou erro</returns>        
        [Authorize]
        [HttpPut]
        public async Task<ActionResult<string>> Update([FromBody] UserPut user)
        {

            try
            {
                bool userUpdated = await _userService.Update(user);
                if (userUpdated == false) return BadRequest("Não foi possível realizar a atualização");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("Usuário atualizado com sucesso.");

        }


    }

}