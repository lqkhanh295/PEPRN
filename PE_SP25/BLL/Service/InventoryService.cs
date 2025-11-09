using DAL;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class InventoryService
    {
        private InventoryRepo _repo = new();

        public List<Inventory> GetAllInventories()
        {
            return _repo.GetAllInventories();
        }

        public Inventory? GetById(int id)
        {
            return _repo.GetById(id);
        }

        public void AddInventory(Inventory inventory)
        {
            _repo.AddInventory(inventory);
        }

        public void UpdateInventory(Inventory inventory)
        {
            _repo.UpdateInventory(inventory);
        }

        public void DeleteInventory(Inventory inventory)
        {
            _repo.DeleteInventory(inventory);
        }
    }
}
