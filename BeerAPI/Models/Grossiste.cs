using System;
using System.Collections.Generic;

namespace BeerAPI.Models;

public partial class Grossiste
{
    public int IdGrossiste { get; set; }

    public string Nom { get; set; } = null!;

    public virtual ICollection<GrossisteBiere> GrossisteBieres { get; set; } = new List<GrossisteBiere>();
}
