using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class PlantRepo
    {
        public List<Plant> GetAllPlants()
        {
            using var context = new Sp25PlantInventoryDbContext();
            return context.Plants.ToList();
        }

        public Plant? GetById(int id)
        {
            using var context = new Sp25PlantInventoryDbContext();
            return context.Plants
                .FirstOrDefault(a => a.PlantId == id);
        }
    }
}
