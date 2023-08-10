using Libreria.LogicaAplicacion.Dto;
using Libreria.LogicaAplicacion.Dto.Auxiliar;
using Libreria.LogicaAplicacion.InterfacesCasoUso.Parametro;
using Libreria.LogicaAplicacion.InterfacesCasoUso.TipoCab;
using Libreria.LogicaNegocio.Entidades;
using Libreria.LogicaNegocio.ExepcionesDominio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Libreria.WebApi.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class TipoCabaniaController : ControllerBase
    {
        private IAltaTipoCabania _altaTipoCab;
        private IGetByNameTipoCabania _getByNameTipCab;
        private IGetTiposCabania _getTipoCab;
        private IRemoveByIdTipoCabania _removeTipoCab;
        private IUpdateTipoCabania _updateTipoCab;
        

        public TipoCabaniaController(
            IAltaTipoCabania ATC,
            IGetByNameTipoCabania NTC,
            IGetTiposCabania GTC,
            IRemoveByIdTipoCabania RTC,
            IUpdateTipoCabania UTC
           
            )
        {
            _altaTipoCab = ATC;
            _getByNameTipCab = NTC;
            _getTipoCab = GTC;
            _removeTipoCab = RTC;
            _updateTipoCab = UTC;

            

        }
        // GET: api/<TipoCabaniaController>
        /// <summary>
        /// Retorna todos los Tipo Cabania que hay en la base de datos
        /// </summary>
        /// <returns>Una lista con todos los Tipos Cabanias. La lista vacía si no hay Tipos Cabanias.</returns>
        /// <response code="200"> Retorna la lista de Tipos Cabanias </response>
        /// <response code="404"> No se encontraron los datos </response>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TipoCabaniaDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<TipoCabaniaDto>> Get()
        {
            try
            {
                var ret = _getTipoCab.FindAll();
                if (ret == null) { BadRequest("No hay Ningun Tipo Cabania aun"); }

                return Ok(ret);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Retorna un Tipo Cabania dada su clave.
        /// </summary>
        /// <param name="id">Identificador del Tipo Cabania</param>
        /// <returns>El Tipo Cabania encontrada, null si no existe un Tipo Cabania con ese Id</returns>
        /// <response code="200"> Encuentra el Tipo cabania </response>
        /// <response code="404"> No se encuentra el tipo cabania </response>
        /// <response code="500"> Error a la hora de buscar el tipo cabania </response>
        // GET api/<TipoCabaniaController>/5
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TipoCabaniaDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}", Name = "GetById")]
        public ActionResult<TipoCabaniaDto> Get(int id)
        {
            try
            {
                if (id == null)
                {

                    return BadRequest("Debe pasar una id");
                }
                var TipoCab = _updateTipoCab.FindById(id);

                if (TipoCab == null)
                {
                    return BadRequest($"No existe un Tipo Cabania con id {id}");
                }

                return Ok(TipoCab);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        /// <summary>
        /// Retorna un Tipo Cabania segun el nombre que se pasa para buscar.
        /// </summary>
        /// <param name="name">Clase que contiene el nombre que se busca</param>
        /// <seealso cref="NombreTipoDto" href=""/>
        /// <returns>El primer tipo cabania que coincida con el nombre que se le pasa</returns>
        /// <response code="200"> Encuentra el tipo Cabania </response>
        /// <response code="404"> No se encuentra el Tipo cabania </response>
        /// <response code="500"> Error a la hora de buscar el tipo cabania </response>

        [Authorize]
        [HttpPost ("Nombre")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TipoCabaniaDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TipoCabania> FindByName(NombreTipoDto name)
        {
            try
            {
                if (name == null)
                {
                    return BadRequest("No se encontro ningun nombre");
                }

                var ret = _getByNameTipCab.FindByName(name.Valor);

                return Ok(ret);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        // POST api/<TipoCabaniaController>

        /// <summary>
        ///  Se da de alta un Tipo Cabania.
        /// </summary>
        /// <param name="tipoCabaniaDto">Objeto que contiene toda la informacion para poder dar de alta un Tipo Cabania</param>
        /// <seealso cref="TipoCabaniaDto" href=""/>
        /// <remarks>
        /// <code>
        ///     Ejemplo de solicitud:
        ///     POST api/TipoCabania
        ///     Incluir en el body:
        ///     {
        ///         "Id": 0,
        ///         "Nombre": "Vip",
        ///         "Descripcion": "Cabania de alto Valor",
        ///         "CostoHuesped": 5000
        ///      }
        ///      </code>
        /// </remarks>
        /// <returns>El Tipo Cabania es creada, BadRecuest si no se logor dar el Alta </returns>
        /// <response code="201"> Se crea el Tipo Cabania </response>
        /// <response code="404"> No se pasaron los datos</response>
        /// <response code="500"> Error a la hora de dar el alta El tipo Cabania </response>

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TipoCabaniaDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TipoCabania> Post([FromBody] TipoCabaniaDto tipoCabaniaDto)
        {
            if (tipoCabaniaDto == null)
            {
                return BadRequest("Debe pasar un tipo Cabania para dar el alta");
            }
            try
            {
                _altaTipoCab.Ejecutar(tipoCabaniaDto);
                return CreatedAtRoute("GetById", new { id = tipoCabaniaDto.Id }, tipoCabaniaDto);
            }
            catch (TipoCabaniaException e)
            {

                return BadRequest(e.Message);
            }
            catch (DbUpdateException ex)
            {

                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        ///  Se actualiza la informacion de un Tipo Cabania.
        /// </summary>
        /// <param name="tipoCabaniaDto">Objeto que contiene toda la informacion para poder actualizar un Tipo Cabania</param>
        /// <seealso cref="TipoCabaniaDto" href=""/>
        /// <param name="id">Identificador del Tipo Cabania</param>
        /// <returns>El Tipo Cabania actualizado, BadRecuest si no se logor dar el Alta </returns>
        /// <response code="201"> Se crea el Tipo Cabania </response>
        /// <response code="404"> No se pasaron los datos, el id no es pasado corrctamente</response>
        /// <response code="500"> Error a la hora de actualizar el tipo Cabania  </response>

        // PUT api/<TipoCabaniaController>/5
        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TipoCabaniaDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TipoCabania> Put(int id, [FromBody] TipoCabaniaDto tipoCabaniaDto)
        {
            if (id == null)
            {
                return BadRequest("Debe proporcionar el identificador del Tipo Cabania a modificar");
            }
            if (tipoCabaniaDto == null)
            {
                return BadRequest("Debe proporcionar el Tipo Cabania con la nueva información");
            }

            try
            {
                _updateTipoCab.Update(tipoCabaniaDto);
                return Ok(tipoCabaniaDto);
            }
            catch (TipoCabaniaException e)
            {

                return BadRequest(e.Message);
            }
            catch (DbUpdateException ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<TipoCabaniaController>/5
        /// <summary>
        /// Permite eliminar un Tipo Cabania dado su id.
        /// </summary>
        /// <param name="id">Identificador del Tipo Cabania a eliminar.</param>
        /// <returns>204 en caso de eliminarlo, indicando que el recurso no existe.</returns>
        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return BadRequest("Debe proporcionar el identificador del Tipo Cabania para modificar");
            }
            try
            {
                _removeTipoCab.RemoveById(id);
                return NoContent();
            }
            catch (TipoCabaniaException e)
            {

                return BadRequest(e.Message);
            }
            catch (DbUpdateException ex)
            {

                return StatusCode(500, ex);
            }
        }
    }
}
