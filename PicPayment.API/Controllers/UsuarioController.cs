using Microsoft.AspNetCore.Mvc;
using PicPayment.Application.Interfaces;
using PicPayment.Application.Services;
using PicPayment.Domain.Domains;
using PicPayment.Domain.Exceptions;

namespace PicPayment.API.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService<Usuario> _usuarioService;

        public UsuarioController( UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("cadastrar")]
        public async Task<IActionResult> CadastrarUsuario(Usuario usuario)
        {
            try
            {
                await _usuarioService.InserirUsuario(usuario);
                return CreatedAtAction(nameof(ObterUsuarioPorId), new { id = usuario.Id }, usuario);
            }

            catch (ServiceException serviceExcp)
            {
                return BadRequest(serviceExcp.Message);
            }
            catch (Exception genericExcp)
            {

                return BadRequest(genericExcp.Message);
            }
        }
        [HttpGet("saldo/{cpf}")]
        public async Task<IActionResult> ObterSaldo(long cpf)
        {
            try
            {
                var saldo = await _usuarioService.ObterSaldo(cpf);
                return Ok(saldo);
            }

            catch (ServiceException serviceExcp)
            {
                return BadRequest(serviceExcp.Message);
            }
            catch (Exception genericExcp)
            {

                return BadRequest(genericExcp.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> ObterUsuarioPorId(Guid id)
        {
            try
            {
                var usuario = await _usuarioService.ObterUsuarioPorId(id);
                return Ok(usuario);
            }
            catch(ServiceException serviceExcp)
            {
                return NotFound(serviceExcp.Message);
            }
            catch (Exception genericExcp)
            {

                return BadRequest(genericExcp.Message);
            }
        }
    }
}
