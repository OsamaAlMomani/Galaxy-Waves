using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Main.Areas.SharedPersonalRefrance.Models
{
    public class General_Info
    {
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? PostalCode { get; set;}
    }
}
