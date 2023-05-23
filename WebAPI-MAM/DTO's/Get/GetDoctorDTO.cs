using System.ComponentModel.DataAnnotations;
using WebAPI_MAM.Entities;

namespace WebAPI_MAM.DTO_s.Get
{
    public class GetDoctorDTOconTODO //Información más relevante para el usuario paciente (el id del doc, el nombre y su forma de contacto)
    {
       public List<Appointments> aptm {get; set;}
    }
}
