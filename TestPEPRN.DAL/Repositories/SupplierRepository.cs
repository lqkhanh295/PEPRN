using System.Collections.Generic;
using System.Linq;
using TestPEPRN.DAL.Models;

namespace TestPEPRN.DAL.Repositories
{
    public class SupplierRepository
    {
        public List<SupplierCompany> GetAll()
        {
            using var context = new AirConditionerShop2024DbContext();
            return context.SupplierCompanies.ToList();
        }

        public SupplierCompany? GetById(string id)
        {
            using var context = new AirConditionerShop2024DbContext();
            return context.SupplierCompanies
                .FirstOrDefault(s => s.SupplierId == id);
        }
    }
}