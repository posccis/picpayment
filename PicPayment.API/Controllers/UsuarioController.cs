using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PicPayment.Application.Autenticacao;
using PicPayment.Application.Interfaces;
using PicPayment.Application.Services;
using PicPayment.Domain.Domains;
using PicPayment.Domain.DTOs;
using PicPayment.Domain.Exceptions;

namespace PicPayment.API.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService<Usuario> _usuarioService;
        private readonly JwtService _jwtService;
        private readonly IMapper _mapper;

        public UsuarioController( IUsuarioService<Usuario> usuarioService, JwtService jwtService, IMapper mapper)
        {
            _usuarioService = usuarioService;
            _jwtService = jwtService;
            _mapper = mapper;
        }
        /// <summary>
        /// Endpoint para realizar login no serviço.
        /// </summary>
        /// <param name="login"></param>
        /// <response code="200">Login realizado com sucesso.</response>
        /// <response code="500">Se ocorreu um erro interno do servidor.</response>
        /// <response code="404">Usuário não localizado ou dados de login incorretos.</response>
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest login)
        {
            try
            {
                _usuarioService.RealizarLogin(login.Cpf, login.Senha);
                var token = _jwtService.GenerateToken(login.Cpf.ToString());
                return Ok(new { Token = token });
            }
            catch (ServiceException serviceExcp)
            {

                return NotFound(serviceExcp.Message);
            }
            catch(Exception genericExcp)
            {
                return BadRequest(genericExcp.Message);
            }
        }

        /// <summary>
        /// Endpoint para cadastrar um usuário
        /// </summary>
        /// <param name="transferencia"></param>
        /// <response code="200">Transferência realizada com sucesso.</response>
        /// <response code="404">Um dos usuário não foi localizado.</response>
        /// <response code="500">Se ocorreu um erro interno do servidor.</response>
        [HttpPost("tranferencia")]
        [Authorize]
        public async Task<IActionResult> RealizaTransferencia([FromBody] TransferenciaDTORequest transferencia)
        {
            try
            {
                var transferenciaRes = _mapper.Map<Transferencia>(transferencia);
                transferenciaRes.DataTransferencia = DateTime.Now;
                transferenciaRes.TipoTransferencia = "Padrão";
                await _usuarioService.TransferirValor(transferenciaRes);
                return Ok("Transferencia realizada com sucesso");
            }
            catch (ServiceException serviceExc)
            {

                switch (serviceExc.codigo)
                {
                    case 1:
                        return NotFound("Ocorreu um erro ao tentar realizar a transferencia:\n" + serviceExc.Message);
                    case 2:
                        return BadRequest("Ocorreu um erro ao tentar realizar a transferencia:\n" + serviceExc.Message);
                    default:
                        return StatusCode(StatusCodes.Status500InternalServerError, serviceExc.Message);
                }
            }
            catch (Exception exp)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exp.Message);
            }
        }

        /// <summary>
        /// Endpoint para cadastrar um usuário
        /// </summary>
        /// <param name="usuario"></param>
        /// <response code="201">Usuário criado com sucesso.</response>
        /// <response code="500">Se ocorreu um erro interno do servidor.</response>
        [HttpPost("cadastrar")]
        public async Task<IActionResult> CadastrarUsuario(UsuarioDTORequest usuario)
        {
            try
            {
                var usuarioRes = _mapper.Map<Usuario>(usuario);
                await _usuarioService.InserirUsuario(usuarioRes);
                return CreatedAtAction(nameof(ObterUsuarioPorId), new { id = usuarioRes.Id }, usuario);
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
        /// <summary>
        /// Endpoint para obter o saldo do usuário.
        /// </summary>
        /// <param name="cpf"></param>
        /// <response code="200">Retorna o saldo do usuário.</response>
        /// <response code="500">Se ocorreu um erro interno do servidor.</response>
        [HttpGet("saldo/{cpf}")]
        [Authorize]
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

        /// <summary>
        /// Endpoint para obter um usuário através do seu Id.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Retorna o usuário.</response>
        /// <response code="404">O usuário não foi localizado.</response>
        /// <response code="500">Se ocorreu um erro interno do servidor.</response>
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
