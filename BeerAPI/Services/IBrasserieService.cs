using BeerAPI.Models;

namespace BeerAPI.Services
{
    public interface IBrasserieService
    {
        IEnumerable<Biere> ListerBieresParBrasserie(int idBrasserie);
        void AjouterBiere(Biere biere);
        void SupprimerBiere(int idBiere);
    }  
}
