using DA_CLIInscripcion.Model;
using DA_CLIInscripcion.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Build.Tasks;
namespace WAppHogwarts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InscripcionController : ControllerBase
    {
        private readonly ILogger<InscripcionController> _logger;
        private readonly IInscripcionDAO _inscripcionDAO;
        public InscripcionController(ILogger<InscripcionController> logger, IInscripcionDAO inscripcionDAO)
        {
            _logger = logger;
            _inscripcionDAO = inscripcionDAO;
        }
        /// <summary>
        /// Devuelve todo lo registros de inscripción
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Listado inscripciones</response>
        /// <response code="204">Lista vacia</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                List<Inscripcion> inscripciones = _inscripcionDAO.GetAll();
                if (inscripciones.Count == 0)
                {
                    return StatusCode(204, "Lista vacia");
                }
                return Ok(inscripciones);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Creación de inscripción
        /// </summary>
        /// <param name="inscripcion">Modelo de inscripción</param>
        /// <returns></returns>
        /// <response code="200">Inscripción creada</response>
        /// <response code="400">Modelo invalido</response>
        /// <response code="404">Casa magica no identificada</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        public async Task<IActionResult> Create(Inscripcion inscripcion)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int createInscripcion = await _inscripcionDAO.CreateInscripcion(inscripcion);
                    if(createInscripcion == 2)
                        return StatusCode(404, "Casa magica no identificada");

                    return StatusCode(200, "Inscripción creada");
                    
                }
                else
                {
                    return BadRequest("Inscripción invalida");
                }
               
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
           
        }
        /// <summary>
        /// Actualización de inscripción
        /// </summary>
        /// <param name="inscripcion">Modelo de inscripción</param>
        /// <returns></returns>
        /// <response code="200">Inscripción actualizada</response>
        /// <response code="204">Inscripción no encontrada</response>
        /// <response code="400">Modelo invalido</response>
        /// <response code="404">Casa magica no identificada</response>
        /// <response code="500">Internal server error</response>
        [HttpPut]
        public async Task<IActionResult> Update(Inscripcion inscripcion)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    int updateInscripcion = await _inscripcionDAO.UpdateInscripcion(inscripcion);

                    if (updateInscripcion == 2)
                        return StatusCode(404, "casa magica no identificada");
                    if (updateInscripcion == 3)
                        return StatusCode(204, "Inscripción no encontrada");

                    return StatusCode(200, "inscripción actualizada");
                }
                else
                {
                    return BadRequest("Inscripción invalida");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Eliminación de inscripción
        /// </summary>
        /// <param name="id">Identificador unico del resgistro de inscripción</param>
        /// <returns></returns>
        /// <response code="200">Inscripción eliminada</response>
        /// <response code="204">Inscripción no encontrada</response>
        /// <response code="400">Modelo invalido</response>
        /// <response code="404">Casa magica no identificada</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int deleteinscripcion = await _inscripcionDAO.DeleteInscripcion(id);

                    if (deleteinscripcion == 3)
                        return StatusCode(204, "Inscripción no encontrada");

                    return StatusCode(200, "Inscripción eliminada");
                }
                else
                {
                    return BadRequest("Inscripción invalida"); 
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
   
    }
}
