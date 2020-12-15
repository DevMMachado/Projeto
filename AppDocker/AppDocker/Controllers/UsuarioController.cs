using AppDocker.Context;
using AppDocker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Get()
        {
            try
            {
               
                return Ok(_banco.Usuario.ToList());
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
                var usuarioAtual = _banco.Usuario.Where(c => c.Id == id).FirstOrDefault();
                if (usuarioAtual != null)
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
                var usuarioAtual = _banco.Usuario.Where(c => c.Id == id).FirstOrDefault();
                if (usuarioAtual != null)
                    return NotFound();
                else
                {
                    usuarioAtual.Name = usuario.Name;
                    usuarioAtual.Email = usuario.Email;
                    usuarioAtual.Password = usuario.Password;
                    usuarioAtual.Phones = usuario.Phones;
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
                var usuarioAtual = _banco.Usuario.Where(c => c.Id == id).FirstOrDefault();
                if (usuarioAtual != null)
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
