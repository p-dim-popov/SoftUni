using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SharedTrip.Services
{
    using Data;
    using ViewModels;

    public class TripService
    {
        private readonly ApplicationDbContext _dbContext;

        public TripService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<TripAllViewModel> GetAll()
        {
            var locale = CultureInfo.GetCultureInfo("bg");
            return _dbContext.Trips
                .Select(x => new TripAllViewModel
                {
                    DepartureTime = x.DepartureTime.ToString(locale),
                    EndPoint = x.EndPoint,
                    Seats = x.Seats - _dbContext.UserTrips.Count(y => y.Trip.Id == x.Id),
                    StartPoint = x.StartPoint,
                    Id = x.Id
                })
                .ToList();
        }

        public void Add(TripAddInputModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}
