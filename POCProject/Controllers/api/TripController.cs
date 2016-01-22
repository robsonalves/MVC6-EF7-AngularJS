using Microsoft.AspNet.Mvc;
using POCProject.Models;

namespace POCProject.API
{
    public class TripController : Controller
    {
        private IWorldRepository _repository;

        public TripController(IWorldRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("api/trips")]
        public JsonResult Get()
        {
            var result = _repository.GetAllTripsWithStops();

            return Json(result);
        }
    }
}