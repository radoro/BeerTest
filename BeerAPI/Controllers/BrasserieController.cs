using BeerAPI.Models;
using BeerAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BrasserieController : ControllerBase
    {
        private readonly IBrasserieService _brasserieService;
        public BrasserieController(IBrasserieService brasserieService)
        {
            _brasserieService = brasserieService;
        }
        [HttpGet("biere/par-brasserie/{idBrasserie}")]
        public IActionResult ListerBieresParBrasserie(int idBrasserie)
        {
            try
            {
                var bieres = _brasserieService.ListerBieresParBrasserie(idBrasserie);
                return Ok(bieres);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Une erreur interne est survenue");
            }
        }

        [HttpPost("AjoutBiere")]
        public IActionResult AjouterBiere([FromBody] Biere biere)
        {
            try
            {
                _brasserieService.AjouterBiere(biere);
                return CreatedAtAction(nameof(ListerBieresParBrasserie), new { idBrasserie = biere.IdBrasserie }, biere);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Une erreur interne est survenue lors de l'ajout de la bière");
            }
        }

        [HttpDelete("DeleteBiere/{idBiere}")]
        public IActionResult SupprimerBiere(int idBiere)
        {
            try
            {
                _brasserieService.SupprimerBiere(idBiere);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("La bière spécifiée n'a pas été trouvée");
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Une erreur interne est survenue lors de la suppression de la bière");
            }
        }

    }
}
