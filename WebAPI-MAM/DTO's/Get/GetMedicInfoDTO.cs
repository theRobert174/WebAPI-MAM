using System.ComponentModel.DataAnnotations;

namespace WebAPI_MAM.DTO_s.Get
{
    public class GetMedicInfoDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(16)]
        public string nss { get; set; }

        [Required]
        public double weight { get; set; }

        [Required]
        public double height { get; set; }

        public string sicknessHistory { get; set; }

    }
}
