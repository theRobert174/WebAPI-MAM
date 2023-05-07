using System.ComponentModel.DataAnnotations.Schema;
using System.IO.Pipes;

namespace WebAPI_MAM.Entities
{
    public class MedicInfo
    {
        public int Id { get; set; }

        public string Nss { get; set; }
        
        public double weight { get; set; }

        public double height { get; set; }

        public string sicknessHistory { get; set; }
    }
}
