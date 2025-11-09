using System.Collections.Generic;
using TestPEPRN.DAL.Models;
using TestPEPRN.DAL.Repositories;

namespace TestPEPRN.BLL.Services
{
    public class AirConService
    {
        private readonly AirConRepositoty _repository = new();

        public List<AirConditioner> GetAllAirConditioners()
        {
            return _repository.GetAll();
        }

        public AirConditioner? GetAirConditionerById(int id)
        {
            return _repository.GetById(id);
        }

        public void CreateAirConditioner(AirConditioner airConditioner)
        {
            _repository.Add(airConditioner);
        }

        public void UpdateAirConditioner(AirConditioner airConditioner)
        {
            _repository.Update(airConditioner);
        }

        public void DeleteAirConditioner(AirConditioner airConditioner)
        {
            _repository.Delete(airConditioner);
        }

        public List<AirConditioner> SearchAirConditioners(string? featureFunction, int? quantity)
        {
            if (string.IsNullOrWhiteSpace(featureFunction) && !quantity.HasValue)
            {
                return GetAllAirConditioners();
            }

            string? sanitizedFeature = string.IsNullOrWhiteSpace(featureFunction) 
                ? null 
                : featureFunction.Trim();

            return _repository.Search(sanitizedFeature, quantity);
        }
    }
}
