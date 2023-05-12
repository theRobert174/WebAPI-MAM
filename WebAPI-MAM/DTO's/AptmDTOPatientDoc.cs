namespace WebAPI_MAM.DTO_s
{
    public class AptmDTOPatientDoc : GetAptmDTO //Valores de la cita y el doctor y paciente que participan en ella 
    {
        public GetDoctorDTO DoctorDTOs { get; set; }
        public GetPatientDTO PatientsDTOs { get; set; }


    }
}
