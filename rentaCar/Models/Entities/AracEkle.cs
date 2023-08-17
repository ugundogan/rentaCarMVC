using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rentaCar.Models.Entities
{
    public class AracEkle
    {
        public string LicensePlate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int ProductYear { get; set; }
        public string Color { get; set; }
        public int km { get; set; }
        public string CarType { get; set; }
        public byte RentState { get; set; }
        public decimal DailyPrice { get; set; }
        public IFormFile Image { get; set; }
    }
}