using Business.Abstract;
using DataAccess.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class PlanetManager : IPlanetService
    {
        IPlanetDal _planetDal;

        public PlanetManager(IPlanetDal planetDal)
        {
            _planetDal = planetDal;
        }

        public void Add(Planet planet)
        {
            _planetDal.Add(planet);
        }

        public void Delete(Planet planet)
        {
            _planetDal.Delete(planet);
        }

        public List<Planet> GetAll(int page, int size, string sortProperty, bool sortOrder)
        {
            sortProperty = sortProperty.ToLower();

            var result = sortProperty switch
            {
                "name" => _planetDal.GetAll(page, size, x => x.Name, sortOrder),
                "temperature" =>  _planetDal.GetAll(page, size, x => x.Temperature, sortOrder),
                _ => _planetDal.GetAll(page, size, x => x.Id, sortOrder)
            };

            return result;
        }

        public Planet GetById(int id)
        {
            return _planetDal.Get(x => x.Id == id);
        }

        public void Update(Planet planet)
        {
            _planetDal.Update(planet);
        }
    }
}
