﻿using AppDocker.Context;
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
                IList<Usuarios> usuario = _banco.Usuario.Include(c => c.Phones).ToList();
             
                
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
                        usuarioAtual.Name = usuario.Name;
                        usuarioAtual.Email = usuario.Email;
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
    }
}
