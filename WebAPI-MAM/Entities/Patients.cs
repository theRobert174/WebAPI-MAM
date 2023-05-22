using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPI_MAM.Validators;
using Newtonsoft.Json;

namespace WebAPI_MAM.Entities
{
    public class Patients
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        [EmailAddress]
        public string mail { get; set; }

        [Required]
        //[FirstLetterUppercase]
        //[PasswordValidation]
        public string password { get; set; }

        [Required]
        [Phone]
        public string phone { get; set; }

        [Required]
        [Phone]
        public string cel { get; set; }

        public List<Appointments> appointments { get; set; }

        public int medicInfoId { get; set; }
        [JsonIgnore]
        public MedicInfo medicInfo { get; set; }
    }
}
