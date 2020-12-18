using AppDocker.Context;
using AppDocker.Models;
using AppDocker.Services;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDocker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly BancoInmemory _banco;
        public UsuarioController(BancoInmemory Banco) => _banco = Banco;

        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            try
            {

                IList<Usuarios> usuario = _banco.Usuario.Include(c => c.Phones).ToList(); 
                
                return Ok(usuario);
            }
            catch (Exception erro)
            {

                return BadRequest(erro);
            }
        }


        [HttpGet("{Id}")]
        [Authorize]
        public IActionResult Get(Guid id)
        {
            try
            {  
                var usuarioAtual = _banco.Usuario.Include(c => c.Phones).Where(c => c.IdUser == id).FirstOrDefault();
                if (usuarioAtual == null)
                    return NotFound();
               
                else
                    return Ok(usuarioAtual);
            }
            catch (Exception erro)
            {

                return BadRequest(erro);
            }
        }



        public string Data = string.Format("{0:d/MM/yyyy HH:mm:ss}", DateTime.UtcNow);



        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody]Usuarios usuario)
        {
            try
            {
               
                bool containsItem = _banco.Usuario.Any(item => item.Email == usuario.Email);
                if (!containsItem)
                {
                    usuario.Created = Data;
                    usuario.Last_login = Data;
                    _banco.Usuario.Add(usuario);

                    _banco.SaveChanges();
                    return Ok();
                }
                else
                {
                    string Existe = "E-mail já existente";
                    return Ok(Existe);
                }

            }
            catch (Exception erro)
            {

                return BadRequest(erro);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Put(Guid id,[FromBody]Usuarios usuario)
        {
            try
            {
                bool containsItem = _banco.Usuario.Any(item => item.Email == usuario.Email);
                if (!containsItem)
                {
                    var usuarioAtual = _banco.Usuario.Include(c => c.Phones).Where(c => c.IdUser == id).FirstOrDefault();
                    if (usuarioAtual == null)
                        return NotFound();
                  
                    else
                    {
                        if(usuario.Name != null)
                        usuarioAtual.Name = usuario.Name;
                        if (usuario.Email != null)
                        usuarioAtual.Email = usuario.Email;
                        if (usuario.Password != null)
                        usuarioAtual.Password = usuario.Password;
                        
                        usuarioAtual.Phones = usuario.Phones;
                        usuarioAtual.Modified = Data;
                        _banco.Update(usuarioAtual);
                        _banco.SaveChanges();
                    }

                    return Ok();
                }
                else
                {
                    string Existe = "E-mail já existente";
                    return Ok(Existe);
                }
            }
            catch (Exception erro)
            {

                return BadRequest(erro);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(Guid id)
        {
            try
            {    
                var usuarioAtual = _banco.Usuario.Include(c => c.Phones).Where(c => c.IdUser == id).FirstOrDefault();
                if (usuarioAtual == null)
                    return NotFound();
               
                else
                {
                    _banco.Remove(usuarioAtual);
                    _banco.SaveChanges();
                }

                return Ok();
            }
            catch (Exception erro)
            {

                return BadRequest(erro);
            }
        }


     
            [HttpPost]
            [Route("login")]
            [AllowAnonymous]
            public async Task<ActionResult<dynamic>> Authenticate([FromBody] Usuarios usuario)
            {
            var user = usuario.Email;
            var id = _banco.Usuario.Where (c => c.Email == user).FirstOrDefault();
         
                if (_banco.Usuario.Any(x=> x.Email != usuario.Email) && _banco.Usuario.Any(x => x.Password != usuario.Password))
                    return Unauthorized(new { message = "Usuário e/ou senha inválidos" });
                if  (_banco.Usuario.Any(item => item.Email == usuario.Email) && _banco.Usuario.Any(x => x.Password != usuario.Password))
                    return Unauthorized(new { message = "Usuário e/ou senha inválidos" });


            var token = TokenService.GenerateToken(usuario);
            id.Token = token;
            _banco.Update(id);
            _banco.SaveChanges();
            usuario.Password = "";
               
            return new
            {
                    user = user,
                    token = token

                };
            }
         
        
    


}
}
