namespace WebAPI_MAM.DTO_s.Get
{
    public class PatientDTOconCitas : GetPatientDTO //Valores del pacienta y la lista de citas que tiene 
    {
        public List<GetAptmDTO> appointments {get; set; }
    }
}
