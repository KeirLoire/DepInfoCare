using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepInfoCare.Model
{
    public sealed class PatientModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int FacilityId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string? MiddleName { get; set; }

        public string? Suffix { get; set; }

        public string? Room { get; set; }

        public string? ImageUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        [NotMapped]
        public string? FullName { 
            get 
            {
                var fullname = FirstName;

                if (!string.IsNullOrWhiteSpace(MiddleName))
                    fullname += $" {MiddleName.First()}.";

                fullname += $" {LastName}";

                if (!string.IsNullOrWhiteSpace(Suffix))
                    fullname += $" {Suffix}";

                return fullname;
            }
        }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
