using System.ComponentModel.DataAnnotations;

namespace WebAPI_MAM.DTO_s.Get
{
    public class GetDiagDTO //Aquí es casi igual a la entidad ya que se necesita para el paciente 
    {
        public int Id { get; set; }
        [Required]
        public string observations { get; set; }
        [Required]
        public string diagnostic { get; set; }
        [Required]
        public string treatment { get; set; }
        [Required]
        public string drugs { get; set; }

        public GetAptmDTO appointment { get; set; }
    }
}
