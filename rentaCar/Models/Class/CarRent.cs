using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using rentaCar.Models.Entities;
namespace rentaCar.Models.Class
{
    public class CarRent
    {
        public IEnumerable<customers> CustomerId { get; set; }
        public IEnumerable<cars> CarList { get; set; }

        public IEnumerable<rent> RentList { get; set; }

    }
}