using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PetResort.DATA//.Metadata
{
    public class PetsMetadata
    {
        [Key]
        [ScaffoldColumn(false)]
        public int PetID { get; set; }

        [Display(Name = "Pet's Name")]
        [Required(ErrorMessage = "*Required")]
        public string Name { get; set; }

        [Display(Name = "Owner Name")]
        public string OwnerID { get; set; }

        [Display(Name = "Pet Photo")]
        public string PetPhoto { get; set; }

        [Display(Name = "Special Notes")]
        public string SpecialNotes { get; set; }

        [Display(Name = "Account Active")]
        public bool IsActive { get; set; }

        //[Display(Name = "Date Added")]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateAdded { get; set; }

        [Display(Name = "Type of Animal")]
        [Required(ErrorMessage = "*Required")]
        public string TypeOfAnimal { get; set; }
    }
    [MetadataType(typeof(PetsMetadata))]
    public partial class Pet { }

}
