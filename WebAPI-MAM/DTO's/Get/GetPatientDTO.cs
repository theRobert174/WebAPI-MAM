using System.ComponentModel.DataAnnotations;

namespace WebAPI_MAM.DTO_s.Get
{
    public class GetPatientDTO
    {
        public int Id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string mail { get; set; }
        [Required]
        public string phone { get; set; }
        [Required]
        public string cel { get; set; }
        [Required(ErrorMessage = "Datos medicos del paciente no establecidos")]
        public GetMedicInfoDTO MedicInfo { get; set; }

        public List<GetAptmDTO> appointments { get; set; }
    }
}
