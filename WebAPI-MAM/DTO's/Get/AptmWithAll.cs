using WebAPI_MAM.Entities;

namespace WebAPI_MAM.DTO_s.Get
{
    public class AptmWithAll : GetAptmDTO
    {
        public MedicInfo medicInfo { get; set; }
    }
}
