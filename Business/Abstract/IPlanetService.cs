using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IPlanetService
    {
        List<Planet> GetAll(int page, int size, string sortProperty, bool sortOrder);
        void Add(Planet car);
        Planet GetById(int id);
        void Delete(Planet planet);
        void Update(Planet planet);
    }
}
