using System;
using System.Collections.Generic;

namespace DAL;

public partial class Plant
{
    public int PlantId { get; set; }

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string Origin { get; set; } = null!;

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
}
