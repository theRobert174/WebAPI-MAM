using System.ComponentModel.DataAnnotations;
using WebAPI_MAM.Validators;

namespace WebAPI_MAM.DTO_s.Set
{
    public class AptmDTO
    {
        [Required]
        public DateTime Date {get;set;}

        [Required]
        [ApStatus]
        public string Status { get;set;}

        [Required]
        public int doctorId { get;set;}

        [Required]
        public int PatientId { get;set;}

        //public int Diagid { get;set;}
    }
}
