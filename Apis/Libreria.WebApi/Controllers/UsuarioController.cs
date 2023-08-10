using Libreria.LogicaAplicacion.Dto;
using Libreria.LogicaAplicacion.InterfacesCasoUso.Usuario;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Drawing.Imaging;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Libreria.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private IGetAllUsuarios _getUsu;

        public UsuarioController(
            IGetAllUsuarios getAllUsuarios
            )
        {
            _getUsu = getAllUsuarios;
        }
        // GET: api/<UsuarioController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        /// <summary>
        ///  Se Loguea un Usuario y se le genera un tocken.
        /// </summary>
        /// <param name="usuarioDto">Objeto que contiene toda la informacion para poder loguearse un usuario</param>
        /// <seealso cref="UsuarioDto" href=""/>
        /// <remarks>
        /// <code>
        ///     Ejemplo de solicitud:
        ///     POST api/Mantenimiento
        ///     Incluir en el body:
        ///     {
        ///         "Email": "pepe@gmail.com",
        ///         "Pass": "Abcd1234"
        ///       
        ///      }
        ///      </code>
        /// </remarks>
        /// <returns>Se genera un token al usuario, BadRecuest si no se logor loguear </returns>
        /// <response code="201"> Se genera el token </response>
        /// <response code="404"> No se pasaron los datos</response>
        /// <response code="500"> Error a la hora de buscar el usuario </response>
        [HttpPost ("login")]
        public ActionResult<UsuarioDto> Login(UsuarioDto usuarioDto) {

            if (_getUsu == null) {
                return NotFound();
            }

            var existe = _getUsu.TraerUsuarios().Any(u => u.Email == usuarioDto.Email && u.Pass == usuarioDto.Pass);

            if (!existe) { 
               return Unauthorized();
            }

            var clave = "F6j3#hC$xIKEdjCnx^5lvUb8";
            var claveEnByte = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(clave));
            var signinCredentials = new SigningCredentials(claveEnByte, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: "identificadorEmisor",
                audience: "identificadorAudiencia",
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(200),
                signingCredentials: signinCredentials
                );
            usuarioDto.Token = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

            return Ok(usuarioDto);
        }
    }
}
