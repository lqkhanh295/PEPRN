using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class InventoryRepo
    {
        public List<Inventory> GetAllInventories()
        {
            using var context = new Sp25PlantInventoryDbContext();
            {
                return context.Inventories.ToList();
            }
        }

        public Inventory? GetById(int id)
        {
            using var context = new Sp25PlantInventoryDbContext();
            return context.Inventories
                .FirstOrDefault(a => a.InventoryId == id);
        }

        public void AddInventory(Inventory inventory)
        {
            using var context = new Sp25PlantInventoryDbContext();
            context.Inventories.Add(inventory);
            context.SaveChanges();
        }

        public void UpdateInventory(Inventory inventory)
        {
            using var context = new Sp25PlantInventoryDbContext();
            context.Inventories.Update(inventory);
            context.SaveChanges();
        }

        public void DeleteInventory(Inventory inventory)
        {
            using var context = new Sp25PlantInventoryDbContext();
           if(inventory != null)
            {
                context.Inventories.Remove(inventory);
                context.SaveChanges();
            }
        }

        public void DeleteInventory(int id)
        {
            using var context = new Sp25PlantInventoryDbContext();
            var mockTest = context.Inventories.Find(id);
            if (mockTest != null)
            {
                context.Inventories.Remove(mockTest);
                context.SaveChanges();
            }
        }

    }
}
