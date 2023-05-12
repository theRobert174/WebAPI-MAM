namespace WebAPI_MAM.DTO_s
{
    public class GetDoctorDTO //Información más relevante para el usuario paciente (el id del doc, el nombre y su forma de contacto)
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
    }
}
