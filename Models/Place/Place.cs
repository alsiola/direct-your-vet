using DYV.Models.User;
using System;
using System.ComponentModel.DataAnnotations;

namespace DYV.Models
{
    public class Place
    {
        public int Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Address 1")]
        public string Address1 { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        public string County { get; set; }


        public string Country { get; set; }
        
        public string PostCode { get; set; }

        [Required]

        public decimal Latitude { get; set; }

        [Required]
        public decimal Longitude { get; set; }

        [Required]
        public string ClientUserId { get; set; }
        public ClientUser ClientUser { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Added")]
        public DateTime DateAdded { get; set; }
    }
}
