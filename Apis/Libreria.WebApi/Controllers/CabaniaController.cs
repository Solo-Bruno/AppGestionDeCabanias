using Libreria.LogicaAplicacion.Dto;
using Libreria.LogicaAplicacion.Dto.Auxiliar;
using Libreria.LogicaAplicacion.InterfacesCasoUso.Cabania;
using Libreria.LogicaAplicacion.InterfacesCasoUso.Cabanias;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Libreria.WebApi.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/[controller]")]
    [ApiController]
    public class CabaniaController : ControllerBase
    {
        private IFindAllCabania _FindAllCab;
        private IAltaCabania _DarDeAltaCabania;
        private IFindByFiltroCabania _FiltrosCab;
        private IFindByIdCabania _getIdCab;
        private IFindCabaniasFiltrado _FindCabFiltrado;

        public CabaniaController(
            IFindAllCabania allCabanias,
            IAltaCabania altaCabania,
            IFindByFiltroCabania getCabFiltros,
            IFindByIdCabania getCabById,
            IFindCabaniasFiltrado findCabaniasFiltrado
            )
        {
            _FindAllCab = allCabanias;  
            _DarDeAltaCabania = altaCabania;
            _FiltrosCab = getCabFiltros;
            _getIdCab = getCabById;
            _FindCabFiltrado = findCabaniasFiltrado;
        }
        // GET: api/<CabaniaController>

        /// <summary>
        /// Retorna todas las Cabania que hay en la base de datos
        /// </summary>
        /// <returns>Una lista con todas las Cabanias. La lista vacía si no hay Cabanias.</returns>
        /// <response code="200"> Retorna la lista de Cabanias </response>
        /// <response code="404"> No se encontraron los datos </response>
        
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CabaniaDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<CabaniaDto>> Get()
        {
            try
            {
                var lista = _FindAllCab.FindAll();

                if (lista == null) { return BadRequest("No hay ninguna cabania"); }

                return Ok(lista);

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Retorna una lista de Cabanias segun el filtro aplicado.
        /// </summary>
        /// <param name="filtros">Clase que contiene los filtros para aplicar</param>
        /// <seealso cref="FiltrosDto" href=""/>
        /// <returns>Las Cabanias  encontradas, null si no existe una Cabania con esos filtros</returns>
        /// <response code="200"> Encuentra la cabania </response>
        /// <response code="404"> No se encuentra la cabania </response>
        /// <response code="500"> Error a la hora de buscar la cabania </response>

        [Authorize]
        [HttpPost ("filtros")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CabaniaDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<CabaniaDto>> GetByFiltros(FiltrosDto filtros)
        {
            try
            {
                var lista = _FiltrosCab.FindByFiltros(filtros.nombre, filtros.tipoId, filtros.cantidad, filtros.hab);

                if (lista.Count() == 0) { return BadRequest("No hay ninguna cabania"); }

                return Ok(lista);

            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Retorna una Cabania dada su clave.
        /// </summary>
        /// <param name="id">Identificador de la Cabania</param>
        /// <returns>La Cabania encontrada, null si no existe una Cabania con ese Id</returns>
        /// <response code="200"> Encuentra la cabania </response>
        /// <response code="404"> No se encuentra la cabania </response>
        /// <response code="500"> Error a la hora de buscar la cabania </response>

        // GET api/<CabaniaController>/5
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CabaniaDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<CabaniaDto> Get(int id)
        {
            if (id == null) { return BadRequest("No se paso la id"); }
            try
            {
                var ret = _getIdCab.FindById(id);
                if (ret == null) { return BadRequest("Se encontro ninguna cabania"); }

                return Ok(ret); 
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Retorna OK si se da de alta la Cabania.
        /// </summary>
        /// <param name="cabaniaDto">Objeto que contiene toda la informacion para poder dar de alta una Cabania</param>
        /// <seealso cref="CabaniaDto" href=""/>
        /// <remarks>
        /// <code>
        ///     Ejemplo de solicitud:
        ///     POST api/Cabania
        ///     Incluir en el body:
        ///     {
        ///         "NumeroHabitacion": 0,
        ///         "Nombre": "Cabania de Eventos",
        ///         "Descripcion": "Cabania utilizada para eventos",
        ///         "TieneJacuzzi": "false",
        ///         "TieneReservas": "true",
        ///         "CantMaxPers": 15,
        ///         "NombreFoto": "Cabania.png",
        ///         "TipoId": 1
        ///      }
        ///      </code>
        /// </remarks>
        /// <returns>La Cabania es creada, BadRecuest si no se logor dar el Alta </returns>
        /// <response code="201"> Se crea la Cabania </response>
        /// <response code="404"> No se encuentra la cabania </response>
        /// <response code="500"> Error a la hora de buscar la cabania </response>

        // POST api/<CabaniaController>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CabaniaDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] CabaniaDto cabaniaDto)
        {
            try
            {
                if (cabaniaDto == null) { return BadRequest("No hay datos de la Cabania"); }
                if (string.IsNullOrEmpty(cabaniaDto.NombreFoto)) { return BadRequest($"No existe esa ruta"); }

                

                _DarDeAltaCabania.Ejecutar(cabaniaDto);
                //_DarDeAltaCabania.GuardarImagen(cabaniaDto, imagen, ruta);
                return Ok();

            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }



        /// <summary>
        ///  No implementado.
        /// </summary>

        // PUT api/<CabaniaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
        /// <summary>
        ///  No implementado.
        /// </summary>
        // DELETE api/<CabaniaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        /// <summary>
        /// Retorna una lista de Cabanias segun el filtro aplicado.
        /// </summary>
        /// <param name="filtro">Clase que contiene los filtros para aplicar</param>
        /// <seealso cref="FiltroCabaniasDto" href=""/>
        /// <returns>Las Cabanias  encontradas, null si no existe una Cabania con esos filtros</returns>
        /// <response code="200"> Encuentra la cabania </response>
        /// <response code="404"> No se encuentra la cabania </response>
        /// <response code="500"> Error a la hora de buscar la cabania </response>

        [Authorize]
        [HttpPost("filtradoCabania")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CabaniaDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<CabaniaDto>> GetByFiltradoCabania(FiltroCabaniasDto filtro)
        {
            try
            {
                var lista = _FindCabFiltrado.ObtenerCabaniasFiltro(filtro.tipoId, filtro.monto);

                if (lista.Count() == 0) { return BadRequest("No hay ninguna cabania"); }

                return Ok(lista);

            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }
    }
}
