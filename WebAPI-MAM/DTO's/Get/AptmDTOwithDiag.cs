using WebAPI_MAM.DTO_s.Set;
using WebAPI_MAM.Entities;

namespace WebAPI_MAM.DTO_s.Get
{
    public class AptmDTOwithDiag : Appointments //Valores de la cita y el doctor y paciente que participan en ella 
    {
        public List<GetDiagDTO> Diagnostic { get; set; }
    }
}
