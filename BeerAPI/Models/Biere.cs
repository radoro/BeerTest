using System;
using System.Collections.Generic;

namespace BeerAPI.Models;

public partial class Biere
{
    public int IdBiere { get; set; }

    public string Nom { get; set; } = null!;

    public int IdBrasserie { get; set; }

    public double TauxAlcool { get; set; }

    public double Prix { get; set; }

    public virtual ICollection<GrossisteBiere> GrossisteBieres { get; set; } = new List<GrossisteBiere>();

    public virtual Brasserie IdBrasserieNavigation { get; set; } = null!;
}
