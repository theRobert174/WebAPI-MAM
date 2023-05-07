using System.Diagnostics.Tracing;

namespace WebAPI_MAM.Entities
{
    public class Appointments
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Status { get; set; }

        public int DoctorId { get; set; }

        public Doctors Doctors { get; set; }

        public int patientId { get; set; }
        //Se utiliza ICollection para evitar que salte una excepción de relación en la base de datos
        public ICollection<Patients> patients { get; set; }

        public int diagId { get; set; }

        public ICollection<Diagnosis> diagnosis { get; set; }


    }
}
