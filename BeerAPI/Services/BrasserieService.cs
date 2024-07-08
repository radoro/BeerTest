using BeerAPI.Models;

namespace BeerAPI.Services
{
    public class BrasserieService : IBrasserieService
    {
        private readonly SqldbFinancePocContext _context; 

        public BrasserieService(SqldbFinancePocContext context)
        {
            _context = context;
        }

        public IEnumerable<Biere> ListerBieresParBrasserie(int idBrasserie)
        {
            return _context.Bieres.Where(b => b.IdBrasserie == idBrasserie).ToList();
        }

        public void AjouterBiere(Biere biere)
        {
            _context.Bieres.Add(biere);
            _context.SaveChanges();
        }

        public void SupprimerBiere(int idBiere)
        {
            var biere = _context.Bieres.Find(idBiere);
            if (biere != null)
            {
                _context.Bieres.Remove(biere);
                _context.SaveChanges();
            }
        }
    }
}
