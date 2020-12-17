using AppDocker.Context;
using AppDocker.Models;
using Microsoft.AspNetCore.Http;
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
        public IActionResult Index()
        {
            try
            {
                IList<Usuarios> usuario = _banco.Usuario.Include(c => c.phones).ToList();
             
                
                return Ok(usuario);
            }
            catch (Exception erro)
            {

                return BadRequest(erro);
            }
        }


        [HttpGet("{Id}")]

        public IActionResult Get(Guid id)
        {
            try
            {

                var usuarioAtual = _banco.Usuario.Include(c => c.phones).Where(c => c.IdUser == id).FirstOrDefault();
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

        [HttpPost]
        public IActionResult Post([FromBody]Usuarios usuario)
        {
            try
            {
             
                _banco.Usuario.Add(usuario);

                _banco.SaveChanges();
                return Ok();

            }
            catch (Exception erro)
            {

                return BadRequest(erro);
            }
        }

        [HttpPut("{id}")]

        public IActionResult Put(Guid id,[FromBody]Usuarios usuario)
        {
            try
            {
                var usuarioAtual = _banco.Usuario.Include(c => c.phones).Where(c => c.IdUser == id).FirstOrDefault();
                if (usuarioAtual == null)
                    return NotFound();
                else
                {
                    usuarioAtual.Name = usuario.Name;
                    usuarioAtual.Email = usuario.Email;
                    usuarioAtual.Password = usuario.Password;
                    usuarioAtual.phones = usuario.phones;
                    _banco.Update(usuarioAtual);
                    _banco.SaveChanges();
                }

                return Ok();
            }
            catch (Exception erro)
            {

                return BadRequest(erro);
            }
        }

        [HttpDelete("{id}")]

        public IActionResult Delete(Guid id)
        {
            try
            {
                var usuarioAtual = _banco.Usuario.Include(c => c.phones).Where(c => c.IdUser == id).FirstOrDefault();
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
    }
}
