namespace WebAPI_MAM.DTO_s
{
    public class PatientDTOconCitas : GetPatientDTO //Valores del pacienta y la lista de citas que tiene 
    {
        public List<GetAptmDTO>  GetaptmDTOs {get; set; }
    }
}
