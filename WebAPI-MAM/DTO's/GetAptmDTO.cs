using System.ComponentModel.DataAnnotations;
using WebAPI_MAM.Validators;

namespace WebAPI_MAM.DTO_s
{
    public class GetAptmDTO 
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Status { get; set; }
    }
}
