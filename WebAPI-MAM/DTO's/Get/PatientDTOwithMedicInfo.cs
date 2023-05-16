using System.ComponentModel.DataAnnotations;

namespace WebAPI_MAM.DTO_s.Get
{
    public class PatientDTOwithMedicInfo : GetPatientDTO
    {
        [Required(ErrorMessage = "Datos medicos del paciente no establecidos")]
        public GetMedicInfoDTO medicInfoDTO { get; set; } //Obtiene la información médica que se ingresa
    }
}
