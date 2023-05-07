namespace WebAPI_MAM.Entities
{
    public class Doctors
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Mail { get; set; }

        public string password { get; set; }

        public List<Appointments> appointments { get; set; }
    }
}
