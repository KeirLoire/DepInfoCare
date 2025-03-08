using System.ComponentModel.DataAnnotations;

namespace DepInfoCare.Model
{
    public sealed class FacilityModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Address { get; set; }
    }
}
