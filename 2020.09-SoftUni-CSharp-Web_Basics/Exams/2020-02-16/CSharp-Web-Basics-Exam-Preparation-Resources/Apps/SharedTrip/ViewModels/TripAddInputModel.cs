using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace SharedTrip.ViewModels
{
    public class TripAddInputModel
    {
        [Required(ErrorMessage = 
            "Starting point" + ValidationHelper.RequiredErrorMessage)]
        public string StartPoint { get; set; }

        [Required(ErrorMessage = 
            "End point" + ValidationHelper.RequiredErrorMessage)]
        public string  EndPoint { get; set; }

        [Required(ErrorMessage = 
            "Departure time" + ValidationHelper.RequiredErrorMessage)]
        public string DepartureTime { get; set; }

        public DateTime ValidDepartureTime
            => DateTime
                .ParseExact(DepartureTime, 
                    "dd.MM.yyyy HH:mm",
                    CultureInfo.InvariantCulture);

        public string ImagePath { get; set; }

        [Range(2, 6, ErrorMessage = 
            "Seats" + ValidationHelper.RangeErrorMessage + "2 and 6")]
        public int Seats { get; set; }

        [Required(ErrorMessage = 
            "Description" + ValidationHelper.RequiredErrorMessage)]
        public string Decription { get; set; }
    }
}
