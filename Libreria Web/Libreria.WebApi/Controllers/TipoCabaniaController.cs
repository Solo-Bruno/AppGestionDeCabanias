using Libreria.LogicaAplicacion.Dto;
using Libreria.LogicaAplicacion.InterfacesCasoUso.Parametro;
using Libreria.LogicaAplicacion.InterfacesCasoUso.TipoCab;
using Libreria.LogicaNegocio.Entidades;
using Libreria.LogicaNegocio.ExepcionesDominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Libreria.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoCabaniaController : ControllerBase
    {
        private IAltaTipoCabania _altaTipoCab;
        private IGetByNameTipoCabania _getByNameTipCab;
        private IGetTiposCabania _getTipoCab;
        private IRemoveByIdTipoCabania _removeTipoCab;
        private IUpdateTipoCabania _updateTipoCab;
        private IGetByIdTipoCabania _getByIdTipoCab;

        public TipoCabaniaController(
            IAltaTipoCabania ATC, 
            IGetByNameTipoCabania NTC,
            IGetTiposCabania GTC,
            IRemoveByIdTipoCabania RTC,
            IUpdateTipoCabania UTC,
            IGetByIdTipoCabania GITC)
        {
            _altaTipoCab = ATC;
            _getByNameTipCab = NTC;
            _getTipoCab = GTC;
            _removeTipoCab = RTC;
            _updateTipoCab = UTC;
            _getByIdTipoCab = GITC;
          
        }
        // GET: api/<TipoCabaniaController>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TipoCabaniaDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
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


        // GET api/<TipoCabaniaController>/5
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TipoCabaniaDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}", Name ="GetById")]
        public ActionResult<TipoCabaniaDto> Get(int id)
        {
            try
            {
                if (id == null)
                {

                    return BadRequest("Debe pasar una id");
                }
                var TipoCab = _getByIdTipoCab.FindById(id);

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

        // POST api/<TipoCabaniaController>
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
                return CreatedAtRoute("GetById", new { id = tipoCabaniaDto.Id}, tipoCabaniaDto);
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

        // PUT api/<TipoCabaniaController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TipoCabaniaDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TipoCabania> Put(int id, [FromBody] TipoCabaniaDto tipoCabaniaDto)
        {
            if (id == null) {
                return BadRequest("Debe proporcionar el identificador del Tipo Cabania a modificar");
            }
            if (tipoCabaniaDto == null) {
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
            catch (DbUpdateException ex) { 
            
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<TipoCabaniaController>/5
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
            catch (DbUpdateException ex) {

                return StatusCode(500, ex);
            }
        }
    }
}
