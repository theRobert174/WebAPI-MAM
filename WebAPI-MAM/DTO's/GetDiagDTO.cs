using System.ComponentModel.DataAnnotations;

namespace WebAPI_MAM.DTO_s
{
    public class GetDiagDTO //Aquí es casi igual a la entidad ya que se necesita para el paciente 
    {
        public int Id { get; set; }
   
        public string observations { get; set; }
   
        public string diagnostic { get; set; }

        public string treatment { get; set; }

        public string drugs { get; set; }
    }
}
