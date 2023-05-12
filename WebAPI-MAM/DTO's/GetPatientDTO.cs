namespace WebAPI_MAM.DTO_s
{
    public class GetPatientDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public MedicInfoDTO medicInfoDTO { get; set; } //Obtiene la información médica que se ingresa

        public GetDiagDTO diagDTO { get; set; } //Obtiene el diagnostico del usuario
       
    }
}
