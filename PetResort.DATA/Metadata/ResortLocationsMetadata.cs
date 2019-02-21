using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PetResort.DATA//.Metadata
{
    public class ResortLocationsMetadata
    {
        [Key]
        [ScaffoldColumn(false)]
        public int ResortLocationID { get; set; }

        [Display(Name = "Resort Name")]
        [Required(ErrorMessage = "*Required")]
        public string ResortName { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "*Required")]
        public string Address { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "*Required")]
        public string City { get; set; }

        [Display(Name = "State")]
        [Required(ErrorMessage = "*Required")]
        [StringLength(2, ErrorMessage = "Must Use State Abbreviations")]
        public string State { get; set; }

        [Display(Name = "Zip Code")]
        [Required(ErrorMessage = "*Required")]
        [MaxLength(5, ErrorMessage = "Must BE 5 Characters Long")]
        [MinLength(5, ErrorMessage = "Must Be 5 Characters Long")]
        public int ZipCode { get; set; }

        [Display(Name = "Reservation Limit")]
        [Required(ErrorMessage = "*Required")]
        public byte ReservationLimit { get; set; }

        
    }
    [MetadataType(typeof(ResortLocationsMetadata))]
    public partial class ResortLocation { }

}
