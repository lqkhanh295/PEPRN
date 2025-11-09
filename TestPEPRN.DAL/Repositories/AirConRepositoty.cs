using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TestPEPRN.DAL.Models;

namespace TestPEPRN.DAL.Repositories
{
    public class AirConRepositoty
    {
        // CRUD: CREATE, READ, UPDATE, DELETE

        public List<AirConditioner> GetAll()
        {
            using var context = new AirConditionerShop2024DbContext();
            return context.AirConditioners
                .Include(a => a.Supplier)
                .ToList();
        }

        public AirConditioner? GetById(int id)
        {
            using var context = new AirConditionerShop2024DbContext();
            return context.AirConditioners
                .FirstOrDefault(a => a.AirConditionerId == id);
        }

        public void Add(AirConditioner airConditioner)
        {
            using var context = new AirConditionerShop2024DbContext();
            context.AirConditioners.Add(airConditioner);
            context.SaveChanges();
        }

        public void Update(AirConditioner airConditioner)
        {
            using var context = new AirConditionerShop2024DbContext();
            context.AirConditioners.Update(airConditioner);
            context.SaveChanges();
        }

        public void Delete(AirConditioner airConditioner)
        {
            using var context = new AirConditionerShop2024DbContext();
            context.AirConditioners.Remove(airConditioner);
            context.SaveChanges();
        }

        public List<AirConditioner> Search(string? featureFunction, int? quantity)
        {
            using var context = new AirConditionerShop2024DbContext();
            IQueryable<AirConditioner> query = context.AirConditioners.Include(a => a.Supplier);

            bool hasFeatureFilter = !string.IsNullOrWhiteSpace(featureFunction);
            bool hasQuantityFilter = quantity.HasValue;

            if (!hasFeatureFilter && !hasQuantityFilter)
            {
                return query.ToList();
            }

            if (hasFeatureFilter && hasQuantityFilter)
            {
                string searchPattern = $"%{featureFunction!.Trim()}%";
                query = query.Where(a =>
                    (a.FeatureFunction != null && EF.Functions.Like(a.FeatureFunction, searchPattern))
                    || (a.Quantity.HasValue && a.Quantity.Value == quantity.Value));
            }
            else if (hasFeatureFilter)
            {
                string searchPattern = $"%{featureFunction!.Trim()}%";
                query = query.Where(a => 
                    a.FeatureFunction != null && EF.Functions.Like(a.FeatureFunction, searchPattern));
            }
            else if (hasQuantityFilter)
            {
                query = query.Where(a => 
                    a.Quantity.HasValue && a.Quantity.Value == quantity.Value);
            }

            return query.ToList();
        }
    }
}
