namespace WebAPI_MAM.DTO_s
{
    public class DoctorsDTOconCitas : GetDoctorDTO //Valores del doctor y la lista de citas que tiene 
    {
        public List<GetAptmDTO> Getappointments { get; set; }  

    }
}
