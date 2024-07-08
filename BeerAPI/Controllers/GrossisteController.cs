using BeerAPI.Models;
using BeerAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GrossisteController : ControllerBase
    {

        private readonly IGrossisteService _grossisteService;

        public GrossisteController(IGrossisteService grossisteService)
        {
            _grossisteService = grossisteService;
        }

        [HttpPost("ajouterVente")]
        public IActionResult AjouterVenteBiere([FromBody] VenteDto vente)
        {
            try
            {
                _grossisteService.AjouterVenteBiere(vente.IdGrossiste, vente.IdBiere, vente.Quantite, vente.PrixFixe);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("mettreAJourQuantite")]
        public IActionResult MettreAJourQuantiteBiere([FromBody] MiseAJourStockDto miseAJour)
        {
            try
            {
                _grossisteService.MettreAJourQuantiteBiere(miseAJour.IdGrossiste, miseAJour.IdBiere, miseAJour.NouvelleQuantite);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("calculerDevis")]
        public IActionResult CalculerDevis([FromBody] DemandeDevisDto demande)
        {
            try
            {
                if (demande == null)
                {
                    return BadRequest("La demande ne peut pas être null.");
                }

                if (demande.Items == null || !demande.Items.Any())
                {
                    return BadRequest("La liste des items ne peut pas être vide.");
                }

                // Convertir CommandeItemDto en CommandeItem
                var commandeItems = demande.Items.Select(item => new CommandeItem
                {
                    IdBiere = item.IdBiere,
                    Quantite = item.Quantite
                }).ToList();

                var devis = _grossisteService.CalculerDevis(demande.IdGrossiste, commandeItems);
                return Ok(devis);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur interne du serveur : {ex.Message}");
            }
        }



    }
}
