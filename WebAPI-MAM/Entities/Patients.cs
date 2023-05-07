namespace WebAPI_MAM.Entities
{
    public class Patients
    {
        public int Id { get; set; }

        public string name { get; set; }

        public string mail { get; set; }

        public string password { get; set; }

        public string phone { get; set; }

        public string cel { get; set; }

        public Appointments appointments { get; set; }

        public MedicInfo medicInfo { get; set; }


    }
}
