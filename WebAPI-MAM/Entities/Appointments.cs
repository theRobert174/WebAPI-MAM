using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPI_MAM.Validators;
using Newtonsoft.Json;

namespace WebAPI_MAM.Entities
{
    public class Appointments
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [ApStatus]
        public string Status { get; set; }

        [Required]
        [ForeignKey("doctor")]
        public int doctorId { get; set; }
        [JsonIgnore]
        public Doctors doctor { get; set; }

        [Required]
        [ForeignKey("patient")]
        public int patientId { get; set; }
        public Patients patient { get; set; }

        public int diagId { get; set; }
        public Diagnosis diagnostic { get; set; }

        //Autorización 
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}
