using Libreria.LogicaAplicacion.Dto;
using Libreria.LogicaAplicacion.Dto.Auxiliar;
using Libreria.LogicaAplicacion.InterfacesCasoUso.Mantenimiento;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Libreria.WebApi.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/[controller]")]
    [ApiController]

    public class MantenimientoController : ControllerBase
    {
        private IAltaMtto _AddMtto;
        private IFindByFechas _FindByFechas;
        private IGetAllMttoXCab _FinAllMtto;
        private IGetMttoGroupBy _getGroup;

        public MantenimientoController(
           IAltaMtto altaMant,
           IFindByFechas FindFechas,
           IGetAllMttoXCab getAllMttoXCab,
           IGetMttoGroupBy getMttoGroupBy
           
        

           )
        {
            _AddMtto = altaMant;
            _FindByFechas = FindFechas;
            _FinAllMtto = getAllMttoXCab;
            _getGroup = getMttoGroupBy;
        }

        /// <summary>
        /// Retorna todos los Mantenimientos de la cabania
        /// </summary>
        /// <returns>Una lista con todos los Manteniminetos de la Cabania. La lista vacía si no hay Manteniminetos.</returns>
        /// <response code="200"> Retorna la lista de los Manteniminetos </response>
        /// <response code="404"> No se encontraron los datos </response>

        [Authorize]
        [HttpPost("All")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MantenimientoDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<MantenimientoDto>> Get(ValoresBusquedaFechaDto Valores)
        {
            try
            {
                int id = (int)Valores.id;
                var lista = _FinAllMtto.Ejecutar(id);

                if (lista == null) { return BadRequest("No hay ningun mantenimiento"); }

                return Ok(lista);

            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Retorna un los Manteniminetos de la cabaia en el rango de dos fechas.
        /// </summary>
        /// <param name="Valores">Clase que contiene las fechas para filtrar</param>
        /// <seealso cref="ValoresBusquedaFechaDto" href=""/>
        /// <returns>Lista con los Manteniminetos que esten comprendidos entre las fechas</returns>
        /// <response code="200"> Encuentra los Manteniminetos </response>
        /// <response code="404"> No se encuentra ningun Mantenimineto </response>
        /// <response code="500"> Error a la hora de buscar los Manteniminetos </response>

        [Authorize]
        [HttpPost("fechas")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MantenimientoDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<MantenimientoDto>> GetByFechas(ValoresBusquedaFechaDto Valores)
        {
            try
            {
                var lista = _FindByFechas.ObtenerPorFechas( Valores.fch1,  Valores.fch2, Valores.id);

                if (lista == null) { return BadRequest("No hay ningun mantenimiento entre esa fecha"); }

                return Ok(lista);

            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        ///  Se da de alta un Mantenimiento.
        /// </summary>
        /// <param name="mantDto">Objeto que contiene toda la informacion para poder dar de alta un Mantenimineto</param>
        /// <seealso cref="MantenimientoDto" href=""/>
        /// <remarks>
        /// <code>
        ///     Ejemplo de solicitud:
        ///     POST api/Mantenimiento
        ///     Incluir en el body:
        ///     {
        ///         "Id": 0,
        ///         "Fecha": "12/04/2023",
        ///         "Descripcion": "Fallo en sistema electrico",
        ///         "Costo": 200,
        ///         "NombreUs": "Jose",
        ///         "CabaniaId": 2
        ///      }
        ///      </code>
        /// </remarks>
        /// <returns>El Mantenimiento es creado, BadRecuest si no se logor dar el Alta </returns>
        /// <response code="201"> Se crea el Mantenimineto </response>
        /// <response code="404"> No se pasaron los datos</response>
        /// <response code="500"> Error a la hora de dar el alta el Mantenimiento </response>

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MantenimientoDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] MantenimientoDto mantDto)
        {
            try
            {
                if (mantDto == null) { return BadRequest("No hay datos de la Cabania"); }


                _AddMtto.Ejecutar(mantDto);

                //_DarDeAltaCabania.GuardarImagen(cabaniaDto, imagen, ruta);
                return Ok();

            }
            catch (Exception e)
            {

                return StatusCode(500, e);
            }
        }

        /// <summary>
        /// Retorna un los Manteniminetos de la cabaia en el rango de dos topes.
        /// </summary>
        /// <param name="topesGroup">Clase que contiene los topes para filtrar</param>
        /// <seealso cref="topesGroupBy" href=""/>
        /// <returns>Lista con los nombres de los empleados y el costo total de todos sus manteniminetos</returns>
        /// <response code="200"> Encuentra los Manteniminetos </response>
        /// <response code="404"> No se encuentra ningun Mantenimineto </response>
        /// <response code="500"> Error a la hora de buscar los Manteniminetos </response>

        [HttpPost("GroupBy")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MantenimientoDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult GetGroupByMtto([FromBody] topesGroupBy topesGroup)
        {
            try
            {
                if (topesGroup.Top1 == null || topesGroup.Top2 == null) { return BadRequest("No hay topes para medir el rango"); }
                var ret = _getGroup.GetMttoGroupBy(topesGroup.Top1, topesGroup.Top2);

               
                return Ok(ret);

            }
            catch (Exception e)
            {

                return StatusCode(500, e);
            }


        }


    }
}
