using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PetResort.DATA//.Metadata
{
    public class ReservationsMetadata
    {
        [Key]
        [ScaffoldColumn(false)]
        public int ReservationID { get; set; }

        [Display(Name = "Reservation Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Required(ErrorMessage = "*Required")]
        public DateTime ReservationDate { get; set; }
    }
    [MetadataType(typeof(ReservationsMetadata))]
    public partial class Reservation{}
}