using System;
using System.Collections.Generic;

namespace DAL;

public partial class Inventory
{
    public int InventoryId { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public DateOnly ArrivalDate { get; set; }

    public int ShelfLife { get; set; }

    public string Supplier { get; set; } = null!;

    public int? PlantId { get; set; }

    public virtual Plant? Plant { get; set; }
}
