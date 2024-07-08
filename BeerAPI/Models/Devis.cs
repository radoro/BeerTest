using System.Collections.Generic;
namespace BeerAPI.Models
{
    public class Devis
    {
        public List<DevisItem> Items { get; set; } = new List<DevisItem>();
        public decimal Total => CalculerTotal();
        public decimal Remise { get; set; }
        public decimal TotalApresRemise => Total - Remise;
        public int TotalQuantite => CalculerTotalQuantite();

        private decimal CalculerTotal()
        {
            decimal total = 0;
            foreach (var item in Items)
            {
                total += item.SousTotal;
            }
            return total;
        }

        private int CalculerTotalQuantite()
        {
            int totalQuantite = 0;
            foreach (var item in Items)
            {
                totalQuantite += item.Quantite;
            }
            return totalQuantite;
        }
    }
}

