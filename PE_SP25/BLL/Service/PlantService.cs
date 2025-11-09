using DAL;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class PlantService
    {
        private PlantRepo _repo = new();

        public List<Plant> GetAllPlants()
        {
            return _repo.GetAllPlants();
        }

        public Plant? GetById(int id)
        {
            return _repo.GetById(id);
        }
    }
}
