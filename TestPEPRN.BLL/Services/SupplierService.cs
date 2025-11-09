using System.Collections.Generic;
using TestPEPRN.DAL.Models;
using TestPEPRN.DAL.Repositories;

namespace TestPEPRN.BLL.Services
{
    public class SupplierService
    {
        private readonly SupplierRepository _repository = new();

        public List<SupplierCompany> GetAllSuppliers()
        {
            return _repository.GetAll();
        }

        public SupplierCompany? GetSupplierById(string id)
        {
            return _repository.GetById(id);
        }
    }
}
