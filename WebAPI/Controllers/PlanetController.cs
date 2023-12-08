using Business.Abstract;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/planets")]
    public class PlanetController : Controller
    {

        IPlanetService _planetService;

        public PlanetController(IPlanetService planetService)
        {
            _planetService = planetService;
        }

        [HttpPost]
        public IActionResult Add(Planet planet)
        {
            try
            {
                var added = _planetService.GetById(planet.Id);
                if (added == null)
                {
                    _planetService.Add(planet);
                    return Ok(planet);
                }
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete]
        public IActionResult Delete(Planet planet)
        {
            try
            {
                var deleted = _planetService.GetById(planet.Id);

                if (deleted != null)
                {
                    _planetService.Delete(planet);
                    return Ok(planet);
                }
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("/all")]
        public IActionResult GetAll(int page, int size, string sortProperty, bool sortOrder)
        {
            try
            {
                var result = _planetService.GetAll(page, size, sortProperty, sortOrder);

                if (result != null)
                {
                    return Ok(result);
                }
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var result = _planetService.GetById(id);

                if (result != null)
                {
                    return Ok(result);
                }

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut]
        public IActionResult Update(int id, Planet planet)
        {
            try
            {
                var updated = _planetService.GetById(id);

                if (updated != null)
                {
                    updated.Temperature = planet.Temperature;
                    updated.Name = planet.Name;

                    _planetService.Update(updated);

                    return Ok(updated);

                }

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPatch]
        public IActionResult Patch(Planet planet)
        {
            try
            {
                var isExist = _planetService.GetById(planet.Id);

                if (isExist != null)
                {
                    _planetService.Update(planet);
                    return Ok(planet);
                }

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

    }
}
