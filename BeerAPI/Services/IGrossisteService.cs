using BeerAPI.Models;

namespace BeerAPI.Services
{
    public interface IGrossisteService
    {
        void AjouterVenteBiere(int idGrossiste, int idBiere, int quantite, float prixFixe);
        void MettreAJourQuantiteBiere(int idGrossiste, int idBiere, int nouvelleQuantite);
        Devis CalculerDevis(int idGrossiste, List<CommandeItem> items);
    }
}
