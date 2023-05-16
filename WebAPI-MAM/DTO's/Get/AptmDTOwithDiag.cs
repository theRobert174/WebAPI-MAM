namespace WebAPI_MAM.DTO_s.Get
{
    public class AptmDTOwithDiag : GetAptmDTO //Valores de la cita y el doctor y paciente que participan en ella 
    {
        public GetDiagDTO diagnostic { get; set; }
    }
}
