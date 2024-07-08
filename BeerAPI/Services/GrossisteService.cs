using BeerAPI.Models;
using BeerAPI.Services;

namespace BeerAPI.Services
{
    public class GrossisteService : IGrossisteService
    {
        private readonly SqldbFinancePocContext _context;

        public GrossisteService(SqldbFinancePocContext context)
        {
            _context = context;
        }

        public void AjouterVenteBiere(int idGrossiste, int idBiere, int quantite, float prixFixe)
        {
            // Vérifier si la relation GrossisteBiere existe déjà
            var grossisteBiere = _context.GrossisteBieres
                .FirstOrDefault(gb => gb.IdGrossiste == idGrossiste && gb.IdBiere == idBiere);

            if (grossisteBiere != null)
            {
                // Si elle existe, mettre à jour la quantité et le prix fixe
                grossisteBiere.Quantite += quantite;
                grossisteBiere.PrixFixe = prixFixe;
            }
            else
            {
                // Sinon, créer une nouvelle relation GrossisteBiere
                _context.GrossisteBieres.Add(new GrossisteBiere
                {
                    IdGrossiste = idGrossiste,
                    IdBiere = idBiere,
                    Quantite = quantite,
                    PrixFixe = prixFixe
                });
            }

            _context.SaveChanges();
        }

        public void MettreAJourQuantiteBiere(int idGrossiste, int idBiere, int nouvelleQuantite)
        {
            var grossisteBiere = _context.GrossisteBieres
                .FirstOrDefault(gb => gb.IdGrossiste == idGrossiste && gb.IdBiere == idBiere);

            if (grossisteBiere != null)
            {
                grossisteBiere.Quantite = nouvelleQuantite;
                _context.SaveChanges();
            }
            else
            {               
                throw new KeyNotFoundException("La relation GrossisteBiere spécifiée n'existe pas.");
            }
        }
        public Devis CalculerDevis(int idGrossiste, List<CommandeItem> items)
        {
            if (!items.Any()) throw new ArgumentException("La commande ne peut pas être vide");
            if (!_context.Grossistes.Any(g => g.IdGrossiste == idGrossiste)) throw new ArgumentException("Le grossiste doit exister");
            if (items.GroupBy(i => i.IdBiere).Any(g => g.Count() > 1)) throw new ArgumentException("Il ne peut y avoir de doublon dans la commande");

            var devis = new Devis();
            foreach (var item in items)
            {
                var stock = _context.GrossisteBieres.FirstOrDefault(gb => gb.IdGrossiste == idGrossiste && gb.IdBiere == item.IdBiere);
                if (stock == null || item.Quantite > stock.Quantite) throw new ArgumentException($"Stock insuffisant pour la bière {item.IdBiere}");

                var sousTotal = item.Quantite * stock.PrixFixe;
                devis.Items.Add(new DevisItem { BiereId = item.IdBiere, Quantite = item.Quantite, SousTotal = (decimal)sousTotal });
            }

            if (devis.TotalQuantite > 20)
            {
                devis.Remise = devis.Total * 0.2m;
            }
            else if (devis.TotalQuantite > 10)
            {
                devis.Remise = devis.Total * 0.1m;
            }
            return devis;
        }


    }
}



