namespace BeerAPI.Models
{
    public class VenteDto
    {
        public int IdGrossiste { get; set; }
        public int IdBiere { get; set; }
        public int Quantite { get; set; }
        public float PrixFixe { get; set; }
    }

    public class MiseAJourStockDto
    {
        public int IdGrossiste { get; set; }
        public int IdBiere { get; set; }
        public int NouvelleQuantite { get; set; }
    }
    public class DemandeDevisDto
    {
        public int IdGrossiste { get; set; }
        public List<CommandeItemDto> Items { get; set; }
    }

    public class CommandeItemDto
    {
        public int IdBiere { get; set; }
        public int Quantite { get; set; }
    }
}
