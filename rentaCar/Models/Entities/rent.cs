//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace rentaCar.Models.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class rent
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int CarId { get; set; }
        public System.DateTime RentalDate { get; set; }
        public System.DateTime ReturnDate { get; set; }
        public string Note { get; set; }
    
        public virtual cars cars { get; set; }
        public virtual customers customers { get; set; }
    }
}
