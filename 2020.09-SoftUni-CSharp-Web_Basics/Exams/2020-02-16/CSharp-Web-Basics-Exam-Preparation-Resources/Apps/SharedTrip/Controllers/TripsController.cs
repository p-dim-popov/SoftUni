using System;
using System.Globalization;
using SharedTrip.Services;
using SharedTrip.ViewModels;
using SUS.HTTP;
using SUS.MvcFramework;

namespace SharedTrip.Controllers
{
    public class TripsController : Controller
    {
        private readonly TripService _tripService;

        public TripsController(TripService tripService)
        {
            _tripService = tripService;
        }
        public HttpResponse All()
        {
            return View(_tripService.GetAll());
        }

        public HttpResponse Add()
        {
            return View();
        }

        [HttpPost]
        public HttpResponse Add(TripAddInputModel model)
        {
            var validation = model.IsValid();
            if (!validation.isSuccessful)
            {
                return Error(validation.errorMessage);
            }

            bool isDateValid = DateTime.TryParseExact(
                model.DepartureTime,
                "dd.MM.yyyy HH:mm",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out _
            );

            if (!isDateValid)
            {
                return Error("Date is not in valid format");
            }

            _tripService.Add(model);
        }

        public HttpResponse Details(string tripId)
        {
            return View();
        }

        public HttpResponse AddUserToTrip(string tripId)
        {
            return View();
        }
    }
}
