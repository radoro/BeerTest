using System;
using System.Collections.Generic;

namespace BeerAPI.Models;

public partial class Brasserie
{
    public int IdBrasserie { get; set; }

    public string Nom { get; set; } = null!;

    public virtual ICollection<Biere> Bieres { get; set; } = new List<Biere>();
}
