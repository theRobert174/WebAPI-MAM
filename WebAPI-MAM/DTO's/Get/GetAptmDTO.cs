using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using WebAPI_MAM.Entities;
using WebAPI_MAM.Validators;

namespace WebAPI_MAM.DTO_s.Get
{
    public class GetAptmDTO 
    {
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [ApStatus]
        public string Status { get; set; }
        [Required]
        public int patientId { get; set; }
        [Required]
        public string patientName { get; set; }
        [Required]
        public int doctorId { get; set; }
        [Required]
       
        public int diagId { get; set; }
        [JsonIgnore]
        public Diagnosis diagnosis { get; set; }
        public string doctorName { get; set; }
        //[JsonIgnore]
        //public GetDiagDTO diagnostic { get; set; }
    }
}
