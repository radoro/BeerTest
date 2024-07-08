using System;
using System.Collections.Generic;

namespace BeerAPI.Models;

public partial class GrossisteBiere
{
    public int IdGrossiste { get; set; }

    public int IdBiere { get; set; }

    public int Quantite { get; set; }

    public double PrixFixe { get; set; }

    public virtual Biere IdBiereNavigation { get; set; } = null!;

    public virtual Grossiste IdGrossisteNavigation { get; set; } = null!;
}
